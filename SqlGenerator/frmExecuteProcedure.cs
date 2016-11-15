using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SqlGenerator
{
    /// <summary>
    /// Pop-up secondaire d'exécution des procédures
    /// </summary>
    public partial class FrmExecuteProcedure : Form
    {
        #region Fields

        // données liées à l'environnement sélectionné
        private EnvironmentEventArgs data;

        // liste des procédures à exécuter
        private Items items;

        private int nbSqlFilesToExecute = 0;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Constructeur
        /// </summary>
        public FrmExecuteProcedure()
        {
            InitializeComponent();

            lblProcInProgress.Text = String.Empty;
            this.items = new Items();
        }

        #endregion Constructors

        #region Private Methods

        // Exécution des procédures
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.FileName = "sqlcmd.exe";

            // récupère les procédures distantes 
            // en fonction de la liste des fichiers SQL présents dans le dossier
            List<StoredProcedure> procListTarget = new List<StoredProcedure>();

            using (Database db = new Database(this.data.ConnectionString))
            {
                procListTarget = db.LoadStoredProcedure(this.items.GetItemsList());
            }
            

            for (var i = 0; i < this.items.StoredProcedureList.Count; i++)
            {
                var file = this.items.StoredProcedureList[i].FullPath;

                if (File.Exists(file))
                {
                    if (procListTarget.Count > 0)
                    {
                        // recherche si le fichier SQL en cours est présent parmis la liste des procédures récupérées en prod
                        var result = (from p in procListTarget
                                      where p.Name == this.items.StoredProcedureList[i].Name
                                      select p).ToList();

                        // si une procédure distante correspond à la procédure en cours qui va être mise en prod
                        if (result.Count == 1)
                        {
                            result[0].Definition.Replace("create ", "alter ").Replace("CREATE ", "ALTER ");
                            file = Tools.CreateSingleSqlFile(result, this.data, String.Format("{0}_modified.sql", this.items.StoredProcedureList[i].Name));
                        }
                    }
                    
                    // récupère la liste d'arguments nécessaire
                    var args = Tools.GetProcessInfoArguments(file, this.data);
                    
                    if (!String.IsNullOrEmpty(args))
                    {
                        processInfo.Arguments = args;
#if(!DEBUG)
                        Process.Start(processInfo);
#endif
                        System.Threading.Thread.Sleep(300);
                    }
                }

                bgWorker.ReportProgress(i + 1, this.items.StoredProcedureList[i]);
            }

            System.Threading.Thread.Sleep(500);
        }

        // Mise à jour de la progressbar en fonction de l'avancement
        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            lblProcInProgress.Text = Path.GetFileNameWithoutExtension((string)e.UserState);
        }

        // Fin du job
        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Application.UseWaitCursor = false;
            btnExecuteStoredProcedure.Enabled = true;
            var status = false;

            // vérifie que toutes les procédures ont bien été créées
            var registeredProc = new List<StoredProcedure>();

            using (Database db = new Database(this.data.ConnectionString))
            {
                registeredProc = db.LoadStoredProcedure(this.items.GetItemsList());
                status = this.items.StoredProcedureList.Count == registeredProc.Count;
            }

            if (!status)
            {
                var errors = from p in this.items.StoredProcedureList
                             where !(from f in registeredProc select f.Name).Contains(p.Name)
                             select p.Name;

                var list = String.Empty;

                foreach (var proc in errors)
                    list += Environment.NewLine + proc;
                
                MessageBox.Show("Des erreurs se sont produites dans les procédures suivantes :" + list, "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // ouverture du dossier contenant les logs
                Process.Start("explorer.exe", String.Format("/root,{0}\\{1}\\Log", Tools.GetApplicationPath(), this.data.Environement));                
            }
            else
            {
                MessageBox.Show("Les procédures ont été exécutées avec succès !", "Youpi !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        // Lancement du job
        private void btnExecuteStoredProcedure_Click(object sender, EventArgs e)
        {
            if (nbSqlFilesToExecute == 0)
            {
                MessageBox.Show("Aucune procédure à exécuter !", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                DialogResult result = MessageBox.Show("Etes-vous sûr d'exécuter les procédures contenues dans le dossier " + this.data.Environement.ToString() + " sur la base de données " + this.data.Database + " du serveur " + this.data.Server + " ?", "Attention !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    btnExecuteStoredProcedure.Enabled = false;
                    Application.UseWaitCursor = true;
                    bgWorker.RunWorkerAsync();
                }
            }
        }

        private void ctrlEnvironment1_RadioChanged(object sender, EnvironmentEventArgs e)
        {
            this.data = e;
            UpdateGUI();
        }

        private void frmExecuteProcedure_Load(object sender, EventArgs e)
        {
            //UpdateGUI();
        }

        /// <summary>
        /// Charge la liste des procédures SQL qui vont être exécutées
        /// <remarks>Toutes les procs sauf le fichier procedures.sql qui regroupe toutes les procédures, celui-là on l'exécute à la main !</remarks>
        /// </summary>
        private void LoadFiles()
        {
            try
            {
                this.items.StoredProcedureList = Tools.GetOrderedStoredProcedureFromSqlFiles(this.data.Environement);

                var count = this.items.StoredProcedureList.Count;

                lblNbProc.Text = count.ToString();
                progressBar.Maximum = count;
                nbSqlFilesToExecute = count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Met à jour l'interface
        /// </summary>
        private void UpdateGUI()
        {
            lblEnvironment.Text = this.data.Environement.ToString();
            LoadFiles();
        }

        #endregion Private Methods
    }
}