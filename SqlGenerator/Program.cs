using System;
using System.Collections.Generic;
using System.Windows.Forms;

[assembly: CLSCompliant(true)]

namespace SqlGenerator
{
    static class Program
    {
        #region Private Methods

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmSqlGenerator());
        }

        #endregion Private Methods
    }
}