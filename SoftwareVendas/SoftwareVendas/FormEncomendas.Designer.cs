namespace SoftwareVendas
{
    partial class FormEncomendas
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
            dgvEncomendas = new DataGridView();
            label2 = new Label();
            label1 = new Label();
            cmbFiltro = new ComboBox();
            txtPesquisa = new TextBox();
            btnPesquisar = new Button();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dgvEncomendas).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // dgvEncomendas
            // 
            dgvEncomendas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEncomendas.Location = new Point(14, 54);
            dgvEncomendas.Name = "dgvEncomendas";
            dgvEncomendas.RowHeadersWidth = 82;
            dgvEncomendas.Size = new Size(2126, 678);
            dgvEncomendas.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 16.125F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(640, 68);
            label2.Name = "label2";
            label2.Size = new Size(382, 49);
            label2.TabIndex = 10;
            label2.Text = "Termo de Pesquisa:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 16.125F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(14, 68);
            label1.Name = "label1";
            label1.Size = new Size(236, 49);
            label1.TabIndex = 9;
            label1.Text = "Filtrar Por:";
            // 
            // cmbFiltro
            // 
            cmbFiltro.Font = new Font("Times New Roman", 13.875F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbFiltro.FormattingEnabled = true;
            cmbFiltro.Location = new Point(256, 68);
            cmbFiltro.Name = "cmbFiltro";
            cmbFiltro.Size = new Size(323, 50);
            cmbFiltro.TabIndex = 6;
            // 
            // txtPesquisa
            // 
            txtPesquisa.Font = new Font("Times New Roman", 13.875F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPesquisa.Location = new Point(1028, 68);
            txtPesquisa.Name = "txtPesquisa";
            txtPesquisa.Size = new Size(728, 50);
            txtPesquisa.TabIndex = 7;
            // 
            // btnPesquisar
            // 
            btnPesquisar.BackColor = Color.SteelBlue;
            btnPesquisar.FlatStyle = FlatStyle.Flat;
            btnPesquisar.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPesquisar.ForeColor = Color.White;
            btnPesquisar.Location = new Point(1777, 61);
            btnPesquisar.Name = "btnPesquisar";
            btnPesquisar.Size = new Size(363, 72);
            btnPesquisar.TabIndex = 8;
            btnPesquisar.Text = "🔍︎ Pesquisar";
            btnPesquisar.UseVisualStyleBackColor = false;
            btnPesquisar.Click += btnPesquisar_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtPesquisa);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(btnPesquisar);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(cmbFiltro);
            groupBox1.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(21, 32);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(2174, 170);
            groupBox1.TabIndex = 11;
            groupBox1.TabStop = false;
            groupBox1.Text = "Pesquisa Encomendas";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dgvEncomendas);
            groupBox2.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox2.Location = new Point(21, 228);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(2162, 760);
            groupBox2.TabIndex = 12;
            groupBox2.TabStop = false;
            groupBox2.Text = "Encomendas";
            // 
            // FormEncomendas
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2218, 1106);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "FormEncomendas";
            Text = "FormEncomendas";
            ((System.ComponentModel.ISupportInitialize)dgvEncomendas).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvEncomendas;
        private Label label2;
        private Label label1;
        private ComboBox cmbFiltro;
        private TextBox txtPesquisa;
        private Button btnPesquisar;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
    }
}