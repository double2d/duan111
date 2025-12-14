namespace Program_Network_Project
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.plnChessBoard = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pctbAvatar = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLAN = new System.Windows.Forms.Button();
            this.txbIP = new System.Windows.Forms.TextBox();
            this.pctbMark = new System.Windows.Forms.PictureBox();
            this.prcbCoolDown = new System.Windows.Forms.ProgressBar();
            this.txbPlayerName = new System.Windows.Forms.TextBox();
            this.tmCoolDown = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vánMớiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thoátTròChơiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctbAvatar)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctbMark)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // plnChessBoard
            // 
            // Anchor to left and stretch vertically so it keeps position when form resizes
            this.plnChessBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom)));
            this.plnChessBoard.Location = new System.Drawing.Point(10, 37);
            this.plnChessBoard.Name = "plnChessBoard";
            this.plnChessBoard.Size = new System.Drawing.Size(458, 373);
            this.plnChessBoard.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pctbAvatar);
            // keep panel2 anchored to top-right so it stays on the right side
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Location = new System.Drawing.Point(473, 37);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(300, 228);
            this.panel2.TabIndex = 1;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // pctbAvatar
            // 
            // avatar should anchor to all sides of its parent to scale with it
            this.pctbAvatar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
| System.Windows.Forms.AnchorStyles.Left)
| System.Windows.Forms.AnchorStyles.Right)));
            this.pctbAvatar.BackgroundImage = global::Program_Network_Project.Properties.Resources.caro1;
            this.pctbAvatar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pctbAvatar.Location = new System.Drawing.Point(3, 3);
            this.pctbAvatar.Name = "pctbAvatar";
            this.pctbAvatar.Size = new System.Drawing.Size(294, 225);
            this.pctbAvatar.TabIndex = 0;
            this.pctbAvatar.TabStop = false;
            // 
            // panel3
            // 
            // Control panel on the right: keep anchored to top-right to remain in place
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.btnLAN);
            this.panel3.Controls.Add(this.txbIP);
            this.panel3.Controls.Add(this.pctbMark);
            this.panel3.Controls.Add(this.prcbCoolDown);
            this.panel3.Controls.Add(this.txbPlayerName);
            this.panel3.Location = new System.Drawing.Point(474, 271);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(294, 139);
            this.panel3.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // anchor label to top-left so it keeps a stable position
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Location = new System.Drawing.Point(72, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(561, 24);
            this.label1.TabIndex = 5;
            this.label1.Text = "Bạn sẽ thắng nếu đi được 5 ô liên tiếp.Thời gian suy nghỉ là 10s";
            // 
            // btnLAN
            // 
            this.btnLAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLAN.Location = new System.Drawing.Point(100, 109);
            this.btnLAN.Name = "btnLAN";
            this.btnLAN.Size = new System.Drawing.Size(75, 23);
            this.btnLAN.TabIndex = 4;
            this.btnLAN.Text = "Kết nối";
            this.btnLAN.UseVisualStyleBackColor = true;
            this.btnLAN.Click += new System.EventHandler(this.btnLAN_Click);
            // 
            // txbIP
            // 
            this.txbIP.Location = new System.Drawing.Point(15, 83);
            this.txbIP.Name = "txbIP";
            this.txbIP.Size = new System.Drawing.Size(160, 20);
            this.txbIP.TabIndex = 3;
            // 
            // pctbMark
            // 
            this.pctbMark.Location = new System.Drawing.Point(181, 18);
            this.pctbMark.Name = "pctbMark";
            this.pctbMark.Size = new System.Drawing.Size(96, 85);
            this.pctbMark.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctbMark.TabIndex = 2;
            this.pctbMark.TabStop = false;
            // 
            // prcbCoolDown
            // 
            this.prcbCoolDown.Location = new System.Drawing.Point(15, 54);
            this.prcbCoolDown.Name = "prcbCoolDown";
            this.prcbCoolDown.Size = new System.Drawing.Size(160, 23);
            this.prcbCoolDown.TabIndex = 1;
            // 
            // txbPlayerName
            // 
            this.txbPlayerName.Location = new System.Drawing.Point(15, 17);
            this.txbPlayerName.Name = "txbPlayerName";
            this.txbPlayerName.ReadOnly = false;
            this.txbPlayerName.Size = new System.Drawing.Size(160, 20);
            this.txbPlayerName.TabIndex = 0;
            // 
            // tmCoolDown
            // 
            this.tmCoolDown.Tick += new System.EventHandler(this.tmCoolDown_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(775, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vánMớiToolStripMenuItem,
            this.thoátTròChơiToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // vánMớiToolStripMenuItem
            // 
            this.vánMớiToolStripMenuItem.Name = "vánMớiToolStripMenuItem";
            this.vánMớiToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.vánMớiToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.vánMớiToolStripMenuItem.Text = "Ván mới";
            this.vánMớiToolStripMenuItem.Click += new System.EventHandler(this.vánMớiToolStripMenuItem_Click);
            // 
            // thoátTròChơiToolStripMenuItem
            // 
            this.thoátTròChơiToolStripMenuItem.Name = "thoátTròChơiToolStripMenuItem";
            this.thoátTròChơiToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.thoátTròChơiToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.thoátTròChơiToolStripMenuItem.Text = "Thoát trò chơi";
            this.thoátTròChơiToolStripMenuItem.Click += new System.EventHandler(this.thoátTròChơiToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 422);
            // enforce a minimum size to avoid control overlap
            this.MinimumSize = new System.Drawing.Size(800, 460);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.plnChessBoard);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "CỜ CARO";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pctbAvatar)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctbMark)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel plnChessBoard;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pctbAvatar;
        private System.Windows.Forms.ProgressBar prcbCoolDown;
        private System.Windows.Forms.TextBox txbPlayerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLAN;
        private System.Windows.Forms.TextBox txbIP;
        private System.Windows.Forms.PictureBox pctbMark;
        private System.Windows.Forms.Timer tmCoolDown;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vánMớiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thoátTròChơiToolStripMenuItem;
    }
}

