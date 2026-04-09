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

            // Prevenção de subscrição dupla do evento
            if (btnProdutos != null)
            {
                btnProdutos.Click -= btnProdutos_Click;
                btnProdutos.Click += btnProdutos_Click;
            }

            if (btnClientes != null)
            {
                btnClientes.Click -= btnClientes_Click;
                btnClientes.Click += btnClientes_Click;
            }
        }

        private void ConfigurarVisual()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.DoubleBuffered = true;

            this.MinimumSize = new Size(800, 600);
        }

        private void FormMenu_Load(object? sender, EventArgs e)
        {
            if (timer1 != null)
            {
                timer1.Interval = 1000;
                timer1.Enabled = true;
                timer1.Start();
            }

            AtualizarLabels();
            OrganizarInterfaceCompleta();
        }

        private void OrganizarInterfaceCompleta()
        {
            int W = this.ClientSize.Width;
            int H = this.ClientSize.Height;
            int centroX = W / 2;
            int centroY = H / 2;

            if (btnNovaVenda != null)
            {
                int wBtn = (int)(W * 0.40);
                int hBtn = (int)(H * 0.15);

                btnNovaVenda.Size = new Size(wBtn, hBtn);
                btnNovaVenda.Location = new Point(centroX - (wBtn / 2), (int)(H * 0.20));

                EstilizarBotao(btnNovaVenda, Color.FromArgb(44, 62, 80));
            }

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

            if (btnGanhos != null)
            {
                int wBtn = (int)(W * 0.40);
                int hBtn = (int)(H * 0.12);

                btnGanhos.Text = "$$ MEUS GANHOS $$";
                btnGanhos.Size = new Size(wBtn, hBtn);
                btnGanhos.Location = new Point(centroX - (wBtn / 2), (int)(H * 0.65));

                EstilizarBotao(btnGanhos, Color.FromArgb(218, 165, 32));
            }

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
            btn.Padding = new Padding(0);
            btn.TextAlign = ContentAlignment.MiddleCenter;

            float tamanhoFonte = btn.Height * 0.25f;
            tamanhoFonte = Math.Clamp(tamanhoFonte, 8f, 32f);

            btn.Font = new Font("Segoe UI", tamanhoFonte, FontStyle.Bold);
        }

        private void AtualizarLabels()
        {
            if (lblNomeVendedor != null)
            {
                lblNomeVendedor.Text = $"{Sessao.Cargo}: {Sessao.Nome}";
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

        private void btnSair_Click(object? sender, EventArgs e)
        {
            DialogResult resposta = MessageBox.Show(
                "O que deseja fazer?\n\n[SIM] Voltar ao Login\n[NÃO] Fechar Aplicação",
                "Encerrar Sessão",
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

        private void btnNovaVenda_Click(object? sender, EventArgs e)
        {
            using (Form1 frmVenda = new Form1())
            {
                frmVenda.ShowDialog();
            }
        }

        private void btnProdutos_Click(object? sender, EventArgs e)
        {
            using (FormProdutos janelaProdutos = new FormProdutos())
            {
                janelaProdutos.PrepararModoGestao();
                janelaProdutos.ShowDialog();
            }
        }

        private void btnClientes_Click(object? sender, EventArgs e)
        {
            using (FormClientes janelaClientes = new FormClientes())
            {
                janelaClientes.ShowDialog();
            }
        }

        private void timerRelogio_Tick(object? sender, EventArgs e)
        {
            if (lblRelogio != null)
            {
                lblRelogio.Text = $"{DateTime.Now:HH:mm:ss} | {DateTime.Now:dd/MM/yyyy}";
                PosicionarRelogio();
            }
        }

        private void FormMenu_Resize(object? sender, EventArgs e)
        {
            PosicionarRelogio();
            OrganizarInterfaceCompleta();
        }
    }
}