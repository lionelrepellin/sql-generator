using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SqlGenerator
{
    /// <summary>
    /// Classe regroupant des méthodes de création de dossiers et de fichiers.
    /// </summary>
    public static class Tools
    {
        #region Fields

        private static object writeLogFileLock = new object();

        #endregion Fields

        #region Public Methods

        /// <summary>
        /// Crée un dossier par environnement ainsi qu'un dossier Log associé
        /// </summary>
        /// <remarks>Le dossier Log sera utilisé lors de l'exécution des procédures pour enregistrer les erreurs</remarks>
        public static void CreateEnvironmentDirectories()
        {
            try
            {
                foreach (WorkingEnvironment env in Enum.GetValues(typeof(WorkingEnvironment)))
                {
                    if (!Directory.Exists(Path.Combine(env.ToString(), "Log")))
                        Directory.CreateDirectory(Path.Combine(env.ToString(), "Log"));
                }
            }
            catch (IOException ex)
            {
                throw new IOException("Erreur lors de la création des dossiers contenant les procédures" + Environment.NewLine + ex.Message);
            }
        }

        /// <summary>
        /// Création du/des fichier(s) .sql contenant les procédures
        /// </summary>
        /// <param name="spList">Liste des procédures stockées (nom des procs séparés par des virgules)</param>
        /// <param name="generateSingleFile">Si True, ne génère qu'un seul fichier SQL contenant toutes les procédures</param>
        /// <param name="env">Données d'environnement sélectionné</param>
        public static void CreateSqlFiles(List<StoredProcedure> spList, bool generateSingleFile, EnvironmentEventArgs env)
        {
            if (spList != null && env != null)
            {
                try
                {
                    // création d'un seul fichier : procedures.sql
                    if (generateSingleFile)
                    {
                        CreateSingleSqlFile(spList, env, Constant.SingleFileName);
                    }
                    else
                    {
                        CreateMultipleSqlFiles(spList, env);
                    }
                }
                catch (IOException ex)
                {
                    throw new IOException("Erreur lors de la création des fichiers SQL." + Environment.NewLine + ex.Message);
                }
            }
        }

        /// <summary>
        /// Crée un seul fichier contenant toutes les procédures 
        /// </summary>
        /// <remarks>Le fichier SQL créé porte le nom : procedures.sql</remarks>
        /// <param name="spList">Liste des procédures à traiter</param>
        /// <param name="env">Environnement dans lequel seront créés les fichiers</param>
        /// <param name="fileName">Le nom du fichier SQL qui sera créé</param>
        /// <param name="addHeaderAndSeparator">Ajoute le header et le séparateur entre chaque procédure</param>
        /// <returns>Retourne le chemin d'accès relatif au fichier créé (ex: DEV\procedure.sql)</returns>
        public static string CreateSingleSqlFile(List<StoredProcedure> spList, EnvironmentEventArgs env, string fileName, bool addHeaderAndSeparator = true)
        {
            var singleFile = Path.Combine(env.Environement.ToString(), fileName);
            
            using (StreamWriter writer = new StreamWriter(singleFile, false, System.Text.Encoding.Unicode))
            {
                // USE [database]
                writer.WriteLine("USE [" + env.Database + "]" + System.Environment.NewLine);

                foreach (StoredProcedure sp in spList)
                {
                    // header : if exists, drop procedure...
                    if(addHeaderAndSeparator)
                        writer.WriteLine(AddHeader(sp.Name, sp.Type));

                    // corps de la procédure
                    writer.WriteLine(sp.Definition);

                    // séparation
                    if(addHeaderAndSeparator)
                        writer.WriteLine(String.Empty.PadRight(80, '-') + System.Environment.NewLine + "GO" + System.Environment.NewLine);
                }

                writer.Close();
            }

            return singleFile;
        }

        /// <summary>
        /// Crée un fichier SQL par procédure
        /// </summary>
        /// <remarks>Le fichier SQL créé porte le nom de la procédure</remarks>
        /// <param name="spList">Liste des procédures à traiter</param>
        /// <param name="env">Environnement dans lequel seront créés les fichiers</param>
        private static void CreateMultipleSqlFiles(List<StoredProcedure> spList, EnvironmentEventArgs env)
        {
            // création d'un fichier par procédure portant le nom de la procédure
            foreach (StoredProcedure sp in spList)
            {
                var procFile = Path.Combine(env.Environement.ToString(), sp.Name + ".sql");

                using (StreamWriter writer = new StreamWriter(procFile, false, System.Text.Encoding.Unicode))
                {
                    // corps de la procédure
                    writer.WriteLine(sp.Definition);

                    writer.Close();
                }
            }
        }


        /// <summary>
        /// Crée un template au format CSV contenant la liste des procédures sélectionnées
        /// </summary>
        /// <param name="items">La liste des procédures sélectionnées</param>
        public static void CreateTemplate(List<string> items)
        {
            if (items != null)
            {
                using (StreamWriter writer = new StreamWriter(Constant.TemplateCsv, false))
                {
                    foreach (var item in items)
                    {
                        writer.WriteLine(item);
                    }
                    writer.Close();
                }
            }
        }

        /// <summary>
        /// Retourne le chemin complet jusqu'à l'exécutable
        /// </summary>
        /// <returns>Chemin complet depuis C:\</returns>
        public static string GetApplicationPath()
        {
            return System.Windows.Forms.Application.StartupPath;
        }

        /// <summary>
        /// Obtient la liste des environnements dynamiquement
        /// </summary>
        /// <returns>Retourne une liste clé/valeur des environements configurés</returns>
        public static Dictionary<string, string> GetEnvironments()
        {
            Dictionary<string, string> connectionStringList = new Dictionary<string, string>();
            var count = ConfigurationManager.ConnectionStrings.Count;

            if (count < 3 || count > 5)
                throw new CustomException("Le nombre d'environnement doit être compris entre 3 et 5", new Exception(), LogAction.EVENT);

            for (var i = 0; i < count; i++)
            {
                var key = ConfigurationManager.ConnectionStrings[i].Name;
                var value = ConfigurationManager.ConnectionStrings[i].ConnectionString;
                connectionStringList.Add(key, value);
            }

            return connectionStringList;
        }

        /// <summary>
        /// Obtient une liste d'objets StoredProcedure en fonction des fichiers SQL présents dans le dossier de l'environnement sélectionné
        /// </summary>
        /// <param name="environment">Environnement sélectionné</param>
        /// <returns></returns>
        public static List<StoredProcedure> GetOrderedStoredProcedureFromSqlFiles(WorkingEnvironment environment)
        {
            var path = Path.Combine(Tools.GetApplicationPath(), environment.ToString());
            var files = new List<string>(Directory.GetFiles(path, "*.sql", SearchOption.TopDirectoryOnly));

            List<StoredProcedure> result = new List<StoredProcedure>();

            foreach (var file in files)
            {
                var filename = Path.GetFileName(file);

                // le fichier procedures.sql est exclu
                if (filename != Constant.SingleFileName)
                {
                    using (StreamReader stream = new StreamReader(file))
                    {
                        result.Add(new StoredProcedure(name: filename, definition: stream.ReadToEnd(), fullpath: file));
                        stream.Close();
                    }
                }
            }

            // ordonne la liste des fichiers en fonctions du nombre de procédures inclues
            var orderedList = (from r in result
                               orderby r.InnerStoredProcedure ascending
                               select r).ToList();

            return orderedList;
        }

        /// <summary>
        /// Retourne la liste des arguments à passer à Sqlcmd
        /// </summary>
        /// <param name="file">Le chemin d'accès complet au fichier</param>
        /// <param name="data">Les données de l'environnement sélectionné</param>
        /// <returns>Retourne la liste des arguments utilisés avec Sqlcmd</returns>
        public static string GetProcessInfoArguments(string file, EnvironmentEventArgs data)
        {
            if (!String.IsNullOrEmpty(file) && data != null)
                return String.Format("-U {0} -P {1} -S {2} -d {3} -l 60 -i {4} -o {5}", data.User, data.Password, data.Server, data.Database, file, Path.Combine(data.Environement.ToString() + "\\Log", Path.GetFileNameWithoutExtension(file) + ".txt"));
            else
                return String.Empty;
        }

        /// <summary>
        /// Retourne la liste des procédures sélectionnées
        /// </summary>
        /// <param name="listBox">La ListBox dans laquelle effectuer la recherche</param>
        /// <returns>'proc1','proc2','proc3'</returns>
        public static string GetSelectedItemsFromListBox(System.Windows.Forms.ListBox listBox)
        {
            if (listBox != null)
            {
                var result = String.Empty;

                foreach (StoredProcedure sp in listBox.Items)
                {
                    if (!String.IsNullOrEmpty(result))
                        result += ", ";

                    result += "'" + sp.Name + "'";
                }

                return result;
            }

            return String.Empty;
        }

        /// <summary>
        /// Récupère le nom des procédures sélectionnées
        /// </summary>
        /// <param name="listBox">La listBox dans laquelle rechercher les procédures</param>
        /// <returns>Retourne la liste des noms des procédures</returns>
        public static List<string> GetSelectedProcName(System.Windows.Forms.ListBox listBox)
        {
            List<String> items = new List<string>();

            if (listBox == null)
                return items;

            foreach (var item in listBox.Items)
            {
                items.Add(((StoredProcedure)item).Name);
            }

            return items;
        }

        /// <summary>
        /// Retourne la version du logiciel
        /// </summary>
        /// <returns>Retourne la version (Majeure, Mineur, Build) du logiciel</returns>
        public static string GetSoftwareVersion()
        {
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            return String.Format("{0}.{1}.{2}", version.Major, version.Minor, version.Build);
        }

        /// <summary>
        /// Déplace les items d'une ListBox à une autre
        /// </summary>
        /// <param name="listBoxFrom">la ListBox source</param>
        /// <param name="listBoxTo">la ListBox cible</param>
        public static void MoveItems(System.Windows.Forms.ListBox listBoxFrom, System.Windows.Forms.ListBox listBoxTo)
        {
            if (listBoxFrom != null && listBoxTo != null)
            {
                while (listBoxFrom.SelectedItems.Count > 0)
                {
                    listBoxTo.Items.Add(listBoxFrom.SelectedItem);
                    listBoxFrom.Items.Remove(listBoxFrom.SelectedItem);
                }
            }
            else return;
        }

        /// <summary>
        /// Recherche une procédure dans la listBox, en fonction de son nom
        /// </summary>
        /// <param name="listBox">ListBox dans laquelle doit s'effectuer la recherche</param>
        /// <param name="name">Nom de la procédure recherchée</param>
        /// <returns>Retourne TRUE si la proc est trouvée</returns>
        public static bool SearchSelectedItemsByName(System.Windows.Forms.ListBox listBox, string name)
        {
            if (listBox != null && !String.IsNullOrEmpty(name))
            {
                var result = listBox.Items.OfType<StoredProcedure>().Where(p => p.Name.ToLower().Equals(name.ToLower())).AsParallel().Count();

                return (result > 0);
            }

            return false;
        }

        /// <summary>
        /// Vérifie l'existence de fichiers SQL portant le même nom que les procédures sélectionnées
        /// </summary>
        /// <param name="environment">Environnement sélectionné</param>
        /// <param name="singleFile">Dans le cas où seul le fichier procedures.sql est recherché</param>
        /// <param name="listToCompare">Liste des noms de procédures sélectionnées à comparer</param>
        /// <remarks>Le fichier procedures.sql est exclu, il devra être exécuté manuellement</remarks>
        /// <returns>Retourne True si au moins une des procédures sélectionnées a déjà été extraite</returns>
        public static bool SqlFilesExists(WorkingEnvironment environment, bool singleFile, List<string> listToCompare)
        {
            var path = Path.Combine(Tools.GetApplicationPath(), environment.ToString());
            var sqlFiles = new List<string>(Directory.GetFiles(path, "*.sql", SearchOption.TopDirectoryOnly));

            if (singleFile)
            {
                foreach (var file in sqlFiles)
                {
                    if (Path.GetFileName(file) == Constant.SingleFileName)
                        return true;
                }
                return false;
            }
            else
            {
                var result = (from f in sqlFiles
                              join p in listToCompare on Path.GetFileNameWithoutExtension(f) equals p
                              select f).ToList();

                if (result.Count > 0)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Enregistre un message d'erreur dans le gestionnaire d'événements Windows
        /// </summary>
        /// <param name="source">Le nom de l'application qui a généré cet événement</param>
        /// <param name="message">Le message détaillant l'erreur</param>
        /// <param name="eventLogEntryType">Type d'événement Error définit par défaut</param>
        public static void WriteLogEvent(string source, string message, EventLogEntryType eventLogEntryType = EventLogEntryType.Error)
        {
            if (!String.IsNullOrEmpty(source) && !String.IsNullOrEmpty(message))
            {
                if (!EventLog.SourceExists(source))
                    EventLog.CreateEventSource(source, "Application");

                EventLog.WriteEntry(source, message, EventLogEntryType.Error);
            }
        }

        /// <summary>
        /// Enregistre un message d'erreur dans un fichier de log
        /// </summary>
        /// <param name="message">Message qui sera inscrit dans le fichier</param>
        public static void WriteLogFile(string message)
        {
            if (!String.IsNullOrEmpty(message))
            {
                lock (writeLogFileLock)
                {
                    var sFileName = DateTime.Now.ToString("yyyyMMdd") + ".txt";

                    using (StreamWriter writer = new StreamWriter(sFileName, true))
                    {
                        writer.WriteLine(message);
                        writer.Close();
                    }
                }
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Ajoute un header à chaque procédure (drop before create)
        /// </summary>
        /// <param name="spName">Nom de la procédure</param>
        /// <param name="spType">Indique s'il s'agit d'une procédure ou d'une fonction</param>
        /// <returns></returns>
        private static string AddHeader(string spName, string spType)
        {
            StringBuilder sb = new StringBuilder();

            if (spType == "P")
            {
                sb.AppendFormat("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[{0}]') AND type in (N'P', N'PC'))", spName);
                sb.AppendLine();
                sb.AppendFormat("DROP PROCEDURE [dbo].[{0}]", spName);
            }
            else
            {
                sb.AppendFormat("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[{0}]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))", spName);
                sb.AppendLine();
                sb.AppendFormat("DROP FUNCTION [dbo].[{0}]", spName);
            }

            sb.AppendLine();
            sb.AppendLine("GO");
            sb.AppendLine("SET ANSI_NULLS ON");
            sb.AppendLine();
            sb.AppendLine("GO");
            sb.AppendLine("SET QUOTED_IDENTIFIER ON");
            sb.AppendLine();
            sb.AppendLine("GO");
            sb.AppendLine();

            return sb.ToString();
        }

        #endregion Private Methods
    }
}