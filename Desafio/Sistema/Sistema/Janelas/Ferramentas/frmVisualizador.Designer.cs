namespace Sistema.Janelas.Ferramentas
{
    partial class frmVisualizador
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVisualizador));
            this.panel1 = new System.Windows.Forms.Panel();
            this.nrPagina = new DevExpress.XtraEditors.TextEdit();
            this.btnAvancar = new DevExpress.XtraEditors.SimpleButton();
            this.btnRetornar = new DevExpress.XtraEditors.SimpleButton();
            this.btnMaisZoon = new DevExpress.XtraEditors.SimpleButton();
            this.btnMenosZoon = new DevExpress.XtraEditors.SimpleButton();
            this.btnEmail = new DevExpress.XtraEditors.SimpleButton();
            this.btnExportar = new DevExpress.XtraEditors.SimpleButton();
            this.btnImprimir = new DevExpress.XtraEditors.SimpleButton();
            this.preview = new FastReport.Preview.PreviewControl();
            this.pnEmail = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.btnEnviar = new DevExpress.XtraEditors.SimpleButton();
            this.txtObservacao = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtTitulo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.gcGrid = new DevExpress.XtraGrid.GridControl();
            this.menuGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excluirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gvGrid = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ds_email = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnAddEmail = new DevExpress.XtraEditors.SimpleButton();
            this.txtEmail = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.menuExportacao = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nrPagina.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnEmail)).BeginInit();
            this.pnEmail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtObservacao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitulo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcGrid)).BeginInit();
            this.menuGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.nrPagina);
            this.panel1.Controls.Add(this.btnAvancar);
            this.panel1.Controls.Add(this.btnRetornar);
            this.panel1.Controls.Add(this.btnMaisZoon);
            this.panel1.Controls.Add(this.btnMenosZoon);
            this.panel1.Controls.Add(this.btnEmail);
            this.panel1.Controls.Add(this.btnExportar);
            this.panel1.Controls.Add(this.btnImprimir);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 47);
            this.panel1.TabIndex = 2;
            // 
            // nrPagina
            // 
            this.nrPagina.EditValue = "";
            this.nrPagina.Location = new System.Drawing.Point(303, 12);
            this.nrPagina.Name = "nrPagina";
            this.nrPagina.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nrPagina.Properties.Appearance.Options.UseFont = true;
            this.nrPagina.Properties.Appearance.Options.UseTextOptions = true;
            this.nrPagina.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.nrPagina.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.nrPagina.Size = new System.Drawing.Size(54, 22);
            this.nrPagina.TabIndex = 0;
            this.nrPagina.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nrPagina_KeyDown);
            this.nrPagina.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nrPagina_KeyPress);
            // 
            // btnAvancar
            // 
            this.btnAvancar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAvancar.Appearance.Options.UseFont = true;
            this.btnAvancar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnAvancar.Image = ((System.Drawing.Image)(resources.GetObject("btnAvancar.Image")));
            this.btnAvancar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnAvancar.Location = new System.Drawing.Point(363, 3);
            this.btnAvancar.Name = "btnAvancar";
            this.btnAvancar.Size = new System.Drawing.Size(44, 41);
            this.btnAvancar.TabIndex = 27;
            this.btnAvancar.Click += new System.EventHandler(this.btnAvancar_Click);
            // 
            // btnRetornar
            // 
            this.btnRetornar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRetornar.Appearance.Options.UseFont = true;
            this.btnRetornar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnRetornar.Image = ((System.Drawing.Image)(resources.GetObject("btnRetornar.Image")));
            this.btnRetornar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnRetornar.Location = new System.Drawing.Point(253, 3);
            this.btnRetornar.Name = "btnRetornar";
            this.btnRetornar.Size = new System.Drawing.Size(44, 41);
            this.btnRetornar.TabIndex = 24;
            this.btnRetornar.Click += new System.EventHandler(this.btnRetornar_Click);
            // 
            // btnMaisZoon
            // 
            this.btnMaisZoon.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaisZoon.Appearance.Options.UseFont = true;
            this.btnMaisZoon.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnMaisZoon.Image = ((System.Drawing.Image)(resources.GetObject("btnMaisZoon.Image")));
            this.btnMaisZoon.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnMaisZoon.Location = new System.Drawing.Point(203, 3);
            this.btnMaisZoon.Name = "btnMaisZoon";
            this.btnMaisZoon.Size = new System.Drawing.Size(44, 41);
            this.btnMaisZoon.TabIndex = 23;
            this.btnMaisZoon.Click += new System.EventHandler(this.btnMaisZoon_Click);
            // 
            // btnMenosZoon
            // 
            this.btnMenosZoon.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenosZoon.Appearance.Options.UseFont = true;
            this.btnMenosZoon.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnMenosZoon.Image = ((System.Drawing.Image)(resources.GetObject("btnMenosZoon.Image")));
            this.btnMenosZoon.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnMenosZoon.Location = new System.Drawing.Point(153, 3);
            this.btnMenosZoon.Name = "btnMenosZoon";
            this.btnMenosZoon.Size = new System.Drawing.Size(44, 41);
            this.btnMenosZoon.TabIndex = 22;
            this.btnMenosZoon.Click += new System.EventHandler(this.btnMenosZoon_Click);
            // 
            // btnEmail
            // 
            this.btnEmail.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmail.Appearance.Options.UseFont = true;
            this.btnEmail.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnEmail.Image = ((System.Drawing.Image)(resources.GetObject("btnEmail.Image")));
            this.btnEmail.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnEmail.Location = new System.Drawing.Point(103, 3);
            this.btnEmail.Name = "btnEmail";
            this.btnEmail.Size = new System.Drawing.Size(44, 41);
            this.btnEmail.TabIndex = 21;
            this.btnEmail.Click += new System.EventHandler(this.btnEmail_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportar.Appearance.Options.UseFont = true;
            this.btnExportar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnExportar.Image = ((System.Drawing.Image)(resources.GetObject("btnExportar.Image")));
            this.btnExportar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExportar.Location = new System.Drawing.Point(53, 3);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(44, 41);
            this.btnExportar.TabIndex = 20;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.Appearance.Options.UseFont = true;
            this.btnImprimir.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnImprimir.Location = new System.Drawing.Point(3, 3);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(44, 41);
            this.btnImprimir.TabIndex = 19;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // preview
            // 
            this.preview.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.preview.Font = new System.Drawing.Font("Tahoma", 8F);
            this.preview.Location = new System.Drawing.Point(0, 45);
            this.preview.Name = "preview";
            this.preview.Size = new System.Drawing.Size(1008, 660);
            this.preview.StatusbarVisible = false;
            this.preview.TabIndex = 7;
            this.preview.ToolbarVisible = false;
            this.preview.UIStyle = FastReport.Utils.UIStyle.VisualStudio2005;
            this.preview.PageChanged += new System.EventHandler(this.preview_PageChanged);
            // 
            // pnEmail
            // 
            this.pnEmail.Controls.Add(this.groupControl1);
            this.pnEmail.Location = new System.Drawing.Point(313, 150);
            this.pnEmail.Name = "pnEmail";
            this.pnEmail.Size = new System.Drawing.Size(383, 373);
            this.pnEmail.TabIndex = 10;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnCancelar);
            this.groupControl1.Controls.Add(this.btnEnviar);
            this.groupControl1.Controls.Add(this.txtObservacao);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtTitulo);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.gcGrid);
            this.groupControl1.Controls.Add(this.btnAddEmail);
            this.groupControl1.Controls.Add(this.txtEmail);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Location = new System.Drawing.Point(5, 5);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(373, 363);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "groupControl1";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Appearance.Options.UseFont = true;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.Location = new System.Drawing.Point(184, 332);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(87, 23);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnEnviar
            // 
            this.btnEnviar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviar.Appearance.Options.UseFont = true;
            this.btnEnviar.Image = ((System.Drawing.Image)(resources.GetObject("btnEnviar.Image")));
            this.btnEnviar.Location = new System.Drawing.Point(277, 332);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(84, 23);
            this.btnEnviar.TabIndex = 4;
            this.btnEnviar.Text = "Enviar";
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // txtObservacao
            // 
            this.txtObservacao.EditValue = "";
            this.txtObservacao.Location = new System.Drawing.Point(51, 214);
            this.txtObservacao.Name = "txtObservacao";
            this.txtObservacao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObservacao.Properties.Appearance.Options.UseFont = true;
            this.txtObservacao.Size = new System.Drawing.Size(310, 112);
            this.txtObservacao.TabIndex = 3;
            this.txtObservacao.UseOptimizedRendering = true;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(13, 220);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(28, 16);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "Obs.";
            // 
            // txtTitulo
            // 
            this.txtTitulo.EditValue = "";
            this.txtTitulo.Location = new System.Drawing.Point(51, 186);
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTitulo.Properties.Appearance.Options.UseFont = true;
            this.txtTitulo.Size = new System.Drawing.Size(310, 22);
            this.txtTitulo.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(13, 189);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(35, 16);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Título";
            // 
            // gcGrid
            // 
            this.gcGrid.ContextMenuStrip = this.menuGrid;
            this.gcGrid.Location = new System.Drawing.Point(13, 45);
            this.gcGrid.MainView = this.gvGrid;
            this.gcGrid.Name = "gcGrid";
            this.gcGrid.Size = new System.Drawing.Size(348, 135);
            this.gcGrid.TabIndex = 60;
            this.gcGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvGrid});
            this.gcGrid.DoubleClick += new System.EventHandler(this.gcGrid_DoubleClick);
            // 
            // menuGrid
            // 
            this.menuGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editarToolStripMenuItem,
            this.excluirToolStripMenuItem});
            this.menuGrid.Name = "menuGrid";
            this.menuGrid.Size = new System.Drawing.Size(110, 48);
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.Image = global::Sistema.Properties.Resources.pencil;
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.editarToolStripMenuItem.Text = "Editar";
            this.editarToolStripMenuItem.Click += new System.EventHandler(this.editarToolStripMenuItem_Click);
            // 
            // excluirToolStripMenuItem
            // 
            this.excluirToolStripMenuItem.Image = global::Sistema.Properties.Resources.delete;
            this.excluirToolStripMenuItem.Name = "excluirToolStripMenuItem";
            this.excluirToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.excluirToolStripMenuItem.Text = "Excluir";
            this.excluirToolStripMenuItem.Click += new System.EventHandler(this.excluirToolStripMenuItem_Click);
            // 
            // gvGrid
            // 
            this.gvGrid.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ds_email});
            this.gvGrid.GridControl = this.gcGrid;
            this.gvGrid.Name = "gvGrid";
            this.gvGrid.OptionsView.ShowGroupPanel = false;
            // 
            // ds_email
            // 
            this.ds_email.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ds_email.AppearanceCell.Options.UseFont = true;
            this.ds_email.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ds_email.AppearanceHeader.Options.UseFont = true;
            this.ds_email.Caption = "E-Mail";
            this.ds_email.FieldName = "ds_email";
            this.ds_email.Name = "ds_email";
            this.ds_email.OptionsColumn.AllowEdit = false;
            this.ds_email.OptionsColumn.ReadOnly = true;
            this.ds_email.Visible = true;
            this.ds_email.VisibleIndex = 0;
            // 
            // btnAddEmail
            // 
            this.btnAddEmail.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddEmail.Appearance.Options.UseFont = true;
            this.btnAddEmail.Image = ((System.Drawing.Image)(resources.GetObject("btnAddEmail.Image")));
            this.btnAddEmail.Location = new System.Drawing.Point(336, 16);
            this.btnAddEmail.Name = "btnAddEmail";
            this.btnAddEmail.Size = new System.Drawing.Size(25, 23);
            this.btnAddEmail.TabIndex = 1;
            this.btnAddEmail.Click += new System.EventHandler(this.btnAddEmail_Click);
            // 
            // txtEmail
            // 
            this.txtEmail.EditValue = "";
            this.txtEmail.Location = new System.Drawing.Point(51, 17);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Properties.AccessibleName = "";
            this.txtEmail.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Properties.Appearance.Options.UseFont = true;
            this.txtEmail.Size = new System.Drawing.Size(279, 22);
            this.txtEmail.TabIndex = 0;
            this.txtEmail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmail_KeyDown);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(13, 20);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(32, 16);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Email";
            // 
            // menuExportacao
            // 
            this.menuExportacao.Name = "menuExportacao";
            this.menuExportacao.Size = new System.Drawing.Size(61, 4);
            // 
            // frmVisualizador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 672);
            this.Controls.Add(this.pnEmail);
            this.Controls.Add(this.preview);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmVisualizador";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visualizador de Relatório";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmVisualizador_FormClosed);
            this.Load += new System.EventHandler(this.frmVisualizador_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nrPagina.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnEmail)).EndInit();
            this.pnEmail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtObservacao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitulo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcGrid)).EndInit();
            this.menuGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.TextEdit nrPagina;
        private DevExpress.XtraEditors.SimpleButton btnAvancar;
        private DevExpress.XtraEditors.SimpleButton btnRetornar;
        private DevExpress.XtraEditors.SimpleButton btnMaisZoon;
        private DevExpress.XtraEditors.SimpleButton btnMenosZoon;
        private DevExpress.XtraEditors.SimpleButton btnEmail;
        private DevExpress.XtraEditors.SimpleButton btnExportar;
        private DevExpress.XtraEditors.SimpleButton btnImprimir;
        private FastReport.Preview.PreviewControl preview;
        private DevExpress.XtraEditors.PanelControl pnEmail;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private DevExpress.XtraEditors.SimpleButton btnEnviar;
        private DevExpress.XtraEditors.MemoEdit txtObservacao;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtTitulo;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraGrid.GridControl gcGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView gvGrid;
        private DevExpress.XtraGrid.Columns.GridColumn ds_email;
        private DevExpress.XtraEditors.SimpleButton btnAddEmail;
        private DevExpress.XtraEditors.TextEdit txtEmail;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.ContextMenuStrip menuExportacao;
        private System.Windows.Forms.ContextMenuStrip menuGrid;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excluirToolStripMenuItem;
    }
}