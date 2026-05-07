using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing.Printing;

namespace from1INTRO
{
    public partial class Reports_Form8 : Form
    {
        // Connection string
        string connStr = @"Server=localhost\SQLEXPRESS;Database=NursingHomeDB;Trusted_Connection=True;";

        public Reports_Form8()
        {
            InitializeComponent();

            // Event hooks
            this.Load += ReportsForm_Load;
            btnLoad.Click += BtnLoad_Click;
            btnExportCsv.Click += BtnExportCsv_Click;
            btnPrintPreview.Click += BtnPrintPreview_Click;
            btnPrint.Click += BtnPrint_Click;
            printDocument.PrintPage += PrintDocument_PrintPage;

            // Populate table list
            cmbTable.Items.AddRange(new string[]
            {
                "RESIDENT",
                "MED_STOCK",
                "MED_TO_RES",
                "ORDER_MED",
                "NURSE",
                "EMEGENCY_ALERT",
                "RES_TO_NURSE"
            });
            cmbTable.SelectedIndex = 0;

            // Make charts fill their group boxes
            chartResidentsRoom.Dock = DockStyle.Fill;
            chartMedUsage.Dock = DockStyle.Fill;
            chartAlerts.Dock = DockStyle.Fill;
        }

        // ===== Form Load =====
        private void ReportsForm_Load(object sender, EventArgs e)
        {
            LoadCharts();
            LoadSelectedTable();
        }

        // ===== Load Charts =====
        private void LoadCharts()
        {
            LoadResidentsPerRoomChart();
            LoadMedStockChart();
            LoadAlertsByResidentChart();
        }

        private void LoadResidentsPerRoomChart()
        {
            try
            {
                DataTable dt = new DataTable();
                string qry = "SELECT Room, COUNT(*) AS ResidentCount FROM RESIDENT GROUP BY Room ORDER BY Room";
                using (SqlConnection con = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(qry, con))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }

                chartResidentsRoom.Series.Clear();
                chartResidentsRoom.ChartAreas.Clear();
                chartResidentsRoom.ChartAreas.Add(new ChartArea());

                Series series = new Series("Residents")
                {
                    ChartType = SeriesChartType.Column,
                    IsValueShownAsLabel = true
                };
                chartResidentsRoom.Series.Add(series);

                foreach (DataRow row in dt.Rows)
                {
                    string room = row["Room"]?.ToString() ?? "(none)";
                    int count = Convert.ToInt32(row["ResidentCount"]);
                    series.Points.AddXY(room, count);
                }

                chartResidentsRoom.Legends.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Residents chart error: " + ex.Message);
            }
        }

        private void LoadMedStockChart()
        {
            try
            {
                DataTable dt = new DataTable();
                string qry = "SELECT Name, Stock FROM MED_STOCK ORDER BY Name";
                using (SqlConnection con = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(qry, con))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }

                chartMedUsage.Series.Clear();
                chartMedUsage.ChartAreas.Clear();
                chartMedUsage.ChartAreas.Add(new ChartArea());

                Series series = new Series("Stock")
                {
                    ChartType = SeriesChartType.Pie,
                    IsValueShownAsLabel = true
                };
                chartMedUsage.Series.Add(series);

                foreach (DataRow row in dt.Rows)
                {
                    string name = row["Name"].ToString();
                    int stock = Convert.ToInt32(row["Stock"]);
                    DataPoint point = new DataPoint();
                    point.YValues = new double[] { stock };
                    point.AxisLabel = name;
                    point.Label = $"{name} ({stock})";
                    series.Points.Add(point);
                }

                chartMedUsage.Legends.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Medication chart error: " + ex.Message);
            }
        }

        private void LoadAlertsByResidentChart()
        {
            try
            {
                DataTable dt = new DataTable();
                string qry = @"
                    SELECT r.Name + ' ' + r.Surname AS ResidentName, COUNT(a.AlertID) AS AlertCount
                    FROM RESIDENT r
                    LEFT JOIN EMEGENCY_ALERT a ON r.ResID = a.ResID
                    GROUP BY r.Name, r.Surname
                    ORDER BY AlertCount DESC";
                using (SqlConnection con = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(qry, con))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }

                chartAlerts.Series.Clear();
                chartAlerts.ChartAreas.Clear();
                chartAlerts.ChartAreas.Add(new ChartArea());

                Series series = new Series("Alerts")
                {
                    ChartType = SeriesChartType.Bar,
                    IsValueShownAsLabel = true
                };
                chartAlerts.Series.Add(series);

                foreach (DataRow row in dt.Rows)
                {
                    string name = row["ResidentName"].ToString();
                    int count = Convert.ToInt32(row["AlertCount"]);
                    series.Points.AddXY(name, count);
                }

                chartAlerts.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
                chartAlerts.Legends.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Alerts chart error: " + ex.Message);
            }
        }

        // ===== Load selected table into DataGridView =====
        private void LoadSelectedTable()
        {
            if (cmbTable.SelectedItem != null)
            {
                string tableName = cmbTable.SelectedItem.ToString();
                try
                {
                    DataTable dt = new DataTable();
                    using (SqlConnection con = new SqlConnection(connStr))
                    using (SqlCommand cmd = new SqlCommand($"SELECT * FROM {tableName}", con))
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    dgvReport.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading table: " + ex.Message);
                }
            }
        }

        // ===== Button Handlers =====
        private void BtnLoad_Click(object sender, EventArgs e)
        {
            LoadCharts();
            LoadSelectedTable();
        }

        private void BtnExportCsv_Click(object sender, EventArgs e)
        {
            if (dgvReport.DataSource == null)
            {
                MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV files (*.csv)|*.csv";
                sfd.FileName = "report.csv";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ExportDataGridViewToCsv(dgvReport, sfd.FileName);
                        MessageBox.Show("Exported CSV successfully.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Export error: " + ex.Message, "Export", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnPrintPreview_Click(object sender, EventArgs e)
        {
            printPreviewDialog.Document = printDocument;
            printPreviewDialog.ShowDialog();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            using (PrintDialog pd = new PrintDialog())
            {
                pd.Document = printDocument;
                if (pd.ShowDialog() == DialogResult.OK)
                    printDocument.Print();
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                Bitmap bmp = new Bitmap(splitContainer.Width, splitContainer.Height);
                splitContainer.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));

                Rectangle m = e.MarginBounds;
                float ratio = Math.Min((float)m.Width / bmp.Width, (float)m.Height / bmp.Height);
                int w = (int)(bmp.Width * ratio);
                int h = (int)(bmp.Height * ratio);
                e.Graphics.DrawImage(bmp, m.Left, m.Top, w, h);

                bmp.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Print error: " + ex.Message);
            }
        }

        // ===== Helpers =====
        private void ExportDataGridViewToCsv(DataGridView dgv, string path)
        {
            var sb = new System.Text.StringBuilder();
            var headers = dgv.Columns.Cast<DataGridViewColumn>().Select(c => QuoteCsv(c.HeaderText));
            sb.AppendLine(string.Join(",", headers));

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;
                var cells = row.Cells.Cast<DataGridViewCell>().Select(cell => QuoteCsv(cell.Value?.ToString() ?? ""));
                sb.AppendLine(string.Join(",", cells));
            }

            File.WriteAllText(path, sb.ToString(), System.Text.Encoding.UTF8);
        }

        private string QuoteCsv(string s)
        {
            if (s.Contains(",") || s.Contains("\"") || s.Contains("\n"))
            {
                s = s.Replace("\"", "\"\"");
                return $"\"{s}\"";
            }
            return s;
        }
    }
}
