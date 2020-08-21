namespace Sistema.Janelas.Relatorios.Cadastros
{
    partial class frmRelCliente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRelCliente));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cbOrdenacao = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnImprimir = new DevExpress.XtraEditors.SimpleButton();
            this.txtcdCliente = new DevExpress.XtraEditors.TextEdit();
            this.txtdsCliente = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.cbOrdenacao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcdCliente.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdsCliente.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(27, 43);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(62, 16);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Ordenção";
            // 
            // cbOrdenacao
            // 
            this.cbOrdenacao.Location = new System.Drawing.Point(95, 40);
            this.cbOrdenacao.Name = "cbOrdenacao";
            this.cbOrdenacao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOrdenacao.Properties.Appearance.Options.UseFont = true;
            this.cbOrdenacao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbOrdenacao.Properties.Items.AddRange(new object[] {
            "Código do Cliente ASC",
            "Código do Cliente DESC",
            "Nome do Cliente ASC",
            "Nome do Cliente DESC"});
            this.cbOrdenacao.Size = new System.Drawing.Size(271, 22);
            this.cbOrdenacao.TabIndex = 1;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.Appearance.Options.UseFont = true;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.Location = new System.Drawing.Point(274, 68);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(92, 23);
            this.btnImprimir.TabIndex = 2;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // txtcdCliente
            // 
            this.txtcdCliente.Location = new System.Drawing.Point(95, 12);
            this.txtcdCliente.Name = "txtcdCliente";
            this.txtcdCliente.Properties.AccessibleName = "Código";
            this.txtcdCliente.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcdCliente.Properties.Appearance.Options.UseFont = true;
            this.txtcdCliente.Properties.Appearance.Options.UseTextOptions = true;
            this.txtcdCliente.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtcdCliente.Size = new System.Drawing.Size(55, 22);
            this.txtcdCliente.TabIndex = 194;
            this.txtcdCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtcdCliente_KeyPress);
            this.txtcdCliente.Validated += new System.EventHandler(this.txtcdCliente_Validated);
            // 
            // txtdsCliente
            // 
            this.txtdsCliente.EditValue = "";
            this.txtdsCliente.Enabled = false;
            this.txtdsCliente.Location = new System.Drawing.Point(156, 12);
            this.txtdsCliente.Name = "txtdsCliente";
            this.txtdsCliente.Properties.AccessibleName = "Nome do Contato";
            this.txtdsCliente.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdsCliente.Properties.Appearance.Options.UseFont = true;
            this.txtdsCliente.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtdsCliente.Properties.MaxLength = 100;
            this.txtdsCliente.Size = new System.Drawing.Size(210, 22);
            this.txtdsCliente.TabIndex = 193;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(7, 15);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(83, 16);
            this.labelControl4.TabIndex = 192;
            this.labelControl4.Text = "Nome Cliente";
            // 
            // frmRelCliente
            // 
            this.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 103);
            this.Controls.Add(this.txtcdCliente);
            this.Controls.Add(this.txtdsCliente);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.cbOrdenacao);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmRelCliente";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatório de Cliente\\Contato";
            this.Load += new System.EventHandler(this.frmRelCliente_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmRelCliente_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.cbOrdenacao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcdCliente.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdsCliente.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cbOrdenacao;
        private DevExpress.XtraEditors.SimpleButton btnImprimir;
        public DevExpress.XtraEditors.TextEdit txtcdCliente;
        private DevExpress.XtraEditors.TextEdit txtdsCliente;
        private DevExpress.XtraEditors.LabelControl labelControl4;
    }
}