using Microsoft.Data.SqlClient;
using System;
using System.Windows.Forms;

namespace SoftwareVendas
{
    public partial class FormNovoProduto : Form
    {
        private readonly string connectionString = @"Server=DESKTOP-P0S20G1\SQLEXPRESS;Database=Software_Vendas_Pai;Trusted_Connection=True;TrustServerCertificate=True;";

        public FormNovoProduto()
        {
            InitializeComponent();
            ConfigurarInterface();
            CarregarCategorias();
        }

        #region Configuração Inicial

        private void ConfigurarInterface()
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.Text = "Adicionar Novo Produto";

            // Restrições de segurança de dados para inputs numéricos
            numPreco.Maximum = 999999;
            numStock.Maximum = 999999;
            numIVA.Maximum = 100;
        }

        private void CarregarCategorias()
        {
            AutoCompleteStringCollection listaCategorias = new AutoCompleteStringCollection();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT DISTINCT ID_Tipo FROM Material WHERE ID_Tipo IS NOT NULL";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader leitor = cmd.ExecuteReader())
                        {
                            while (leitor.Read())
                            {
                                string categoriaLida = leitor["ID_Tipo"].ToString() ?? string.Empty;
                                if (!string.IsNullOrWhiteSpace(categoriaLida))
                                {
                                    listaCategorias.Add(categoriaLida);
                                }
                            }
                        }
                    }
                }
                catch
                {
                }
            }

            txtCategoria.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtCategoria.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtCategoria.AutoCompleteCustomSource = listaCategorias;
        }

        #endregion

        #region Operações de Base de Dados (Gravar)

        private void btnGravar_Click(object? sender, EventArgs e)
        {
            if (!ValidarCamposObrigatorios()) return;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    if (VerificarProdutoExistente(con, txtCodigo.Text.Trim()))
                    {
                        MessageBox.Show("Já existe um produto registado com o Código especificado.", "Conflito de Dados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCodigo.Focus();
                        return;
                    }

                    InserirNovoProduto(con);

                    MessageBox.Show("Produto registado com sucesso.", "Operação Concluída", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocorreu um erro ao gravar o produto: {ex.Message}", "Erro de Base de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool VerificarProdutoExistente(SqlConnection con, string codigoProduto)
        {
            string queryVerifica = "SELECT COUNT(*) FROM Material WHERE Codigo = @cod";
            using (SqlCommand cmdVerifica = new SqlCommand(queryVerifica, con))
            {
                cmdVerifica.Parameters.AddWithValue("@cod", codigoProduto);
                int existe = (int)(cmdVerifica.ExecuteScalar() ?? 0);
                return existe > 0;
            }
        }

        private void InserirNovoProduto(SqlConnection con)
        {
            string queryInsert = @"
                INSERT INTO Material (Codigo, Descricao, PVP_Unidade, Stock, Taxa_IVA, ID_Tipo, Unidade_Venda) 
                VALUES (@cod, @desc, @preco, @stock, @iva, @tipo, 'UN')";

            using (SqlCommand cmd = new SqlCommand(queryInsert, con))
            {
                cmd.Parameters.AddWithValue("@cod", txtCodigo.Text.Trim());
                cmd.Parameters.AddWithValue("@desc", txtDescricao.Text.Trim());
                cmd.Parameters.AddWithValue("@preco", numPreco.Value);
                cmd.Parameters.AddWithValue("@stock", (int)numStock.Value);
                cmd.Parameters.AddWithValue("@iva", numIVA.Value);

                string categoria = string.IsNullOrWhiteSpace(txtCategoria.Text) ? "Geral" : txtCategoria.Text.Trim();
                cmd.Parameters.AddWithValue("@tipo", categoria);

                cmd.ExecuteNonQuery();
            }
        }

        #endregion

        #region Validações e Eventos UI

        private bool ValidarCamposObrigatorios()
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("O preenchimento do 'Código' é obrigatório.", "Validação de Dados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCodigo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescricao.Text))
            {
                MessageBox.Show("O preenchimento da 'Descrição' é obrigatório.", "Validação de Dados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescricao.Focus();
                return false;
            }

            return true;
        }

        private void btnCancelar_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void groupBox1_Enter(object? sender, EventArgs e){ }

        #endregion
    }
}