using Microsoft.Data.SqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SoftwareVendas
{
    public partial class FormAtualizarStock : Form
    {
        string connectionString = @"Server=DESKTOP-P0S20G1\SQLEXPRESS;Database=Software_Vendas_Pai;Trusted_Connection=True;TrustServerCertificate=True;";

        // Guardamos o código do produto que recebemos para usar no SQL
        private string codigoProdutoParaAtualizar;

        // O Construtor agora pede um ProdutoDTO quando a janela é criada!
        public FormAtualizarStock(ProdutoDTO produto)
        {
            InitializeComponent();

            // Configurações visuais básicas da janela
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = "Atualizar Stock";

            // Preenche os dados visuais com o que veio da janela anterior
            codigoProdutoParaAtualizar = produto.Codigo;
            lblCodigo.Text = "Código: " + produto.Codigo;
            lblNome.Text = produto.Descricao;
            lblStockAtual.Text = "Stock Atual: " + produto.Stock.ToString() + " unidades";

            // Configura a caixa de números
            numNovoStock.Minimum = 0; // Não permite stock negativo
            numNovoStock.Maximum = 99999;
            numNovoStock.Value = produto.Stock; // Começa com o valor atual para facilitar
        }

        private void btnGravar_Click(object? sender, EventArgs e)
        {
            int novoStock = (int)numNovoStock.Value;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    // Atualiza o stock na Tabela Material
                    string query = "UPDATE Material SET Stock = @novoStock WHERE Codigo = @cod";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@novoStock", novoStock);
                        cmd.Parameters.AddWithValue("@cod", codigoProdutoParaAtualizar);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Stock atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Diz à janela anterior que correu tudo bem e fecha
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao atualizar stock: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancelar_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}