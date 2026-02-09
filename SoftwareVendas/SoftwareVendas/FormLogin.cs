using Microsoft.Data.SqlClient;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SoftwareVendas
{
    public partial class FormLogin : Form
    {
        string connectionString = @"Server=DESKTOP-P0S20G1\SQLEXPRESS;Database=Software_Vendas_Pai;Trusted_Connection=True;TrustServerCertificate=True;";
        bool modoPin = true;

        public FormLogin()
        {
            InitializeComponent();
            ConfigurarVisual();
        }

        private void ConfigurarVisual()
        {
            // --- MANTER A ESTÉTICA ORIGINAL (SEM ALTERAÇÕES) ---
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            try
            {
                string caminhoImagem = Path.Combine(Application.StartupPath, "fundo.jpg");
                if (File.Exists(caminhoImagem))
                {
                    this.BackgroundImage = Image.FromFile(caminhoImagem);
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                }
                else
                {
                    this.BackColor = Color.FromArgb(44, 62, 80);
                }
            }
            catch
            {
                this.BackColor = Color.FromArgb(44, 62, 80);
            }

            pnlCentral.BackColor = Color.Transparent;

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            pnlModoPin.Visible = true;
            pnlModoEmail.Visible = false;

            AlinharPaineis();
        }

        private void AlinharPaineis()
        {
            if (pnlModoEmail != null && pnlModoPin != null)
            {
                pnlModoEmail.Location = pnlModoPin.Location;
                pnlModoEmail.Size = pnlModoPin.Size;
            }
            CentrarPainelCentral();
        }

        private void CentrarPainelCentral()
        {
            if (pnlCentral != null)
            {
                pnlCentral.Left = (this.ClientSize.Width - pnlCentral.Width) / 2;
                pnlCentral.Top = (this.ClientSize.Height - pnlCentral.Height) / 2;
            }

            if (btnSairApp != null)
            {
                btnSairApp.FlatStyle = FlatStyle.Flat;
                btnSairApp.FlatAppearance.BorderSize = 0;
                btnSairApp.BackColor = Color.Transparent;
                btnSairApp.ForeColor = Color.White;

                btnSairApp.Left = this.ClientSize.Width - 60;
                btnSairApp.Top = 20;
                btnSairApp.BringToFront();
            }
        }

        private void FormLogin_Resize(object sender, EventArgs e)
        {
            CentrarPainelCentral();
            AlinharPaineis();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            CentrarPainelCentral();
            AlinharPaineis();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            modoPin = false;
            pnlModoPin.Visible = false;
            pnlModoEmail.Visible = true;
            txtEmail.Focus();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            modoPin = true;
            pnlModoEmail.Visible = false;
            pnlModoPin.Visible = true;
            txtPIN.Focus();
        }

        // --- LÓGICA DE LOGIN (ATUALIZADA) ---

        // 1. Entrar com PIN (Adicionei o campo Cargo à pesquisa)
        private void btnEntrar_Click_1(object sender, EventArgs e)
        {
            ExecutarLogin("SELECT ID_Vendedor, Nome, Percentagem_Comissao, Cargo FROM Vendedores WHERE PIN = @p1 AND Ativo = 1", txtPIN.Text, null);
        }

        // 2. Entrar com Email e Senha (Adicionei o campo Cargo à pesquisa)
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                MessageBox.Show("Preencha email e senha.");
                return;
            }
            // A query agora verifica Email E Senha
            ExecutarLogin("SELECT ID_Vendedor, Nome, Percentagem_Comissao, Cargo FROM Vendedores WHERE Email = @p1 AND Senha = @p2 AND Ativo = 1", txtEmail.Text, txtSenha.Text);
        }

        private void ExecutarLogin(string query, string p1, string? p2)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@p1", p1);
                    // Aqui o C# já não reclama porque marcaste p2 como opcional (?)
                    if (p2 != null) cmd.Parameters.AddWithValue("@p2", p2);

                    SqlDataReader leitor = cmd.ExecuteReader();

                    if (leitor.Read())
                    {
                        Sessao.ID_Vendedor = Convert.ToInt32(leitor["ID_Vendedor"]);
                        Sessao.Nome = leitor["Nome"]?.ToString() ?? "Utilizador";
                        Sessao.PercentagemComissao = Convert.ToDecimal(leitor["Percentagem_Comissao"]);
                        Sessao.Cargo = leitor["Cargo"]?.ToString() ?? "Vendedor";

                        FormMenu menu = new FormMenu();
                        menu.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Dados incorretos!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (p2 == null) txtPIN.Clear();
                        else txtSenha.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro de conexão: " + ex.Message);
                }
            }
        }

        private void btnSairApp_Click(object sender, EventArgs e)
        {
            DialogResult resposta = MessageBox.Show(
                "Tem a certeza que deseja encerrar a aplicação?",
                "Confirmar Saída",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resposta == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void txtPIN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (pnlModoPin.Visible) btnEntrar_Click_1(sender, e);
                else button1_Click(sender, e);
            }
        }
    }
}