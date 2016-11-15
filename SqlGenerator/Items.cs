using System;
using System.Collections.Generic;
using System.Linq;

namespace SqlGenerator
{
    /// <summary>
    /// Classe regoupant les méthodes de manipulation des procédures
    /// </summary>
    public class Items
    {
        #region Fields

        private List<StoredProcedure> storedProcedureList;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Constructeur, initialise la liste des procédures
        /// </summary>
        public Items()
        {
            storedProcedureList = new List<StoredProcedure>();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Liste des procédures stockées et fonctions
        /// </summary>
        public List<StoredProcedure> StoredProcedureList
        {
            get { return storedProcedureList; }
            set { storedProcedureList = value; }
        }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Vide tous les éléments contenus dans la liste
        /// </summary>
        public void Clear()
        {
            StoredProcedureList.Clear();
        }

        /// <summary>
        /// Remplit la ListBox en tenant compte des éléments déjà séléctionnés dans l'autre ListBox
        /// </summary>
        /// <param name="listBox">ListBox qui sera remplie</param>
        /// <param name="listBoxSelected">ListBox qui contient des éléments déjà sélectionnés</param>
        /// <param name="list">Liste de procédures qui sera insérée dans la ListBox</param>
        public void FillStoredProcedureList(System.Windows.Forms.ListBox listBox, System.Windows.Forms.ListBox listBoxSelected, List<StoredProcedure> list = null)
        {
            if (listBox != null && listBoxSelected != null)
            {
                listBox.Items.Clear();

                List<StoredProcedure> spListSource = (list == null) ? StoredProcedureList : list;

                var spToAdd = (from p in spListSource
                               where !(from s in listBoxSelected.Items.OfType<StoredProcedure>() select s.Name).Contains(p.Name)
                               select p);

                listBox.Items.AddRange(spToAdd.ToArray());
            }
        }

        /// <summary>
        /// Retourne la liste des procédures qui contiennent le mot recherché
        /// </summary>
        /// <param name="word">Mot à recherché dans la liste des procédures</param>
        /// <returns>Liste des procédures filtrés en fonction du mot recherché</returns>
        public List<StoredProcedure> GetFilteredList(string word)
        {
            if (!String.IsNullOrEmpty(word))
            {
                return this.StoredProcedureList.Where(p => p.Name.ToLower().Contains(word.Trim().ToLower())).AsParallel().ToList();
            }
            else return new List<StoredProcedure>();
        }

        /// <summary>
        /// Retourne la liste des procédures séparées par des virgules (ex: 'proc1','proc2','proc3')
        /// </summary>
        /// <returns>'proc1','proc2','proc3'</returns>
        public string GetItemsList()
        {
            if (StoredProcedureList != null)
            {
                var result = String.Empty;

                foreach (StoredProcedure sp in StoredProcedureList)
                {
                    if (!String.IsNullOrEmpty(result))
                        result += ", ";

                    result += "'" + sp.Name + "'";
                }

                return result;
            }

            return String.Empty;
        }

        #endregion Public Methods
    }
}