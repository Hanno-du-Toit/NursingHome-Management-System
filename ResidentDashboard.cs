using from1INTRO;
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
    public partial class ResidentDashboard : Form
    {
        public ResidentDashboard()
        {
            InitializeComponent();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
            Login_Form1 login = new Login_Form1();
            login.Show();
        }

        private void btnResidents_Click(object sender, EventArgs e)
        {
            Resident_Form2 frm = new Resident_Form2();
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void btnAlerts_Click(object sender, EventArgs e)
        {
            Alert_Form7 frm = new Alert_Form7();
            frm.ShowDialog();
        }
    }
}
