using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MED_TO_RES
{
    public partial class MedStores_Form6 : Form
    {
        string connectionString = @"Server=HANNO\SQLEXPRESS;Database=NursingHomeDB;Trusted_Connection=True;";

        public MedStores_Form6()
        {
            InitializeComponent();

            // Wire up events properly
            this.Load += MedStores_Form6_Load;
            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            dgvMedToRes.CellClick += DgvMedToRes_CellClick;
        }

        private void MedStores_Form6_Load(object sender, EventArgs e)
        {
            LoadResidents();
            LoadMedications();
            LoadBatches();
            LoadData(); // ✅ DataGridView auto-populates at startup
        }

        // 🔹 Load Residents into ComboBox
        private void LoadResidents()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT ResID, Name + ' ' + Surname AS FullName FROM RESIDENT";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbResident.DataSource = dt;
                cmbResident.DisplayMember = "FullName";
                cmbResident.ValueMember = "ResID";
            }
        }

        // 🔹 Load Medications into ComboBox
        private void LoadMedications()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT MedID, Name FROM MED_STOCK";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbMedication.DataSource = dt;
                cmbMedication.DisplayMember = "Name";
                cmbMedication.ValueMember = "MedID";
            }
        }

        // 🔹 Load Batches into ComboBox
        private void LoadBatches()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT DISTINCT MedBatch FROM ORDER_MED";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbBatch.DataSource = dt;
                cmbBatch.DisplayMember = "MedBatch";
                cmbBatch.ValueMember = "MedBatch";
            }
        }

        // 🔹 Load DataGridView
        private void LoadData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT M.ResID, R.Name + ' ' + R.Surname AS Resident,
                                        M.MedID, S.Name AS Medication,
                                        M.Dosage, M.Time, M.MedBatch
                                 FROM MED_TO_RES M
                                 INNER JOIN RESIDENT R ON M.ResID = R.ResID
                                 INNER JOIN MED_STOCK S ON M.MedID = S.MedID";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvMedToRes.DataSource = dt;
                dgvMedToRes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // ✅ Auto fit
            }
        }

        // 🔹 Add
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (cmbResident.SelectedValue == null || cmbMedication.SelectedValue == null || cmbBatch.SelectedValue == null)
            {
                MessageBox.Show("Please select all fields.");
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO MED_TO_RES (ResID, MedID, Dosage, Time, MedBatch) VALUES (@ResID, @MedID, @Dosage, @Time, @MedBatch)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ResID", cmbResident.SelectedValue);
                cmd.Parameters.AddWithValue("@MedID", cmbMedication.SelectedValue);
                cmd.Parameters.AddWithValue("@Dosage", txtDosage.Text);
                cmd.Parameters.AddWithValue("@Time", timePicker.Value.ToString("HH:mm:ss"));
                cmd.Parameters.AddWithValue("@MedBatch", cmbBatch.SelectedValue);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            LoadData();
        }

        // 🔹 Update
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvMedToRes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a record to update.");
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE MED_TO_RES SET Dosage=@Dosage, Time=@Time, MedBatch=@MedBatch WHERE ResID=@ResID AND MedID=@MedID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ResID", cmbResident.SelectedValue);
                cmd.Parameters.AddWithValue("@MedID", cmbMedication.SelectedValue);
                cmd.Parameters.AddWithValue("@Dosage", txtDosage.Text);
                cmd.Parameters.AddWithValue("@Time", timePicker.Value.ToString("HH:mm:ss"));
                cmd.Parameters.AddWithValue("@MedBatch", cmbBatch.SelectedValue);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            LoadData();
        }

        // 🔹 Delete
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvMedToRes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a record to delete.");
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM MED_TO_RES WHERE ResID=@ResID AND MedID=@MedID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ResID", cmbResident.SelectedValue);
                cmd.Parameters.AddWithValue("@MedID", cmbMedication.SelectedValue);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            LoadData();
        }

        // 🔹 Populate fields on grid click
        private void DgvMedToRes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvMedToRes.Rows[e.RowIndex];

                cmbResident.SelectedValue = row.Cells["ResID"].Value;
                cmbMedication.SelectedValue = row.Cells["MedID"].Value;
                txtDosage.Text = row.Cells["Dosage"].Value.ToString();

                // Handle Time column
                var timeValue = row.Cells["Time"].Value;
                if (timeValue is TimeSpan ts)
                    timePicker.Value = DateTime.Today.Add(ts);
                else if (timeValue is DateTime dt)
                    timePicker.Value = dt;

                cmbBatch.SelectedValue = row.Cells["MedBatch"].Value;
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
