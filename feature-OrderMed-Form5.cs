using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace from1INTRO
{
    public partial class OrderMed_Form5 : Form
    {
        private string connectionString = @"Server=HANNO\SQLEXPRESS;Database=NursingHomeDB;Trusted_Connection=True;";

        public OrderMed_Form5()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadMedications();
            LoadOrders();

            dgvBatches.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBatches.ReadOnly = true;
            dgvBatches.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadMedications()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT MedID, Name FROM MED_STOCK", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cboMed.DataSource = dt;
                cboMed.DisplayMember = "Name";
                cboMed.ValueMember = "MedID";
            }
        }

        private void LoadOrders()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT o.MedBatch,
                                o.MedID,
                                m.Name AS MedName,
                                o.Stock,
                                o.Expiry_date
                         FROM ORDER_MED o
                         INNER JOIN MED_STOCK m ON o.MedID = m.MedID";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvBatches.DataSource = dt;
            }
        }

        private bool ValidateInputs(out int batch, out int medId, out int stock, out DateTime expiry)
        {
            batch = 0; medId = 0; stock = 0; expiry = DateTime.MinValue;

            if (!int.TryParse(txtMedBatch.Text.Trim(), out batch) || batch <= 0)
            { MessageBox.Show("Enter a valid MedBatch."); return false; }

            if (cboMed.SelectedValue == null || !int.TryParse(cboMed.SelectedValue.ToString(), out medId))
            { MessageBox.Show("Select a medication."); return false; }

            stock = (int)numStock.Value;
            if (stock <= 0)
            { MessageBox.Show("Stock must be > 0."); return false; }

            expiry = dtpExpiry.Value.Date;
            if (expiry <= DateTime.Today)
            { MessageBox.Show("Expiry date must be in the future."); return false; }

            return true;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out int batch, out int medId, out int stock, out DateTime expiry)) return;

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("spInsertOrderMed", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MedBatch", batch);
                cmd.Parameters.AddWithValue("@MedID", medId);
                cmd.Parameters.AddWithValue("@Stock", stock);
                cmd.Parameters.AddWithValue("@Expiry_date", expiry);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Order created successfully.");
            LoadOrders();
        }

        private void button1_Click(object sender, EventArgs e) // Update
        {
            if (!ValidateInputs(out int batch, out int medId, out int stock, out DateTime expiry)) return;

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("spUpdateOrderMed", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MedBatch", batch);
                cmd.Parameters.AddWithValue("@MedID", medId);
                cmd.Parameters.AddWithValue("@Stock", stock);
                cmd.Parameters.AddWithValue("@Expiry_date", expiry);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Order updated successfully.");
            LoadOrders();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMedBatch.Text.Trim(), out int batch))
            { MessageBox.Show("Enter a valid MedBatch."); return; }

            if (MessageBox.Show("Are you sure you want to delete this order?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("spDeleteOrderMed", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MedBatch", batch);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Order deleted successfully.");
            LoadOrders();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            LoadOrders();
        }

        private void dgvBatches_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBatches.CurrentRow == null) return;

            txtMedBatch.Text = dgvBatches.CurrentRow.Cells["MedBatch"].Value.ToString();
            cboMed.Text = dgvBatches.CurrentRow.Cells["MedName"].Value.ToString();
            numStock.Value = Convert.ToDecimal(dgvBatches.CurrentRow.Cells["Stock"].Value);
            dtpExpiry.Value = Convert.ToDateTime(dgvBatches.CurrentRow.Cells["Expiry_date"].Value);
        }
    }
}
