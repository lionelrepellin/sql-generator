using System;
using System.Text;
using System.Text.RegularExpressions;

namespace SqlGenerator
{
    /// <summary>
    /// Classe de données identifiant une procédure / fonction
    /// </summary>
    public class StoredProcedure
    {
        #region Constructors

        /// <summary>
        /// Constructuer
        /// </summary>
        /// <param name="name">Nom de la procédure</param>
        /// <param name="type">Indique s'il s'agit d'une procédure ou d'une fonction</param>
        /// <param name="definition">Le corps de la procédure</param>
        /// <param name="fullpath">Le chemin d'accès complet au fichier SQL</param>
        public StoredProcedure(string name, string type = null, string definition = null, string fullpath = null)
        {
            if (!String.IsNullOrEmpty(name))
                Name = name.Trim();
            else
                return;

            if (!String.IsNullOrEmpty(type))
                Type = type.Trim();

            if (!String.IsNullOrEmpty(definition))
            {
                Definition = Encoding.Unicode.GetString(Encoding.Convert(Encoding.UTF8, Encoding.Unicode, Encoding.UTF8.GetBytes(definition)));

                Regex regex = new Regex(@"exec[\s]|execute[\s]");
                var collection = regex.Matches(definition);
                InnerStoredProcedure = collection.Count;
            }

            if (!String.IsNullOrEmpty(fullpath))
                FullPath = fullpath;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Obtient le corps de la procédure / fonction
        /// </summary>
        public string Definition
        {
            get;
            private set;
        }

        /// <summary>
        /// Obtient le chemin complet vers le fichier SQL
        /// </summary>
        public string FullPath
        {
            get;
            private set;
        }

        /// <summary>
        /// Nombre de procédures stockées inclues dans la procédure en cours
        /// </summary>
        public int InnerStoredProcedure
        {
            get;
            private set;
        }

        /// <summary>
        /// Obtient le nom de la procédure
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Obtient le type P / F pour Procédure ou Fonction
        /// </summary>
        public string Type
        {
            get;
            private set;
        }

        #endregion Properties
    }
}