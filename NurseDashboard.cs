using CareHomeSystem;
using from1INTRO;
using MED_TO_RES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main_Project
{
    public partial class NurseDashboard : Form
    {
        public NurseDashboard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void btnResidents_Click(object sender, EventArgs e)
        {
            Resident_Form2 frm = new Resident_Form2();
            frm.ShowDialog();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
            Login_Form1 login = new Login_Form1();
            login.Show();

        }

        private void btnNurses_Click(object sender, EventArgs e)
        {
            Nurse_Form3 frm = new Nurse_Form3();
            frm.ShowDialog();

        }

        private void btnMedStock_Click(object sender, EventArgs e)
        {
            Medstock_Form4 frm = new Medstock_Form4();
            frm.ShowDialog();

        }

        private void btnOrderMed_Click(object sender, EventArgs e)
        {
            OrderMed_Form5 frm = new OrderMed_Form5();
            frm.ShowDialog();

        }

        private void btnMedToRes_Click(object sender, EventArgs e)
        {
            MedStores_Form6 frm = new MedStores_Form6();
            frm.ShowDialog();

        }

        private void btnAlerts_Click(object sender, EventArgs e)
        {
            Alert_Form7 frm = new Alert_Form7();
            frm.ShowDialog();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            Reports_Form8 frm = new Reports_Form8();
            frm.ShowDialog();

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }
    }
}
