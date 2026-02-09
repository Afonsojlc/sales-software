using System;
using System.Drawing;
using System.Windows.Forms;

namespace SoftwareVendas
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
            ConfigurarVisual();
            if (btnProdutos != null)
            {
                btnProdutos.Click += btnProdutos_Click;
            }
        }

        private void ConfigurarVisual()
        {
            // 1. CONFIGURAÇÃO DE TELA E FUNDO
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.DoubleBuffered = true;

            // Define um tamanho mínimo para não "esmagar" os botões
            this.MinimumSize = new Size(800, 600);
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            // Iniciar o Relógio
            if (timer1 != null)
            {
                timer1.Interval = 1000;
                timer1.Enabled = true;
                timer1.Start();
            }

            // Configurar Cabeçalho (COM A ALTERAÇÃO DO CARGO)
            AtualizarLabels();

            // ORGANIZAR A INTERFACE
            OrganizarInterfaceCompleta();
        }

        private void OrganizarInterfaceCompleta()
        {
            // Variáveis baseadas no tamanho ATUAL da janela
            int W = this.ClientSize.Width;
            int H = this.ClientSize.Height;
            int centroX = W / 2;
            int centroY = H / 2;

            // --- 1. BOTÃO DESTAQUE: NOVA VENDA (40% da largura, 15% da altura) ---
            if (btnNovaVenda != null)
            {
                int wBtn = (int)(W * 0.40);
                int hBtn = (int)(H * 0.15);

                btnNovaVenda.Size = new Size(wBtn, hBtn);
                btnNovaVenda.Location = new Point(centroX - (wBtn / 2), (int)(H * 0.20));

                EstilizarBotao(btnNovaVenda, Color.FromArgb(44, 62, 80));
            }

            // --- 2. LINHA DE GESTÃO (3 Botões alinhados) ---
            int wGestao = (int)(W * 0.22);
            int hGestao = (int)(H * 0.10);
            int yGestao = centroY - (hGestao / 2);
            int espaco = (int)(W * 0.02);

            int larguraTotalGrupo = (wGestao * 3) + (espaco * 2);
            int inicioX = centroX - (larguraTotalGrupo / 2);

            if (btnProdutos != null)
            {
                btnProdutos.Size = new Size(wGestao, hGestao);
                btnProdutos.Location = new Point(inicioX, yGestao);
                EstilizarBotao(btnProdutos, Color.FromArgb(108, 117, 125));

            }

            if (btnClientes != null)
            {
                btnClientes.Size = new Size(wGestao, hGestao);
                btnClientes.Location = new Point(inicioX + wGestao + espaco, yGestao);
                EstilizarBotao(btnClientes, Color.FromArgb(108, 117, 125));
            }

            if (btnEncomendas != null)
            {
                btnEncomendas.Size = new Size(wGestao, hGestao);
                btnEncomendas.Location = new Point(inicioX + (wGestao + espaco) * 2, yGestao);
                EstilizarBotao(btnEncomendas, Color.FromArgb(108, 117, 125));
                btnEncomendas.Text = btnEncomendas.Text.Trim();
            }

            // --- 3. BOTÃO DE GANHOS ---
            if (btnGanhos != null)
            {
                int wBtn = (int)(W * 0.40);
                int hBtn = (int)(H * 0.12);

                btnGanhos.Text = "$$ MEUS GANHOS $$";
                btnGanhos.Size = new Size(wBtn, hBtn);
                btnGanhos.Location = new Point(centroX - (wBtn / 2), (int)(H * 0.65));

                EstilizarBotao(btnGanhos, Color.FromArgb(218, 165, 32));
            }

            // --- 4. BOTÕES DE SISTEMA ---
            int btnSysW = 250;
            int btnSysH = 55;
            int margem = 40;

            if (btnDefinicoes != null)
            {
                btnDefinicoes.Size = new Size(btnSysW, btnSysH);
                btnDefinicoes.Location = new Point(margem, H - btnSysH - margem);
                EstilizarBotao(btnDefinicoes, Color.FromArgb(52, 73, 94));
            }

            if (btnSair != null)
            {
                btnSair.Size = new Size(btnSysW, btnSysH);
                btnSair.Location = new Point(W - btnSysW - margem, H - btnSysH - margem);
                EstilizarBotao(btnSair, Color.FromArgb(192, 57, 43));
            }
        }

        private void EstilizarBotao(Button btn, Color cor)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = cor;
            btn.ForeColor = Color.White;
            btn.Cursor = Cursors.Hand;

            // Remove margens internas que possam desalinhar o texto
            btn.Padding = new Padding(0);

            // Força o alinhamento ao centro absoluto
            btn.TextAlign = ContentAlignment.MiddleCenter;

            float tamanhoFonte = btn.Height * 0.25f;
            if (tamanhoFonte < 8) tamanhoFonte = 8;
            if (tamanhoFonte > 32) tamanhoFonte = 32;

            btn.Font = new Font("Segoe UI", tamanhoFonte, FontStyle.Bold);
        }

        // --- LABELS E CABEÇALHO ---
        private void AtualizarLabels()
        {
            if (lblNomeVendedor != null)
            {
                // AQUI ESTÁ A ALTERAÇÃO: Usa o Cargo guardado na Sessão
                // Se Sessao.Cargo for "Gerente", aparece "Gerente: José Carvalho"
                lblNomeVendedor.Text = Sessao.Cargo + ": " + Sessao.Nome;

                lblNomeVendedor.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                lblNomeVendedor.ForeColor = Color.FromArgb(44, 62, 80);
                lblNomeVendedor.Location = new Point(30, 30);
                lblNomeVendedor.AutoSize = true;
            }

            if (lblRelogio != null)
            {
                lblRelogio.Font = new Font("Segoe UI", 20, FontStyle.Bold);
                lblRelogio.ForeColor = Color.FromArgb(44, 62, 80);
                lblRelogio.TextAlign = ContentAlignment.MiddleRight;
                PosicionarRelogio();
            }
        }

        private void PosicionarRelogio()
        {
            if (lblRelogio != null)
            {
                lblRelogio.Left = this.ClientSize.Width - lblRelogio.Width - 30;
                lblRelogio.Top = 30;
            }
        }

        // --- EVENTOS ---

        private void btnSair_Click(object sender, EventArgs e)
        {
            DialogResult resposta = MessageBox.Show(
                "O que deseja fazer?\n\n[SIM] Voltar ao Login\n[NÃO] Fechar Aplicação",
                "Sair",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (resposta == DialogResult.Yes)
            {
                this.Close();
                new FormLogin().Show();
            }
            else if (resposta == DialogResult.No)
            {
                Application.Exit();
            }
        }

        private void btnNovaVenda_Click(object sender, EventArgs e)
        {
            new Form1().ShowDialog();
        }

        private void btnProdutos_Click(object? sender, EventArgs? e)
        {
            FormProdutos janelaProdutos = new FormProdutos();

            // Abre a janela de forma "Modal" (o utilizador não pode mexer no Menu enquanto não fechar os Produtos)
            janelaProdutos.ShowDialog();
        }

        private void timerRelogio_Tick(object sender, EventArgs e)
        {
            if (lblRelogio != null)
            {
                lblRelogio.Text = DateTime.Now.ToString("HH:mm:ss") + " | " + DateTime.Now.ToString("dd/MM/yyyy");
                PosicionarRelogio();
            }
        }

        private void FormMenu_Resize(object sender, EventArgs e)
        {
            PosicionarRelogio();
            OrganizarInterfaceCompleta();
        }
    }
}