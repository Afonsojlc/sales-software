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

        // Label do Totalizador programática
        private Label lblTotalizador = new Label();

        public FormEncomendas()
        {
            InitializeComponent();
            ConfigurarInterface();

            this.Resize += FormEncomendas_Resize;

            txtPesquisa.Enter += txtPesquisa_Enter;
            txtPesquisa.Leave += txtPesquisa_Leave;
            txtPesquisa.KeyDown += txtPesquisa_KeyDown;
            txtPesquisa.Click += txtPesquisa_Click;

            if (cmbFiltro != null)
                cmbFiltro.SelectedIndexChanged += cmbFiltro_SelectedIndexChanged;

            if (dgvEncomendas != null)
            {
                dgvEncomendas.CellDoubleClick += dgvEncomendas_CellDoubleClick;
                dgvEncomendas.CellFormatting += dgvEncomendas_CellFormatting;
            }

            // Ligar os novos botões via código (se já existirem no Design com estes nomes)
            if (this.Controls.Find("btnNovaEncomenda", true).Length > 0)
            {
                Button btnNova = (Button)this.Controls.Find("btnNovaEncomenda", true)[0];
                btnNova.Click += btnNovaEncomenda_Click;
            }

            if (this.Controls.Find("btnEliminar", true).Length > 0)
            {
                Button btnDel = (Button)this.Controls.Find("btnEliminar", true)[0];
                btnDel.Click += btnEliminar_Click;
            }

            CarregarFiltros();
            CarregarEncomendasRecentes();
        }

        #region 1. Configuração da Interface

        private void ConfigurarInterface()
        {
            this.Text = "Gestão e Consulta de Encomendas";
            this.StartPosition = FormStartPosition.CenterScreen;

            EstilizarGrelha(dgvEncomendas);

            // Configurar a Label do Totalizador
            lblTotalizador.AutoSize = true;
            lblTotalizador.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblTotalizador.ForeColor = Color.SeaGreen;
            lblTotalizador.Text = "Total Válido: 0,00 €";
            lblTotalizador.Visible = false;

            // Injeta a Label no GroupBox2
            Control[] groupBoxes = this.Controls.Find("groupBox2", true);
            if (groupBoxes.Length > 0 && groupBoxes[0] is GroupBox gb)
            {
                gb.Controls.Add(lblTotalizador);
            }
            else
            {
                this.Controls.Add(lblTotalizador);
            }
        }

        private void FormEncomendas_Resize(object? sender, EventArgs e)
        {
            if (dgvEncomendas == null) return;
            if (this.ClientSize.Height < 200) return;

            int margemExterna = 20;
            int alturaRodape = 40;

            if (dgvEncomendas.Parent is GroupBox gb2)
            {
                int topoGb2 = gb2.Top > 0 ? gb2.Top : 130;
                gb2.Size = new Size(this.ClientSize.Width - (margemExterna * 2), this.ClientSize.Height - topoGb2 - margemExterna);

                dgvEncomendas.Location = new Point(10, 25);
                dgvEncomendas.Size = new Size(gb2.Width - 20, gb2.Height - 30 - alturaRodape);
            }
            else
            {
                int topo = 120;
                dgvEncomendas.Location = new Point(margemExterna, topo);
                dgvEncomendas.Size = new Size(this.ClientSize.Width - (margemExterna * 2), this.ClientSize.Height - topo - margemExterna - alturaRodape);
            }

            PosicionarTotalizador();
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

            if (dgvEncomendas.Columns["Estado"] != null) dgvEncomendas.Columns["Estado"].Width = 230;
        }

        private void dgvEncomendas_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvEncomendas.Columns[e.ColumnIndex].Name == "Estado" && e.Value != null)
            {
                string estado = e.Value.ToString()?.Trim().ToUpper() ?? "";
                e.CellStyle!.Font = new Font(dgvEncomendas.Font, FontStyle.Bold);

                if (estado == "PENDENTE")
                    e.CellStyle.ForeColor = Color.DarkOrange;
                else if (estado == "FECHADA" || estado == "PAGA")
                    e.CellStyle.ForeColor = Color.SeaGreen;
                else if (estado == "CANCELADA")
                    e.CellStyle.ForeColor = Color.Crimson;
                else if (estado == "DESPACHADO" || estado == "A ENTREGAR")
                    e.CellStyle.ForeColor = Color.DodgerBlue;
            }
        }

        #endregion

        #region 2. Filtros, Pesquisa e AutoComplete

        private void CarregarFiltros()
        {
            cmbFiltro.Items.Clear();
            cmbFiltro.Items.AddRange(new string[] { "Nº Encomenda", "Nome do Cliente", "Data (dd/mm/yyyy)", "Mês/Ano (mm/yyyy)", "Estado" });
            cmbFiltro.SelectedIndex = 1;
        }

        private void cmbFiltro_SelectedIndexChanged(object? sender, EventArgs e)
        {
            AtualizarSugestoesPesquisa();
            CalcularTotalVisivel();
        }

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
                catch { }
            }

            txtPesquisa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtPesquisa.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtPesquisa.AutoCompleteCustomSource = lista;

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
                e.SuppressKeyPress = true;
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
                        CalcularTotalVisivel();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao carregar encomendas:\n{ex.Message}", "Erro de SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CalcularTotalVisivel()
        {
            string filtroAtual = cmbFiltro.SelectedItem?.ToString() ?? "";

            if (filtroAtual != "Data (dd/mm/yyyy)" && filtroAtual != "Mês/Ano (mm/yyyy)")
            {
                lblTotalizador.Visible = false;
                return;
            }

            decimal soma = 0;
            foreach (DataGridViewRow linha in dgvEncomendas.Rows)
            {
                if (linha.Cells["Estado"].Value != null && linha.Cells["Total (€)"].Value != null)
                {
                    string estado = linha.Cells["Estado"].Value.ToString()?.Trim().ToUpper() ?? "";

                    if (estado != "CANCELADA")
                    {
                        soma += Convert.ToDecimal(linha.Cells["Total (€)"].Value);
                    }
                }
            }

            lblTotalizador.Text = $"Total Válido: {soma:C2}";
            lblTotalizador.Visible = true;
            lblTotalizador.BringToFront();
            PosicionarTotalizador();
        }

        private void PosicionarTotalizador()
        {
            if (lblTotalizador.Parent == null) return;

            int margem = 20;
            lblTotalizador.Left = lblTotalizador.Parent.Width - margem - lblTotalizador.Width;
            lblTotalizador.Top = dgvEncomendas.Bottom + 5;
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

        #region 4. Operações Adicionais (Nova e Eliminar)

        private void btnNovaEncomenda_Click(object? sender, EventArgs e)
        {
            // Abre o formulário da Fatura (que tu chamaste de Form1)
            using (Form1 formNovaVenda = new Form1())
            {
                formNovaVenda.ShowDialog();

                // Quando a janela da venda fechar, recarregamos a grelha para mostrar a nova fatura!
                btnPesquisar.PerformClick();
            }
        }

        private void btnEliminar_Click(object? sender, EventArgs e)
        {
            // 1. Verifica se há alguma linha selecionada
            if (dgvEncomendas.CurrentRow == null)
            {
                MessageBox.Show("Por favor, selecione a encomenda que deseja eliminar clicando nela primeiro.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int numeroEncomenda = Convert.ToInt32(dgvEncomendas.CurrentRow.Cells["Nº Venda"].Value);
            string nomeCliente = dgvEncomendas.CurrentRow.Cells["Cliente"].Value?.ToString() ?? "Desconhecido";

            // 2. Pergunta de Segurança Rigorosa
            DialogResult resposta = MessageBox.Show(
                $"Atenção: Tem a certeza absoluta que deseja eliminar a Encomenda Nº {numeroEncomenda} do cliente '{nomeCliente}'?\n\nEsta ação irá apagar definitivamente todos os produtos associados a esta venda e não poderá ser revertida!",
                "Confirmar Eliminação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Stop,
                MessageBoxDefaultButton.Button2); // O botão "Não" fica selecionado por defeito para evitar acidentes

            if (resposta == DialogResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();

                        // 3. A Magia da Integridade Referencial:
                        // O SQL exige que apaguemos primeiro os filhos (Linha_Encomenda) para podermos apagar o Pai (Encomenda)
                        string queryEliminar = @"
                            DELETE FROM Linha_Encomenda WHERE NE = @id;
                            DELETE FROM Encomenda WHERE Numero_Encomenda = @id;";

                        using (SqlCommand cmd = new SqlCommand(queryEliminar, con))
                        {
                            cmd.Parameters.AddWithValue("@id", numeroEncomenda);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("A encomenda foi eliminada com sucesso.", "Operação Concluída", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // 4. Atualiza a lista automaticamente
                        btnPesquisar.PerformClick();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ocorreu um erro ao tentar eliminar a encomenda:\n{ex.Message}", "Erro de Base de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        #endregion

        private void btnPesquisar_Click_1(object? sender, EventArgs e) { }
    }
}