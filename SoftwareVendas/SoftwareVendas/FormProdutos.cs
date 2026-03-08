using System;
using System.Collections.Generic;
using System.Drawing; // Necessário para as cores (Verde/Vermelho)
using System.Windows.Forms;
using Microsoft.Data.SqlClient; // O driver para SQL Server

namespace SoftwareVendas
{
    public partial class FormProdutos : Form
    {
        // A Connection String para o teu PC (DESKTOP-P0S20G1)
        string connectionString = @"Server=DESKTOP-P0S20G1\SQLEXPRESS;Database=Software_Vendas_Pai;Trusted_Connection=True;TrustServerCertificate=True;";

        // Esta propriedade vai guardar o produto que escolheres para usar depois na fatura
        public ProdutoDTO? ProdutoSelecionado { get; private set; }

        public FormProdutos()
        {
            InitializeComponent();
            ConfigurarLista();
        }

        private void label11_Click(object? sender, EventArgs e)
        {

        }

        // Configura a lista para começar escondida e mostrar o texto certo
        private void ConfigurarLista()
        {
            lstSugestoes.DisplayMember = "DisplayText"; // Vai mostrar "Codigo - Nome"
            lstSugestoes.ValueMember = "Codigo";
            lstSugestoes.Visible = false;
        }

        // =========================================================
        // 1. EVENTO DE PESQUISA (Enquanto escreves)
        // =========================================================
        // Associa este evento à tua TextBox (txtPesquisa) -> Propriedades -> Eventos (Raio) -> TextChanged
        private void txtPesquisa_TextChanged(object? sender, EventArgs e)
        {
            string termo = txtPesquisa.Text.Trim();

            // Se tiver menos de 2 letras, não pesquisa nada e esconde a lista
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
                    // Vai buscar Codigo, Descricao, Preço, Stock e o Tipo (Categoria)
                    string query = @"SELECT TOP 20 Codigo, Descricao, PVP_Unidade, Stock, Taxa_IVA, ID_Tipo 
                                     FROM Material 
                                     WHERE Codigo LIKE @Search OR Descricao LIKE @Search";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Search", "%" + termo + "%");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listaProdutos.Add(new ProdutoDTO
                                {
                                    Codigo = reader["Codigo"]?.ToString() ?? "",
                                    Descricao = reader["Descricao"]?.ToString() ?? "",
                                    Categoria = reader["ID_Tipo"] != DBNull.Value ? reader["ID_Tipo"]?.ToString() ?? "" : "Geral",

                                    // Tratamento seguro de números (previne erros se estiver vazio na BD)
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
                    // Se der erro (ex: servidor desligado), avisa
                    MessageBox.Show("Erro de conexão: " + ex.Message);
                }
            }

            // Se encontrou produtos, mostra a lista
            if (listaProdutos.Count > 0)
            {
                lstSugestoes.DataSource = listaProdutos;
                lstSugestoes.Visible = true;
                lstSugestoes.BringToFront(); // Força a lista a ficar por cima dos detalhes
            }
            else
            {
                lstSugestoes.Visible = false;
            }
        }

        // =========================================================
        // 2. EVENTO DE SELEÇÃO (Clicar na Lista)
        // =========================================================
        // Associa à ListBox (lstSugestoes) -> Evento SelectedIndexChanged
        private void lstSugestoes_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (lstSugestoes.SelectedItem == null) return;

            ProdutoDTO p = (ProdutoDTO)lstSugestoes.SelectedItem;

            // Preencher a Ficha de Detalhes (Certifica-te que os Labels têm estes nomes)
            lblCodigo.Text = p.Codigo;
            lblNome.Text = p.Descricao;
            lblCategoria.Text = p.Categoria;

            // Formatar Preço (Ex: 12,50 €)
            lblPreco.Text = p.Preco.ToString("C2");

            // Lógica Visual do Stock
            if (p.Stock > 0)
            {
                lblStock.Text = $"{p.Stock} Unidades (Disponível)";
                lblStock.ForeColor = Color.SeaGreen; // Verde
            }
            else
            {
                lblStock.Text = "Sem Stock";
                lblStock.ForeColor = Color.Crimson; // Vermelho
            }
        }

        // =========================================================
        // 3. BOTÕES E FINALIZAÇÃO
        // =========================================================

        // Botão Selecionar
        private void btnSelecionar_Click(object? sender, EventArgs e)
        {
            ConfirmarSelecao();
        }

        // Botão Cancelar
        private void btnCancelar_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Atalho: Duplo clique na lista confirma logo
        private void lstSugestoes_DoubleClick(object? sender, EventArgs e)
        {
            ConfirmarSelecao();
        }

        // Atalho: Enter na lista confirma logo
        private void lstSugestoes_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConfirmarSelecao();
                e.Handled = true; // Remove o som de "bip" do Windows
            }
        }

        private void ConfirmarSelecao()
        {
            // Proteção: Se o botão selecionar estiver invisível (Modo Gestão), 
            // impedir que o utilizador selecione com "Duplo Clique" ou "Enter".
            if (btnSelecionar.Visible == false) return;

            if (lstSugestoes.SelectedItem != null)
            {
                ProdutoSelecionado = (ProdutoDTO)lstSugestoes.SelectedItem;
                this.DialogResult = DialogResult.OK; // Informa que correu tudo bem
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor, selecione um produto da lista primeiro.");
            }
        }

        // O evento do botão Adicionar Produto
        private void btnAdicionarProduto_Click(object? sender, EventArgs e)
        {
            using (FormNovoProduto frmNovo = new FormNovoProduto())
            {
                // Se a pessoa gravou o produto com sucesso e a janela fechou com OK...
                if (frmNovo.ShowDialog() == DialogResult.OK)
                {
                    // LIMPAMOS A PESQUISA E RECARREGAMOS A LISTA PARA MOSTRAR O NOVO PRODUTO LOGO ALI!
                    string textoAtual = txtPesquisa.Text;
                    txtPesquisa.Text = "";
                    txtPesquisa.Text = textoAtual;

                    MessageBox.Show("A lista foi atualizada com o novo produto.");
                }
            }
        }

        // =========================================================
        // NOVO: EVENTO DO BOTÃO ELIMINAR PRODUTO
        // =========================================================
        private void btnEliminarProduto_Click(object? sender, EventArgs e)
        {
            // 1. Verificar se há algum produto selecionado na lista para eliminar
            if (lstSugestoes.SelectedItem == null)
            {
                MessageBox.Show("Por favor, pesquise e selecione um produto na lista para o eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ProdutoDTO produtoClicado = (ProdutoDTO)lstSugestoes.SelectedItem;
            string codigoAEliminar = produtoClicado.Codigo;

            // 2. Pedir confirmação dupla (segurança)
            DialogResult resposta = MessageBox.Show($"Tem a certeza absoluta que deseja eliminar o produto: {codigoAEliminar} - {produtoClicado.Descricao}?\n\nEsta ação não pode ser desfeita!", "Confirmar Eliminação", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

            if (resposta == DialogResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();

                        // 3. SEGURANÇA MÁXIMA: Verificar se o produto já foi vendido (está na Linha_Encomenda)
                        string queryVerificacao = "SELECT COUNT(*) FROM Linha_Encomenda WHERE Codigo_Material = @cod";
                        using (SqlCommand cmdVerifica = new SqlCommand(queryVerificacao, con))
                        {
                            cmdVerifica.Parameters.AddWithValue("@cod", codigoAEliminar);
                            int vendas = (int)(cmdVerifica.ExecuteScalar() ?? 0);

                            if (vendas > 0)
                            {
                                MessageBox.Show("Não é possível eliminar este produto porque ele já faz parte de faturas passadas. Se o apagasse, iria quebrar o histórico de vendas.", "Erro de Segurança", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        // 4. Se passou pela segurança, elimina o produto da tabela Material
                        string queryDelete = "DELETE FROM Material WHERE Codigo = @cod";
                        using (SqlCommand cmdDelete = new SqlCommand(queryDelete, con))
                        {
                            cmdDelete.Parameters.AddWithValue("@cod", codigoAEliminar);
                            cmdDelete.ExecuteNonQuery();
                        }

                        MessageBox.Show("Produto eliminado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // 5. Limpar os detalhes no ecrã e recarregar a pesquisa
                        lblCodigo.Text = "...";
                        lblNome.Text = "...";
                        lblCategoria.Text = "...";
                        lblPreco.Text = "...";
                        lblStock.Text = "...";

                        txtPesquisa.Text = ""; // Força a limpar a lista
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocorreu um erro ao tentar eliminar o produto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void DefinirPesquisa(string texto)
        {
            // Escreve na caixa de texto
            txtPesquisa.Text = texto;

            // Como tens o evento TextChanged, a pesquisa arranca sozinha!
            // Se quiseres garantir, podes chamar CarregarSugestoes(texto) aqui também.
        }

        public void PrepararModoVendas()
        {
            // Se vem da Fatura: Mostra Selecionar. Esconde botões de Gestão.
            btnSelecionar.Visible = true;

            // Verifica se os botões existem para não dar erro se mudares o design
            if (this.Controls.Find("btnAdicionarProduto", true).Length > 0)
                btnAdicionarProduto.Visible = false;

            if (this.Controls.Find("btnEliminarProduto", true).Length > 0)
                btnEliminarProduto.Visible = false;
        }

        public void PrepararModoGestao()
        {
            // Se vem do Menu: Esconde Selecionar. Mostra botões de Gestão.
            btnSelecionar.Visible = false;

            if (this.Controls.Find("btnAdicionarProduto", true).Length > 0)
                btnAdicionarProduto.Visible = true;

            if (this.Controls.Find("btnEliminarProduto", true).Length > 0)
                btnEliminarProduto.Visible = true;
        }

        private void lblStock_MouseClick(object? sender, MouseEventArgs e)
        {
            // Verifica se foi o botão DIREITO do rato
            if (e.Button == MouseButtons.Right)
            {
                // Verifica se há um produto selecionado na lista
                if (lstSugestoes.SelectedItem != null)
                {
                    ProdutoDTO produtoClicado = (ProdutoDTO)lstSugestoes.SelectedItem;

                    // Abre a nossa nova janela e passa-lhe o produto
                    using (FormAtualizarStock frmStock = new FormAtualizarStock(produtoClicado))
                    {
                        if (frmStock.ShowDialog() == DialogResult.OK)
                        {
                            // Se a atualização correu bem, forçamos a pesquisa a correr novamente
                            // para que a interface mostre logo o stock novo atualizado!
                            string textoAtual = txtPesquisa.Text;
                            txtPesquisa.Text = ""; // Apaga
                            txtPesquisa.Text = textoAtual; // Escreve outra vez para ativar a pesquisa
                        }
                    }
                }
            }
        }
    }

    // A classe de dados (DTO) atualizada com Categoria
    public class ProdutoDTO
    {
        public string Codigo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int Stock { get; set; }
        public decimal Iva { get; set; }

        public string DisplayText => $"{Codigo} - {Descricao}";

        // --- ADICIONA ISTO AQUI EM BAIXO ---
        public override string ToString()
        {
            return $"{Codigo} - {Descricao}";
        }
    }
}