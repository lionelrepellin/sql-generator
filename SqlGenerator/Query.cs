using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SqlGenerator
{
    public partial class Database
    {
        #region Public Methods

        /// <summary>
        /// Récupère la liste des procédures stockées et des fonctions
        /// </summary>
        /// <param name="procedureList">Liste des procédures séparées par des virgules (ex: 'proc1','proc2','proc3')</param>
        /// <returns>Liste des procédures - le contenu est retourné si une liste de procédures est passé en paramètre</returns>
        public List<StoredProcedure> LoadStoredProcedure(string procedureList = null)
        {
            var spList = new List<StoredProcedure>();

            try
            {
                using (SqlCommand cmd = this.sqlConnection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    if (String.IsNullOrEmpty(procedureList))
                    {
                        cmd.CommandText = @"SELECT name
                                            FROM sys.objects
                                            WHERE type IN ('P','FN','TF')
                                            ORDER BY name ASC";
                    }
                    else
                    {
                        cmd.CommandText = @"SELECT o.name, o.type, m.definition
                                            FROM sys.objects as o
                                            JOIN sys.sql_modules m ON o.object_id = m.object_id
                                            WHERE o.name IN (" + procedureList + ")";
                    }

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.VisibleFieldCount == 3)
                                spList.Add(new StoredProcedure(reader.GetString(0), reader.GetString(1), reader.GetString(2)));
                            else
                                spList.Add(new StoredProcedure(reader.GetString(0)));
                        }
                        reader.Close();
                    }
                }
            }
            catch (SqlException sql)
            {                
                throw new CustomException("Erreur lors de la récupération des procédures.", sql, LogAction.EVENT);
            }
            
            return spList;
        }

        #endregion Public Methods
    }
}