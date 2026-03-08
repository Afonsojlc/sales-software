namespace SoftwareVendas
{
    partial class FormProdutos
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
            label1 = new Label();
            label2 = new Label();
            txtPesquisa = new TextBox();
            lstSugestoes = new ListBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            lblCodigo = new Label();
            lblNome = new Label();
            lblCategoria = new Label();
            label8 = new Label();
            groupBox1 = new GroupBox();
            lblStock = new Label();
            lblPreco = new Label();
            label10 = new Label();
            label9 = new Label();
            btnSelecionar = new Button();
            btnCancelar = new Button();
            btnAdicionarProduto = new Button();
            btnEliminarProduto = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(21, 34);
            label1.Name = "label1";
            label1.Size = new Size(470, 42);
            label1.TabIndex = 0;
            label1.Text = "PESQUISA INTELIGENTE";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 13.125F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(61, 106);
            label2.Name = "label2";
            label2.Size = new Size(577, 40);
            label2.TabIndex = 1;
            label2.Text = "Digite o Nome ou Código do produto:";
            // 
            // txtPesquisa
            // 
            txtPesquisa.Font = new Font("Segoe UI", 12F);
            txtPesquisa.Location = new Point(87, 174);
            txtPesquisa.Name = "txtPesquisa";
            txtPesquisa.Size = new Size(1990, 50);
            txtPesquisa.TabIndex = 2;
            txtPesquisa.TextChanged += txtPesquisa_TextChanged;
            // 
            // lstSugestoes
            // 
            lstSugestoes.FormattingEnabled = true;
            lstSugestoes.ItemHeight = 37;
            lstSugestoes.Location = new Point(87, 246);
            lstSugestoes.Name = "lstSugestoes";
            lstSugestoes.Size = new Size(1990, 152);
            lstSugestoes.TabIndex = 3;
            lstSugestoes.SelectedIndexChanged += lstSugestoes_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.BackColor = Color.LightGray;
            label3.Location = new Point(61, 422);
            label3.Name = "label3";
            label3.Size = new Size(2048, 2);
            label3.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(21, 442);
            label4.Name = "label4";
            label4.Size = new Size(763, 42);
            label4.TabIndex = 5;
            label4.Text = "DETALHES DO PRODUTO SELECIONADO";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Times New Roman", 13.125F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(61, 510);
            label5.Name = "label5";
            label5.Size = new Size(192, 40);
            label5.TabIndex = 6;
            label5.Text = "🏷️  Código:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Times New Roman", 13.125F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(61, 568);
            label6.Name = "label6";
            label6.Size = new Size(162, 40);
            label6.TabIndex = 7;
            label6.Text = "📝 Nome:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Times New Roman", 13.125F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(61, 622);
            label7.Name = "label7";
            label7.Size = new Size(226, 40);
            label7.TabIndex = 8;
            label7.Text = "📁 Categoria:";
            // 
            // lblCodigo
            // 
            lblCodigo.AutoSize = true;
            lblCodigo.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCodigo.Location = new Point(247, 513);
            lblCodigo.Name = "lblCodigo";
            lblCodigo.Size = new Size(39, 36);
            lblCodigo.TabIndex = 9;
            lblCodigo.Text = "...";
            // 
            // lblNome
            // 
            lblNome.AutoSize = true;
            lblNome.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            lblNome.Location = new Point(229, 571);
            lblNome.Name = "lblNome";
            lblNome.Size = new Size(39, 36);
            lblNome.TabIndex = 10;
            lblNome.Text = "...";
            // 
            // lblCategoria
            // 
            lblCategoria.AutoSize = true;
            lblCategoria.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            lblCategoria.Location = new Point(284, 625);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Size = new Size(39, 36);
            lblCategoria.TabIndex = 11;
            lblCategoria.Text = "...";
            // 
            // label8
            // 
            label8.BackColor = Color.LightGray;
            label8.Location = new Point(61, 699);
            label8.Name = "label8";
            label8.Size = new Size(2048, 2);
            label8.TabIndex = 12;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lblStock);
            groupBox1.Controls.Add(lblPreco);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(label9);
            groupBox1.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold);
            groupBox1.Location = new Point(61, 730);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(2016, 200);
            groupBox1.TabIndex = 13;
            groupBox1.TabStop = false;
            groupBox1.Text = "Inventário e Venda";
            // 
            // lblStock
            // 
            lblStock.AutoSize = true;
            lblStock.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStock.Location = new Point(271, 136);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(39, 36);
            lblStock.TabIndex = 11;
            lblStock.Text = "...";
            lblStock.MouseClick += lblStock_MouseClick;
            // 
            // lblPreco
            // 
            lblPreco.AutoSize = true;
            lblPreco.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPreco.Location = new Point(317, 72);
            lblPreco.Name = "lblPreco";
            lblPreco.Size = new Size(39, 36);
            lblPreco.TabIndex = 10;
            lblPreco.Text = "...";
            lblPreco.Click += label11_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Times New Roman", 13.125F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.Location = new Point(26, 132);
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
            // btnSelecionar
            // 
            btnSelecionar.BackColor = Color.DodgerBlue;
            btnSelecionar.FlatStyle = FlatStyle.Flat;
            btnSelecionar.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold);
            btnSelecionar.ForeColor = Color.White;
            btnSelecionar.Location = new Point(738, 1027);
            btnSelecionar.Name = "btnSelecionar";
            btnSelecionar.Size = new Size(246, 68);
            btnSelecionar.TabIndex = 14;
            btnSelecionar.Text = "Selecionar";
            btnSelecionar.UseVisualStyleBackColor = false;
            btnSelecionar.Click += btnSelecionar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.Firebrick;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold);
            btnCancelar.ForeColor = SystemColors.ControlLightLight;
            btnCancelar.Location = new Point(1053, 1027);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(270, 68);
            btnCancelar.TabIndex = 15;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnAdicionarProduto
            // 
            btnAdicionarProduto.BackColor = Color.ForestGreen;
            btnAdicionarProduto.FlatStyle = FlatStyle.Flat;
            btnAdicionarProduto.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold);
            btnAdicionarProduto.ForeColor = SystemColors.Control;
            btnAdicionarProduto.Location = new Point(484, 1027);
            btnAdicionarProduto.Name = "btnAdicionarProduto";
            btnAdicionarProduto.Size = new Size(514, 68);
            btnAdicionarProduto.TabIndex = 16;
            btnAdicionarProduto.Text = "Adicionar Novo Produto";
            btnAdicionarProduto.UseVisualStyleBackColor = false;
            btnAdicionarProduto.Click += btnAdicionarProduto_Click;
            // 
            // btnEliminarProduto
            // 
            btnEliminarProduto.BackColor = Color.Firebrick;
            btnEliminarProduto.FlatStyle = FlatStyle.Flat;
            btnEliminarProduto.Font = new Font("Times New Roman", 10.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEliminarProduto.ForeColor = SystemColors.ControlLightLight;
            btnEliminarProduto.Location = new Point(1807, 950);
            btnEliminarProduto.Name = "btnEliminarProduto";
            btnEliminarProduto.Size = new Size(270, 44);
            btnEliminarProduto.TabIndex = 17;
            btnEliminarProduto.Text = "Eliminar Artigo";
            btnEliminarProduto.UseVisualStyleBackColor = false;
            btnEliminarProduto.Click += btnEliminarProduto_Click;
            // 
            // FormProdutos
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(2344, 1251);
            Controls.Add(btnEliminarProduto);
            Controls.Add(btnAdicionarProduto);
            Controls.Add(btnCancelar);
            Controls.Add(btnSelecionar);
            Controls.Add(groupBox1);
            Controls.Add(label8);
            Controls.Add(lblCategoria);
            Controls.Add(lblNome);
            Controls.Add(lblCodigo);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(lstSugestoes);
            Controls.Add(txtPesquisa);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(2, 3, 2, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormProdutos";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Localizar Produto";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtPesquisa;
        private ListBox lstSugestoes;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label lblCodigo;
        private Label lblNome;
        private Label lblCategoria;
        private Label label8;
        private GroupBox groupBox1;
        private Label label10;
        private Label label9;
        private Label lblStock;
        private Label lblPreco;
        private Button btnSelecionar;
        private Button btnCancelar;
        private Button btnAdicionarProduto;
        private Button btnEliminarProduto;
    }
}