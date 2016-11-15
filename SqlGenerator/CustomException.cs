using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace SqlGenerator
{
    #region Enumerations

    /// <summary>
    /// Type d'action suite à une exception
    /// </summary>
    public enum LogAction
    {
        /// <summary>
        /// Aucune action
        /// </summary>
        NONE,

        /// <summary>
        /// Enregistre l'exception dans le gestionnaire d'événement
        /// </summary>
        EVENT,

        /// <summary>
        /// Enregistre l'exception dans un fichier de log
        /// </summary>
        FILE,

        /// <summary>
        /// Enregistre l'exception dans le gestionnaire d'événement et un fichier de log
        /// </summary>
        BOTH
    }

    #endregion Enumerations

    /// <summary>
    /// Classe qui hérite de Exception, permet d'enregistrer les messages dans le gestionnaire d'événements Windows
    /// </summary>
    public class CustomException : System.Exception
    {
        #region Fields

        private string source = "SqlGenerator";

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="message">Message de l'exception</param>
        public CustomException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="message">Message de l'exception</param>
        /// <param name="innerException">Exception interne</param>
        public CustomException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Enregistre l'exception dans le gestionnaire d'événements
        /// </summary>
        /// <param name="message">Message en "clair"</param>
        /// <param name="innerException">Exception</param>
        /// <param name="logAction">Action faite suite à la levée d'exception</param>
        public CustomException(string message, Exception innerException, LogAction logAction)
            : base(message, innerException)
        {
            if (!String.IsNullOrEmpty(message) && innerException != null)
            {
                var msg = message + Environment.NewLine + innerException.Message;

                if (logAction == LogAction.FILE)
                    Tools.WriteLogFile(msg);
                else if (logAction == LogAction.EVENT)
                    Tools.WriteLogEvent(this.source, msg);
                else if (logAction == LogAction.BOTH)
                {
                    Tools.WriteLogEvent(this.source, msg);
                    Tools.WriteLogFile(msg);
                }
            }
        }

        #endregion Constructors
    }
}