namespace SoftwareVendas
{
    partial class FormMenu
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
            lblNomeVendedor = new Label();
            lblRelogio = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            btnNovaVenda = new Button();
            btnProdutos = new Button();
            btnClientes = new Button();
            btnEncomendas = new Button();
            btnGanhos = new Button();
            btnDefinicoes = new Button();
            btnSair = new Button();
            SuspendLayout();
            // 
            // lblNomeVendedor
            // 
            lblNomeVendedor.AutoSize = true;
            lblNomeVendedor.Location = new Point(79, 64);
            lblNomeVendedor.Name = "lblNomeVendedor";
            lblNomeVendedor.Size = new Size(78, 32);
            lblNomeVendedor.TabIndex = 0;
            lblNomeVendedor.Text = "label1";
            // 
            // lblRelogio
            // 
            lblRelogio.AutoSize = true;
            lblRelogio.Location = new Point(1607, 64);
            lblRelogio.Name = "lblRelogio";
            lblRelogio.Size = new Size(78, 32);
            lblRelogio.TabIndex = 1;
            lblRelogio.Text = "label2";
            // 
            // timer1
            // 
            timer1.Tick += timerRelogio_Tick;
            // 
            // btnNovaVenda
            // 
            btnNovaVenda.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnNovaVenda.Location = new Point(545, 214);
            btnNovaVenda.Name = "btnNovaVenda";
            btnNovaVenda.Size = new Size(734, 160);
            btnNovaVenda.TabIndex = 2;
            btnNovaVenda.Text = "NOVA VENDA";
            btnNovaVenda.UseVisualStyleBackColor = true;
            btnNovaVenda.Click += btnNovaVenda_Click;
            // 
            // btnProdutos
            // 
            btnProdutos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnProdutos.Location = new Point(183, 470);
            btnProdutos.Name = "btnProdutos";
            btnProdutos.Size = new Size(336, 120);
            btnProdutos.TabIndex = 3;
            btnProdutos.Text = "PRODUTOS";
            btnProdutos.UseVisualStyleBackColor = true;
            btnProdutos.Click += btnProdutos_Click;
            // 
            // btnClientes
            // 
            btnClientes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnClientes.Location = new Point(753, 471);
            btnClientes.Name = "btnClientes";
            btnClientes.Size = new Size(336, 120);
            btnClientes.TabIndex = 4;
            btnClientes.Text = "CLIENTES";
            btnClientes.UseVisualStyleBackColor = true;
            // 
            // btnEncomendas
            // 
            btnEncomendas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnEncomendas.Location = new Point(1251, 470);
            btnEncomendas.Name = "btnEncomendas";
            btnEncomendas.Size = new Size(336, 120);
            btnEncomendas.TabIndex = 5;
            btnEncomendas.Text = "ENCOMENDAS";
            btnEncomendas.UseVisualStyleBackColor = true;
            // 
            // btnGanhos
            // 
            btnGanhos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnGanhos.Location = new Point(545, 651);
            btnGanhos.Name = "btnGanhos";
            btnGanhos.Size = new Size(734, 108);
            btnGanhos.TabIndex = 6;
            btnGanhos.Text = "$$ MEUS GANHOS $$";
            btnGanhos.UseVisualStyleBackColor = true;
            // 
            // btnDefinicoes
            // 
            btnDefinicoes.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDefinicoes.Location = new Point(73, 982);
            btnDefinicoes.Name = "btnDefinicoes";
            btnDefinicoes.Size = new Size(216, 46);
            btnDefinicoes.TabIndex = 7;
            btnDefinicoes.Text = "DEFINIÇÕES";
            btnDefinicoes.UseVisualStyleBackColor = true;
            // 
            // btnSair
            // 
            btnSair.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSair.Location = new Point(1646, 982);
            btnSair.Name = "btnSair";
            btnSair.Size = new Size(150, 46);
            btnSair.TabIndex = 8;
            btnSair.Text = "SAIR";
            btnSair.UseVisualStyleBackColor = true;
            btnSair.Click += btnSair_Click;
            // 
            // FormMenu
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1842, 1062);
            Controls.Add(btnSair);
            Controls.Add(btnDefinicoes);
            Controls.Add(btnGanhos);
            Controls.Add(btnEncomendas);
            Controls.Add(btnClientes);
            Controls.Add(btnProdutos);
            Controls.Add(btnNovaVenda);
            Controls.Add(lblRelogio);
            Controls.Add(lblNomeVendedor);
            Name = "FormMenu";
            Text = "FormMenu";
            Load += FormMenu_Load;
            Resize += FormMenu_Resize;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNomeVendedor;
        private Label lblRelogio;
        private System.Windows.Forms.Timer timer1;
        private Button btnNovaVenda;
        private Button btnProdutos;
        private Button btnClientes;
        private Button btnEncomendas;
        private Button btnGanhos;
        private Button btnDefinicoes;
        private Button btnSair;
    }
}