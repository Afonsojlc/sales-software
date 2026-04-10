using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SoftwareVendas
{
    public partial class FormEncomendas : Form
    {
        private readonly string connectionString = @"Server=DESKTOP-P0S20G1\SQLEXPRESS;Database=Software_Vendas_Pai;Trusted_Connection=True;TrustServerCertificate=True;";

        public FormEncomendas()
        {
            InitializeComponent();
            ConfigurarInterface();

            // Ligar os eventos essenciais via código
            this.Resize += FormEncomendas_Resize;

            // Eventos da barra de pesquisa
            txtPesquisa.Enter += txtPesquisa_Enter;
            txtPesquisa.Leave += txtPesquisa_Leave;
            txtPesquisa.KeyDown += txtPesquisa_KeyDown;
            txtPesquisa.Click += txtPesquisa_Click;

            // Evento para atualizar as sugestões sempre que mudamos o filtro
            if (cmbFiltro != null)
                cmbFiltro.SelectedIndexChanged += cmbFiltro_SelectedIndexChanged;

            // Evento para abrir os detalhes com duplo clique
            if (dgvEncomendas != null)
                dgvEncomendas.CellDoubleClick += dgvEncomendas_CellDoubleClick;

            CarregarFiltros();
            CarregarEncomendasRecentes();
        }

        #region 1. Configuração da Interface

        private void ConfigurarInterface()
        {
            this.Text = "Gestão e Consulta de Encomendas";

            // REMOVIDO: O código que forçava o tamanho (this.Size = ...). 
            // Agora a janela abre com o tamanho exato que definiste no Design do Visual Studio.
            this.StartPosition = FormStartPosition.CenterScreen;

            EstilizarGrelha(dgvEncomendas);
        }

        private void FormEncomendas_Resize(object? sender, EventArgs e)
        {
            if (dgvEncomendas == null) return;
            if (this.ClientSize.Height < 200) return;

            int margem = 20;
            int topo = 120; // Espaço em cima para a pesquisa e botões

            dgvEncomendas.Location = new Point(margem, topo);
            dgvEncomendas.Size = new Size(this.ClientSize.Width - (margem * 2), this.ClientSize.Height - topo - margem);
        }

        private void EstilizarGrelha(DataGridView dgv)
        {
            if (dgv == null) return;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
        }

        private void FormatarGrelha()
        {
            if (dgvEncomendas.Columns.Count == 0) return;

            if (dgvEncomendas.Columns["Nº Venda"] != null) dgvEncomendas.Columns["Nº Venda"].Width = 100;
            if (dgvEncomendas.Columns["Data"] != null) dgvEncomendas.Columns["Data"].Width = 150;

            if (dgvEncomendas.Columns["Cliente"] != null)
            {
                dgvEncomendas.Columns["Cliente"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (dgvEncomendas.Columns["Total (€)"] != null)
            {
                dgvEncomendas.Columns["Total (€)"].Width = 150;
                dgvEncomendas.Columns["Total (€)"].DefaultCellStyle.Format = "C2";
            }

            if (dgvEncomendas.Columns["Estado"] != null) dgvEncomendas.Columns["Estado"].Width = 200;
        }

        #endregion

        #region 2. Filtros, Pesquisa e AutoComplete

        private void CarregarFiltros()
        {
            cmbFiltro.Items.Clear();
            cmbFiltro.Items.AddRange(new string[] { "Nº Encomenda", "Nome do Cliente", "Data (dd/mm/yyyy)", "Mês/Ano (mm/yyyy)", "Estado" });
            cmbFiltro.SelectedIndex = 1; // Arranca selecionado no "Nome do Cliente"
        }

        private void cmbFiltro_SelectedIndexChanged(object? sender, EventArgs e)
        {
            AtualizarSugestoesPesquisa();
        }

        // Gera a lista de sugestões dinamicamente indo à BD ler os dados
        private void AtualizarSugestoesPesquisa()
        {
            string filtro = cmbFiltro.SelectedItem?.ToString() ?? "Nome do Cliente";
            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "";

                    // Escolhe a coluna e a tabela certa consoante o filtro escolhido
                    if (filtro == "Nº Encomenda")
                        query = "SELECT DISTINCT CAST(Numero_Encomenda AS VARCHAR) FROM Encomenda";
                    else if (filtro == "Nome do Cliente")
                        query = "SELECT DISTINCT Nome_Cliente FROM Clientes WHERE Nome_Cliente IS NOT NULL";
                    else if (filtro == "Estado")
                        query = "SELECT DISTINCT Estado FROM Encomenda WHERE Estado IS NOT NULL";
                    else if (filtro == "Data (dd/mm/yyyy)")
                        query = "SELECT DISTINCT CONVERT(varchar, Data_Encomenda, 103) FROM Encomenda WHERE Data_Encomenda IS NOT NULL";
                    else if (filtro == "Mês/Ano (mm/yyyy)")
                        query = "SELECT DISTINCT RIGHT(CONVERT(varchar, Data_Encomenda, 103), 7) FROM Encomenda WHERE Data_Encomenda IS NOT NULL";

                    if (!string.IsNullOrEmpty(query))
                    {
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            using (SqlDataReader rd = cmd.ExecuteReader())
                            {
                                while (rd.Read())
                                {
                                    lista.Add(rd[0].ToString() ?? "");
                                }
                            }
                        }
                    }
                }
                catch { } // Ignora erros de BD para não interromper a interface
            }

            // Aplica a lista à TextBox
            txtPesquisa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtPesquisa.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtPesquisa.AutoCompleteCustomSource = lista;

            // Mete o placeholder e tira o foco
            txtPesquisa.Text = $"Escreva o {filtro}...";
            txtPesquisa.ForeColor = Color.Gray;
            this.ActiveControl = null;
        }

        private void txtPesquisa_Enter(object? sender, EventArgs e)
        {
            if (txtPesquisa.Text.StartsWith("Escreva"))
            {
                txtPesquisa.Clear();
                txtPesquisa.ForeColor = Color.Black;
            }
        }

        private void txtPesquisa_Click(object? sender, EventArgs e)
        {
            txtPesquisa.Clear();
            txtPesquisa.ForeColor = Color.Black;

            // Força a lista a abrir se o utilizador clicar na caixa
            SendKeys.Send("{DOWN}");
        }

        private void txtPesquisa_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPesquisa.Text))
            {
                string filtro = cmbFiltro.SelectedItem?.ToString() ?? "pesquisa";
                txtPesquisa.Text = $"Escreva o {filtro}...";
                txtPesquisa.ForeColor = Color.Gray;
            }
        }

        private void txtPesquisa_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnPesquisar.PerformClick();
                e.SuppressKeyPress = true; // Retira aquele som de "beep" irritante do Windows
            }
        }

        private void btnPesquisar_Click(object? sender, EventArgs e)
        {
            string termo = txtPesquisa.Text.StartsWith("Escreva") ? "" : txtPesquisa.Text.Trim();
            string filtro = cmbFiltro.SelectedItem?.ToString() ?? "Nome do Cliente";

            RealizarPesquisa(termo, filtro);
        }

        private void CarregarEncomendasRecentes()
        {
            RealizarPesquisa("", "Recentes");
        }

        private void RealizarPesquisa(string termo, string filtro)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string queryBase = @"
                        SELECT TOP 100 
                            E.Numero_Encomenda as 'Nº Venda', 
                            E.Data_Encomenda as 'Data', 
                            C.Nome_Cliente as 'Cliente', 
                            E.Valor_Total as 'Total (€)', 
                            E.Estado 
                        FROM Encomenda E
                        INNER JOIN Clientes C ON E.ID_Cliente = C.ID_Cliente";

                    string queryFiltro = "";

                    if (!string.IsNullOrWhiteSpace(termo) && filtro != "Recentes")
                    {
                        switch (filtro)
                        {
                            case "Nº Encomenda":
                                queryFiltro = " WHERE E.Numero_Encomenda LIKE @termo";
                                break;
                            case "Nome do Cliente":
                                queryFiltro = " WHERE C.Nome_Cliente LIKE @termo";
                                break;
                            case "Estado":
                                queryFiltro = " WHERE E.Estado LIKE @termo";
                                break;
                            case "Data (dd/mm/yyyy)":
                                queryFiltro = " WHERE CONVERT(varchar, E.Data_Encomenda, 103) LIKE @termo";
                                break;
                            case "Mês/Ano (mm/yyyy)":
                                queryFiltro = " WHERE RIGHT(CONVERT(varchar, E.Data_Encomenda, 103), 7) LIKE @termo";
                                break;
                        }
                    }

                    string queryFinal = queryBase + queryFiltro + " ORDER BY E.Data_Encomenda DESC, E.Numero_Encomenda DESC";

                    using (SqlCommand cmd = new SqlCommand(queryFinal, con))
                    {
                        if (!string.IsNullOrWhiteSpace(termo) && filtro != "Recentes")
                        {
                            cmd.Parameters.AddWithValue("@termo", $"%{termo}%");
                        }

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvEncomendas.DataSource = dt;

                        FormatarGrelha();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao carregar encomendas:\n{ex.Message}", "Erro de SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region 3. Abertura de Detalhes

        private void dgvEncomendas_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int numeroEncomenda = Convert.ToInt32(dgvEncomendas.Rows[e.RowIndex].Cells["Nº Venda"].Value);
                MessageBox.Show($"Clicaste na encomenda nº {numeroEncomenda}!\nAqui vai abrir o formulário de detalhes no futuro próximo.", "Em construção...");
            }
        }

        #endregion

        private void btnPesquisar_Click_1(object? sender, EventArgs e) { }
    }
}