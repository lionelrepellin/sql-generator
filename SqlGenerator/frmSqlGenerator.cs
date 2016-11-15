using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SqlGenerator
{
    /// <summary>
    /// Fenêtre principale de l'application SqlGenerator
    /// </summary>
    public partial class FrmSqlGenerator : Form
    {
        #region Fields

        private EnvironmentEventArgs data;

        // Marque l'état d'initialisation
        private bool initInProgress;
        private Items items;

        // Titre de l'application
        private string title = String.Empty;
        private ToolTip toolTips = new ToolTip();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Constructeur
        /// </summary>
        public FrmSqlGenerator()
        {
            InitializeComponent();

            //try
            //{
            //    this.environments = Tools.GetEnvironments();
            //}
            //catch (CustomException ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    Application.Exit();
            //}

            this.items = new Items();
            this.title = Text + " - " + Tools.GetSoftwareVersion();

            listBoxStoredProcedure.DisplayMember = "Name";
            listBoxSelectedStoredProcedure.DisplayMember = "Name";

            AddToolTips();
        }

        #endregion Constructors

        #region Private Methods

        /// <summary>
        /// Ajoute des info bulles sur certains contrôles
        /// </summary>
        private void AddToolTips()
        {
            toolTips.SetToolTip(btnErase, "Recharge la liste des procédures");
            toolTips.SetToolTip(btnExecute, "Exécute toutes les procédures déjà extraites");
            toolTips.SetToolTip(btnOpenCSV, "Récupère la liste des procédures contenues dans un fichier CSV");
            toolTips.SetToolTip(btnExtractStoredProcedures, "Extrait toutes les procédures sélectionnées dans des fichiers");
        }

        private bool Analyse(string definition)
        {
            Regex regex = new Regex(@"(\(|\r|\n|\t|\s|.|,)@{1}[\w_-]*[(\r|\n|\t|\s)$]");
            var collection = regex.Matches(definition);
            return regex.IsMatch(definition);
        }

        void bgExtract_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var spDefinition = new List<StoredProcedure>();
                var spSelectedName = Tools.GetSelectedItemsFromListBox(listBoxSelectedStoredProcedure);

                if (!String.IsNullOrEmpty(spSelectedName))
                {
                    using (Database database = new Database(this.data.ConnectionString))
                    {
                        spDefinition = database.LoadStoredProcedure(spSelectedName);
                    }

                    Tools.CreateSqlFiles(spDefinition, chkGenerateSingleFile.Checked, this.data);
                }
                //foreach (var def in spDefinition)
                //{
                //    Analyse(def.Definition);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void bgExtract_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Application.UseWaitCursor = false;
            btnExtractStoredProcedures.Enabled = true;
            toolStripStatusLabel.Text = "Extraction terminée.";
        }

        void bgLoadSp_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                using (Database database = new Database(this.data.ConnectionString))
                {
                    this.items.StoredProcedureList = database.LoadStoredProcedure();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void bgLoadSp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.items.FillStoredProcedureList(listBoxStoredProcedure, listBoxSelectedStoredProcedure);

            Application.UseWaitCursor = false;
            btnOpenCSV.Enabled = true;

            toolStripStatusLabel.Text = String.Format("Chargement terminé. ({0} procédures)", this.items.StoredProcedureList.Count);
        }

        // Initialise les listes et recharge les procédures
        private void btnErase_Click(object sender, EventArgs e)
        {
            InitListBox(false);

            this.items.FillStoredProcedureList(listBoxStoredProcedure, listBoxSelectedStoredProcedure);

            if (listBoxStoredProcedure.Items.Count > 0)
                btnOpenCSV.Enabled = true;
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            using (FrmExecuteProcedure window = new FrmExecuteProcedure())
            {
                window.ShowDialog();
            }
        }

        // Extrait les procédures dans des fichiers SQL
        private void btnExtractStoredProcedures_Click(object sender, EventArgs e)
        {
            if (listBoxSelectedStoredProcedure.Items.Count > 0)
            {
                var items = Tools.GetSelectedProcName(listBoxSelectedStoredProcedure);

                if (chkGenerateTemplate.Checked)
                {
                    Tools.CreateTemplate(items);
                }

                var sqlFilesExists = Tools.SqlFilesExists(this.data.Environement, chkGenerateSingleFile.Checked, items);

                if (sqlFilesExists)
                {
                    DialogResult result = MessageBox.Show("Le dossier " + this.data.Environement.ToString() + " contient déjà des fichiers, ils seront écrasés." + Environment.NewLine + "Voulez-vous continuer ?", "Attention !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        RunExtraction();
                    }
                }
                else
                {
                    RunExtraction();
                }
            }
        }

        // Ouvre un fichier CSV, lit son contenu et affiche les procédures trouvées dans la liste
        private void btnOpenCSV_Click(object sender, EventArgs e)
        {
            DialogResult result = openCsvFileDialog.ShowDialog();

            try
            {
                if (result == DialogResult.OK)
                {
                    if (File.Exists(openCsvFileDialog.FileName))
                    {
                        string[] file = File.ReadAllLines(openCsvFileDialog.FileName);
                        var listStoredProcedureToRemove = new List<StoredProcedure>();
                        var procedureNotFound = String.Empty;

                        // parcours les noms des procédures dans le fichier
                        foreach (string procedureInFile in file)
                        {
                            var proc = procedureInFile.CleanProc();

                            // parcours les procédures enregistrées
                            foreach (StoredProcedure sp in listBoxStoredProcedure.Items)
                            {
                                if (sp.Name.ToLower() == proc.ToLower() && !Tools.SearchSelectedItemsByName(listBoxSelectedStoredProcedure, procedureInFile))
                                {
                                    listBoxSelectedStoredProcedure.Items.Add(sp);
                                    listStoredProcedureToRemove.Add(sp);
                                }
                            }
                        }

                        // supprime les procédures trouvées
                        foreach (StoredProcedure spToRemove in listStoredProcedureToRemove)
                        {
                            listBoxStoredProcedure.Items.Remove(spToRemove);
                        }

                        // vérifie que toutes les procédures contenues dans le fichier CSV ont bien été trouvées
                        if (file.Length > listBoxSelectedStoredProcedure.Items.Count)
                        {
                            foreach(var procedureInCsv in file)
                            {
                                var procedure = procedureInCsv.CleanProc();

                                if (!Tools.SearchSelectedItemsByName(listBoxSelectedStoredProcedure, procedure))
                                    procedureNotFound += procedure + Environment.NewLine;
                            }
                        }

                        UpdateSelectedStoredProcedureCount();

                        if (procedureNotFound.Length > 0)
                            MessageBox.Show("Les procédures suivantes n'ont pas été trouvées : " + Environment.NewLine + procedureNotFound, "Attention !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sélectionne les procédures stockées >>>
        private void btnSelectStoredProcedure_Click(object sender, EventArgs e)
        {
            Tools.MoveItems(listBoxStoredProcedure, listBoxSelectedStoredProcedure);
            UpdateSelectedStoredProcedureCount();
        }

        // Désélectionne les procédures stockées <<<
        private void btnUnselectStoredProcedure_Click(object sender, EventArgs e)
        {
            Tools.MoveItems(listBoxSelectedStoredProcedure, listBoxStoredProcedure);
            UpdateSelectedStoredProcedureCount();
        }

        // Sélection d'un environnement via bouton radio
        private void ctrlEnvironment1_RadioChanged(object sender, EnvironmentEventArgs e)
        {
            this.data = e;
            this.items.Clear();

            InitListBox();
            LoadStoredProcedure();
        }

        /// <summary>
        /// Recherche une procédure dans la listBox en fonction de son nom ou d'une partie seulement
        /// </summary>
        /// <param name="word">Nom ou partie du nom de la procédure</param>
        private void FindStoredProcedure(string word)
        {
            var filteredList = this.items.GetFilteredList(word);

            if (filteredList.Count > 0)
                this.items.FillStoredProcedureList(listBoxStoredProcedure, listBoxSelectedStoredProcedure, filteredList);
            else
                this.items.FillStoredProcedureList(listBoxStoredProcedure, listBoxSelectedStoredProcedure);
        }

        private void FrmSqlGenerator_FormClosing(object sender, FormClosingEventArgs e)
        {
            toolTips.Dispose();
        }

        /// <summary>
        /// Initialise les listes déroulantes, la barre de statut, le menu, le champ de recherche...
        /// </summary>
        /// <param name="clearSelectedItemsList">supprime la liste des éléments sélectionnés</param>
        private void InitListBox(bool clearSelectedItemsList = true)
        {
            this.initInProgress = true;

            btnOpenCSV.Enabled = false;

            txtFindStoredProcedure.Text = String.Empty;
            txtFindStoredProcedure.Focus();

            listBoxStoredProcedure.Items.Clear();

            if(clearSelectedItemsList)
                listBoxSelectedStoredProcedure.Items.Clear();

            Text = String.Format("{0} - [{1}]", this.title, this.data.Environement);

            if(listBoxSelectedStoredProcedure.Items.Count == 0)
                toolStripStatusLabel.Text = "";

            this.initInProgress = false;
        }

        /// <summary>
        /// Chargement de la liste des procédures
        /// </summary>
        private void LoadStoredProcedure()
        {
            toolStripStatusLabel.Text = "Chargement des procédures en cours...";
            Application.UseWaitCursor = true;

            using (BackgroundWorker bgLoadSp = new BackgroundWorker())
            {
                bgLoadSp.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgLoadSp_RunWorkerCompleted);
                bgLoadSp.DoWork += new DoWorkEventHandler(bgLoadSp_DoWork);
                bgLoadSp.RunWorkerAsync();
            }
        }

        private void lstSelectedStoredProcedure_DoubleClick(object sender, EventArgs e)
        {
            btnUnselectStoredProcedure_Click(null, null);
        }

        private void lstStoredProcedure_DoubleClick(object sender, EventArgs e)
        {
            btnSelectStoredProcedure_Click(null, null);
        }

        /// <summary>
        /// Lance l'extraction des procédures sélectionnées
        /// </summary>
        private void RunExtraction()
        {
            toolStripStatusLabel.Text = "Extraction des procédures en cours...";
            Application.UseWaitCursor = true;
            btnExtractStoredProcedures.Enabled = false;

            using (BackgroundWorker bgExtract = new BackgroundWorker())
            {
                bgExtract.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgExtract_RunWorkerCompleted);
                bgExtract.DoWork += new DoWorkEventHandler(bgExtract_DoWork);
                bgExtract.RunWorkerAsync();
            }
        }

        // Chargement de l'application
        private void SqlGenerator_Load(object sender, EventArgs e)
        {
            Tools.CreateEnvironmentDirectories();
        }

        // Champ de recherche
        private void txtFindStoredProcedure_TextChanged(object sender, EventArgs e)
        {
            var text = txtFindStoredProcedure.Text.Trim();

            if (!this.initInProgress && text.Length >= 3)
                FindStoredProcedure(text);
            else if (text.Length == 0)
                btnErase_Click(null, null);
        }

        /// <summary>
        /// Met à jour l'affichage du nombre de procédures sélectionnées
        /// </summary>
        private void UpdateSelectedStoredProcedureCount()
        {
            var nbProc = listBoxSelectedStoredProcedure.Items.Count;

            if (nbProc <= 1)
                toolStripStatusLabel.Text = String.Format("{0} procédure sélectionnée", nbProc);
            else
                toolStripStatusLabel.Text = String.Format("{0} procédures sélectionnées", nbProc);
        }

        #endregion Private Methods        
    }
}