using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SqlGenerator
{
    /// <summary>
    /// Classe d'accès à la base de données
    /// </summary>
    public partial class Database : IDisposable
    {
        #region Fields

        private string connectionString;
        private bool disposed;
        private SqlConnection sqlConnection;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialisation et connexion à la base de données
        /// </summary>
        /// <param name="databaseConnectionString">Chaîne de connexion</param>
        public Database(string databaseConnectionString)
        {
            try
            {
                this.connectionString = databaseConnectionString;
                this.sqlConnection = new SqlConnection(databaseConnectionString);
                this.sqlConnection.Open();
            }
            catch (SqlException sql)
            {
                throw new CustomException("Erreur de connexion à la base de données.", sql, LogAction.EVENT);
            }
        }

        /// <summary>
        /// Desctructeur appelé par le Garbage Collector
        /// </summary>
        ~Database()
        {
            Dispose(false);
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Méthode de libération de la mémoire
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Libère la mémoire / Dispose les objets managés
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // libère la mémoires des objets managés
                    if (this.sqlConnection != null && this.sqlConnection.State == ConnectionState.Open)
                        this.sqlConnection.Close();

                    this.sqlConnection.Dispose();
                }
                this.sqlConnection = null;
                this.disposed = true;
            }
        }

        #endregion Protected Methods
    }
}