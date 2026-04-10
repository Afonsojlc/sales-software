using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SoftwareVendas
{
    public partial class FormClientes : Form
    {
        private readonly string connectionString = @"Server=DESKTOP-P0S20G1\SQLEXPRESS;Database=Software_Vendas_Pai;Trusted_Connection=True;TrustServerCertificate=True;";
        private bool isProgrammaticChange = false;

        public FormClientes()
        {
            InitializeComponent();
            ConfigurarInterface();

            this.Resize += FormClientes_Resize;
            cmbFiltro.SelectedIndexChanged += cmbFiltro_SelectedIndexChanged;

            txtPesquisa.Enter += txtPesquisa_Enter;
            txtPesquisa.Leave += txtPesquisa_Leave;

            if (dgvClientes != null)
            {
                dgvClientes.SelectionChanged -= dgvClientes_SelectionChanged;
                dgvClientes.SelectionChanged += dgvClientes_SelectionChanged;
            }

            CarregarFiltros();
            CarregarTodosClientes();
        }

        #region Configuração e Responsividade (Auto-Resize)

        private void ConfigurarInterface()
        {
            this.Text = "Gestão de Clientes e Histórico de Vendas";

            EstilizarGrelha(dgvClientes);
            EstilizarGrelha(dgvHistorico);
        }

        private void FormClientes_Resize(object? sender, EventArgs e)
        {
            if (dgvClientes == null || dgvHistorico == null) return;
            if (this.ClientSize.Height < 200) return;

            int margem = 20;
            int topo = 130;

            int alturaDisponivel = this.ClientSize.Height - topo - (margem * 3);
            int alturaCadaGrelha = alturaDisponivel / 2;

            dgvClientes.Location = new Point(margem, topo);
            dgvClientes.Size = new Size(this.ClientSize.Width - (margem * 2), alturaCadaGrelha);

            dgvHistorico.Location = new Point(margem, topo + alturaCadaGrelha + margem);
            dgvHistorico.Size = new Size(this.ClientSize.Width - (margem * 2), alturaCadaGrelha);
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
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
        }

        private void FormatarGrelhaClientes()
        {
            if (dgvClientes.Columns.Count == 0) return;

            if (dgvClientes.Columns["ID_Cliente"] != null) dgvClientes.Columns["ID_Cliente"].Visible = false;

            if (dgvClientes.Columns["Nome_Cliente"] != null)
            {
                dgvClientes.Columns["Nome_Cliente"].HeaderText = "Nome do Cliente";
                dgvClientes.Columns["Nome_Cliente"].Width = 550;
            }

            if (dgvClientes.Columns["NIF"] != null)
            {
                dgvClientes.Columns["NIF"].HeaderText = "NIF";
                dgvClientes.Columns["NIF"].Width = 170;
            }

            if (dgvClientes.Columns["Morada_Completa"] != null)
            {
                dgvClientes.Columns["Morada_Completa"].HeaderText = "Morada";
                dgvClientes.Columns["Morada_Completa"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (dgvClientes.Columns["Codigo_Postal"] != null)
            {
                dgvClientes.Columns["Codigo_Postal"].HeaderText = "C. Postal";
                dgvClientes.Columns["Codigo_Postal"].Width = 160;
            }

            if (dgvClientes.Columns["Cidade"] != null)
            {
                dgvClientes.Columns["Cidade"].HeaderText = "Localidade";
                dgvClientes.Columns["Cidade"].Width = 200;
            }

            if (dgvClientes.Columns["Email"] != null)
            {
                dgvClientes.Columns["Email"].HeaderText = "Email";
                dgvClientes.Columns["Email"].Width = 330;
            }

            if (dgvClientes.Columns["Telefone"] != null)
            {
                dgvClientes.Columns["Telefone"].HeaderText = "Telefone";
                dgvClientes.Columns["Telefone"].Width = 165;
            }
        }

        private void FormatarGrelhaHistorico()
        {
            if (dgvHistorico.Columns.Count == 0) return;

            if (dgvHistorico.Columns.Contains("Nº Venda")) dgvHistorico.Columns["Nº Venda"].Width = 130;
            if (dgvHistorico.Columns.Contains("Data")) dgvHistorico.Columns["Data"].Width = 170;

            if (dgvHistorico.Columns.Contains("Total (€)"))
            {
                dgvHistorico.Columns["Total (€)"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvHistorico.Columns["Total (€)"].DefaultCellStyle.Format = "C2";
            }

            if (dgvHistorico.Columns.Contains("Estado")) dgvHistorico.Columns["Estado"].Width = 150;
            if (dgvHistorico.Columns.Contains("Desconto (%)")) dgvHistorico.Columns["Desconto (%)"].Width = 180;
        }

        #endregion

        #region Pesquisa e Sugestões (Autocomplete)

        private void CarregarFiltros()
        {
            cmbFiltro.Items.Clear();
            cmbFiltro.Items.AddRange(new string[] { "Nome", "NIF", "Email", "Localidade", "Morada" });
            cmbFiltro.SelectedIndex = 0;
        }

        private void cmbFiltro_SelectedIndexChanged(object? sender, EventArgs e)
        {
            AtualizarSugestoesPesquisa();
        }

        private void txtPesquisa_Enter(object? sender, EventArgs e)
        {
            if (txtPesquisa.Text.StartsWith("Escreva o "))
            {
                txtPesquisa.Clear();
                txtPesquisa.ForeColor = Color.Black;
            }
        }

        private void txtPesquisa_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPesquisa.Text))
            {
                string filtro = cmbFiltro.SelectedItem?.ToString() ?? "Nome";
                txtPesquisa.Text = $"Escreva o {filtro} e pressione Enter...";
                txtPesquisa.ForeColor = Color.Gray;
            }
        }

        private void AtualizarSugestoesPesquisa()
        {
            string filtro = cmbFiltro.SelectedItem?.ToString() ?? "Nome";
            string colunaBD = "Nome_Cliente";

            if (filtro == "NIF") colunaBD = "NIF";
            else if (filtro == "Email") colunaBD = "Email";
            else if (filtro == "Localidade") colunaBD = "Cidade";
            else if (filtro == "Morada") colunaBD = "Morada_Completa";

            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = $"SELECT DISTINCT {colunaBD} FROM Clientes WHERE {colunaBD} IS NOT NULL";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            while (rd.Read()) lista.Add(rd[0].ToString() ?? "");
                        }
                    }
                }
                catch { }
            }

            txtPesquisa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtPesquisa.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtPesquisa.AutoCompleteCustomSource = lista;

            txtPesquisa.Text = $"Escreva o {filtro} e pressione Enter...";
            txtPesquisa.ForeColor = Color.Gray;

            this.ActiveControl = null;
        }

        private void btnPesquisar_Click(object? sender, EventArgs e)
        {
            string termo = txtPesquisa.Text.StartsWith("Escreva o") ? "" : txtPesquisa.Text.Trim();
            PesquisarClientes(termo, cmbFiltro.SelectedItem?.ToString() ?? "Nome");
        }

        private void PesquisarClientes(string termo, string filtro)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT * FROM Clientes";

                    if (!string.IsNullOrWhiteSpace(termo) && filtro != "Todos")
                    {
                        string coluna = "Nome_Cliente";
                        if (filtro == "NIF") coluna = "NIF";
                        else if (filtro == "Email") coluna = "Email";
                        else if (filtro == "Localidade") coluna = "Cidade";
                        else if (filtro == "Morada") coluna = "Morada_Completa";

                        query += $" WHERE {coluna} LIKE @t";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        if (!string.IsNullOrWhiteSpace(termo) && filtro != "Todos")
                            cmd.Parameters.AddWithValue("@t", $"%{termo}%");

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();

                        isProgrammaticChange = true;

                        da.Fill(dt);
                        dgvClientes.DataSource = dt;

                        FormatarGrelhaClientes();

                        isProgrammaticChange = false;

                        if (dgvClientes.Rows.Count > 0)
                        {
                            dgvClientes.Rows[0].Selected = true;
                            AtualizarHistoricoSelecaoAtual();
                        }
                        else
                        {
                            dgvHistorico.DataSource = null;
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Erro SQL: " + ex.Message); }
                finally { isProgrammaticChange = false; }
            }
        }

        private void CarregarTodosClientes() { PesquisarClientes("", "Todos"); }

        #endregion

        #region Histórico de Compras

        private void dgvClientes_SelectionChanged(object? sender, EventArgs e)
        {
            if (isProgrammaticChange) return;
            AtualizarHistoricoSelecaoAtual();
        }

        private void AtualizarHistoricoSelecaoAtual()
        {
            if (dgvClientes.CurrentRow != null)
            {
                string id = dgvClientes.CurrentRow.Cells["ID_Cliente"].Value?.ToString() ?? "";
                if (!string.IsNullOrEmpty(id)) CarregarHistorico(id);
            }
        }

        private void CarregarHistorico(string idCliente)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = @"SELECT Numero_Encomenda as 'Nº Venda', Data_Encomenda as 'Data', 
                                    Valor_Total as 'Total (€)', Estado, Desconto_Global as 'Desconto (%)'
                                    FROM Encomenda WHERE ID_Cliente = @id ORDER BY Data_Encomenda DESC";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", idCliente);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvHistorico.DataSource = dt;
                        FormatarGrelhaHistorico();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Erro no Histórico: " + ex.Message); }
            }
        }

        #endregion

        #region Proteção do Designer (Métodos Vazios)
        private void txtPesquisa_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { btnPesquisar.PerformClick(); e.SuppressKeyPress = true; }
        }
        private void btnPesquisar_Click_1(object? sender, EventArgs e) { }
        private void label1_Click(object? sender, EventArgs e) { }
        private void label2_Click(object? sender, EventArgs e) { }
        private void label3_Click(object? sender, EventArgs e) { }
        private void label4_Click(object? sender, EventArgs e) { }
        private void label5_Click(object? sender, EventArgs e) { }
        private void txtPesquisa_Click(object? sender, EventArgs e) { }
        #endregion
    }
}