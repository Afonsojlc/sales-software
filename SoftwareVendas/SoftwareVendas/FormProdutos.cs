using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SoftwareVendas
{
    public partial class FormProdutos : Form
    {
        private readonly string connectionString = @"Server=DESKTOP-P0S20G1\SQLEXPRESS;Database=Software_Vendas_Pai;Trusted_Connection=True;TrustServerCertificate=True;";

        public ProdutoDTO? ProdutoSelecionado { get; private set; }

        public FormProdutos()
        {
            InitializeComponent();
            ConfigurarLista();
        }

        #region Inicialização e Configuração

        private void ConfigurarLista()
        {
            lstSugestoes.DisplayMember = "DisplayText";
            lstSugestoes.ValueMember = "Codigo";
            lstSugestoes.Visible = false;
        }

        private void label11_Click(object? sender, EventArgs e) { }

        #endregion

        #region Lógica de Pesquisa

        private void txtPesquisa_TextChanged(object? sender, EventArgs e)
        {
            string termo = txtPesquisa.Text.Trim();

            if (termo.Length < 2)
            {
                lstSugestoes.DataSource = null;
                lstSugestoes.Visible = false;
                return;
            }

            CarregarSugestoes(termo);
        }

        private void CarregarSugestoes(string termo)
        {
            var listaProdutos = new List<ProdutoDTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT TOP 20 Codigo, Descricao, PVP_Unidade, Stock, Taxa_IVA, ID_Tipo 
                                     FROM Material 
                                     WHERE Codigo LIKE @Search OR Descricao LIKE @Search";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Search", $"%{termo}%");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listaProdutos.Add(new ProdutoDTO
                                {
                                    Codigo = reader["Codigo"]?.ToString() ?? string.Empty,
                                    Descricao = reader["Descricao"]?.ToString() ?? string.Empty,
                                    Categoria = reader["ID_Tipo"] != DBNull.Value ? reader["ID_Tipo"]?.ToString() ?? string.Empty : "Geral",
                                    Preco = reader["PVP_Unidade"] != DBNull.Value ? Convert.ToDecimal(reader["PVP_Unidade"]) : 0,
                                    Stock = reader["Stock"] != DBNull.Value ? Convert.ToInt32(reader["Stock"]) : 0,
                                    Iva = reader["Taxa_IVA"] != DBNull.Value ? Convert.ToDecimal(reader["Taxa_IVA"]) : 0
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocorreu um erro ao carregar os produtos: {ex.Message}", "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (listaProdutos.Count > 0)
            {
                lstSugestoes.DataSource = listaProdutos;
                lstSugestoes.Visible = true;
                lstSugestoes.BringToFront();
            }
            else
            {
                lstSugestoes.Visible = false;
            }
        }

        public void DefinirPesquisa(string texto)
        {
            txtPesquisa.Text = texto;
        }

        #endregion

        #region Eventos de Seleção

        private void lstSugestoes_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (lstSugestoes.SelectedItem == null) return;

            ProdutoDTO p = (ProdutoDTO)lstSugestoes.SelectedItem;

            lblCodigo.Text = p.Codigo;
            lblNome.Text = p.Descricao;
            lblCategoria.Text = p.Categoria;
            lblPreco.Text = p.Preco.ToString("C2");

            if (p.Stock > 0)
            {
                lblStock.Text = $"{p.Stock} Unidades (Disponível)";
                lblStock.ForeColor = Color.SeaGreen;
            }
            else
            {
                lblStock.Text = "Sem Stock";
                lblStock.ForeColor = Color.Crimson;
            }
        }

        private void btnSelecionar_Click(object? sender, EventArgs e)
        {
            ConfirmarSelecao();
        }

        private void lstSugestoes_DoubleClick(object? sender, EventArgs e)
        {
            ConfirmarSelecao();
        }

        private void lstSugestoes_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConfirmarSelecao();
                e.Handled = true;
            }
        }

        private void ConfirmarSelecao()
        {
            if (!btnSelecionar.Visible) return;

            if (lstSugestoes.SelectedItem != null)
            {
                ProdutoSelecionado = (ProdutoDTO)lstSugestoes.SelectedItem;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor, selecione um produto da lista antes de prosseguir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion

        #region Operações CRUD (Adicionar, Atualizar, Eliminar)

        private void btnAdicionarProduto_Click(object? sender, EventArgs e)
        {
            using (FormNovoProduto frmNovo = new FormNovoProduto())
            {
                if (frmNovo.ShowDialog() == DialogResult.OK)
                {
                    // Recarrega a pesquisa para refletir o novo produto
                    string textoAtual = txtPesquisa.Text;
                    txtPesquisa.Text = string.Empty;
                    txtPesquisa.Text = textoAtual;

                    MessageBox.Show("A lista foi atualizada com o novo artigo.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnEliminarProduto_Click(object? sender, EventArgs e)
        {
            if (lstSugestoes.SelectedItem == null)
            {
                MessageBox.Show("Por favor, pesquise e selecione um produto na lista para o eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ProdutoDTO produtoClicado = (ProdutoDTO)lstSugestoes.SelectedItem;
            string codigoAEliminar = produtoClicado.Codigo;

            DialogResult resposta = MessageBox.Show(
                $"Tem a certeza absoluta que deseja eliminar o produto:\n{codigoAEliminar} - {produtoClicado.Descricao}?\n\nEsta ação não pode ser desfeita.",
                "Confirmar Eliminação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Stop);

            if (resposta == DialogResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();

                        // Verificação de Integridade Referencial
                        string queryVerificacao = "SELECT COUNT(*) FROM Linha_Encomenda WHERE Codigo_Material = @cod";
                        using (SqlCommand cmdVerifica = new SqlCommand(queryVerificacao, con))
                        {
                            cmdVerifica.Parameters.AddWithValue("@cod", codigoAEliminar);
                            int vendas = (int)(cmdVerifica.ExecuteScalar() ?? 0);

                            if (vendas > 0)
                            {
                                MessageBox.Show("Não é possível eliminar este produto pois já se encontra associado a faturas existentes. A eliminação comprometeria o histórico de vendas.", "Restrição de Integridade", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        string queryDelete = "DELETE FROM Material WHERE Codigo = @cod";
                        using (SqlCommand cmdDelete = new SqlCommand(queryDelete, con))
                        {
                            cmdDelete.Parameters.AddWithValue("@cod", codigoAEliminar);
                            cmdDelete.ExecuteNonQuery();
                        }

                        MessageBox.Show("Produto eliminado com sucesso.", "Operação Concluída", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        lblCodigo.Text = "...";
                        lblNome.Text = "...";
                        lblCategoria.Text = "...";
                        lblPreco.Text = "...";
                        lblStock.Text = "...";

                        txtPesquisa.Text = string.Empty;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ocorreu um erro ao tentar eliminar o produto: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void lblStock_MouseClick(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && lstSugestoes.SelectedItem != null)
            {
                ProdutoDTO produtoClicado = (ProdutoDTO)lstSugestoes.SelectedItem;

                using (FormAtualizarStock frmStock = new FormAtualizarStock(produtoClicado))
                {
                    if (frmStock.ShowDialog() == DialogResult.OK)
                    {
                        string textoAtual = txtPesquisa.Text;
                        txtPesquisa.Text = string.Empty;
                        txtPesquisa.Text = textoAtual;
                    }
                }
            }
        }

        #endregion

        #region Controlo de Modos (Vendas / Gestão)

        public void PrepararModoVendas()
        {
            btnSelecionar.Visible = true;

            if (this.Controls.Find("btnAdicionarProduto", true).Length > 0)
                btnAdicionarProduto.Visible = false;

            if (this.Controls.Find("btnEliminarProduto", true).Length > 0)
                btnEliminarProduto.Visible = false;
        }

        public void PrepararModoGestao()
        {
            btnSelecionar.Visible = false;

            if (this.Controls.Find("btnAdicionarProduto", true).Length > 0)
                btnAdicionarProduto.Visible = true;

            if (this.Controls.Find("btnEliminarProduto", true).Length > 0)
                btnEliminarProduto.Visible = true;
        }

        #endregion
    }

    public class ProdutoDTO
    {
        public string Codigo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int Stock { get; set; }
        public decimal Iva { get; set; }

        public string DisplayText => $"{Codigo} - {Descricao}";

        public override string ToString()
        {
            return $"{Codigo} - {Descricao}";
        }
    }
}