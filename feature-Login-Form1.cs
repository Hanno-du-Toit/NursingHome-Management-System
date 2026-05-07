using Main_Project;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace from1INTRO
{
    public partial class Login_Form1 : Form
    {
        string connectionString = @"Server=.\SQLEXPRESS;Database=NursingHomeDB;Trusted_Connection=True;";

        public Login_Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txbUser.Text.Trim();
            string password = txbPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblError.Visible = true;
                lblError.Text = "⚠ Please enter both Username and Password.";
                return;
            }

            string role = ValidateLogin(username, password);

            if (role == "Nurse")
            {
                this.Hide(); // hide login
                NurseDashboard nurseDash = new NurseDashboard();
                nurseDash.Show();
            }
            else if (role == "Resident")
            {
                this.Hide(); // hide login
                ResidentDashboard resForm = new ResidentDashboard();
                resForm.Show();
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "❌ Invalid Username or Password.";

                txbUser.Clear();
                txbPassword.Clear();

                txbUser.Focus();
            }
        }


        private string ValidateLogin(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Check Nurses
                string sqlNurse = "SELECT COUNT(*) FROM NURSE WHERE UserName=@user AND Login=@pass";
                using (SqlCommand cmd = new SqlCommand(sqlNurse, conn))
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@pass", password);

                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                        return "Nurse";
                }

                // Check Residents
                string sqlResident = "SELECT COUNT(*) FROM RESIDENT WHERE UserName=@user AND Login=@pass";
                using (SqlCommand cmd = new SqlCommand(sqlResident, conn))
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@pass", password);

                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                        return "Resident";
                }
            }

            return "Invalid"; // not found
        }

        private void Login_Form1_Load(object sender, EventArgs e)
        {
            lblError.Visible = false; // hide error label when form loads
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
