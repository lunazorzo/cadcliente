namespace Sistema
{
    partial class frmPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("SIS001 - Sistema", 1, 1);
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Exemplo", 0, 0, new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.menuPrincipal = new System.Windows.Forms.MenuStrip();
            this.ArvoreAcesso = new System.Windows.Forms.TreeView();
            this.BarraStatus = new System.Windows.Forms.StatusStrip();
            this.SuspendLayout();
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "folder_stand.png");
            this.imgList.Images.SetKeyName(1, "folder.png");
            // 
            // menuPrincipal
            // 
            this.menuPrincipal.BackColor = System.Drawing.Color.Transparent;
            this.menuPrincipal.Location = new System.Drawing.Point(0, 0);
            this.menuPrincipal.Name = "menuPrincipal";
            this.menuPrincipal.Size = new System.Drawing.Size(1021, 24);
            this.menuPrincipal.TabIndex = 1;
            // 
            // ArvoreAcesso
            // 
            this.ArvoreAcesso.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ArvoreAcesso.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.ArvoreAcesso.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ArvoreAcesso.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ArvoreAcesso.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ArvoreAcesso.ImageIndex = 0;
            this.ArvoreAcesso.ImageList = this.imgList;
            this.ArvoreAcesso.Indent = 15;
            this.ArvoreAcesso.ItemHeight = 25;
            this.ArvoreAcesso.Location = new System.Drawing.Point(12, 39);
            this.ArvoreAcesso.Name = "ArvoreAcesso";
            treeNode3.ImageIndex = 1;
            treeNode3.Name = "Node17";
            treeNode3.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeNode3.SelectedImageIndex = 1;
            treeNode3.Text = "SIS001 - Sistema";
            treeNode4.ImageIndex = 0;
            treeNode4.Name = "Node2";
            treeNode4.SelectedImageIndex = 0;
            treeNode4.Text = "Exemplo";
            this.ArvoreAcesso.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4});
            this.ArvoreAcesso.SelectedImageIndex = 0;
            this.ArvoreAcesso.ShowNodeToolTips = true;
            this.ArvoreAcesso.Size = new System.Drawing.Size(305, 660);
            this.ArvoreAcesso.TabIndex = 3;
            this.ArvoreAcesso.Visible = false;
            this.ArvoreAcesso.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.ArvoreAcesso_NodeMouseClick);
            // 
            // BarraStatus
            // 
            this.BarraStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BarraStatus.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.BarraStatus.Location = new System.Drawing.Point(0, 718);
            this.BarraStatus.Name = "BarraStatus";
            this.BarraStatus.Size = new System.Drawing.Size(1021, 22);
            this.BarraStatus.TabIndex = 4;
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1021, 740);
            this.Controls.Add(this.BarraStatus);
            this.Controls.Add(this.ArvoreAcesso);
            this.Controls.Add(this.menuPrincipal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.MenuStrip menuPrincipal;
        private System.Windows.Forms.TreeView ArvoreAcesso;
        private System.Windows.Forms.StatusStrip BarraStatus;


    }
}

