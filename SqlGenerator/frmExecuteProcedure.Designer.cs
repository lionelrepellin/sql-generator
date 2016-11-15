namespace SqlGenerator
{
    /// <summary>
    /// Module d'exécution des procédures
    /// </summary>
    partial class FrmExecuteProcedure
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExecuteProcedure));
            this.label1 = new System.Windows.Forms.Label();
            this.lblEnvironment = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNbProc = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnExecuteStoredProcedure = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblProcInProgress = new System.Windows.Forms.Label();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.ctrlEnvironment1 = new SqlGenerator.CtrlEnvironment();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dossier sélectionné : ";
            // 
            // lblEnvironment
            // 
            this.lblEnvironment.AutoSize = true;
            this.lblEnvironment.Location = new System.Drawing.Point(236, 77);
            this.lblEnvironment.Name = "lblEnvironment";
            this.lblEnvironment.Size = new System.Drawing.Size(76, 13);
            this.lblEnvironment.TabIndex = 1;
            this.lblEnvironment.Text = "lblEnvironment";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nombre de procédures trouvées :";
            // 
            // lblNbProc
            // 
            this.lblNbProc.AutoSize = true;
            this.lblNbProc.Location = new System.Drawing.Point(236, 108);
            this.lblNbProc.Name = "lblNbProc";
            this.lblNbProc.Size = new System.Drawing.Size(53, 13);
            this.lblNbProc.TabIndex = 3;
            this.lblNbProc.Text = "lblNbProc";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(28, 174);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(460, 22);
            this.progressBar.Step = 1;
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 4;
            // 
            // btnExecuteStoredProcedure
            // 
            this.btnExecuteStoredProcedure.Image = ((System.Drawing.Image)(resources.GetObject("btnExecuteStoredProcedure.Image")));
            this.btnExecuteStoredProcedure.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExecuteStoredProcedure.Location = new System.Drawing.Point(391, 77);
            this.btnExecuteStoredProcedure.Name = "btnExecuteStoredProcedure";
            this.btnExecuteStoredProcedure.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnExecuteStoredProcedure.Size = new System.Drawing.Size(104, 45);
            this.btnExecuteStoredProcedure.TabIndex = 5;
            this.btnExecuteStoredProcedure.Text = "Exécuter";
            this.btnExecuteStoredProcedure.UseVisualStyleBackColor = true;
            this.btnExecuteStoredProcedure.Click += new System.EventHandler(this.btnExecuteStoredProcedure_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Procédure en cours d\'exécution :";
            // 
            // lblProcInProgress
            // 
            this.lblProcInProgress.AutoSize = true;
            this.lblProcInProgress.Location = new System.Drawing.Point(236, 139);
            this.lblProcInProgress.Name = "lblProcInProgress";
            this.lblProcInProgress.Size = new System.Drawing.Size(89, 13);
            this.lblProcInProgress.TabIndex = 7;
            this.lblProcInProgress.Text = "lblProcInProgress";
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // ctrlEnvironment1
            // 
            this.ctrlEnvironment1.Location = new System.Drawing.Point(10, 7);
            this.ctrlEnvironment1.Name = "ctrlEnvironment1";
            this.ctrlEnvironment1.Size = new System.Drawing.Size(497, 54);
            this.ctrlEnvironment1.TabIndex = 8;
            this.ctrlEnvironment1.RadioChanged += new System.EventHandler<SqlGenerator.EnvironmentEventArgs>(this.ctrlEnvironment1_RadioChanged);
            // 
            // frmExecuteProcedure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 212);
            this.Controls.Add(this.ctrlEnvironment1);
            this.Controls.Add(this.lblProcInProgress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnExecuteStoredProcedure);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblNbProc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblEnvironment);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmExecuteProcedure";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Exécute les procédures sur la base de données sélectionnée";
            this.Load += new System.EventHandler(this.frmExecuteProcedure_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblEnvironment;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblNbProc;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnExecuteStoredProcedure;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblProcInProgress;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private CtrlEnvironment ctrlEnvironment1;
    }
}