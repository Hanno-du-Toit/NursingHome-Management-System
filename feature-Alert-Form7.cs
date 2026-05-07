using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace from1INTRO
{
    public partial class Alert_Form7 : Form
    {
        string connStr = @"Server=localhost\SQLEXPRESS;Database=NursingHomeDB;Trusted_Connection=True;";

        public Alert_Form7()
        {
            InitializeComponent();
            this.Load += EmergencyAlertsForm_Load;
        }

        private void EmergencyAlertsForm_Load(object sender, EventArgs e)
        {
            LoadResidents();
            LoadNurses();
            LoadAlerts();
        }

        // 🔹 Load Residents into ComboBox
        private void LoadResidents()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT ResID, Name, Surname FROM RESIDENT", con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    cbxResID.Items.Clear();
                    while (reader.Read())
                    {
                        string display = $"{reader["ResID"]} - {reader["Name"]} {reader["Surname"]}";
                        cbxResID.Items.Add(display);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading residents: " + ex.Message);
            }
        }

        // 🔹 Load Nurses into ComboBox
        private void LoadNurses()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT NurseID, Name, Surname FROM NURSE", con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    cbxNurseID.Items.Clear();
                    while (reader.Read())
                    {
                        string display = $"{reader["NurseID"]} - {reader["Name"]} {reader["Surname"]}";
                        cbxNurseID.Items.Add(display);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading nurses: " + ex.Message);
            }
        }

        // 🔹 Load Alerts into DataGridView
        private void LoadAlerts()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    string query = @"SELECT ea.AlertID, ea.ResID, r.Name + ' ' + r.Surname AS Resident,
                                            ea.NurseID, n.Name + ' ' + n.Surname AS Nurse,
                                            ea.Date_of_Alert, ea.Time_of_Alert, ea.Description, ea.Response_Time
                                     FROM EMEGENCY_ALERT ea
                                     INNER JOIN RESIDENT r ON ea.ResID = r.ResID
                                     INNER JOIN NURSE n ON ea.NurseID = n.NurseID";

                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvAlerts.DataSource = dt;

                    dgvAlerts.Columns["Description"].Width = 200;
                    dgvAlerts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvAlerts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgvAlerts.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading alerts: " + ex.Message);
            }
        }

        // 🔹 Save Alert
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cbxResID.SelectedIndex < 0 || cbxNurseID.SelectedIndex < 0 || string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Please select resident, nurse, and enter a description.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int resID = int.Parse(cbxResID.SelectedItem.ToString().Split('-')[0].Trim());
            int nurseID = int.Parse(cbxNurseID.SelectedItem.ToString().Split('-')[0].Trim());
            DateTime date = dtpDate.Value.Date;
            TimeSpan time = dtpAlertTime.Value.TimeOfDay;
            string description = txtDescription.Text;
            // Optional Response Time input
            // TimeSpan response = mtxtResponseTime.MaskFull ? TimeSpan.Parse(mtxtResponseTime.Text) : TimeSpan.Zero;

            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();

                    // Generate next AlertID
                    SqlCommand cmdMax = new SqlCommand("SELECT ISNULL(MAX(AlertID), 0) + 1 FROM EMEGENCY_ALERT", con);
                    int alertID = (int)cmdMax.ExecuteScalar();

                    string insertQuery = @"INSERT INTO EMEGENCY_ALERT 
                                           (AlertID, ResID, NurseID, Date_of_Alert, Time_of_Alert, Description, Response_Time)
                                           VALUES (@AlertID, @ResID, @NurseID, @Date_of_Alert, @Time_of_Alert, @Description, @Response_Time)";

                    SqlCommand cmd = new SqlCommand(insertQuery, con);
                    cmd.Parameters.AddWithValue("@AlertID", alertID);
                    cmd.Parameters.AddWithValue("@ResID", resID);
                    cmd.Parameters.AddWithValue("@NurseID", nurseID);
                    cmd.Parameters.AddWithValue("@Date_of_Alert", date);
                    cmd.Parameters.AddWithValue("@Time_of_Alert", time);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@Response_Time", DBNull.Value); // No response time yet

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Emergency alert saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadAlerts(); // Refresh grid
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving alert: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
