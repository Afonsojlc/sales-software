namespace SoftwareVendas
{
    partial class FormLogin
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
            pnlCentral = new Panel();
            pnlModoEmail = new Panel();
            button1 = new Button();
            label6 = new Label();
            label3 = new Label();
            label5 = new Label();
            label4 = new Label();
            txtEmail = new TextBox();
            txtSenha = new TextBox();
            pnlModoPin = new Panel();
            label2 = new Label();
            label1 = new Label();
            btnEntrar = new Button();
            txtPIN = new TextBox();
            btnSairApp = new Button();
            pnlCentral.SuspendLayout();
            pnlModoEmail.SuspendLayout();
            pnlModoPin.SuspendLayout();
            SuspendLayout();
            // 
            // pnlCentral
            // 
            pnlCentral.BackColor = Color.Transparent;
            pnlCentral.Controls.Add(pnlModoEmail);
            pnlCentral.Controls.Add(pnlModoPin);
            pnlCentral.Location = new Point(709, 152);
            pnlCentral.Name = "pnlCentral";
            pnlCentral.Size = new Size(878, 1048);
            pnlCentral.TabIndex = 0;
            // 
            // pnlModoEmail
            // 
            pnlModoEmail.BackColor = Color.FromArgb(180, 0, 0, 0);
            pnlModoEmail.Controls.Add(button1);
            pnlModoEmail.Controls.Add(label6);
            pnlModoEmail.Controls.Add(label3);
            pnlModoEmail.Controls.Add(label5);
            pnlModoEmail.Controls.Add(label4);
            pnlModoEmail.Controls.Add(txtEmail);
            pnlModoEmail.Controls.Add(txtSenha);
            pnlModoEmail.Location = new Point(70, 518);
            pnlModoEmail.Name = "pnlModoEmail";
            pnlModoEmail.Size = new Size(740, 466);
            pnlModoEmail.TabIndex = 2;
            pnlModoEmail.Visible = false;
            // 
            // button1
            // 
            button1.BackColor = Color.Transparent;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = SystemColors.ButtonFace;
            button1.Location = new Point(302, 355);
            button1.Name = "button1";
            button1.Size = new Size(150, 54);
            button1.TabIndex = 6;
            button1.Text = "Login";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = SystemColors.ButtonHighlight;
            label6.Location = new Point(286, 0);
            label6.Name = "label6";
            label6.Size = new Size(188, 65);
            label6.TabIndex = 5;
            label6.Text = "LOGIN ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Cursor = Cursors.Hand;
            label3.Font = new Font("Segoe UI", 10.125F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.ButtonFace;
            label3.Location = new Point(286, 301);
            label3.Name = "label3";
            label3.Size = new Size(188, 37);
            label3.TabIndex = 3;
            label3.Text = "Voltar ao PIN";
            label3.TextAlign = ContentAlignment.TopCenter;
            label3.Click += label3_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label5.ForeColor = SystemColors.ButtonFace;
            label5.Location = new Point(118, 179);
            label5.Name = "label5";
            label5.Size = new Size(190, 51);
            label5.TabIndex = 3;
            label5.Text = "Password";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label4.ForeColor = SystemColors.ButtonFace;
            label4.Location = new Point(118, 79);
            label4.Name = "label4";
            label4.Size = new Size(119, 51);
            label4.TabIndex = 2;
            label4.Text = "Email";
            // 
            // txtEmail
            // 
            txtEmail.BackColor = Color.White;
            txtEmail.BorderStyle = BorderStyle.None;
            txtEmail.Font = new Font("Segoe UI", 12F);
            txtEmail.Location = new Point(118, 133);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(511, 43);
            txtEmail.TabIndex = 1;
            // 
            // txtSenha
            // 
            txtSenha.BorderStyle = BorderStyle.None;
            txtSenha.Font = new Font("Segoe UI", 12F);
            txtSenha.Location = new Point(118, 233);
            txtSenha.Name = "txtSenha";
            txtSenha.PasswordChar = '*';
            txtSenha.Size = new Size(511, 43);
            txtSenha.TabIndex = 0;
            // 
            // pnlModoPin
            // 
            pnlModoPin.BackColor = Color.FromArgb(180, 0, 0, 0);
            pnlModoPin.Controls.Add(label2);
            pnlModoPin.Controls.Add(label1);
            pnlModoPin.Controls.Add(btnEntrar);
            pnlModoPin.Controls.Add(txtPIN);
            pnlModoPin.Location = new Point(70, 30);
            pnlModoPin.Name = "pnlModoPin";
            pnlModoPin.Size = new Size(740, 466);
            pnlModoPin.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Cursor = Cursors.Hand;
            label2.Font = new Font("Segoe UI", 10.125F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ButtonFace;
            label2.Location = new Point(258, 194);
            label2.Name = "label2";
            label2.Size = new Size(234, 37);
            label2.TabIndex = 2;
            label2.Text = "Entrar com Email";
            label2.TextAlign = ContentAlignment.TopCenter;
            label2.Click += label2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(286, 0);
            label1.Name = "label1";
            label1.Size = new Size(188, 65);
            label1.TabIndex = 2;
            label1.Text = "LOGIN ";
            // 
            // btnEntrar
            // 
            btnEntrar.BackColor = Color.Transparent;
            btnEntrar.FlatStyle = FlatStyle.Flat;
            btnEntrar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEntrar.ForeColor = SystemColors.ButtonFace;
            btnEntrar.Location = new Point(302, 248);
            btnEntrar.Name = "btnEntrar";
            btnEntrar.Size = new Size(150, 54);
            btnEntrar.TabIndex = 4;
            btnEntrar.Text = "Login";
            btnEntrar.UseVisualStyleBackColor = false;
            btnEntrar.Click += btnEntrar_Click_1;
            // 
            // txtPIN
            // 
            txtPIN.BackColor = Color.White;
            txtPIN.BorderStyle = BorderStyle.None;
            txtPIN.Font = new Font("Segoe UI", 26F);
            txtPIN.Location = new Point(258, 98);
            txtPIN.Name = "txtPIN";
            txtPIN.PasswordChar = '*';
            txtPIN.Size = new Size(240, 93);
            txtPIN.TabIndex = 3;
            txtPIN.TextAlign = HorizontalAlignment.Center;
            // 
            // btnSairApp
            // 
            btnSairApp.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSairApp.BackColor = Color.Transparent;
            btnSairApp.BackgroundImageLayout = ImageLayout.Zoom;
            btnSairApp.Cursor = Cursors.Help;
            btnSairApp.FlatStyle = FlatStyle.Flat;
            btnSairApp.Font = new Font("Segoe UI", 13.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSairApp.ForeColor = SystemColors.ButtonFace;
            btnSairApp.Location = new Point(1987, 1352);
            btnSairApp.Name = "btnSairApp";
            btnSairApp.Size = new Size(362, 80);
            btnSairApp.TabIndex = 1;
            btnSairApp.Text = "Sair Aplicação";
            btnSairApp.UseVisualStyleBackColor = false;
            btnSairApp.Click += btnSairApp_Click;
            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(2352, 1444);
            Controls.Add(btnSairApp);
            Controls.Add(pnlCentral);
            Name = "FormLogin";
            Text = "FormLogin";
            Load += FormLogin_Load;
            Resize += FormLogin_Resize;
            pnlCentral.ResumeLayout(false);
            pnlModoEmail.ResumeLayout(false);
            pnlModoEmail.PerformLayout();
            pnlModoPin.ResumeLayout(false);
            pnlModoPin.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlCentral;
        private Button btnSairApp;
        private Panel pnlModoEmail;
        private TextBox txtEmail;
        private TextBox txtSenha;
        private Panel pnlModoPin;
        private Label label1;
        private Button btnEntrar;
        private TextBox txtPIN;
        private Label label2;
        private Label label3;
        private Label label6;
        private Label label5;
        private Label label4;
        private Button button1;
    }
}