namespace LoginSystemWithRSA
{
    partial class RegistrationForm
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
            this.firstNameLabel = new MetroFramework.Controls.MetroLabel();
            this.UserNameTextbox = new MetroFramework.Controls.MetroTextBox();
            this.RegiterBtn = new MetroFramework.Controls.MetroButton();
            this.PasswordTextbox = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.EmailTextbox = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // firstNameLabel
            // 
            this.firstNameLabel.AutoSize = true;
            this.firstNameLabel.Location = new System.Drawing.Point(109, 48);
            this.firstNameLabel.Name = "firstNameLabel";
            this.firstNameLabel.Size = new System.Drawing.Size(75, 19);
            this.firstNameLabel.TabIndex = 0;
            this.firstNameLabel.Text = "User Name";
            // 
            // UserNameTextbox
            // 
            this.UserNameTextbox.Location = new System.Drawing.Point(207, 40);
            this.UserNameTextbox.Multiline = true;
            this.UserNameTextbox.Name = "UserNameTextbox";
            this.UserNameTextbox.Size = new System.Drawing.Size(229, 27);
            this.UserNameTextbox.TabIndex = 1;
            // 
            // RegiterBtn
            // 
            this.RegiterBtn.Location = new System.Drawing.Point(207, 179);
            this.RegiterBtn.Name = "RegiterBtn";
            this.RegiterBtn.Size = new System.Drawing.Size(94, 36);
            this.RegiterBtn.TabIndex = 2;
            this.RegiterBtn.Text = "Registration";
            this.RegiterBtn.Click += new System.EventHandler(this.RegiterBtn_Click);
            // 
            // PasswordTextbox
            // 
            this.PasswordTextbox.Location = new System.Drawing.Point(207, 86);
            this.PasswordTextbox.Multiline = true;
            this.PasswordTextbox.Name = "PasswordTextbox";
            this.PasswordTextbox.Size = new System.Drawing.Size(229, 27);
            this.PasswordTextbox.TabIndex = 4;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(109, 94);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(67, 19);
            this.metroLabel1.TabIndex = 3;
            this.metroLabel1.Text = "Password ";
            // 
            // EmailTextbox
            // 
            this.EmailTextbox.Location = new System.Drawing.Point(207, 128);
            this.EmailTextbox.Multiline = true;
            this.EmailTextbox.Name = "EmailTextbox";
            this.EmailTextbox.Size = new System.Drawing.Size(229, 27);
            this.EmailTextbox.TabIndex = 6;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(109, 136);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(41, 19);
            this.metroLabel2.TabIndex = 5;
            this.metroLabel2.Text = "Email";
            // 
            // RegistrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.EmailTextbox);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.PasswordTextbox);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.RegiterBtn);
            this.Controls.Add(this.UserNameTextbox);
            this.Controls.Add(this.firstNameLabel);
            this.Name = "RegistrationForm";
            this.Text = "Registration Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel firstNameLabel;
        private MetroFramework.Controls.MetroTextBox UserNameTextbox;
        private MetroFramework.Controls.MetroButton RegiterBtn;
        private MetroFramework.Controls.MetroTextBox PasswordTextbox;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox EmailTextbox;
        private MetroFramework.Controls.MetroLabel metroLabel2;
    }
}