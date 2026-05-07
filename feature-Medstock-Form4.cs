using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Main_Project
{
    public partial class Medstock_Form4 : Form
    {
        // connection string - adjust if needed
        private string connectionString = @"Server=HANNO\SQLEXPRESS;Database=NursingHomeDB;Trusted_Connection=True;";

        public Medstock_Form4()
        {
            InitializeComponent();
            LoadMedStock();

            // Ensure event handlers are wired
            dgvItems.SelectionChanged += dgvItems_SelectionChanged;
            dgvItems.CellClick += dgvItems_CellClick;
        }

        private void LoadMedStock()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM MED_STOCK", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvItems.DataSource = dt;
            }

            dgvItems.ClearSelection();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO MED_STOCK (MedID, Name, Stock) VALUES (@MedID, @Name, @Stock)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@MedID", GetNextMedID());
                    cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Stock", (int)numQty.Value);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show($"✅ {txtName.Text} (Qty: {numQty.Value}) added successfully.",
                                    "Medicine Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadMedStock();
                    ClearFields();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvItems.CurrentRow == null) return;

            int medId = Convert.ToInt32(dgvItems.CurrentRow.Cells["MedID"].Value);

            if (ValidateInputs())
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE MED_STOCK SET Name=@Name, Stock=@Stock WHERE MedID=@MedID";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@MedID", medId);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Stock", (int)numQty.Value);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Medicine updated successfully.");
                    LoadMedStock();
                    ClearFields();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a medicine to delete.");
                return;
            }

            DataGridViewRow row = dgvItems.SelectedRows[0];
            int medId = Convert.ToInt32(row.Cells["MedID"].Value);
            string medName = row.Cells["Name"].Value.ToString();
            int medStock = Convert.ToInt32(row.Cells["Stock"].Value);

            var confirm = MessageBox.Show($"⚠ Are you sure you want to delete:\n\n" +
                                          $"Medicine: {medName}\nStock: {medStock}",
                                          "Confirm Delete",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM MED_STOCK WHERE MedID=@MedID";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@MedID", medId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show($"🗑 {medName} deleted successfully.",
                                    "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadMedStock();
                    ClearFields();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
            LoadMedStock();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                LoadMedStock(); // if search box is empty, reload all
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM MED_STOCK WHERE Name LIKE @search";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@search", "%" + txtSearch.Text + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvItems.DataSource = dt;
            }

            dgvItems.ClearSelection();
        }

        // Auto-populate when selecting row
        private void dgvItems_SelectionChanged(object sender, EventArgs e)
        {
            PopulateFieldsFromGrid();
        }

        // Also handle single-click
        private void dgvItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            PopulateFieldsFromGrid();
        }

        private void PopulateFieldsFromGrid()
        {
            if (dgvItems.CurrentRow != null)
            {
                DataGridViewRow row = dgvItems.CurrentRow;

                if (row.Cells["Name"].Value != DBNull.Value)
                    txtName.Text = row.Cells["Name"].Value.ToString();

                if (row.Cells["Stock"].Value != DBNull.Value)
                    numQty.Value = Convert.ToDecimal(row.Cells["Stock"].Value);
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Medicine name is required.");
                return false;
            }

            if (numQty.Value < 0)
            {
                MessageBox.Show("Stock cannot be negative.");
                return false;
            }

            if (numReorder != null && numQty.Value < numReorder.Value)
            {
                MessageBox.Show("⚠ Stock is below the reorder level! Please reorder soon.",
                                "Stock Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return true;
        }

        private int GetNextMedID()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT ISNULL(MAX(MedID), 0) + 1 FROM MED_STOCK";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        private void ClearFields()
        {
            txtName.Clear();
            numQty.Value = 0;
            txtSearch.Clear();
            dgvItems.ClearSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
