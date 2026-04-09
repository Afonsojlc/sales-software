namespace SoftwareVendas
{
    partial class FormClientes
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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            cmbFiltro = new ComboBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            txtPesquisa = new TextBox();
            btnPesquisar = new Button();
            dgvClientes = new DataGridView();
            dgvHistorico = new DataGridView();
            groupBox1 = new GroupBox();
            label2 = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvHistorico).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // cmbFiltro
            // 
            cmbFiltro.Font = new Font("Times New Roman", 16.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbFiltro.FormattingEnabled = true;
            cmbFiltro.Location = new Point(284, 71);
            cmbFiltro.Name = "cmbFiltro";
            cmbFiltro.Size = new Size(281, 57);
            cmbFiltro.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(32, 32);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // txtPesquisa
            // 
            txtPesquisa.Font = new Font("Times New Roman", 16.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPesquisa.Location = new Point(1141, 77);
            txtPesquisa.Name = "txtPesquisa";
            txtPesquisa.Size = new Size(728, 57);
            txtPesquisa.TabIndex = 2;
            txtPesquisa.Text = "Digite o Nome, NIF, Cidade, ou Email...";
            // 
            // btnPesquisar
            // 
            btnPesquisar.BackColor = Color.SteelBlue;
            btnPesquisar.FlatStyle = FlatStyle.Flat;
            btnPesquisar.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPesquisar.ForeColor = Color.White;
            btnPesquisar.Location = new Point(1964, 58);
            btnPesquisar.Name = "btnPesquisar";
            btnPesquisar.Size = new Size(400, 84);
            btnPesquisar.TabIndex = 3;
            btnPesquisar.Text = "🔍︎ Pesquisar";
            btnPesquisar.UseVisualStyleBackColor = false;
            btnPesquisar.Click += btnPesquisar_Click;
            // 
            // dgvClientes
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvClientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Times New Roman", 11F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvClientes.DefaultCellStyle = dataGridViewCellStyle2;
            dgvClientes.Location = new Point(16, 49);
            dgvClientes.Name = "dgvClientes";
            dgvClientes.RowHeadersWidth = 82;
            dgvClientes.Size = new Size(2398, 395);
            dgvClientes.TabIndex = 4;
            // 
            // dgvHistorico
            // 
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Times New Roman", 15F, FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvHistorico.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvHistorico.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Window;
            dataGridViewCellStyle4.Font = new Font("Times New Roman", 10.875F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle4.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dgvHistorico.DefaultCellStyle = dataGridViewCellStyle4;
            dgvHistorico.Location = new Point(16, 49);
            dgvHistorico.Name = "dgvHistorico";
            dgvHistorico.RowHeadersWidth = 82;
            dgvHistorico.Size = new Size(2406, 462);
            dgvHistorico.TabIndex = 5;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(cmbFiltro);
            groupBox1.Controls.Add(txtPesquisa);
            groupBox1.Controls.Add(btnPesquisar);
            groupBox1.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(49, 46);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(2442, 170);
            groupBox1.TabIndex = 12;
            groupBox1.TabStop = false;
            groupBox1.Text = "🔍︎ Pesquisa e Filtros de Cliente";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(714, 75);
            label2.Name = "label2";
            label2.Size = new Size(425, 55);
            label2.TabIndex = 5;
            label2.Text = "Termo de Pesquisa:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(16, 69);
            label1.Name = "label1";
            label1.Size = new Size(262, 55);
            label1.TabIndex = 4;
            label1.Text = "Filtrar Por:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dgvClientes);
            groupBox2.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold);
            groupBox2.Location = new Point(49, 234);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(2442, 450);
            groupBox2.TabIndex = 13;
            groupBox2.TabStop = false;
            groupBox2.Text = "🗓️ Listagem de Clientes";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(dgvHistorico);
            groupBox3.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold);
            groupBox3.Location = new Point(49, 702);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(2442, 534);
            groupBox3.TabIndex = 14;
            groupBox3.TabStop = false;
            groupBox3.Text = "⏲Historico de Compras";
            // 
            // FormClientes
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2532, 1314);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "FormClientes";
            Text = "FormClientes";
            ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvHistorico).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cmbFiltro;
        private ContextMenuStrip contextMenuStrip1;
        private TextBox txtPesquisa;
        private Button btnPesquisar;
        private DataGridView dgvClientes;
        private DataGridView dgvHistorico;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
    }
}