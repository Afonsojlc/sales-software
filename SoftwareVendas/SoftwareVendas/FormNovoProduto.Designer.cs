namespace SoftwareVendas
{
    partial class FormNovoProduto
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
            groupBox1 = new GroupBox();
            numIVA = new NumericUpDown();
            label1 = new Label();
            numStock = new NumericUpDown();
            numPreco = new NumericUpDown();
            label10 = new Label();
            label9 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            txtCodigo = new TextBox();
            txtDescricao = new TextBox();
            txtCategoria = new TextBox();
            btnGravar = new Button();
            btnCancelar = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numIVA).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numStock).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPreco).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(numIVA);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(numStock);
            groupBox1.Controls.Add(numPreco);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(label9);
            groupBox1.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold);
            groupBox1.Location = new Point(61, 324);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1443, 234);
            groupBox1.TabIndex = 22;
            groupBox1.TabStop = false;
            groupBox1.Text = "Inventário e Venda";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // numIVA
            // 
            numIVA.Location = new Point(804, 64);
            numIVA.Name = "numIVA";
            numIVA.Size = new Size(240, 50);
            numIVA.TabIndex = 12;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 13.125F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(633, 68);
            label1.Name = "label1";
            label1.Size = new Size(135, 40);
            label1.TabIndex = 11;
            label1.Text = "💰 IVA:";
            // 
            // numStock
            // 
            numStock.Location = new Point(282, 150);
            numStock.Name = "numStock";
            numStock.Size = new Size(240, 50);
            numStock.TabIndex = 10;
            // 
            // numPreco
            // 
            numPreco.Location = new Point(330, 64);
            numPreco.Name = "numPreco";
            numPreco.Size = new Size(240, 50);
            numPreco.TabIndex = 9;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Times New Roman", 13.125F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.Location = new Point(26, 154);
            label10.Name = "label10";
            label10.Size = new Size(250, 40);
            label10.TabIndex = 8;
            label10.Text = "📦 Stock Atual:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Times New Roman", 13.125F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(26, 68);
            label9.Name = "label9";
            label9.Size = new Size(298, 40);
            label9.TabIndex = 7;
            label9.Text = "💰 Preço Unitário:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Times New Roman", 13.125F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(61, 216);
            label7.Name = "label7";
            label7.Size = new Size(226, 40);
            label7.TabIndex = 17;
            label7.Text = "📁 Categoria:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Times New Roman", 13.125F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(61, 162);
            label6.Name = "label6";
            label6.Size = new Size(162, 40);
            label6.TabIndex = 16;
            label6.Text = "📝 Nome:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Times New Roman", 13.125F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(61, 104);
            label5.Name = "label5";
            label5.Size = new Size(192, 40);
            label5.TabIndex = 15;
            label5.Text = "🏷️  Código:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(21, 36);
            label4.Name = "label4";
            label4.Size = new Size(599, 42);
            label4.TabIndex = 14;
            label4.Text = "DETALHES DO NOVO PRODUTO";
            // 
            // txtCodigo
            // 
            txtCodigo.Location = new Point(259, 107);
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(402, 39);
            txtCodigo.TabIndex = 23;
            // 
            // txtDescricao
            // 
            txtDescricao.Location = new Point(229, 165);
            txtDescricao.Name = "txtDescricao";
            txtDescricao.Size = new Size(950, 39);
            txtDescricao.TabIndex = 24;
            // 
            // txtCategoria
            // 
            txtCategoria.Location = new Point(293, 219);
            txtCategoria.Name = "txtCategoria";
            txtCategoria.Size = new Size(368, 39);
            txtCategoria.TabIndex = 25;
            // 
            // btnGravar
            // 
            btnGravar.BackColor = Color.ForestGreen;
            btnGravar.FlatStyle = FlatStyle.Flat;
            btnGravar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnGravar.ForeColor = SystemColors.ButtonHighlight;
            btnGravar.Location = new Point(269, 628);
            btnGravar.Name = "btnGravar";
            btnGravar.Size = new Size(424, 64);
            btnGravar.TabIndex = 26;
            btnGravar.Text = "Adicionar novo produto";
            btnGravar.UseVisualStyleBackColor = false;
            btnGravar.Click += btnGravar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.Firebrick;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancelar.ForeColor = SystemColors.ButtonHighlight;
            btnCancelar.Location = new Point(905, 628);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(422, 64);
            btnCancelar.TabIndex = 27;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // FormNovoProduto
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1558, 770);
            Controls.Add(btnCancelar);
            Controls.Add(btnGravar);
            Controls.Add(txtCategoria);
            Controls.Add(txtDescricao);
            Controls.Add(txtCodigo);
            Controls.Add(groupBox1);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Name = "FormNovoProduto";
            Text = "FormNovoProduto";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numIVA).EndInit();
            ((System.ComponentModel.ISupportInitialize)numStock).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPreco).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private Label label10;
        private Label label9;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private TextBox txtCodigo;
        private TextBox txtDescricao;
        private TextBox txtCategoria;
        private Label label1;
        private NumericUpDown numStock;
        private NumericUpDown numPreco;
        private NumericUpDown numIVA;
        private Button btnGravar;
        private Button btnCancelar;
    }
}