using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SqlGenerator
{
    /// <summary>
    /// Contrôle utilisateur signalant à son parent le changement d'environnement de travail
    /// </summary>
    public partial class CtrlEnvironment : UserControl
    {
        #region Constructors

        /// <summary>
        /// Constructeur
        /// </summary>
        public CtrlEnvironment()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// Evénement transmis lors d'un clique sur un des boutons radio
        /// </summary>
        public event EventHandler<EnvironmentEventArgs> RadioChanged;

        #endregion Events

        #region Private Methods

        // Présélectionne l'environnement Local
        private void ctrlEnvironment_Load(object sender, EventArgs e)
        {
            radioBddLocal.Checked = true;
        }

        // Environnement de développement Local
        private void radioBddLocal_CheckedChanged(object sender, EventArgs e)
        {
            RaiseEvent(sender);
        }

        // Environnement de développement de Pré-prod
        private void radioBddPreProd_CheckedChanged(object sender, EventArgs e)
        {
            RaiseEvent(sender);
        }

        // Environnement de développement de Prod
        private void radioBddProd_CheckedChanged(object sender, EventArgs e)
        {
            RaiseEvent(sender);
        }

        private void radioRecette_CheckedChanged(object sender, EventArgs e)
        {
            RaiseEvent(sender);
        }

        // Lève l'événement
        private void RaiseEvent(object sender)
        {
            WorkingEnvironment env;
            RadioButton btnRadio = sender as RadioButton;

            if (btnRadio.Checked)
            {
                if ((string)btnRadio.Tag == "DEV")
                    env = WorkingEnvironment.DEV;
                else if ((string)btnRadio.Tag == "PREPROD")
                    env = WorkingEnvironment.PREPROD;
                else if ((string)btnRadio.Tag == "RECETTE")
                    env = WorkingEnvironment.RECETTE;
                else
                    env = WorkingEnvironment.PROD;

                var handler = RadioChanged;

                if (handler != null)
                    handler(this, new EnvironmentEventArgs(env));
            }
        }

        #endregion Private Methods
    }

    /// <summary>
    /// Objet transmis lorsqu'un événement est levé
    /// </summary>
    public class EnvironmentEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>
        /// Classe contenant les données liées à la chaîne de connexion et à l'environnement sélectionné
        /// </summary>
        /// <param name="environement">Environnement sélectionné</param>
        public EnvironmentEventArgs(WorkingEnvironment environement)
        {
            try
            {
                Environement = environement;
                ConnectionString = ConfigurationManager.ConnectionStrings[environement.ToString()].ConnectionString;

                SqlConnectionStringBuilder sql = new SqlConnectionStringBuilder(ConnectionString);
                Database = sql.InitialCatalog;
                Password = sql.Password;
                User = sql.UserID;
                Server = sql.DataSource;
            }
            catch (ArgumentException arg)
            {
                throw new CustomException("Erreur de chargement de la chaîne de connexion.", arg, LogAction.EVENT);
            }
            catch (ConfigurationErrorsException ex)
            {
                throw new CustomException("Erreur de chargement de la chaîne de connexion.", ex, LogAction.EVENT);
            }
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Chaîne de connexion à la base de données
        /// </summary>
        public string ConnectionString
        {
            get; private set;
        }

        /// <summary>
        /// Nom de la base de données
        /// </summary>
        public string Database
        {
            get; private set;
        }

        /// <summary>
        /// Environnement sélectionné
        /// </summary>
        public WorkingEnvironment Environement
        {
            get; private set;
        }

        /// <summary>
        /// Mot de passe
        /// </summary>
        public string Password
        {
            get; private set;
        }

        /// <summary>
        /// Nom du serveur
        /// </summary>
        public string Server
        {
            get; private set;
        }

        /// <summary>
        /// Utilisateur
        /// </summary>
        public string User
        {
            get; private set;
        }

        #endregion Properties
    }
}