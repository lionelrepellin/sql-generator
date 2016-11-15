namespace SqlGenerator
{
    partial class CtrlEnvironment
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

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.radioBddProd = new System.Windows.Forms.RadioButton();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.radioRecette = new System.Windows.Forms.RadioButton();
            this.radioBddPreProd = new System.Windows.Forms.RadioButton();
            this.radioBddLocal = new System.Windows.Forms.RadioButton();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioBddProd
            // 
            this.radioBddProd.AutoSize = true;
            this.radioBddProd.Dock = System.Windows.Forms.DockStyle.Right;
            this.radioBddProd.Location = new System.Drawing.Point(380, 16);
            this.radioBddProd.Name = "radioBddProd";
            this.radioBddProd.Padding = new System.Windows.Forms.Padding(0, 0, 30, 5);
            this.radioBddProd.Size = new System.Drawing.Size(106, 29);
            this.radioBddProd.TabIndex = 2;
            this.radioBddProd.Tag = "PROD";
            this.radioBddProd.Text = "Production";
            this.radioBddProd.UseVisualStyleBackColor = true;
            this.radioBddProd.CheckedChanged += new System.EventHandler(this.radioBddProd_CheckedChanged);
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.radioRecette);
            this.groupBox.Controls.Add(this.radioBddProd);
            this.groupBox.Controls.Add(this.radioBddPreProd);
            this.groupBox.Controls.Add(this.radioBddLocal);
            this.groupBox.Location = new System.Drawing.Point(4, 3);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(489, 48);
            this.groupBox.TabIndex = 12;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Choix de l\'environnement";
            // 
            // radioRecette
            // 
            this.radioRecette.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioRecette.AutoSize = true;
            this.radioRecette.Location = new System.Drawing.Point(285, 19);
            this.radioRecette.Name = "radioRecette";
            this.radioRecette.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.radioRecette.Size = new System.Drawing.Size(63, 22);
            this.radioRecette.TabIndex = 3;
            this.radioRecette.Tag = "RECETTE";
            this.radioRecette.Text = "Recette";
            this.radioRecette.UseVisualStyleBackColor = true;
            this.radioRecette.CheckedChanged += new System.EventHandler(this.radioRecette_CheckedChanged);
            // 
            // radioBddPreProd
            // 
            this.radioBddPreProd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioBddPreProd.AutoSize = true;
            this.radioBddPreProd.Location = new System.Drawing.Point(164, 19);
            this.radioBddPreProd.Name = "radioBddPreProd";
            this.radioBddPreProd.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.radioBddPreProd.Size = new System.Drawing.Size(95, 22);
            this.radioBddPreProd.TabIndex = 1;
            this.radioBddPreProd.Tag = "PREPROD";
            this.radioBddPreProd.Text = "Pré-Production";
            this.radioBddPreProd.UseVisualStyleBackColor = true;
            this.radioBddPreProd.CheckedChanged += new System.EventHandler(this.radioBddPreProd_CheckedChanged);
            // 
            // radioBddLocal
            // 
            this.radioBddLocal.AutoSize = true;
            this.radioBddLocal.Dock = System.Windows.Forms.DockStyle.Left;
            this.radioBddLocal.Location = new System.Drawing.Point(3, 16);
            this.radioBddLocal.Name = "radioBddLocal";
            this.radioBddLocal.Padding = new System.Windows.Forms.Padding(30, 0, 0, 5);
            this.radioBddLocal.Size = new System.Drawing.Size(130, 29);
            this.radioBddLocal.TabIndex = 0;
            this.radioBddLocal.Tag = "DEV";
            this.radioBddLocal.Text = "Développement";
            this.radioBddLocal.UseVisualStyleBackColor = true;
            this.radioBddLocal.CheckedChanged += new System.EventHandler(this.radioBddLocal_CheckedChanged);
            // 
            // CtrlEnvironment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox);
            this.Name = "CtrlEnvironment";
            this.Size = new System.Drawing.Size(497, 54);
            this.Load += new System.EventHandler(this.ctrlEnvironment_Load);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton radioBddProd;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.RadioButton radioBddPreProd;
        private System.Windows.Forms.RadioButton radioBddLocal;
        private System.Windows.Forms.RadioButton radioRecette;
    }
}
