using Microsoft.Data.SqlClient;
using System;
using System.Windows.Forms;

namespace SoftwareVendas
{
    public partial class FormAtualizarStock : Form
    {
        private readonly string connectionString = @"Server=DESKTOP-P0S20G1\SQLEXPRESS;Database=Software_Vendas_Pai;Trusted_Connection=True;TrustServerCertificate=True;";
        private readonly string codigoProdutoParaAtualizar;

        public FormAtualizarStock(ProdutoDTO produto)
        {
            InitializeComponent();

            codigoProdutoParaAtualizar = produto.Codigo;
            ConfigurarInterface(produto);
        }

        #region Configuração da Interface

        private void ConfigurarInterface(ProdutoDTO produto)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = "Atualizar Stock";

            lblCodigo.Text = $"Código: {produto.Codigo}";
            lblNome.Text = produto.Descricao;
            lblStockAtual.Text = $"Stock Atual: {produto.Stock} unidades";

            numNovoStock.Minimum = 0;
            numNovoStock.Maximum = 99999;
            numNovoStock.Value = produto.Stock;
        }

        #endregion

        #region Operações de Base de Dados

        private void btnGravar_Click(object? sender, EventArgs e)
        {
            int novoStock = (int)numNovoStock.Value;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "UPDATE Material SET Stock = @novoStock WHERE Codigo = @cod";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@novoStock", novoStock);
                        cmd.Parameters.AddWithValue("@cod", codigoProdutoParaAtualizar);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Inventário atualizado com sucesso.", "Operação Concluída", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Não foi possível atualizar o stock.\nDetalhe: {ex.Message}", "Erro de Gravação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Eventos UI

        private void btnCancelar_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion
    }
}