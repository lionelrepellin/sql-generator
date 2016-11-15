using System;

namespace SqlGenerator
{
    #region Enumerations

    /// <summary>
    /// Liste des environnements
    /// </summary>
    public enum WorkingEnvironment
    {
        /// <summary>
        /// Développement
        /// </summary>
        DEV,

        /// <summary>
        /// Production
        /// </summary>
        PROD,

        /// <summary>
        /// Pré-production
        /// </summary>
        PREPROD,

        /// <summary>
        /// Recette
        /// </summary>
        RECETTE
    }

    #endregion Enumerations

    /// <summary>
    /// Classe regroupant les constantes
    /// </summary>
    public static class Constant
    {
        #region Fields

        /// <summary>
        /// Le nom du fichier unique regroupant toutes les procédures
        /// </summary>
        public static readonly string SingleFileName = "procedures.sql";

        /// <summary>
        /// Le nom du fichier template contenant les procédures sélectionnées
        /// </summary>
        public static readonly string TemplateCsv = "template.csv";

        #endregion Fields
    }

    /// <summary>
    /// Classe d'extension de la classe String
    /// </summary>
    public static class StringExtension
    {
        #region Public Methods

        /// <summary>
        /// Nettoie le nom de la procédure stockée du CSV
        /// </summary>
        /// <param name="procName">Le nom de la procédure</param>
        /// <returns>Retourne le nom des procédures nettoyé</returns>
        public static string CleanProc(this string procName)
        {
            if(!String.IsNullOrEmpty(procName))
                return procName.Replace(";", String.Empty).Trim();

            return string.Empty;
        }

        #endregion Public Methods
    }
}