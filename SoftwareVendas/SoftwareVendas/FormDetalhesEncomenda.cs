using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SoftwareVendas
{
    public partial class FormDetalhesEncomenda : Form
    {
        private readonly string connectionString = @"Server=DESKTOP-P0S20G1\SQLEXPRESS;Database=Software_Vendas_Pai;Trusted_Connection=True;TrustServerCertificate=True;";
        private readonly int idEncomenda;
        private string estadoOriginal = "";

        public FormDetalhesEncomenda(int numeroEncomenda)
        {
            InitializeComponent();
            idEncomenda = numeroEncomenda;

            ConfigurarInterface();
            CarregarDadosGlobais();
        }

        #region 1. Configuração de Interface e UI

        private void ConfigurarInterface()
        {
            cmbEstado.Items.AddRange(new string[] { "PENDENTE", "PAGA", "FECHADA", "DESPACHADO", "A ENTREGAR", "CANCELADA" });
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.SelectedIndexChanged += cmbEstado_SelectedIndexChanged;

            EstilizarGrelha();

            ConfigurarBotao("btnSair", "Sair", Color.FromArgb(231, 76, 60), btnSair_Click);
            ConfigurarBotao("btnModificar", "Modificar Encomenda", Color.FromArgb(52, 152, 219), btnModificar_Click);
            ConfigurarBotao("btnGuardar", "Guardar Alterações", Color.FromArgb(46, 204, 113), btnGuardar_Click, false);
        }

        private void ConfigurarBotao(string nomeCrtl, string texto, Color corFundo, EventHandler eventoClick, bool visivel = true)
        {
            Control[] controls = Controls.Find(nomeCrtl, true);
            if (controls.Length > 0 && controls[0] is Button btn)
            {
                btn.Text = texto;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = corFundo;
                btn.ForeColor = Color.White;
                btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                btn.Cursor = Cursors.Hand;
                btn.Visible = visivel;

                if (eventoClick != null)
                {
                    btn.Click -= eventoClick;
                    btn.Click += eventoClick;
                }
            }
        }

        private void EstilizarGrelha()
        {
            if (dgvLinhas == null) return;

            dgvLinhas.BackgroundColor = Color.White;
            dgvLinhas.BorderStyle = BorderStyle.FixedSingle;
            dgvLinhas.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvLinhas.EnableHeadersVisualStyles = false;
            dgvLinhas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dgvLinhas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLinhas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvLinhas.DefaultCellStyle.SelectionBackColor = Color.White;
            dgvLinhas.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvLinhas.RowHeadersVisible = false;
            dgvLinhas.ReadOnly = true;
            dgvLinhas.AllowUserToAddRows = false;
        }

        private void AtualizarComboBoxEstado(string estado)
        {
            cmbEstado.SelectedIndexChanged -= cmbEstado_SelectedIndexChanged;
            cmbEstado.SelectedItem = cmbEstado.Items.Contains(estado) ? estado : cmbEstado.Items[0];
            cmbEstado.SelectedIndexChanged += cmbEstado_SelectedIndexChanged;
        }

        #endregion

        #region 2. Acesso a Dados (Otimizado via Conexão Única)

        private void CarregarDadosGlobais()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    CarregarDadosEncomenda(con);
                    CarregarLinhasEncomenda(con);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Falha ao comunicar com a Base de Dados:\n{ex.Message}", "Erro Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CarregarDadosEncomenda(SqlConnection con)
        {
            string query = @"
                SELECT 
                    E.Data_Encomenda, E.Valor_Total, E.Estado, E.Desconto_Global,
                    C.Nome_Cliente, C.NIF, C.Email, C.Telefone
                FROM Encomenda E
                INNER JOIN Clientes C ON E.ID_Cliente = C.ID_Cliente
                WHERE E.Numero_Encomenda = @id";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@id", idEncomenda);
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        decimal total = Convert.ToDecimal(rd["Valor_Total"]);
                        decimal desconto = rd["Desconto_Global"] != DBNull.Value ? Convert.ToDecimal(rd["Desconto_Global"]) : 0;
                        estadoOriginal = rd["Estado"].ToString()?.Trim().ToUpper() ?? "PENDENTE";

                        label6.Text = rd["Nome_Cliente"].ToString() ?? "Desconhecido";
                        label7.Text = rd["NIF"].ToString() ?? "N/A";
                        label8.Text = rd["Email"].ToString() ?? "N/A";
                        label9.Text = rd["Telefone"].ToString() ?? "N/A";

                        label11.Text = idEncomenda.ToString();
                        label13.Text = Convert.ToDateTime(rd["Data_Encomenda"]).ToString("dd/MM/yyyy");
                        label16.Text = total.ToString("C2");
                        label19.Text = desconto > 0 ? $"{desconto}%" : "0%";

                        AtualizarComboBoxEstado(estadoOriginal);
                    }
                }
            }
        }

        private void CarregarLinhasEncomenda(SqlConnection con)
        {
            string query = @"
                SELECT 
                    Codigo_Material as 'Código', Descricao as 'Produto / Descrição', 
                    Quantidade as 'Qtd', Preco as 'Preço Unit. (€)', 
                    Desconto as 'Desc. (%)', Imposto as 'IVA (%)',
                    (Quantidade * Preco) as 'Subtotal (€)'
                FROM Linha_Encomenda 
                WHERE NE = @id ORDER BY Linha_Encomenda ASC";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@id", idEncomenda);
                DataTable dt = new DataTable();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }

                dgvLinhas.DataSource = dt;

                if (dgvLinhas.Columns.Count > 0)
                {
                    dgvLinhas.Columns["Código"].Width = 80;
                    dgvLinhas.Columns["Produto / Descrição"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dgvLinhas.Columns["Preço Unit. (€)"].DefaultCellStyle.Format = "C2";
                    dgvLinhas.Columns["Subtotal (€)"].DefaultCellStyle.Format = "C2";
                }
                dgvLinhas.ClearSelection();
            }
        }

        #endregion

        #region 3. Regras de Negócio e Ações

        private void cmbEstado_SelectedIndexChanged(object? sender, EventArgs e)
        {
            Control[] btnControls = Controls.Find("btnGuardar", true);
            if (btnControls.Length > 0 && btnControls[0] is Button btnGuardar)
            {
                btnGuardar.Visible = (cmbEstado.SelectedItem?.ToString() ?? "") != estadoOriginal;
            }
        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            string novoEstado = cmbEstado.SelectedItem?.ToString() ?? "";

            if (novoEstado == estadoOriginal) return;

            DialogResult resposta = MessageBox.Show(
                $"Confirma a alteração do estado da Encomenda Nº {idEncomenda} para '{novoEstado}'?",
                "Confirmar Gravação", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (resposta == DialogResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("UPDATE Encomenda SET Estado = @estado WHERE Numero_Encomenda = @id", con))
                        {
                            cmd.Parameters.AddWithValue("@estado", novoEstado);
                            cmd.Parameters.AddWithValue("@id", idEncomenda);
                            cmd.ExecuteNonQuery();
                        }

                        estadoOriginal = novoEstado;
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Falha ao atualizar o estado:\n{ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnModificar_Click(object? sender, EventArgs e)
        {
            Hide();
            using (Form1 formModificar = new Form1())
            {
                formModificar.PrepararModoModificacao(idEncomenda);
                if (formModificar.ShowDialog() == DialogResult.OK)
                {
                    CarregarDadosGlobais();
                }
            }
            Show();
        }

        private void btnSair_Click(object? sender, EventArgs e)
        {
            if ((cmbEstado.SelectedItem?.ToString() ?? "") != estadoOriginal)
            {
                if (MessageBox.Show("O estado foi alterado mas não gravado.\nDeseja sair mesmo assim?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }
            }
            Close();
        }

        #endregion

        #region Dependências Ocultas do Designer

        private void label16_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label18_Click(object sender, EventArgs e) { }

        #endregion
    }
}