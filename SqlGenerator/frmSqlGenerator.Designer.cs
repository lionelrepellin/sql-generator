namespace SqlGenerator
{
	partial class FrmSqlGenerator
	{
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		/// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Code généré par le Concepteur Windows Form

		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSqlGenerator));
            this.listBoxStoredProcedure = new System.Windows.Forms.ListBox();
            this.txtFindStoredProcedure = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxSelectedStoredProcedure = new System.Windows.Forms.ListBox();
            this.btnSelectStoredProcedure = new System.Windows.Forms.Button();
            this.btnUnselectStoredProcedure = new System.Windows.Forms.Button();
            this.btnErase = new System.Windows.Forms.Button();
            this.openCsvFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnOpenCSV = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnExtractStoredProcedures = new System.Windows.Forms.Button();
            this.chkGenerateSingleFile = new System.Windows.Forms.CheckBox();
            this.btnExecute = new System.Windows.Forms.Button();
            this.ctrlEnvironment1 = new SqlGenerator.CtrlEnvironment();
            this.chkGenerateTemplate = new System.Windows.Forms.CheckBox();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxStoredProcedure
            // 
            this.listBoxStoredProcedure.FormattingEnabled = true;
            this.listBoxStoredProcedure.Location = new System.Drawing.Point(12, 117);
            this.listBoxStoredProcedure.Name = "listBoxStoredProcedure";
            this.listBoxStoredProcedure.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxStoredProcedure.Size = new System.Drawing.Size(300, 498);
            this.listBoxStoredProcedure.Sorted = true;
            this.listBoxStoredProcedure.TabIndex = 0;
            this.listBoxStoredProcedure.DoubleClick += new System.EventHandler(this.lstStoredProcedure_DoubleClick);
            // 
            // txtFindStoredProcedure
            // 
            this.txtFindStoredProcedure.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtFindStoredProcedure.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtFindStoredProcedure.Location = new System.Drawing.Point(12, 91);
            this.txtFindStoredProcedure.Name = "txtFindStoredProcedure";
            this.txtFindStoredProcedure.Size = new System.Drawing.Size(265, 20);
            this.txtFindStoredProcedure.TabIndex = 1;
            this.txtFindStoredProcedure.TextChanged += new System.EventHandler(this.txtFindStoredProcedure_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Rechercher une procédure :";
            // 
            // listBoxSelectedStoredProcedure
            // 
            this.listBoxSelectedStoredProcedure.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxSelectedStoredProcedure.FormattingEnabled = true;
            this.listBoxSelectedStoredProcedure.Location = new System.Drawing.Point(407, 117);
            this.listBoxSelectedStoredProcedure.Name = "listBoxSelectedStoredProcedure";
            this.listBoxSelectedStoredProcedure.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxSelectedStoredProcedure.Size = new System.Drawing.Size(300, 459);
            this.listBoxSelectedStoredProcedure.Sorted = true;
            this.listBoxSelectedStoredProcedure.TabIndex = 3;
            this.listBoxSelectedStoredProcedure.DoubleClick += new System.EventHandler(this.lstSelectedStoredProcedure_DoubleClick);
            // 
            // btnSelectStoredProcedure
            // 
            this.btnSelectStoredProcedure.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectStoredProcedure.Image")));
            this.btnSelectStoredProcedure.Location = new System.Drawing.Point(318, 257);
            this.btnSelectStoredProcedure.Name = "btnSelectStoredProcedure";
            this.btnSelectStoredProcedure.Size = new System.Drawing.Size(83, 58);
            this.btnSelectStoredProcedure.TabIndex = 4;
            this.btnSelectStoredProcedure.UseVisualStyleBackColor = true;
            this.btnSelectStoredProcedure.Click += new System.EventHandler(this.btnSelectStoredProcedure_Click);
            // 
            // btnUnselectStoredProcedure
            // 
            this.btnUnselectStoredProcedure.Image = ((System.Drawing.Image)(resources.GetObject("btnUnselectStoredProcedure.Image")));
            this.btnUnselectStoredProcedure.Location = new System.Drawing.Point(318, 357);
            this.btnUnselectStoredProcedure.Name = "btnUnselectStoredProcedure";
            this.btnUnselectStoredProcedure.Size = new System.Drawing.Size(83, 58);
            this.btnUnselectStoredProcedure.TabIndex = 5;
            this.btnUnselectStoredProcedure.UseVisualStyleBackColor = true;
            this.btnUnselectStoredProcedure.Click += new System.EventHandler(this.btnUnselectStoredProcedure_Click);
            // 
            // btnErase
            // 
            this.btnErase.Image = ((System.Drawing.Image)(resources.GetObject("btnErase.Image")));
            this.btnErase.Location = new System.Drawing.Point(283, 86);
            this.btnErase.Name = "btnErase";
            this.btnErase.Size = new System.Drawing.Size(29, 26);
            this.btnErase.TabIndex = 6;
            this.btnErase.UseVisualStyleBackColor = true;
            this.btnErase.Click += new System.EventHandler(this.btnErase_Click);
            // 
            // openCsvFileDialog
            // 
            this.openCsvFileDialog.DefaultExt = "*.csv";
            this.openCsvFileDialog.Filter = "Fichiers CSV|*.csv";
            this.openCsvFileDialog.Title = "Sélectionnez un fichier .csv";
            // 
            // btnOpenCSV
            // 
            this.btnOpenCSV.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenCSV.Image")));
            this.btnOpenCSV.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOpenCSV.Location = new System.Drawing.Point(544, 20);
            this.btnOpenCSV.Name = "btnOpenCSV";
            this.btnOpenCSV.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnOpenCSV.Size = new System.Drawing.Size(163, 36);
            this.btnOpenCSV.TabIndex = 9;
            this.btnOpenCSV.Text = "Importer un fichier .CSV";
            this.btnOpenCSV.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOpenCSV.UseVisualStyleBackColor = true;
            this.btnOpenCSV.Click += new System.EventHandler(this.btnOpenCSV_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 631);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(719, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.Stretch = false;
            this.statusStrip.TabIndex = 10;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // btnExtractStoredProcedures
            // 
            this.btnExtractStoredProcedures.Image = ((System.Drawing.Image)(resources.GetObject("btnExtractStoredProcedures.Image")));
            this.btnExtractStoredProcedures.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExtractStoredProcedures.Location = new System.Drawing.Point(407, 582);
            this.btnExtractStoredProcedures.Name = "btnExtractStoredProcedures";
            this.btnExtractStoredProcedures.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnExtractStoredProcedures.Size = new System.Drawing.Size(163, 37);
            this.btnExtractStoredProcedures.TabIndex = 13;
            this.btnExtractStoredProcedures.Text = "Extraire les procédures";
            this.btnExtractStoredProcedures.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExtractStoredProcedures.UseVisualStyleBackColor = true;
            this.btnExtractStoredProcedures.Click += new System.EventHandler(this.btnExtractStoredProcedures_Click);
            // 
            // chkGenerateSingleFile
            // 
            this.chkGenerateSingleFile.AutoSize = true;
            this.chkGenerateSingleFile.Location = new System.Drawing.Point(407, 95);
            this.chkGenerateSingleFile.Name = "chkGenerateSingleFile";
            this.chkGenerateSingleFile.Size = new System.Drawing.Size(210, 17);
            this.chkGenerateSingleFile.TabIndex = 15;
            this.chkGenerateSingleFile.Text = "Générer un seul fichier : procedures.sql";
            this.chkGenerateSingleFile.UseVisualStyleBackColor = true;
            // 
            // btnExecute
            // 
            this.btnExecute.Image = ((System.Drawing.Image)(resources.GetObject("btnExecute.Image")));
            this.btnExecute.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExecute.Location = new System.Drawing.Point(576, 582);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Padding = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.btnExecute.Size = new System.Drawing.Size(131, 37);
            this.btnExecute.TabIndex = 16;
            this.btnExecute.Text = "Exécuter";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // ctrlEnvironment1
            // 
            this.ctrlEnvironment1.Location = new System.Drawing.Point(9, 8);
            this.ctrlEnvironment1.Name = "ctrlEnvironment1";
            this.ctrlEnvironment1.Size = new System.Drawing.Size(503, 57);
            this.ctrlEnvironment1.TabIndex = 17;
            this.ctrlEnvironment1.RadioChanged += new System.EventHandler<SqlGenerator.EnvironmentEventArgs>(this.ctrlEnvironment1_RadioChanged);
            // 
            // chkGenerateTemplate
            // 
            this.chkGenerateTemplate.AutoSize = true;
            this.chkGenerateTemplate.Location = new System.Drawing.Point(407, 75);
            this.chkGenerateTemplate.Name = "chkGenerateTemplate";
            this.chkGenerateTemplate.Size = new System.Drawing.Size(179, 17);
            this.chkGenerateTemplate.TabIndex = 18;
            this.chkGenerateTemplate.Text = "Générer un fichier : template.csv";
            this.chkGenerateTemplate.UseVisualStyleBackColor = true;
            // 
            // FrmSqlGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 653);
            this.Controls.Add(this.chkGenerateTemplate);
            this.Controls.Add(this.ctrlEnvironment1);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.chkGenerateSingleFile);
            this.Controls.Add(this.btnExtractStoredProcedures);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.btnOpenCSV);
            this.Controls.Add(this.btnErase);
            this.Controls.Add(this.btnUnselectStoredProcedure);
            this.Controls.Add(this.btnSelectStoredProcedure);
            this.Controls.Add(this.listBoxSelectedStoredProcedure);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFindStoredProcedure);
            this.Controls.Add(this.listBoxStoredProcedure);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(735, 691);
            this.Name = "FrmSqlGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SqlGenerator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSqlGenerator_FormClosing);
            this.Load += new System.EventHandler(this.SqlGenerator_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox listBoxStoredProcedure;
		private System.Windows.Forms.TextBox txtFindStoredProcedure;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox listBoxSelectedStoredProcedure;
		private System.Windows.Forms.Button btnSelectStoredProcedure;
		private System.Windows.Forms.Button btnUnselectStoredProcedure;
		private System.Windows.Forms.Button btnErase;
		private System.Windows.Forms.OpenFileDialog openCsvFileDialog;
		private System.Windows.Forms.Button btnOpenCSV;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
		private System.Windows.Forms.Button btnExtractStoredProcedures;
		private System.Windows.Forms.CheckBox chkGenerateSingleFile;
		private System.Windows.Forms.Button btnExecute;
		private CtrlEnvironment ctrlEnvironment1;
        private System.Windows.Forms.CheckBox chkGenerateTemplate;
	}
}

