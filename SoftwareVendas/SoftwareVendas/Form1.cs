using Microsoft.Data.SqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SoftwareVendas
{
    public partial class Form1 : Form
    {
        private ContextMenuStrip? menuGrelha;
        private readonly string connectionString = @"Server=DESKTOP-P0S20G1\SQLEXPRESS;Database=Software_Vendas_Pai;Trusted_Connection=True;TrustServerCertificate=True;";

        public Form1()
        {
            InitializeComponent();
            ConfigurarDesign();
        }

        #region Inicialização e Load

        private void Form1_Load(object? sender, EventArgs e)
        {
            dtpDataEncomenda.Value = DateTime.Now;

            CarregarAutoCompletar();
            CarregarClientesAutoCompletar();

            btnRemoverLinha.Click -= btnRemoverLinha_Click;
            btnRemoverLinha.Click += btnRemoverLinha_Click;

            ConfigurarMenuContexto();
        }

        #endregion

        #region Adição de Produtos à Tabela

        private void button1_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodigoProduto.Text))
            {
                MessageBox.Show("Por favor, introduza o código do produto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string query = "SELECT Descricao, PVP_Unidade, Taxa_IVA FROM Material WHERE Codigo = @cod";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@cod", txtCodigoProduto.Text.Trim());

                        using (SqlDataReader leitor = cmd.ExecuteReader())
                        {
                            if (leitor.Read())
                            {
                                string nome = leitor["Descricao"]?.ToString() ?? string.Empty;
                                decimal precoBase = Convert.ToDecimal(leitor["PVP_Unidade"]);
                                decimal taxaIVA = Convert.ToDecimal(leitor["Taxa_IVA"]);
                                int qtd = (int)numQuantidade.Value;
                                string txtDesc = txtDescontoProduto.Text?.Trim() ?? string.Empty;

                                decimal precoComDesconto = CalcularDescontoComposto(precoBase, txtDesc);
                                decimal totalLinha = precoComDesconto * qtd;

                                dgvItens.Rows.Add(
                                    txtCodigoProduto.Text.Trim(),
                                    qtd,
                                    nome,
                                    precoBase.ToString("C2"),
                                    txtDesc,
                                    totalLinha.ToString("C2")
                                );

                                AtualizarTotais();
                                LimparCamposProduto();
                            }
                            else
                            {
                                MessageBox.Show("Produto não encontrado na base de dados.", "Não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void LimparCamposProduto()
        {
            txtCodigoProduto.Clear();
            numQuantidade.Value = 1;
            txtDescontoProduto.Clear();
            txtCodigoProduto.Focus();
        }

        private void txtCodigoProduto_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        #endregion

        #region Cálculos e Totais

        private decimal CalcularDescontoComposto(decimal precoOriginal, string textoDesconto)
        {
            if (string.IsNullOrWhiteSpace(textoDesconto)) return precoOriginal;

            string textoLimpo = textoDesconto.Replace("%", "").Replace(" ", "");
            string[] descontos = textoLimpo.Split('+');

            decimal precoFinal = precoOriginal;

            foreach (string desc in descontos)
            {
                if (decimal.TryParse(desc, out decimal percentagem))
                {
                    precoFinal -= (precoFinal * (percentagem / 100));
                }
            }
            return precoFinal;
        }

        private void AtualizarTotais()
        {
            decimal totalIliquido = 0;

            foreach (DataGridViewRow linha in dgvItens.Rows)
            {
                if (linha.Cells["colTotal"]?.Value != null)
                {
                    string valorTexto = linha.Cells["colTotal"].Value.ToString()?.Replace("€", "").Trim() ?? "0";
                    if (decimal.TryParse(valorTexto, System.Globalization.NumberStyles.Currency, null, out decimal valorLinha))
                    {
                        totalIliquido += valorLinha;
                    }
                }
            }

            decimal.TryParse(txtDescontoFinal.Text, out decimal descFinal);

            totalIliquido -= (totalIliquido * (descFinal / 100));
            decimal totalIVA = totalIliquido * 0.23m; // Assumindo taxa fixa de 23% para simplificação global
            decimal totalPagar = totalIliquido + totalIVA;

            lblTotalIliquido.Text = totalIliquido.ToString("C2");
            lblTotalIVA.Text = totalIVA.ToString("C2");
            lblTotalPagar.Text = totalPagar.ToString("C2");
        }

        private void RecalcularLinha(DataGridViewRow row)
        {
            try
            {
                int qtd = 0;
                if (row.Cells[1].Value != null)
                    int.TryParse(row.Cells[1].Value.ToString(), out qtd);

                string textoPreco = row.Cells[3].Value?.ToString()?.Replace("€", "").Trim() ?? "0";
                decimal.TryParse(textoPreco, System.Globalization.NumberStyles.Currency, null, out decimal precoUnit);

                string textoDesc = row.Cells[4].Value?.ToString() ?? string.Empty;

                decimal precoComDesconto = CalcularDescontoComposto(precoUnit, textoDesc);
                decimal totalLinha = precoComDesconto * qtd;

                row.Cells[5].Value = totalLinha.ToString("C2");
            }
            catch
            {
                row.Cells[5].Value = "0,00 €";
            }
        }

        #endregion

        #region Finalizar Venda (Transação SQL)

        private void btnFinalizar_Click(object? sender, EventArgs e)
        {
            if (dgvItens.Rows.Count == 0)
            {
                MessageBox.Show("Não existem artigos na lista para finalizar a venda.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNIF.Text))
            {
                MessageBox.Show("É necessário identificar o cliente através do NIF.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string textoTotal = lblTotalPagar.Text?.Replace("€", "").Trim() ?? "0";
            decimal.TryParse(textoTotal, System.Globalization.NumberStyles.Currency, null, out decimal valorTotalVenda);
            decimal.TryParse(txtDescontoFinal.Text, out decimal descontoGlobal);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlTransaction transacao = con.BeginTransaction();

                try
                {
                    // Validar Cliente
                    string queryBuscaID = "SELECT ID_Cliente FROM Clientes WHERE NIF = @nif";
                    string idCliente = string.Empty;

                    using (SqlCommand cmdBusca = new SqlCommand(queryBuscaID, con, transacao))
                    {
                        cmdBusca.Parameters.AddWithValue("@nif", txtNIF.Text.Trim());
                        object? resultado = cmdBusca.ExecuteScalar();

                        if (resultado != null)
                        {
                            idCliente = resultado.ToString() ?? string.Empty;
                        }
                        else
                        {
                            transacao.Rollback();
                            MessageBox.Show("Cliente não encontrado na base de dados. Por favor, verifique o NIF inserido.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Registar Encomenda
                    string queryEnc = @"
                        INSERT INTO Encomenda (Data_Encomenda, Valor_Total, Estado, ID_Cliente, Desconto_Global) 
                        VALUES (@data, @total, 'Fechada', @idCliente, @descGlobal);
                        SELECT SCOPE_IDENTITY();";

                    int numEncomenda = 0;
                    using (SqlCommand cmdEnc = new SqlCommand(queryEnc, con, transacao))
                    {
                        cmdEnc.Parameters.AddWithValue("@data", dtpDataEncomenda.Value);
                        cmdEnc.Parameters.AddWithValue("@total", valorTotalVenda);
                        cmdEnc.Parameters.AddWithValue("@idCliente", idCliente);
                        cmdEnc.Parameters.AddWithValue("@descGlobal", descontoGlobal);

                        object? resEncomenda = cmdEnc.ExecuteScalar();
                        numEncomenda = resEncomenda != null ? Convert.ToInt32(resEncomenda) : 0;
                    }

                    // Processar Linhas e Abater Stock
                    int numeroLinha = 1;
                    foreach (DataGridViewRow row in dgvItens.Rows)
                    {
                        if (row.IsNewRow) continue;

                        string codigoArtigo = row.Cells[0].Value?.ToString() ?? string.Empty;
                        int.TryParse(row.Cells[1].Value?.ToString(), out int qtd);
                        string descricao = row.Cells[2].Value?.ToString() ?? string.Empty;

                        string precoTexto = row.Cells[3].Value?.ToString()?.Replace("€", "").Trim() ?? "0";
                        decimal.TryParse(precoTexto, System.Globalization.NumberStyles.Currency, null, out decimal precoUnit);

                        string textoDesconto = row.Cells[4].Value?.ToString() ?? string.Empty;

                        // Obter Taxa de IVA específica do produto
                        decimal taxaIvaLinha = 23.0m;
                        string queryIVA = "SELECT Taxa_IVA FROM Material WHERE Codigo = @cod";
                        using (SqlCommand cmdIVA = new SqlCommand(queryIVA, con, transacao))
                        {
                            cmdIVA.Parameters.AddWithValue("@cod", codigoArtigo);
                            object? resIVA = cmdIVA.ExecuteScalar();
                            if (resIVA != null) taxaIvaLinha = Convert.ToDecimal(resIVA);
                        }

                        // Registar a linha
                        string queryLinha = @"
                            INSERT INTO Linha_Encomenda 
                            (NE, Linha_Encomenda, Quantidade, Descricao, Preco, Desconto, Imposto, Codigo_Material, Desconto_Texto)
                            VALUES 
                            (@ne, @numLinha, @qtd, @desc, @preco, 0, @iva, @codMat, @txtDesc)";

                        using (SqlCommand cmdLinha = new SqlCommand(queryLinha, con, transacao))
                        {
                            cmdLinha.Parameters.AddWithValue("@ne", numEncomenda);
                            cmdLinha.Parameters.AddWithValue("@numLinha", numeroLinha);
                            cmdLinha.Parameters.AddWithValue("@qtd", qtd);
                            cmdLinha.Parameters.AddWithValue("@desc", descricao);
                            cmdLinha.Parameters.AddWithValue("@preco", precoUnit);
                            cmdLinha.Parameters.AddWithValue("@iva", taxaIvaLinha);
                            cmdLinha.Parameters.AddWithValue("@codMat", codigoArtigo);
                            cmdLinha.Parameters.AddWithValue("@txtDesc", textoDesconto);

                            cmdLinha.ExecuteNonQuery();
                        }

                        // Atualizar Inventário
                        string queryStock = "UPDATE Material SET Stock = Stock - @qtdAbater WHERE Codigo = @codArtigo";
                        using (SqlCommand cmdStock = new SqlCommand(queryStock, con, transacao))
                        {
                            cmdStock.Parameters.AddWithValue("@qtdAbater", qtd);
                            cmdStock.Parameters.AddWithValue("@codArtigo", codigoArtigo);
                            cmdStock.ExecuteNonQuery();
                        }

                        numeroLinha++;
                    }

                    // Commit da Transação
                    transacao.Commit();
                    MessageBox.Show($"Venda n.º {numEncomenda} registada e inventário atualizado com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LimparVenda();
                }
                catch (Exception ex)
                {
                    transacao.Rollback();
                    MessageBox.Show($"Não foi possível concluir a transação.\nTodas as alterações foram revertidas.\nDetalhe: {ex.Message}", "Erro Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LimparVenda()
        {
            dgvItens.Rows.Clear();
            txtNIF.Clear();
            txtNomeCliente.Clear();
            txtDescontoFinal.Clear();

            lblTotalIliquido.Text = "0,00 €";
            lblTotalIVA.Text = "0,00 €";
            lblTotalPagar.Text = "0,00 €";

            txtNIF.Focus();
        }

        #endregion

        #region Autocomplete e Gestão de Clientes

        private void CarregarAutoCompletar()
        {
            AutoCompleteStringCollection listaCodigos = new AutoCompleteStringCollection();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT Codigo FROM Material";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader leitor = cmd.ExecuteReader())
                        {
                            while (leitor.Read())
                            {
                                string codigoLido = leitor["Codigo"]?.ToString() ?? string.Empty;
                                if (!string.IsNullOrWhiteSpace(codigoLido))
                                    listaCodigos.Add(codigoLido);
                            }
                        }
                    }
                }
                catch { /* Continua a execução sem autocompletar em caso de falha de ligação não crítica */ }
            }

            txtCodigoProduto.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtCodigoProduto.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtCodigoProduto.AutoCompleteCustomSource = listaCodigos;
        }

        private void CarregarClientesAutoCompletar()
        {
            AutoCompleteStringCollection listaNIFs = new AutoCompleteStringCollection();
            AutoCompleteStringCollection listaNomes = new AutoCompleteStringCollection();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT NIF, Nome_Cliente FROM Clientes";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader leitor = cmd.ExecuteReader())
                        {
                            while (leitor.Read())
                            {
                                string nif = leitor["NIF"]?.ToString() ?? string.Empty;
                                string nome = leitor["Nome_Cliente"]?.ToString() ?? string.Empty;

                                if (!string.IsNullOrWhiteSpace(nif)) listaNIFs.Add(nif);
                                if (!string.IsNullOrWhiteSpace(nome)) listaNomes.Add(nome);
                            }
                        }
                    }
                }
                catch { /* Falha silenciosa permitida para o Autocomplete */ }
            }

            txtNIF.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtNIF.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtNIF.AutoCompleteCustomSource = listaNIFs;

            txtNomeCliente.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtNomeCliente.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtNomeCliente.AutoCompleteCustomSource = listaNomes;
        }

        private void txtNIF_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(txtNIF.Text))
                {
                    BuscarCliente(txtNIF.Text.Trim(), ehNIF: true);
                    e.SuppressKeyPress = true;
                    txtNomeCliente.Focus();
                }
            }
        }

        private void txtNomeCliente_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(txtNomeCliente.Text))
                {
                    BuscarCliente(txtNomeCliente.Text.Trim(), ehNIF: false);
                    e.SuppressKeyPress = true;
                    txtCodigoProduto.Focus();
                }
            }
        }

        private void BuscarCliente(string termoPesquisa, bool ehNIF)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = ehNIF ? "SELECT * FROM Clientes WHERE NIF = @termo" : "SELECT * FROM Clientes WHERE Nome_Cliente = @termo";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@termo", termoPesquisa);

                        using (SqlDataReader leitor = cmd.ExecuteReader())
                        {
                            if (leitor.Read())
                            {
                                txtNIF.Text = leitor["NIF"].ToString();
                                txtNomeCliente.Text = leitor["Nome_Cliente"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Falha na pesquisa de cliente: {ex.Message}", "Erro de Leitura", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Eventos da Grelha (DataGridView) e Menu de Contexto

        private void dgvItens_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvItens.Rows[e.RowIndex];

            // Atualização de Produto mediante alteração manual de Código
            if (e.ColumnIndex == 0)
            {
                string novoCodigo = row.Cells[0].Value?.ToString() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(novoCodigo)) return;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        string query = "SELECT Descricao, PVP_Unidade FROM Material WHERE Codigo = @cod";

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@cod", novoCodigo);

                            using (SqlDataReader leitor = cmd.ExecuteReader())
                            {
                                if (leitor.Read())
                                {
                                    row.Cells[2].Value = leitor["Descricao"].ToString();
                                    row.Cells[3].Value = Convert.ToDecimal(leitor["PVP_Unidade"]).ToString("C2");

                                    if (row.Cells[1].Value == null || row.Cells[1].Value.ToString() == "0")
                                    {
                                        row.Cells[1].Value = 1;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("O código inserido não existe no inventário.", "Código Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    row.Cells[0].Value = string.Empty;
                                    return;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ocorreu um erro ao validar o código da grelha: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            // Recalcular totais perante alterações na linha
            if (e.ColumnIndex == 0 || e.ColumnIndex == 1 || e.ColumnIndex == 4)
            {
                RecalcularLinha(row);
                AtualizarTotais();
            }
        }

        private void dgvItens_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && dgvItens.SelectedRows.Count > 0)
            {
                DialogResult resposta = MessageBox.Show("Confirma a remoção dos artigos selecionados?", "Remover Artigo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resposta == DialogResult.Yes)
                {
                    // Evitar modificação da coleção durante a iteração
                    for (int i = dgvItens.SelectedRows.Count - 1; i >= 0; i--)
                    {
                        DataGridViewRow row = dgvItens.SelectedRows[i];
                        if (!row.IsNewRow)
                        {
                            dgvItens.Rows.Remove(row);
                        }
                    }
                    AtualizarTotais();
                }
            }
        }

        private void btnRemoverLinha_Click(object? sender, EventArgs e)
        {
            if (dgvItens.CurrentRow != null && !dgvItens.CurrentRow.IsNewRow)
            {
                dgvItens.Rows.Remove(dgvItens.CurrentRow);
                AtualizarTotais();
                txtCodigoProduto.Focus();
            }
            else
            {
                MessageBox.Show("Selecione uma linha válida para remover.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvItens_EditingControlShowing(object? sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvItens.CurrentCell.ColumnIndex == 0)
            {
                if (e.Control is TextBox txt)
                {
                    txt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    txt.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    txt.AutoCompleteCustomSource = txtCodigoProduto.AutoCompleteCustomSource;
                }
            }
            else
            {
                if (e.Control is TextBox txt)
                {
                    txt.AutoCompleteMode = AutoCompleteMode.None;
                }
            }
        }

        // --- INTEGRAÇÃO COM FORMULÁRIO DE PRODUTOS ---
        private void ConfigurarMenuContexto()
        {
            menuGrelha = new ContextMenuStrip();
            ToolStripMenuItem itemStock = new ToolStripMenuItem("Ver Stock / Alterar Artigo");
            itemStock.Click += ItemVerStock_Click;

            menuGrelha.Items.Add(itemStock);
            dgvItens.ContextMenuStrip = menuGrelha;
            dgvItens.CellMouseDown += dgvItens_CellMouseDown;
        }

        private void dgvItens_CellMouseDown(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dgvItens.CurrentCell = dgvItens.Rows[e.RowIndex].Cells[e.ColumnIndex];
                dgvItens.Rows[e.RowIndex].Selected = true;
            }
        }

        private void ItemVerStock_Click(object? sender, EventArgs e)
        {
            if (dgvItens.CurrentRow != null && !dgvItens.CurrentRow.IsNewRow)
            {
                string codigoProduto = dgvItens.CurrentRow.Cells[0].Value?.ToString() ?? string.Empty;

                if (!string.IsNullOrEmpty(codigoProduto))
                {
                    using (FormProdutos frmProdutos = new FormProdutos())
                    {
                        frmProdutos.PrepararModoVendas();
                        frmProdutos.DefinirPesquisa(codigoProduto);

                        if (frmProdutos.ShowDialog() == DialogResult.OK && frmProdutos.ProdutoSelecionado != null)
                        {
                            DataGridViewRow linha = dgvItens.CurrentRow;

                            linha.Cells[0].Value = frmProdutos.ProdutoSelecionado.Codigo;
                            linha.Cells[2].Value = frmProdutos.ProdutoSelecionado.Descricao;
                            linha.Cells[3].Value = frmProdutos.ProdutoSelecionado.Preco.ToString("C2");
                            linha.Cells[4].Value = string.Empty;

                            if (linha.Cells[1].Value == null || linha.Cells[1].Value.ToString() == "0")
                            {
                                linha.Cells[1].Value = 1;
                            }

                            RecalcularLinha(linha);
                            AtualizarTotais();
                        }
                    }
                }
            }
        }

        #endregion

        #region

        private void ConfigurarDesign()
        {
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            dgvItens.BackgroundColor = Color.White;
            dgvItens.BorderStyle = BorderStyle.None;
            dgvItens.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvItens.EnableHeadersVisualStyles = false;

            dgvItens.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dgvItens.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvItens.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvItens.ColumnHeadersDefaultCellStyle.Padding = new Padding(10);
            dgvItens.ColumnHeadersHeight = 50;

            dgvItens.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgvItens.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvItens.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvItens.DefaultCellStyle.Padding = new Padding(5);
            dgvItens.RowTemplate.Height = 45;
            dgvItens.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvItens.RowHeadersVisible = false;

            EstilizarBotao(button1, Color.FromArgb(39, 174, 96));

            if (this.Controls.Find("btnFinalizar", true).Length > 0)
                EstilizarBotao(btnFinalizar, Color.FromArgb(41, 128, 185));

            if (this.Controls.Find("btnRemoverLinha", true).Length > 0)
                EstilizarBotao(btnRemoverLinha, Color.FromArgb(231, 76, 60));

            foreach (Control c in this.Controls)
            {
                if (c is TextBox txt && txt.Name != "txtDescontoFinal")
                {
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }

                if (c is GroupBox gb)
                {
                    foreach (Control child in gb.Controls)
                    {
                        if (child is TextBox txtChild)
                        {
                            txtChild.BorderStyle = BorderStyle.FixedSingle;
                        }
                    }
                }
            }
        }

        private void EstilizarBotao(Button btn, Color corFundo)
        {
            if (btn == null) return;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = corFundo;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
        }

        // Handles não utilizados deixados vazios por dependência do designer
        private void groupBox2_Enter(object? sender, EventArgs e) { }
        private void dgvItens_CellContentClick(object? sender, DataGridViewCellEventArgs e) { }
        private void btnRemoverLinha_Click_1(object? sender, EventArgs e) { }

        #endregion
    }
}