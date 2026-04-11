namespace SoftwareVendas
{
    partial class FormDetalhesEncomenda
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            groupBox1 = new GroupBox();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            cmbEstado = new ComboBox();
            label19 = new Label();
            label18 = new Label();
            label16 = new Label();
            label15 = new Label();
            label14 = new Label();
            label13 = new Label();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            groupBox3 = new GroupBox();
            dgvLinhas = new DataGridView();
            btnGuardar = new Button();
            btnSair = new Button();
            btnModificar = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLinhas).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(43, 32);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(2200, 118);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Info Cliente";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Times New Roman", 12F);
            label9.Location = new Point(1901, 56);
            label9.Name = "label9";
            label9.Size = new Size(93, 36);
            label9.TabIndex = 8;
            label9.Text = "label9";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Times New Roman", 12F);
            label8.Location = new Point(1351, 56);
            label8.Name = "label8";
            label8.Size = new Size(93, 36);
            label8.TabIndex = 7;
            label8.Text = "label8";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Times New Roman", 12F);
            label7.Location = new Point(935, 56);
            label7.Name = "label7";
            label7.Size = new Size(93, 36);
            label7.TabIndex = 6;
            label7.Text = "label7";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(140, 56);
            label6.Name = "label6";
            label6.Size = new Size(93, 36);
            label6.TabIndex = 5;
            label6.Text = "label6";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(1737, 50);
            label5.Name = "label5";
            label5.Size = new Size(173, 42);
            label5.TabIndex = 4;
            label5.Text = "Contacto:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1241, 50);
            label4.Name = "label4";
            label4.Size = new Size(123, 42);
            label4.TabIndex = 3;
            label4.Text = "Email:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(867, 50);
            label3.Name = "label3";
            label3.Size = new Size(80, 42);
            label3.TabIndex = 2;
            label3.Text = "Nif:";
            label3.Click += label3_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1054, 100);
            label2.Name = "label2";
            label2.Size = new Size(0, 42);
            label2.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 13.125F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(34, 52);
            label1.Name = "label1";
            label1.Size = new Size(124, 40);
            label1.TabIndex = 0;
            label1.Text = "Nome: ";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(cmbEstado);
            groupBox2.Controls.Add(label19);
            groupBox2.Controls.Add(label18);
            groupBox2.Controls.Add(label16);
            groupBox2.Controls.Add(label15);
            groupBox2.Controls.Add(label14);
            groupBox2.Controls.Add(label13);
            groupBox2.Controls.Add(label12);
            groupBox2.Controls.Add(label11);
            groupBox2.Controls.Add(label10);
            groupBox2.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold);
            groupBox2.Location = new Point(43, 174);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(2200, 140);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Info Encomenda";
            // 
            // cmbEstado
            // 
            cmbEstado.Font = new Font("Times New Roman", 12F);
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Location = new Point(1425, 57);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(306, 44);
            cmbEstado.TabIndex = 19;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("Times New Roman", 12F);
            label19.Location = new Point(2015, 65);
            label19.Name = "label19";
            label19.Size = new Size(109, 36);
            label19.TabIndex = 18;
            label19.Text = "label19";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Times New Roman", 13.125F, FontStyle.Bold);
            label18.Location = new Point(1752, 61);
            label18.Name = "label18";
            label18.Size = new Size(276, 40);
            label18.TabIndex = 17;
            label18.Text = "Desconto Global:";
            label18.Click += label18_Click;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Times New Roman", 12F);
            label16.Location = new Point(1102, 65);
            label16.Name = "label16";
            label16.Size = new Size(109, 36);
            label16.TabIndex = 15;
            label16.Text = "label16";
            label16.Click += label16_Click;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Times New Roman", 13.125F, FontStyle.Bold);
            label15.Location = new Point(1300, 61);
            label15.Name = "label15";
            label15.Size = new Size(133, 40);
            label15.TabIndex = 14;
            label15.Text = "Estado:";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Times New Roman", 13.125F, FontStyle.Bold);
            label14.Location = new Point(915, 61);
            label14.Name = "label14";
            label14.Size = new Size(196, 40);
            label14.TabIndex = 13;
            label14.Text = "Valor Total:";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Times New Roman", 12F);
            label13.Location = new Point(703, 65);
            label13.Name = "label13";
            label13.Size = new Size(109, 36);
            label13.TabIndex = 12;
            label13.Text = "label13";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Times New Roman", 13.125F, FontStyle.Bold);
            label12.Location = new Point(431, 61);
            label12.Name = "label12";
            label12.Size = new Size(286, 40);
            label12.TabIndex = 11;
            label12.Text = "Data Encomenda:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Times New Roman", 12F);
            label11.Location = new Point(273, 65);
            label11.Name = "label11";
            label11.Size = new Size(108, 36);
            label11.TabIndex = 10;
            label11.Text = "label11";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Times New Roman", 13.125F, FontStyle.Bold);
            label10.Location = new Point(34, 61);
            label10.Name = "label10";
            label10.Size = new Size(250, 40);
            label10.TabIndex = 9;
            label10.Text = "Nº Encomenda:\r\n";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(dgvLinhas);
            groupBox3.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold);
            groupBox3.Location = new Point(43, 330);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(2200, 650);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Encomenda";
            // 
            // dgvLinhas
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvLinhas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvLinhas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Times New Roman", 10.875F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvLinhas.DefaultCellStyle = dataGridViewCellStyle2;
            dgvLinhas.Location = new Point(16, 49);
            dgvLinhas.Name = "dgvLinhas";
            dgvLinhas.RowHeadersWidth = 82;
            dgvLinhas.Size = new Size(2168, 583);
            dgvLinhas.TabIndex = 0;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.SteelBlue;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(910, 986);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(417, 58);
            btnGuardar.TabIndex = 4;
            btnGuardar.Text = "Guardar Alterações";
            btnGuardar.UseVisualStyleBackColor = false;
            // 
            // btnSair
            // 
            btnSair.Location = new Point(2085, 986);
            btnSair.Name = "btnSair";
            btnSair.Size = new Size(158, 58);
            btnSair.TabIndex = 5;
            btnSair.Text = "Sair";
            btnSair.UseVisualStyleBackColor = true;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(43, 986);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(350, 58);
            btnModificar.TabIndex = 6;
            btnModificar.Text = "Alterar Encomenda";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // FormDetalhesEncomenda
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2276, 1082);
            Controls.Add(btnModificar);
            Controls.Add(btnSair);
            Controls.Add(btnGuardar);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "FormDetalhesEncomenda";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLinhas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label label2;
        private Label label1;
        private GroupBox groupBox2;
        private Label label3;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label13;
        private Label label12;
        private Label label11;
        private Label label10;
        private Label label16;
        private Label label15;
        private Label label14;
        private ComboBox cmbEstado;
        private Label label19;
        private Label label18;
        private GroupBox groupBox3;
        private DataGridView dgvLinhas;
        private Button btnGuardar;
        private Button btnSair;
        private Button btnModificar;
    }
}