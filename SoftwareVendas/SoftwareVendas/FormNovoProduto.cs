using Microsoft.Data.SqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SoftwareVendas
{
    public partial class FormNovoProduto : Form
    {
        string connectionString = @"Server=DESKTOP-P0S20G1\SQLEXPRESS;Database=Software_Vendas_Pai;Trusted_Connection=True;TrustServerCertificate=True;";

        public FormNovoProduto()
        {
            InitializeComponent();

            // Configurações da Janela
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.Text = "Adicionar Novo Produto";

            // Valores máximos e mínimos para proteger a Base de Dados
            numPreco.Maximum = 999999;
            numStock.Maximum = 999999;
            numIVA.Maximum = 100;

            // --- NOVO: Carregar as sugestões de categorias assim que a janela abre ---
            CarregarCategorias();
        }

        private void groupBox1_Enter(object? sender, EventArgs e)
        {

        }

        private void btnGravar_Click(object? sender, EventArgs e)
        {
            // 1. VALIDAÇÕES: Evitar guardar coisas vazias
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("O Código do produto é obrigatório!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCodigo.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDescricao.Text))
            {
                MessageBox.Show("A Descrição do produto é obrigatória!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescricao.Focus();
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // 2. VERIFICAR SE O CÓDIGO JÁ EXISTE NA BASE DE DADOS
                    string queryVerifica = "SELECT COUNT(*) FROM Material WHERE Codigo = @cod";
                    using (SqlCommand cmdVerifica = new SqlCommand(queryVerifica, con))
                    {
                        cmdVerifica.Parameters.AddWithValue("@cod", txtCodigo.Text.Trim());
                        int existe = (int)(cmdVerifica.ExecuteScalar() ?? 0);

                        if (existe > 0)
                        {
                            MessageBox.Show("Já existe um produto com esse Código. Escolha outro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtCodigo.Focus();
                            return; // Para o processo aqui
                        }
                    }

                    // 3. INSERIR O NOVO PRODUTO NA TABELA MATERIAL
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

                        // Se não escreveres nada na categoria, guarda NULL ou Geral
                        string categoria = string.IsNullOrWhiteSpace(txtCategoria.Text) ? "Geral" : txtCategoria.Text.Trim();
                        cmd.Parameters.AddWithValue("@tipo", categoria);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Produto adicionado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Diz OK e fecha a janela
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao gravar produto: " + ex.Message, "Erro Base de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancelar_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // --- NOVO: MÉTODO PARA O AUTOCOMPLETAR DA CATEGORIA ---
        private void CarregarCategorias()
        {
            AutoCompleteStringCollection listaCategorias = new AutoCompleteStringCollection();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    // Vai procurar todos os ID_Tipo (categorias) únicos que já existem na tabela Material
                    string query = "SELECT DISTINCT ID_Tipo FROM Material WHERE ID_Tipo IS NOT NULL";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader leitor = cmd.ExecuteReader())
                        {
                            while (leitor.Read())
                            {
                                string categoriaLida = leitor["ID_Tipo"].ToString() ?? "";
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
                    // Se der erro a carregar categorias, ignora silenciosamente.
                    // O utilizador simplesmente não terá sugestões, mas o form abre à mesma.
                }
            }

            // Ativa a magia na caixa de texto txtCategoria
            txtCategoria.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtCategoria.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtCategoria.AutoCompleteCustomSource = listaCategorias;
        }
    }
}