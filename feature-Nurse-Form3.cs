using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CareHomeSystem
{
    public partial class Nurse_Form3 : Form
    {
        private string connectionString =
            @"Server=HANNO\SQLEXPRESS;Database=NursingHomeDB;Trusted_Connection=True;";
        private bool isExampleMode = true;

        public Nurse_Form3()
        {
            InitializeComponent();
        }

        private void Nurse_Form3_Load(object sender, EventArgs e)
        {
            LoadNurses();
        }

        private void LoadNurses()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM NURSE";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvNurses.DataSource = dt;
            }
        }

        private int GetNextNurseID()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(NurseID), 0) + 1 FROM NURSE", con))
            {
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (isExampleMode)
            {
                txtNurseID.Text = GetNextNurseID().ToString();
                txtIDNumber.Text = "9001011234567";
                txtSurname.Text = "Example";
                txtName.Text = "Nurse";
                txtUserName.Text = "example.nurse";
                txtLogin.Text = "password123";

                MessageBox.Show("Example data populated. Edit as needed and click Add again to save the Nurse.");
                isExampleMode = false;
                return;
            }

            if (ValidateInputs())
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO NURSE 
                                    (NurseID, ID_Number, Surname, Name, UserName, Login)
                                     VALUES (@NurseID, @ID_Number, @Surname, @Name, @UserName, @Login)";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@NurseID", txtNurseID.Text);
                    cmd.Parameters.AddWithValue("@ID_Number", txtIDNumber.Text);
                    cmd.Parameters.AddWithValue("@Surname", txtSurname.Text);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@UserName", txtUserName.Text);
                    cmd.Parameters.AddWithValue("@Login", txtLogin.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Nurse added successfully.");
                    LoadNurses();
                    ClearFields();
                    isExampleMode = true;
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE NURSE 
                                     SET ID_Number=@ID_Number, Surname=@Surname, 
                                         Name=@Name, UserName=@UserName, Login=@Login 
                                     WHERE NurseID=@NurseID";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@NurseID", txtNurseID.Text);
                    cmd.Parameters.AddWithValue("@ID_Number", txtIDNumber.Text);
                    cmd.Parameters.AddWithValue("@Surname", txtSurname.Text);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@UserName", txtUserName.Text);
                    cmd.Parameters.AddWithValue("@Login", txtLogin.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Nurse updated successfully.");
                    LoadNurses();
                    ClearFields();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNurseID.Text))
            {
                MessageBox.Show("Please select a nurse to delete.");
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this nurse?",
                                                  "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM NURSE WHERE NurseID=@NurseID";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@NurseID", txtNurseID.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Nurse deleted successfully.");
                    LoadNurses();
                    ClearFields();
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;

                string query = "SELECT * FROM NURSE WHERE 1=1";

                // NurseID (exact match)
                if (!string.IsNullOrWhiteSpace(txtNurseID.Text))
                {
                    if (int.TryParse(txtNurseID.Text.Trim(), out int nurseId))
                    {
                        query += " AND NurseID = @NurseID";
                        cmd.Parameters.AddWithValue("@NurseID", nurseId);
                    }
                    else
                    {
                        MessageBox.Show("Nurse ID must be numeric.", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // ID Number
                if (!string.IsNullOrWhiteSpace(txtIDNumber.Text))
                {
                    query += " AND ID_Number LIKE @ID_Number";
                    cmd.Parameters.AddWithValue("@ID_Number", "%" + txtIDNumber.Text.Trim() + "%");
                }

                // Surname
                if (!string.IsNullOrWhiteSpace(txtSurname.Text))
                {
                    query += " AND Surname LIKE @Surname";
                    cmd.Parameters.AddWithValue("@Surname", "%" + txtSurname.Text.Trim() + "%");
                }

                // Name
                if (!string.IsNullOrWhiteSpace(txtName.Text))
                {
                    query += " AND Name LIKE @Name";
                    cmd.Parameters.AddWithValue("@Name", "%" + txtName.Text.Trim() + "%");
                }

                // Username
                if (!string.IsNullOrWhiteSpace(txtUserName.Text))
                {
                    query += " AND UserName LIKE @UserName";
                    cmd.Parameters.AddWithValue("@UserName", "%" + txtUserName.Text.Trim() + "%");
                }

                // Login
                if (!string.IsNullOrWhiteSpace(txtLogin.Text))
                {
                    query += " AND Login LIKE @Login";
                    cmd.Parameters.AddWithValue("@Login", "%" + txtLogin.Text.Trim() + "%");
                }

                cmd.CommandText = query;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvNurses.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No matching records found.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        private void dgvNurses_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNurses.Rows[e.RowIndex];

                txtNurseID.Text = row.Cells["NurseID"].Value?.ToString() ?? "";
                txtIDNumber.Text = row.Cells["ID_Number"].Value?.ToString() ?? "";
                txtSurname.Text = row.Cells["Surname"].Value?.ToString() ?? "";
                txtName.Text = row.Cells["Name"].Value?.ToString() ?? "";
                txtUserName.Text = row.Cells["UserName"].Value?.ToString() ?? "";
                txtLogin.Text = row.Cells["Login"].Value?.ToString() ?? "";
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtNurseID.Text) ||
                string.IsNullOrWhiteSpace(txtIDNumber.Text) ||
                string.IsNullOrWhiteSpace(txtSurname.Text) ||
                string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtUserName.Text) ||
                string.IsNullOrWhiteSpace(txtLogin.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return false;
            }
            return true;
        }

        private void ClearFields()
        {
            txtNurseID.Clear();
            txtIDNumber.Clear();
            txtSurname.Clear();
            txtName.Clear();
            txtUserName.Clear();
            txtLogin.Clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
            LoadNurses();
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
