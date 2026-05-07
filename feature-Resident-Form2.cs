using Main_Project;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace from1INTRO
{
    public partial class Resident_Form2 : Form
    {
        public Resident_Form2()
        {
            InitializeComponent();

            // Hook input restrictions
            txbResID.KeyPress += txbResID_KeyPress;
            txbIDNum.KeyPress += txbIDNum_KeyPress;
        }

        string connStr = @"Server=HANNO\SQLEXPRESS;Database=NursingHomeDB;Trusted_Connection=True;";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adap;
        DataTable dt;

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadResidents();
        }

        private void LoadResidents()
        {
            using (con = new SqlConnection(connStr))
            {
                adap = new SqlDataAdapter("SELECT * FROM RESIDENT", con);
                dt = new DataTable();
                adap.Fill(dt);

                dgvResidents.DataSource = dt;

                if (dgvResidents.Columns.Contains("ResID"))
                {
                    dgvResidents.Columns["ResID"].HeaderText = "Resident ID";
                    dgvResidents.Columns["ResID"].Visible = true;
                }

                if (dgvResidents.Columns.Contains("ID_Number"))
                    dgvResidents.Columns["ID_Number"].HeaderText = "ID Number";
            }
        }

        private bool ValidateInputs()
        {
            // Resident ID required & must be numeric
            if (string.IsNullOrWhiteSpace(txbResID.Text))
            {
                MessageBox.Show("Resident ID is required.");
                return false;
            }
            if (!int.TryParse(txbResID.Text.Trim(), out int resId))
            {
                MessageBox.Show("Resident ID must be numeric.");
                return false;
            }
            if (resId <= 0)
            {
                MessageBox.Show("Resident ID must be greater than zero.");
                return false;
            }

            // Required fields
            if (string.IsNullOrWhiteSpace(txbIDNum.Text) ||
                string.IsNullOrWhiteSpace(txbName.Text) ||
                string.IsNullOrWhiteSpace(txbSurname.Text))
            {
                MessageBox.Show("ID Number, Name and Surname are required.");
                return false;
            }

            // ID number format: must be 13 numeric digits
            if (txbIDNum.Text.Length != 13 || !txbIDNum.Text.All(char.IsDigit))
            {
                MessageBox.Show("ID Number must be 13 digits and numeric.");
                return false;
            }

            // Optional fields - trim nulls
            txbRoom.Text = txbRoom.Text?.Trim() ?? "";
            txbUserName.Text = txbUserName.Text?.Trim() ?? "";
            txbLogin.Text = txbLogin.Text?.Trim() ?? "";

            return true;
        }

        private int GetNextResID()
        {
            using (con = new SqlConnection(connStr))
            using (SqlCommand c = new SqlCommand("SELECT ISNULL(MAX(ResID), 0) + 1 FROM RESIDENT", con))
            {
                con.Open();
                return Convert.ToInt32(c.ExecuteScalar());
            }
        }

        private bool ResIDExists(int resId)
        {
            using (con = new SqlConnection(connStr))
            using (SqlCommand c = new SqlCommand("SELECT COUNT(*) FROM RESIDENT WHERE ResID = @ResID", con))
            {
                c.Parameters.AddWithValue("@ResID", resId);
                con.Open();
                int count = (int)c.ExecuteScalar();
                return count > 0;
            }
        }

        // Add new resident
        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool allEmpty =
                string.IsNullOrWhiteSpace(txbResID.Text) &&
                string.IsNullOrWhiteSpace(txbIDNum.Text) &&
                string.IsNullOrWhiteSpace(txbName.Text) &&
                string.IsNullOrWhiteSpace(txbSurname.Text);

            if (allEmpty)
            {
                int nextId = GetNextResID();
                txbResID.Text = nextId.ToString();
                txbIDNum.Text = "1234567890123";
                txbName.Text = "John";
                txbSurname.Text = "Doe";
                txbRoom.Text = "A101";
                txbUserName.Text = "jdoe";
                txbLogin.Text = "password123";

                MessageBox.Show("Example data populated. Edit as needed and click Add again to save the resident.",
                    "Example Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!ValidateInputs())
                return;

            if (!int.TryParse(txbResID.Text.Trim(), out int resID))
            {
                MessageBox.Show("Invalid Resident ID entered.");
                return;
            }

            if (ResIDExists(resID))
            {
                MessageBox.Show("Resident ID already exists. Use Update to modify an existing resident, or choose another ID.",
                    "Duplicate ResID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (con = new SqlConnection(connStr))
                {
                    string query = "INSERT INTO RESIDENT (ResID, ID_Number, Surname, Name, Room, UserName, Login) " +
                                   "VALUES (@ResID, @ID_Number, @Surname, @Name, @Room, @UserName, @Login)";
                    cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@ResID", resID);
                    cmd.Parameters.AddWithValue("@ID_Number", txbIDNum.Text.Trim());
                    cmd.Parameters.AddWithValue("@Surname", txbSurname.Text.Trim());
                    cmd.Parameters.AddWithValue("@Name", txbName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Room", txbRoom.Text.Trim());
                    cmd.Parameters.AddWithValue("@UserName", txbUserName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Login", txbLogin.Text.Trim());

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Resident Added Successfully", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadResidents();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding resident: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Update existing resident
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvResidents.CurrentRow == null)
            {
                MessageBox.Show("Please select a resident to update.");
                return;
            }

            if (!ValidateInputs())
                return;

            if (!int.TryParse(txbResID.Text.Trim(), out int selectedID))
            {
                MessageBox.Show("Invalid Resident ID entered.");
                return;
            }

            try
            {
                using (con = new SqlConnection(connStr))
                {
                    string query = "UPDATE RESIDENT " +
                                   "SET ID_Number=@ID_Number, Surname=@Surname, Name=@Name, Room=@Room, UserName=@UserName, Login=@Login " +
                                   "WHERE ResID=@ResID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ResID", selectedID);
                        cmd.Parameters.AddWithValue("@ID_Number", txbIDNum.Text.Trim());
                        cmd.Parameters.AddWithValue("@Surname", txbSurname.Text.Trim());
                        cmd.Parameters.AddWithValue("@Name", txbName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Room", txbRoom.Text.Trim());
                        cmd.Parameters.AddWithValue("@UserName", txbUserName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Login", txbLogin.Text.Trim());

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Resident Updated Successfully", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadResidents();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating resident: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Delete resident
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvResidents.CurrentRow == null)
            {
                MessageBox.Show("Please select a resident to delete.");
                return;
            }

            if (!int.TryParse(dgvResidents.CurrentRow.Cells["ResID"].Value?.ToString(), out int selectedID))
            {
                MessageBox.Show("Unable to read selected Resident ID.");
                return;
            }

            var confirm = MessageBox.Show($"Are you sure you want to delete Resident ID {selectedID}?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                using (con = new SqlConnection(connStr))
                {
                    string query = "DELETE FROM RESIDENT WHERE ResID=@ResID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ResID", selectedID);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Resident Deleted Successfully", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadResidents();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting resident: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Search residents
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchQuery = "SELECT * FROM RESIDENT WHERE 1=1";

            using (SqlConnection con = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;

                if (!string.IsNullOrWhiteSpace(txbResID.Text))
                {
                    if (int.TryParse(txbResID.Text, out int resId))
                    {
                        searchQuery += " AND ResID = @ResID";
                        cmd.Parameters.AddWithValue("@ResID", resId);
                    }
                    else
                    {
                        MessageBox.Show("Resident ID must be numeric.", "Search Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (!string.IsNullOrWhiteSpace(txbIDNum.Text))
                {
                    searchQuery += " AND ID_Number LIKE @ID_Number";
                    cmd.Parameters.AddWithValue("@ID_Number", "%" + txbIDNum.Text.Trim() + "%");
                }

                if (!string.IsNullOrWhiteSpace(txbName.Text))
                {
                    searchQuery += " AND Name LIKE @Name";
                    cmd.Parameters.AddWithValue("@Name", "%" + txbName.Text.Trim() + "%");
                }

                if (!string.IsNullOrWhiteSpace(txbSurname.Text))
                {
                    searchQuery += " AND Surname LIKE @Surname";
                    cmd.Parameters.AddWithValue("@Surname", "%" + txbSurname.Text.Trim() + "%");
                }

                if (!string.IsNullOrWhiteSpace(txbRoom.Text))
                {
                    searchQuery += " AND Room LIKE @Room";
                    cmd.Parameters.AddWithValue("@Room", "%" + txbRoom.Text.Trim() + "%");
                }

                if (!string.IsNullOrWhiteSpace(txbUserName.Text))
                {
                    searchQuery += " AND UserName LIKE @UserName";
                    cmd.Parameters.AddWithValue("@UserName", "%" + txbUserName.Text.Trim() + "%");
                }

                if (!string.IsNullOrWhiteSpace(txbLogin.Text))
                {
                    searchQuery += " AND Login LIKE @Login";
                    cmd.Parameters.AddWithValue("@Login", "%" + txbLogin.Text.Trim() + "%");
                }

                cmd.CommandText = searchQuery;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvResidents.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No matching residents found.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
            LoadResidents();
            MessageBox.Show("Fields cleared.", "Clear", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvResidents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvResidents.Rows[e.RowIndex];

                txbResID.Text = row.Cells["ResID"].Value?.ToString() ?? "";
                txbIDNum.Text = row.Cells["ID_Number"].Value?.ToString() ?? "";
                txbName.Text = row.Cells["Name"].Value?.ToString() ?? "";
                txbSurname.Text = row.Cells["Surname"].Value?.ToString() ?? "";
                txbRoom.Text = row.Cells["Room"].Value?.ToString() ?? "";
                txbUserName.Text = row.Cells["UserName"].Value?.ToString() ?? "";
                txbLogin.Text = row.Cells["Login"].Value?.ToString() ?? "";
            }
        }

        private void ClearFields()
        {
            txbResID.Clear();
            txbIDNum.Clear();
            txbName.Clear();
            txbSurname.Clear();
            txbLogin.Clear();
            txbRoom.Clear();
            txbUserName.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
            Login_Form1 login = new Login_Form1();
            login.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        // Prevent invalid typing
        private void txbResID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Only numbers allowed in Resident ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txbIDNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("ID Number must be numeric.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
