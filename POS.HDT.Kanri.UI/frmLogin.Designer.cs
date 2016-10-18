namespace POS.HDT.Kanri.UI
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblServer = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.chkAdvanced = new System.Windows.Forms.CheckBox();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblPassword = new System.Windows.Forms.Label();
            this.btnAgree = new System.Windows.Forms.Button();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.lblServer);
            this.panel1.Controls.Add(this.txtServer);
            this.panel1.Controls.Add(this.chkAdvanced);
            this.panel1.Controls.Add(this.pbImage);
            this.panel1.Controls.Add(this.lblUsername);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.lblPassword);
            this.panel1.Controls.Add(this.btnAgree);
            this.panel1.Controls.Add(this.txtUsername);
            this.panel1.Controls.Add(this.txtPassword);
            this.panel1.Location = new System.Drawing.Point(66, 128);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(556, 222);
            this.panel1.TabIndex = 1;
            // 
            // lblServer
            // 
            this.lblServer.Location = new System.Drawing.Point(192, 134);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(128, 20);
            this.lblServer.TabIndex = 27;
            this.lblServer.Text = "Máy chủ";
            this.lblServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblServer.Visible = false;
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(324, 131);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(221, 20);
            this.txtServer.TabIndex = 26;
            this.txtServer.Visible = false;
            // 
            // chkAdvanced
            // 
            this.chkAdvanced.AutoSize = true;
            this.chkAdvanced.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAdvanced.Location = new System.Drawing.Point(324, 104);
            this.chkAdvanced.Name = "chkAdvanced";
            this.chkAdvanced.Size = new System.Drawing.Size(88, 21);
            this.chkAdvanced.TabIndex = 25;
            this.chkAdvanced.Text = "Nâng cao";
            this.chkAdvanced.UseVisualStyleBackColor = true;
            // 
            // pbImage
            // 
            this.pbImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbImage.ImageLocation = "";
            this.pbImage.Location = new System.Drawing.Point(13, 12);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(173, 157);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImage.TabIndex = 24;
            this.pbImage.TabStop = false;
            // 
            // lblUsername
            // 
            this.lblUsername.Location = new System.Drawing.Point(192, 44);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(128, 20);
            this.lblUsername.TabIndex = 18;
            this.lblUsername.Text = "Tên đăng nhập";
            this.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(425, 161);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(122, 48);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Hủy bỏ";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // lblPassword
            // 
            this.lblPassword.Location = new System.Drawing.Point(192, 76);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(128, 20);
            this.lblPassword.TabIndex = 17;
            this.lblPassword.Text = "Mật khẩu";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnAgree
            // 
            this.btnAgree.BackColor = System.Drawing.Color.White;
            this.btnAgree.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgree.Location = new System.Drawing.Point(295, 161);
            this.btnAgree.Name = "btnAgree";
            this.btnAgree.Size = new System.Drawing.Size(122, 48);
            this.btnAgree.TabIndex = 2;
            this.btnAgree.Text = "Đồng ý";
            this.btnAgree.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAgree.UseVisualStyleBackColor = false;
            this.btnAgree.Click += new System.EventHandler(this.btnAgree_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(324, 42);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(221, 20);
            this.txtUsername.TabIndex = 0;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(324, 73);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(221, 20);
            this.txtPassword.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(50, 23);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(590, 79);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "POS SYSTEM";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 372);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblTitle);
            this.Name = "frmLogin";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.CheckBox chkAdvanced;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Button btnAgree;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblTitle;
    }
}

