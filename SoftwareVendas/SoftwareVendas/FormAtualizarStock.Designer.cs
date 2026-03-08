namespace SoftwareVendas
{
    partial class FormAtualizarStock
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
            lblNome = new Label();
            lblCodigo = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            lblStockAtual = new Label();
            label10 = new Label();
            numNovoStock = new NumericUpDown();
            label1 = new Label();
            btnGravar = new Button();
            btnCancelar = new Button();
            ((System.ComponentModel.ISupportInitialize)numNovoStock).BeginInit();
            SuspendLayout();
            // 
            // lblNome
            // 
            lblNome.AutoSize = true;
            lblNome.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            lblNome.Location = new Point(243, 165);
            lblNome.Name = "lblNome";
            lblNome.Size = new Size(39, 36);
            lblNome.TabIndex = 18;
            lblNome.Text = "...";
            // 
            // lblCodigo
            // 
            lblCodigo.AutoSize = true;
            lblCodigo.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCodigo.Location = new Point(261, 107);
            lblCodigo.Name = "lblCodigo";
            lblCodigo.Size = new Size(39, 36);
            lblCodigo.TabIndex = 17;
            lblCodigo.Text = "...";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Times New Roman", 13.125F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(75, 162);
            label6.Name = "label6";
            label6.Size = new Size(162, 40);
            label6.TabIndex = 15;
            label6.Text = "📝 Nome:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Times New Roman", 13.125F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(75, 104);
            label5.Name = "label5";
            label5.Size = new Size(192, 40);
            label5.TabIndex = 14;
            label5.Text = "🏷️  Código:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(289, 30);
            label4.Name = "label4";
            label4.Size = new Size(479, 42);
            label4.TabIndex = 13;
            label4.Text = "DETALHES DO PRODUTO";
            // 
            // lblStockAtual
            // 
            lblStockAtual.AutoSize = true;
            lblStockAtual.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStockAtual.Location = new Point(320, 226);
            lblStockAtual.Name = "lblStockAtual";
            lblStockAtual.Size = new Size(39, 36);
            lblStockAtual.TabIndex = 20;
            lblStockAtual.Text = "...";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Times New Roman", 13.125F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.Location = new Point(75, 222);
            label10.Name = "label10";
            label10.Size = new Size(250, 40);
            label10.TabIndex = 19;
            label10.Text = "📦 Stock Atual:";
            // 
            // numNovoStock
            // 
            numNovoStock.Font = new Font("Times New Roman", 13.875F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numNovoStock.Location = new Point(509, 299);
            numNovoStock.Name = "numNovoStock";
            numNovoStock.Size = new Size(240, 50);
            numNovoStock.TabIndex = 21;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 13.125F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(256, 302);
            label1.Name = "label1";
            label1.Size = new Size(247, 40);
            label1.TabIndex = 22;
            label1.Text = "📦 Stock Novo:";
            // 
            // btnGravar
            // 
            btnGravar.BackColor = Color.ForestGreen;
            btnGravar.FlatStyle = FlatStyle.Flat;
            btnGravar.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold);
            btnGravar.ForeColor = SystemColors.ButtonHighlight;
            btnGravar.Location = new Point(169, 392);
            btnGravar.Name = "btnGravar";
            btnGravar.Size = new Size(334, 64);
            btnGravar.TabIndex = 23;
            btnGravar.Text = "Atualizar Stock";
            btnGravar.UseVisualStyleBackColor = false;
            btnGravar.Click += btnGravar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.Firebrick;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold);
            btnCancelar.ForeColor = SystemColors.ButtonHighlight;
            btnCancelar.Location = new Point(529, 392);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(284, 64);
            btnCancelar.TabIndex = 24;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // FormAtualizarStock
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 550);
            Controls.Add(btnCancelar);
            Controls.Add(btnGravar);
            Controls.Add(label1);
            Controls.Add(numNovoStock);
            Controls.Add(lblStockAtual);
            Controls.Add(label10);
            Controls.Add(lblNome);
            Controls.Add(lblCodigo);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Name = "FormAtualizarStock";
            Text = "FormAtualizarStock";
            ((System.ComponentModel.ISupportInitialize)numNovoStock).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNome;
        private Label lblCodigo;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label lblStockAtual;
        private Label label10;
        private NumericUpDown numNovoStock;
        private Label label1;
        private Button btnGravar;
        private Button btnCancelar;
    }
}