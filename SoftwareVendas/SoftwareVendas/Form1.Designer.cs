namespace SoftwareVendas
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            dtpDataEncomenda = new DateTimePicker();
            txtNomeCliente = new TextBox();
            txtNIF = new TextBox();
            groupBox2 = new GroupBox();
            txtDescontoProduto = new TextBox();
            label7 = new Label();
            button1 = new Button();
            numQuantidade = new NumericUpDown();
            label5 = new Label();
            txtCodigoProduto = new TextBox();
            label4 = new Label();
            groupBox3 = new GroupBox();
            btnRemoverLinha = new Button();
            txtDescontoFinal = new TextBox();
            label10 = new Label();
            lblTotalPagar = new Label();
            label9 = new Label();
            lblTotalIVA = new Label();
            label8 = new Label();
            lblTotalIliquido = new Label();
            label6 = new Label();
            btnFinalizar = new Button();
            dgvItens = new DataGridView();
            colCodigo = new DataGridViewTextBoxColumn();
            colQtd = new DataGridViewTextBoxColumn();
            colDescricao = new DataGridViewTextBoxColumn();
            colPreco = new DataGridViewTextBoxColumn();
            colDesconto = new DataGridViewTextBoxColumn();
            colTotal = new DataGridViewTextBoxColumn();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numQuantidade).BeginInit();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItens).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(dtpDataEncomenda);
            groupBox1.Controls.Add(txtNomeCliente);
            groupBox1.Controls.Add(txtNIF);
            groupBox1.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(12, 26);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(2492, 310);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Dados do Cliente";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(62, 234);
            label3.Name = "label3";
            label3.Size = new Size(422, 36);
            label3.TabIndex = 5;
            label3.Text = "Insira a Data de Encomenda: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(62, 159);
            label2.Name = "label2";
            label2.Size = new Size(374, 36);
            label2.TabIndex = 4;
            label2.Text = "Insira o Nome do Cliente: ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(62, 80);
            label1.Name = "label1";
            label1.Size = new Size(200, 36);
            label1.TabIndex = 3;
            label1.Text = "Insira o NIF: ";
            // 
            // dtpDataEncomenda
            // 
            dtpDataEncomenda.Font = new Font("Times New Roman", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            dtpDataEncomenda.Location = new Point(506, 228);
            dtpDataEncomenda.Name = "dtpDataEncomenda";
            dtpDataEncomenda.Size = new Size(400, 44);
            dtpDataEncomenda.TabIndex = 2;
            // 
            // txtNomeCliente
            // 
            txtNomeCliente.Location = new Point(456, 151);
            txtNomeCliente.Name = "txtNomeCliente";
            txtNomeCliente.Size = new Size(1542, 50);
            txtNomeCliente.TabIndex = 1;
            txtNomeCliente.KeyDown += txtNomeCliente_KeyDown;
            // 
            // txtNIF
            // 
            txtNIF.Location = new Point(268, 77);
            txtNIF.Name = "txtNIF";
            txtNIF.Size = new Size(774, 50);
            txtNIF.TabIndex = 0;
            txtNIF.KeyDown += txtNIF_KeyDown;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(txtDescontoProduto);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(button1);
            groupBox2.Controls.Add(numQuantidade);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(txtCodigoProduto);
            groupBox2.Controls.Add(label4);
            groupBox2.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox2.Location = new Point(12, 356);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(2492, 240);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Adicionar Produto";
            groupBox2.Enter += groupBox2_Enter;
            // 
            // txtDescontoProduto
            // 
            txtDescontoProduto.Location = new Point(1415, 70);
            txtDescontoProduto.Name = "txtDescontoProduto";
            txtDescontoProduto.Size = new Size(294, 50);
            txtDescontoProduto.TabIndex = 6;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(1196, 78);
            label7.Name = "label7";
            label7.Size = new Size(213, 36);
            label7.TabIndex = 5;
            label7.Text = "Desconto (%):";
            // 
            // button1
            // 
            button1.Font = new Font("Times New Roman", 13.125F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Location = new Point(688, 166);
            button1.Name = "button1";
            button1.Size = new Size(600, 68);
            button1.TabIndex = 4;
            button1.Text = "Adicionar à Lista";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // numQuantidade
            // 
            numQuantidade.Location = new Point(954, 70);
            numQuantidade.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numQuantidade.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numQuantidade.Name = "numQuantidade";
            numQuantidade.Size = new Size(164, 50);
            numQuantidade.TabIndex = 3;
            numQuantidade.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(868, 77);
            label5.Name = "label5";
            label5.Size = new Size(80, 36);
            label5.TabIndex = 2;
            label5.Text = "Qtd:";
            // 
            // txtCodigoProduto
            // 
            txtCodigoProduto.Location = new Point(354, 70);
            txtCodigoProduto.Name = "txtCodigoProduto";
            txtCodigoProduto.Size = new Size(402, 50);
            txtCodigoProduto.TabIndex = 1;
            txtCodigoProduto.KeyDown += txtCodigoProduto_KeyDown;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(62, 78);
            label4.Name = "label4";
            label4.Size = new Size(286, 36);
            label4.TabIndex = 0;
            label4.Text = "Código do Produto:";
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox3.Controls.Add(btnRemoverLinha);
            groupBox3.Controls.Add(txtDescontoFinal);
            groupBox3.Controls.Add(label10);
            groupBox3.Controls.Add(lblTotalPagar);
            groupBox3.Controls.Add(label9);
            groupBox3.Controls.Add(lblTotalIVA);
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(lblTotalIliquido);
            groupBox3.Controls.Add(label6);
            groupBox3.Controls.Add(btnFinalizar);
            groupBox3.Controls.Add(dgvItens);
            groupBox3.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox3.Location = new Point(12, 618);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(2492, 846);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Lista de Artigos";
            // 
            // btnRemoverLinha
            // 
            btnRemoverLinha.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnRemoverLinha.Location = new Point(2122, 680);
            btnRemoverLinha.Name = "btnRemoverLinha";
            btnRemoverLinha.Size = new Size(326, 46);
            btnRemoverLinha.TabIndex = 10;
            btnRemoverLinha.Text = "Remover Artigo";
            btnRemoverLinha.UseVisualStyleBackColor = true;
            btnRemoverLinha.Click += btnRemoverLinha_Click_1;
            // 
            // txtDescontoFinal
            // 
            txtDescontoFinal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtDescontoFinal.Location = new Point(335, 708);
            txtDescontoFinal.Name = "txtDescontoFinal";
            txtDescontoFinal.Size = new Size(622, 50);
            txtDescontoFinal.TabIndex = 9;
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label10.AutoSize = true;
            label10.Location = new Point(62, 711);
            label10.Name = "label10";
            label10.Size = new Size(276, 42);
            label10.TabIndex = 8;
            label10.Text = "Desc. Final (%):";
            // 
            // lblTotalPagar
            // 
            lblTotalPagar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblTotalPagar.AutoSize = true;
            lblTotalPagar.Location = new Point(1196, 795);
            lblTotalPagar.Name = "lblTotalPagar";
            lblTotalPagar.Size = new Size(45, 42);
            lblTotalPagar.TabIndex = 7;
            lblTotalPagar.Text = "...";
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label9.AutoSize = true;
            label9.Location = new Point(876, 795);
            label9.Name = "label9";
            label9.Size = new Size(326, 42);
            label9.TabIndex = 6;
            label9.Text = "TOTAL A PAGAR:";
            // 
            // lblTotalIVA
            // 
            lblTotalIVA.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblTotalIVA.AutoSize = true;
            lblTotalIVA.Location = new Point(661, 797);
            lblTotalIVA.Name = "lblTotalIVA";
            lblTotalIVA.Size = new Size(45, 42);
            lblTotalIVA.TabIndex = 5;
            lblTotalIVA.Text = "...";
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Location = new Point(485, 797);
            label8.Name = "label8";
            label8.Size = new Size(182, 42);
            label8.TabIndex = 4;
            label8.Text = "Total IVA:";
            // 
            // lblTotalIliquido
            // 
            lblTotalIliquido.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblTotalIliquido.AutoSize = true;
            lblTotalIliquido.Location = new Point(312, 796);
            lblTotalIliquido.Name = "lblTotalIliquido";
            lblTotalIliquido.Size = new Size(45, 42);
            lblTotalIliquido.TabIndex = 3;
            lblTotalIliquido.Text = "...";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Location = new Point(62, 796);
            label6.Name = "label6";
            label6.Size = new Size(244, 42);
            label6.TabIndex = 2;
            label6.Text = "Total Ilíquido:";
            // 
            // btnFinalizar
            // 
            btnFinalizar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnFinalizar.Location = new Point(1966, 751);
            btnFinalizar.Name = "btnFinalizar";
            btnFinalizar.Size = new Size(520, 86);
            btnFinalizar.TabIndex = 1;
            btnFinalizar.Text = "TERMINAR VENDA";
            btnFinalizar.UseVisualStyleBackColor = true;
            btnFinalizar.Click += btnFinalizar_Click;
            // 
            // dgvItens
            // 
            dgvItens.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvItens.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvItens.Columns.AddRange(new DataGridViewColumn[] { colCodigo, colQtd, colDescricao, colPreco, colDesconto, colTotal });
            dgvItens.Location = new Point(62, 60);
            dgvItens.Name = "dgvItens";
            dgvItens.RowHeadersWidth = 82;
            dgvItens.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvItens.Size = new Size(2386, 614);
            dgvItens.TabIndex = 0;
            dgvItens.CellContentClick += dgvItens_CellContentClick;
            dgvItens.CellEndEdit += dgvItens_CellEndEdit;
            dgvItens.EditingControlShowing += dgvItens_EditingControlShowing;
            dgvItens.KeyDown += dgvItens_KeyDown;
            // 
            // colCodigo
            // 
            colCodigo.HeaderText = "Código";
            colCodigo.MinimumWidth = 10;
            colCodigo.Name = "colCodigo";
            colCodigo.Width = 200;
            // 
            // colQtd
            // 
            colQtd.HeaderText = "Qtd";
            colQtd.MinimumWidth = 10;
            colQtd.Name = "colQtd";
            colQtd.Width = 200;
            // 
            // colDescricao
            // 
            colDescricao.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colDescricao.HeaderText = "Descrição";
            colDescricao.MinimumWidth = 10;
            colDescricao.Name = "colDescricao";
            // 
            // colPreco
            // 
            colPreco.HeaderText = "Preço Unit.";
            colPreco.MinimumWidth = 10;
            colPreco.Name = "colPreco";
            colPreco.Width = 200;
            // 
            // colDesconto
            // 
            colDesconto.HeaderText = "Desconto (%)";
            colDesconto.MinimumWidth = 10;
            colDesconto.Name = "colDesconto";
            colDesconto.Width = 200;
            // 
            // colTotal
            // 
            colTotal.HeaderText = "Total";
            colTotal.MinimumWidth = 10;
            colTotal.Name = "colTotal";
            colTotal.Width = 200;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2535, 1476);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "Form1";
            Text = "Nova Venda";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numQuantidade).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItens).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label label3;
        private Label label2;
        private Label label1;
        private DateTimePicker dtpDataEncomenda;
        private TextBox txtNomeCliente;
        private TextBox txtNIF;
        private GroupBox groupBox2;
        private Label label4;
        private TextBox txtCodigoProduto;
        private NumericUpDown numQuantidade;
        private Label label5;
        private Button button1;
        private GroupBox groupBox3;
        private DataGridView dgvItens;
        private Label lblTotalIliquido;
        private Label label6;
        private Button btnFinalizar;
        private DataGridViewTextBoxColumn colCodigo;
        private DataGridViewTextBoxColumn colQtd;
        private DataGridViewTextBoxColumn colDescricao;
        private DataGridViewTextBoxColumn colPreco;
        private DataGridViewTextBoxColumn colDesconto;
        private DataGridViewTextBoxColumn colTotal;
        private Label lblTotalPagar;
        private Label label9;
        private Label lblTotalIVA;
        private Label label8;
        private TextBox txtDescontoProduto;
        private Label label7;
        private TextBox txtDescontoFinal;
        private Label label10;
        private Button btnRemoverLinha;
    }
}
