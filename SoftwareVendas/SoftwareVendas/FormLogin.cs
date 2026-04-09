using Microsoft.Data.SqlClient;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SoftwareVendas
{
    public partial class FormLogin : Form
    {
        private readonly string connectionString = @"Server=DESKTOP-P0S20G1\SQLEXPRESS;Database=Software_Vendas_Pai;Trusted_Connection=True;TrustServerCertificate=True;";
        private bool modoPin = true;

        public FormLogin()
        {
            InitializeComponent();
            ConfigurarVisual();
        }

        private void ConfigurarVisual()
        {
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

        private void FormLogin_Resize(object? sender, EventArgs e)
        {
            CentrarPainelCentral();
            AlinharPaineis();
        }

        private void FormLogin_Load(object? sender, EventArgs e)
        {
            CentrarPainelCentral();
            AlinharPaineis();
        }

        // Alterna para o modo de autenticação via Email
        private void label2_Click(object? sender, EventArgs e)
        {
            modoPin = false;
            pnlModoPin.Visible = false;
            pnlModoEmail.Visible = true;
            txtEmail.Focus();
        }

        // Alterna para o modo de autenticação via PIN
        private void label3_Click(object? sender, EventArgs e)
        {
            modoPin = true;
            pnlModoEmail.Visible = false;
            pnlModoPin.Visible = true;
            txtPIN.Focus();
        }

        private void btnEntrar_Click_1(object? sender, EventArgs e)
        {
            string query = "SELECT ID_Vendedor, Nome, Percentagem_Comissao, Cargo FROM Vendedores WHERE PIN = @p1 AND Ativo = 1";
            ExecutarLogin(query, txtPIN.Text, null);
        }

        private void button1_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                MessageBox.Show("Por favor, preencha o email e a senha.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "SELECT ID_Vendedor, Nome, Percentagem_Comissao, Cargo FROM Vendedores WHERE Email = @p1 AND Senha = @p2 AND Ativo = 1";
            ExecutarLogin(query, txtEmail.Text, txtSenha.Text);
        }

        // Executa a autenticação do utilizador na base de dados de forma centralizada e segura.
        private void ExecutarLogin(string query, string p1, string? p2)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@p1", p1);

                        if (p2 != null)
                        {
                            cmd.Parameters.AddWithValue("@p2", p2);
                        }

                        using (SqlDataReader leitor = cmd.ExecuteReader())
                        {
                            if (leitor.Read())
                            {
                                // Inicialização dos dados da Sessão
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
                                MessageBox.Show("As credenciais inseridas estão incorretas.", "Falha na Autenticação", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                if (p2 == null)
                                    txtPIN.Clear();
                                else
                                    txtSenha.Clear();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocorreu um erro de comunicação com a base de dados.\nDetalhes: {ex.Message}", "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSairApp_Click(object? sender, EventArgs e)
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

        private void txtPIN_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (pnlModoPin.Visible)
                    btnEntrar_Click_1(sender, e);
                else
                    button1_Click(sender, e);
            }
        }
    }
}