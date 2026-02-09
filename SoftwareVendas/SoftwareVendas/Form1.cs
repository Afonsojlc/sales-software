// Mudei para a biblioteca nova da Microsoft, que é mais rápida e segura
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SoftwareVendas
{
    public partial class Form1 : Form
    {
        // Variável para o menu de clique direito
        private ContextMenuStrip menuGrelha;

        // A chave para entrar na base de dados.
        // NOTA: Confirma se o nome "DESKTOP-P0S2OG1" é mesmo o do teu PC atual!
        // O "TrustServerCertificate=True" evita erros de segurança chatos.
        string connectionString = @"Server=DESKTOP-P0S20G1\SQLEXPRESS;Database=Software_Vendas_Pai;Trusted_Connection=True;TrustServerCertificate=True;";

        public Form1()
        {
            InitializeComponent();
            ConfigurarDesign();
        }

        // --- QUANDO O PROGRAMA ARRANCA ---
        // CORREÇÃO: Adicionado '?' no sender
        private void Form1_Load(object? sender, EventArgs e)
        {
            // Meter a data de hoje automaticamente
            dtpDataEncomenda.Value = DateTime.Now;

            // Preparar a lista de sugestões de códigos
            CarregarAutoCompletar();

            // Carregar sugestões de Clientes
            CarregarClientesAutoCompletar();

            btnRemoverLinha.Click -= btnRemoverLinha_Click; // Remove para não duplicar
            btnRemoverLinha.Click += btnRemoverLinha_Click;

            // Adiciona isto dentro do Form1_Load:
            ConfigurarMenuContexto();
        }

        // --- BOTÃO ADICIONAR (O que estava a encravar) ---
        // CORREÇÃO: Adicionado '?' no sender
        private void button1_Click(object? sender, EventArgs e)
        {
            // Se não escreveu nada, manda aviso
            if (txtCodigoProduto.Text == "")
            {
                MessageBox.Show("Falta escrever o código do produto!");
                return;
            }

            // Tenta ligar à base de dados
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open(); // Abre a porta

                    // Vai procurar o produto pelo código
                    string query = "SELECT Descricao, PVP_Unidade, Taxa_IVA FROM Material WHERE Codigo = @cod";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@cod", txtCodigoProduto.Text);

                    // Executa a busca
                    SqlDataReader leitor = cmd.ExecuteReader();

                    if (leitor.Read()) // Se encontrou alguma coisa
                    {
                        // Buscar os dados da BD
                        string nome = leitor["Descricao"]?.ToString() ?? "";
                        decimal precoBase = Convert.ToDecimal(leitor["PVP_Unidade"]);
                        decimal taxaIVA = Convert.ToDecimal(leitor["Taxa_IVA"]);

                        int qtd = (int)numQuantidade.Value;

                        // Ver se há descontos tipo "50+10"
                        string txtDesc = txtDescontoProduto.Text ?? "";
                        decimal precoComDesconto = CalcularDescontoComposto(precoBase, txtDesc);

                        // Conta final da linha
                        decimal totalLinha = precoComDesconto * qtd;

                        // Meter tudo na tabela (Grelha)
                        dgvItens.Rows.Add(
                            txtCodigoProduto.Text,
                            qtd,
                            nome,
                            precoBase.ToString("C2"), // Mete o símbolo do Euro
                            txtDesc,
                            totalLinha.ToString("C2")
                        );

                        // Atualiza os totais lá em baixo
                        AtualizarTotais();

                        // Limpar as caixas para o próximo
                        txtCodigoProduto.Clear();
                        numQuantidade.Value = 1;
                        txtDescontoProduto.Clear();
                        txtCodigoProduto.Focus(); // Põe o cursor pronto a escrever
                    }
                    else
                    {
                        MessageBox.Show("Não encontrei esse produto!");
                    }
                }
                catch (Exception ex)
                {
                    // Se der erro, mostra qual foi (ajuda a saber porque encravou)
                    MessageBox.Show("Erro na base de dados: " + ex.Message);
                }
            }
        }

        // --- FUNÇÃO PARA AS SUGESTÕES DE CÓDIGO ---
        private void CarregarAutoCompletar()
        {
            AutoCompleteStringCollection listaCodigos = new AutoCompleteStringCollection();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT Codigo FROM Material";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader leitor = cmd.ExecuteReader();

                    while (leitor.Read())
                    {
                        string codigoLido = leitor["Codigo"]?.ToString() ?? "";
                        if (codigoLido != "")
                            listaCodigos.Add(codigoLido);
                    }
                }
                catch
                {
                    // Se der erro aqui, ignora, só fica sem sugestões
                }
            }

            // Configurar a caixa de texto para sugerir
            txtCodigoProduto.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtCodigoProduto.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtCodigoProduto.AutoCompleteCustomSource = listaCodigos;
        }

        // --- TECLA ENTER (Para não usar o rato) ---
        // CORREÇÃO: Adicionado '?' no sender
        private void txtCodigoProduto_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Simula o clique no botão adicionar
                // IMPORTANTE: Se o teu botão tiver outro nome (ex: btnAdicionar), muda aqui!
                button1.PerformClick();

                e.SuppressKeyPress = true; // Tira o barulho "ding"
            }
        }

        // --- MATEMÁTICA DOS DESCONTOS (50+10) ---
        private decimal CalcularDescontoComposto(decimal precoOriginal, string textoDesconto)
        {
            // Se estiver vazio, devolve o preço normal
            if (string.IsNullOrWhiteSpace(textoDesconto)) return precoOriginal;

            // 1. LIMPEZA TOTAL: Tira espaços e tira o símbolo '%'
            string textoLimpo = textoDesconto.Replace("%", "").Replace(" ", "");

            // 2. Parte o texto pelos sinais de '+'
            string[] descontos = textoLimpo.Split('+');

            decimal precoFinal = precoOriginal;

            foreach (string desc in descontos)
            {
                // Tenta ler o número (agora já sem lixo)
                if (decimal.TryParse(desc, out decimal percentagem))
                {
                    // Aplica o desconto sobre o valor que vem de trás (Desconto Composto)
                    precoFinal = precoFinal - (precoFinal * (percentagem / 100));
                }
            }
            return precoFinal;
        }

        // --- SOMAR TUDO NO RODAPÉ ---
        private void AtualizarTotais()
        {
            decimal totalIliquido = 0;
            decimal totalIVA = 0;

            // Varrer as linhas todas da tabela
            foreach (DataGridViewRow linha in dgvItens.Rows)
            {
                // O nome "colTotal" tem de ser igual ao que puseste nas propriedades da tabela!
                // Se não for, troca por linha.Cells[5] (ou o número da coluna)
                if (linha.Cells["colTotal"].Value != null)
                {
                    string valorTexto = linha.Cells["colTotal"].Value?.ToString()?.Replace("€", "").Trim() ?? "0";
                    if (decimal.TryParse(valorTexto, System.Globalization.NumberStyles.Currency, null, out decimal valorLinha))
                    {
                        totalIliquido += valorLinha;
                    }
                }
            }

            // Ver se há desconto final na fatura
            decimal descFinal = 0;
            decimal.TryParse(txtDescontoFinal.Text, out descFinal);

            // Contas finais
            totalIliquido = totalIliquido - (totalIliquido * (descFinal / 100));
            totalIVA = totalIliquido * 0.23m; // IVA a 23%
            decimal totalPagar = totalIliquido + totalIVA;

            // Mostrar nas etiquetas
            lblTotalIliquido.Text = totalIliquido.ToString("C2");
            lblTotalIVA.Text = totalIVA.ToString("C2");
            lblTotalPagar.Text = totalPagar.ToString("C2");
        }

        // CORREÇÃO: Adicionado '?' no sender
        private void groupBox2_Enter(object? sender, EventArgs e)
        {
        }

        // CORREÇÃO: Adicionado '?' no sender
        private void txtNIF_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Pesquisar usando o que está escrito no NIF
                if (!string.IsNullOrWhiteSpace(txtNIF.Text))
                {
                    BuscarCliente(txtNIF.Text, true); // true = é pesquisa por NIF
                    e.SuppressKeyPress = true; // Parar o "ding" do Windows

                    // Opcional: Passar o foco para o próximo campo
                    txtNomeCliente.Focus();
                }
            }
        }

        // CORREÇÃO: Adicionado '?' no sender
        private void txtNomeCliente_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Pesquisar usando o que está escrito no NOME
                if (!string.IsNullOrWhiteSpace(txtNomeCliente.Text))
                {
                    BuscarCliente(txtNomeCliente.Text, false); // false = é pesquisa por Nome
                    e.SuppressKeyPress = true;

                    // Opcional: Passar o foco para o código do produto
                    txtCodigoProduto.Focus();
                }
            }
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
                    // Vamos buscar o NIF e o Nome para encher as listas
                    string query = "SELECT NIF, Nome_Cliente FROM Clientes";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader leitor = cmd.ExecuteReader();

                    while (leitor.Read())
                    {
                        string nif = leitor["NIF"]?.ToString() ?? "";
                        string nome = leitor["Nome_Cliente"]?.ToString() ?? "";

                        if (!string.IsNullOrEmpty(nif)) listaNIFs.Add(nif);
                        if (!string.IsNullOrEmpty(nome)) listaNomes.Add(nome);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar clientes: " + ex.Message);
                }
            }

            // Configurar o Autocomplete do NIF
            txtNIF.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtNIF.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtNIF.AutoCompleteCustomSource = listaNIFs;

            // Configurar o Autocomplete do Nome
            txtNomeCliente.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtNomeCliente.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtNomeCliente.AutoCompleteCustomSource = listaNomes;
        }

        private void BuscarCliente(string termoPesquisa, bool ehNIF)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "";

                    // Decide a query baseada no tipo de pesquisa
                    if (ehNIF)
                    {
                        query = "SELECT * FROM Clientes WHERE NIF = @termo";
                    }
                    else
                    {
                        query = "SELECT * FROM Clientes WHERE Nome_Cliente = @termo";
                    }

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@termo", termoPesquisa);

                    SqlDataReader leitor = cmd.ExecuteReader();

                    if (leitor.Read())
                    {
                        // Encontrou! Vamos preencher os campos contrários.
                        // Se pesquisei NIF, preenche o Nome. Se pesquisei Nome, preenche NIF.
                        txtNIF.Text = leitor["NIF"].ToString();
                        txtNomeCliente.Text = leitor["Nome_Cliente"].ToString();

                        // Podes adicionar aqui outros campos se tiveres na interface (ex: Morada)
                        // txtMorada.Text = leitor["Morada_Completa"].ToString();
                    }
                    else
                    {
                        // Opcional: Avisar se não encontrar, ou limpar os campos
                        // MessageBox.Show("Cliente não encontrado.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao buscar cliente: " + ex.Message);
                }
            }
        }

        // CORREÇÃO: Adicionado '?' no sender
        private void btnFinalizar_Click(object? sender, EventArgs e)
        {
            // 1. VALIDAÇÕES BÁSICAS
            if (dgvItens.Rows.Count == 0)
            {
                MessageBox.Show("Não há artigos na lista para vender!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNIF.Text))
            {
                MessageBox.Show("Por favor, identifique o cliente (NIF).");
                return;
            }

            // 2. PREPARAR DADOS PARA A BASE DE DADOS
            decimal valorTotalVenda = 0;
            // Proteção: Garante que o texto não é nulo com "??"
            string textoTotal = lblTotalPagar.Text?.Replace("€", "").Trim() ?? "0";
            decimal.TryParse(textoTotal, System.Globalization.NumberStyles.Currency, null, out valorTotalVenda);

            decimal descontoGlobal = 0;
            decimal.TryParse(txtDescontoFinal.Text, out descontoGlobal);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlTransaction transacao = con.BeginTransaction(); // INÍCIO DA SEGURANÇA

                try
                {
                    // A. OBTER O ID DO CLIENTE PELO NIF
                    string idCliente = "";
                    string queryBuscaID = "SELECT ID_Cliente FROM Clientes WHERE NIF = @nif";
                    SqlCommand cmdBusca = new SqlCommand(queryBuscaID, con, transacao);
                    cmdBusca.Parameters.AddWithValue("@nif", txtNIF.Text);

                    // CORREÇÃO ERRO 1 (Linha ~382): O resultado pode ser nulo
                    object? resultado = cmdBusca.ExecuteScalar();
                    if (resultado != null)
                    {
                        idCliente = resultado.ToString() ?? "";
                    }
                    else
                    {
                        transacao.Rollback();
                        MessageBox.Show("Cliente não encontrado na base de dados. Verifique o NIF.");
                        return;
                    }

                    // B. INSERIR A ENCOMENDA (Cabeçalho)
                    string queryEnc = @"
                        INSERT INTO Encomenda (Data_Encomenda, Valor_Total, Estado, ID_Cliente, Desconto_Global) 
                        VALUES (@data, @total, 'Fechada', @idCliente, @descGlobal);
                        SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdEnc = new SqlCommand(queryEnc, con, transacao);
                    cmdEnc.Parameters.AddWithValue("@data", dtpDataEncomenda.Value);
                    cmdEnc.Parameters.AddWithValue("@total", valorTotalVenda);
                    cmdEnc.Parameters.AddWithValue("@idCliente", idCliente);
                    cmdEnc.Parameters.AddWithValue("@descGlobal", descontoGlobal);

                    // Executa e guarda o número da encomenda
                    // Proteção extra: Converter o resultado de forma segura
                    object? resEncomenda = cmdEnc.ExecuteScalar();
                    int numEncomenda = (resEncomenda != null) ? Convert.ToInt32(resEncomenda) : 0;

                    // C. INSERIR AS LINHAS (Produtos)
                    int numeroLinha = 1;

                    foreach (DataGridViewRow row in dgvItens.Rows)
                    {
                        if (row.IsNewRow) continue;

                        // CORREÇÃO ERRO 2 (Linha ~415): Usar "?." e "?? """ para evitar nulos
                        string codigoArtigo = row.Cells[0].Value?.ToString() ?? "";

                        // Conversão segura da quantidade
                        int qtd = 0;
                        if (row.Cells[1].Value != null)
                            int.TryParse(row.Cells[1].Value.ToString(), out qtd);

                        // CORREÇÃO ERRO 3 (Linha ~417): Descrição segura
                        string descricao = row.Cells[2].Value?.ToString() ?? "";

                        // CORREÇÃO ERRO 4 (Linha ~420): Preço Unitário
                        // Primeiro garantimos que temos uma string válida, se for nulo assume "0"
                        string precoTexto = row.Cells[3].Value?.ToString()?.Replace("€", "").Trim() ?? "0";
                        decimal.TryParse(precoTexto, System.Globalization.NumberStyles.Currency, null, out decimal precoUnit);

                        string textoDesconto = row.Cells[4].Value?.ToString() ?? "";

                        // Buscar Taxa IVA
                        decimal taxaIvaLinha = 23.0m;
                        string queryIVA = "SELECT Taxa_IVA FROM Material WHERE Codigo = @cod";
                        SqlCommand cmdIVA = new SqlCommand(queryIVA, con, transacao);
                        cmdIVA.Parameters.AddWithValue("@cod", codigoArtigo);
                        object? resIVA = cmdIVA.ExecuteScalar(); // Pode ser nulo
                        if (resIVA != null) taxaIvaLinha = Convert.ToDecimal(resIVA);

                        // Query da Linha
                        string queryLinha = @"
                            INSERT INTO Linha_Encomenda 
                            (NE, Linha_Encomenda, Quantidade, Descricao, Preco, Desconto, Imposto, Codigo_Material, Desconto_Texto)
                            VALUES 
                            (@ne, @numLinha, @qtd, @desc, @preco, 0, @iva, @codMat, @txtDesc)";

                        SqlCommand cmdLinha = new SqlCommand(queryLinha, con, transacao);
                        cmdLinha.Parameters.AddWithValue("@ne", numEncomenda);
                        cmdLinha.Parameters.AddWithValue("@numLinha", numeroLinha);
                        cmdLinha.Parameters.AddWithValue("@qtd", qtd);
                        cmdLinha.Parameters.AddWithValue("@desc", descricao);
                        cmdLinha.Parameters.AddWithValue("@preco", precoUnit);
                        cmdLinha.Parameters.AddWithValue("@iva", taxaIvaLinha);
                        cmdLinha.Parameters.AddWithValue("@codMat", codigoArtigo);
                        cmdLinha.Parameters.AddWithValue("@txtDesc", textoDesconto);

                        cmdLinha.ExecuteNonQuery();

                        string queryStock = "UPDATE Material SET Stock = Stock - @qtdAbater WHERE Codigo = @codArtigo";
                        SqlCommand cmdStock = new SqlCommand(queryStock, con, transacao);
                        cmdStock.Parameters.AddWithValue("@qtdAbater", qtd);
                        cmdStock.Parameters.AddWithValue("@codArtigo", codigoArtigo);
                        cmdStock.ExecuteNonQuery();

                        numeroLinha++;
                    }

                    // D. CONFIRMAR TUDO
                    transacao.Commit();
                    MessageBox.Show($"Venda nº {numEncomenda} gravada com sucesso!");

                    // E. LIMPAR
                    LimparVenda();
                }
                catch (Exception ex)
                {
                    transacao.Rollback();
                    MessageBox.Show("Erro ao gravar venda: " + ex.Message);
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

            // Repor foco no início
            txtNIF.Focus();
        }

        // CORREÇÃO: Adicionado '?' no sender
        private void dgvItens_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
        }

        // CORREÇÃO: Adicionado '?' no sender
        private void dgvItens_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            // Ignora se for o cabeçalho
            if (e.RowIndex < 0) return;

            // Pega na linha atual que está a ser mexida
            DataGridViewRow row = dgvItens.Rows[e.RowIndex];

            // --- CENÁRIO 1: MUDASTE O CÓDIGO DO PRODUTO (Coluna 0) ---
            if (e.ColumnIndex == 0)
            {
                string novoCodigo = row.Cells[0].Value?.ToString() ?? "";

                if (string.IsNullOrWhiteSpace(novoCodigo)) return;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        string query = "SELECT Descricao, PVP_Unidade, Taxa_IVA FROM Material WHERE Codigo = @cod";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@cod", novoCodigo);

                        SqlDataReader leitor = cmd.ExecuteReader();

                        if (leitor.Read())
                        {
                            // Atualiza a Descrição (Coluna 2) e o Preço (Coluna 3)
                            row.Cells[2].Value = leitor["Descricao"].ToString();
                            decimal precoBase = Convert.ToDecimal(leitor["PVP_Unidade"]);
                            row.Cells[3].Value = precoBase.ToString("C2");

                            // Define quantidade como 1 se estiver a zero ou vazia
                            if (row.Cells[1].Value == null || row.Cells[1].Value.ToString() == "0")
                            {
                                row.Cells[1].Value = 1;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Código não encontrado na base de dados.");
                            // Podes optar por limpar a linha ou deixar o user corrigir
                            row.Cells[0].Value = "";
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao atualizar linha: " + ex.Message);
                    }
                }
            }

            // --- CENÁRIO 2: MUDASTE QUANTIDADE (Col 1), CÓDIGO (Col 0) OU DESCONTO (Col 4) ---
            // Sempre que mexes num destes, temos de recalcular o total da linha
            if (e.ColumnIndex == 0 || e.ColumnIndex == 1 || e.ColumnIndex == 4)
            {
                RecalcularLinha(row);
                AtualizarTotais(); // Atualiza o rodapé (IVA, Total Final)
            }
        }

        // --- FUNÇÃO AJUDANTE PARA RECALCULAR UMA LINHA SOZINHA ---
        private void RecalcularLinha(DataGridViewRow row)
        {
            try
            {
                // 1. Obter Quantidade (Segurança contra nulos)
                int qtd = 0;
                if (row.Cells[1].Value != null)
                    int.TryParse(row.Cells[1].Value.ToString(), out qtd);

                // 2. Obter Preço Unitário (Limpar o simbolo €)
                decimal precoUnit = 0;
                string textoPreco = row.Cells[3].Value?.ToString()?.Replace("€", "").Trim() ?? "0";
                decimal.TryParse(textoPreco, System.Globalization.NumberStyles.Currency, null, out precoUnit);

                // 3. Obter Desconto
                string textoDesc = row.Cells[4].Value?.ToString() ?? "";

                // 4. Fazer a conta
                decimal precoComDesconto = CalcularDescontoComposto(precoUnit, textoDesc);
                decimal totalLinha = precoComDesconto * qtd;

                // 5. Escrever o novo total na célula (Coluna 5)
                row.Cells[5].Value = totalLinha.ToString("C2");
            }
            catch
            {
                // Se der erro (ex: escrever letras na quantidade), metemos o total a zero
                row.Cells[5].Value = "0,00 €";
            }
        }

        // --- APAGAR LINHA COM A TECLA DELETE ---
        // CORREÇÃO: Adicionado '?' no sender
        private void dgvItens_KeyDown(object? sender, KeyEventArgs e)
        {
            // Verifica se a tecla pressionada foi o DELETE
            if (e.KeyCode == Keys.Delete)
            {
                // Verifica se há linhas selecionadas
                if (dgvItens.SelectedRows.Count > 0)
                {
                    // Pergunta de segurança (Opcional, mas recomendado)
                    if (MessageBox.Show("Tem a certeza que quer remover este artigo?", "Remover", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow row in dgvItens.SelectedRows)
                        {
                            // Não deixa apagar a linha nova em branco (a última)
                            if (!row.IsNewRow)
                            {
                                dgvItens.Rows.Remove(row);
                            }
                        }

                        // IMPORTANTE: Recalcular os totais depois de apagar!
                        AtualizarTotais();
                    }
                }
            }
        }

        // CORREÇÃO: Adicionado '?' no sender
        private void btnRemoverLinha_Click(object? sender, EventArgs e)
        {
            // Verifica se há alguma linha ou célula selecionada
            if (dgvItens.CurrentRow != null && !dgvItens.CurrentRow.IsNewRow)
            {
                // Remove a linha onde está o cursor
                dgvItens.Rows.Remove(dgvItens.CurrentRow);

                // Recalcula tudo
                AtualizarTotais();

                // Devolve o foco ao código para continuares a vender
                txtCodigoProduto.Focus();
            }
            else
            {
                MessageBox.Show("Selecione uma linha para remover.");
            }
        }

        // CORREÇÃO: Adicionado '?' no sender
        private void dgvItens_EditingControlShowing(object? sender, DataGridViewEditingControlShowingEventArgs e)
        {// Verifica se estamos na coluna do Código (Índice 0)
            if (dgvItens.CurrentCell.ColumnIndex == 0)
            {
                // --- A CORREÇÃO ESTÁ AQUI ---
                // Em vez de "as TextBox", usamos "is TextBox txt".
                // Isto verifica se é uma TextBox e cria a variável "txt" de forma segura num só passo.
                if (e.Control is TextBox txt)
                {
                    txt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    txt.AutoCompleteSource = AutoCompleteSource.CustomSource;

                    // Usa a lista que já tens carregada na caixa de texto de cima
                    txt.AutoCompleteCustomSource = txtCodigoProduto.AutoCompleteCustomSource;
                }
            }
            else
            {
                // Importante: Desliga o autocomplete nas outras colunas (como Quantidade)
                // para não aparecerem sugestões de produtos onde não devem.
                if (e.Control is TextBox txt)
                {
                    txt.AutoCompleteMode = AutoCompleteMode.None;
                }
            }
        }

        private void ConfigurarDesign()
        {
            // --- 1. CORES GERAIS E FONTE ---
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            // --- 2. ESTILIZAR A TABELA (GRELHA) ---
            dgvItens.BackgroundColor = Color.White;
            dgvItens.BorderStyle = BorderStyle.None;
            dgvItens.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvItens.EnableHeadersVisualStyles = false;

            // Cabeçalho
            dgvItens.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dgvItens.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvItens.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvItens.ColumnHeadersDefaultCellStyle.Padding = new Padding(10);
            dgvItens.ColumnHeadersHeight = 50;

            // Linhas
            dgvItens.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgvItens.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvItens.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvItens.DefaultCellStyle.Padding = new Padding(5);
            dgvItens.RowTemplate.Height = 45;
            dgvItens.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvItens.RowHeadersVisible = false;

            // --- 3. ESTILIZAR BOTÕES ---
            EstilizarBotao(button1, Color.FromArgb(39, 174, 96)); // Verde
            if (this.Controls.Find("btnFinalizar", true).Length > 0) // Verifica se existe para não dar erro
            {
                // Tenta encontrar o botão pelo nome, ou usa o nome direto se souberes qual é
                // Exemplo: EstilizarBotao(btnTerminar, Color.FromArgb(41, 128, 185));
            }

            // Vou assumir que se chama 'btnFinalizar' (se não for, muda o nome aqui!)
            // Cor: 41, 128, 185 é um azul profissional excelente
            EstilizarBotao(btnFinalizar, Color.FromArgb(41, 128, 185));
            // Se tiveres outros botões, descomenta:
            // EstilizarBotao(btnFinalizar, Color.FromArgb(41, 128, 185));
            // EstilizarBotao(btnRemoverLinha, Color.FromArgb(231, 76, 60));

            // Botão Remover (Vermelho) - NOVO!
            // (Verifica se o nome no design é mesmo btnRemoverLinha)
            if (this.Controls.Find("btnRemoverLinha", true).Length > 0)
                EstilizarBotao(btnRemoverLinha, Color.FromArgb(231, 76, 60));

            // --- 4. OPCIONAL: ESTILIZAR TODAS AS OUTRAS CAIXAS DE TEXTO ---
            // Isto faz com que as caixas fiquem planas (Flat) em vez de 3D.
            // O código varre todos os controlos à procura de TextBoxes.
            foreach (Control c in this.Controls)
            {
                // Se a caixa estiver solta no formulário
                if (c is TextBox txt && txt.Name != "txtDescontoFinal") // Ignora a do desconto (tratamos à parte)
                {
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }

                // Se as caixas estiverem dentro de GroupBoxes (como os dados do cliente)
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

        // Pequena função auxiliar para os botões
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

        // CORREÇÃO: Adicionado '?' no sender
        private void btnRemoverLinha_Click_1(object? sender, EventArgs e)
        {

        }

        // --- CÓDIGO DO MENU DE CONTEXTO (ADICIONAR NO FIM) ---

        private void ConfigurarMenuContexto()
        {
            // 1. Criar o Menu
            menuGrelha = new ContextMenuStrip();

            // 2. Criar o Botão "Ver Stock"
            ToolStripMenuItem itemStock = new ToolStripMenuItem("Ver Stock / Detalhes");
            itemStock.Click += ItemVerStock_Click; // Liga ao evento de clique

            // 3. Adicionar botão ao menu
            menuGrelha.Items.Add(itemStock);

            // 4. Associar o menu à tua tabela
            dgvItens.ContextMenuStrip = menuGrelha;

            // 5. Adicionar evento para selecionar a linha com o botão direito
            dgvItens.CellMouseDown += dgvItens_CellMouseDown;
        }

        // Evento: Seleciona a linha quando clicas com o botão direito
        // CORREÇÃO: Adicionado '?' no sender
        private void dgvItens_CellMouseDown(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Seleciona a célula e a linha onde clicaste
                dgvItens.CurrentCell = dgvItens.Rows[e.RowIndex].Cells[e.ColumnIndex];
                dgvItens.Rows[e.RowIndex].Selected = true;
            }
        }

        // Evento: O que acontece quando clicas na opção "Ver Stock"
        // CORREÇÃO: Adicionado '?' no sender
        private void ItemVerStock_Click(object? sender, EventArgs e)
        {
            // Verifica se há uma linha selecionada
            if (dgvItens.CurrentRow != null && !dgvItens.CurrentRow.IsNewRow)
            {
                // Pega no Código do Produto (que está na Coluna 0)
                // CORREÇÃO CS8600: Adicionado "?? string.Empty" para garantir que não é nulo
                string codigoProduto = dgvItens.CurrentRow.Cells[0].Value?.ToString() ?? "";

                if (!string.IsNullOrEmpty(codigoProduto))
                {
                    // Abre a janela de produtos
                    FormProdutos frm = new FormProdutos();

                    // PREENCHE A PESQUISA AUTOMATICAMENTE
                    // (Isto requer que tenhas criado o método 'DefinirPesquisa' no passo anterior)
                    frm.DefinirPesquisa(codigoProduto);

                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Esta linha não tem código de produto.");
                }
            }
        }
    }
}