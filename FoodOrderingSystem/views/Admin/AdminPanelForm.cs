using FoodOrderingSystem.views.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodOrderingSystem
{
    public partial class AdminPanelForm : Form
    {
        public AdminPanelForm()
        {
            InitializeComponent();
        }
        public AdminPanelForm(int id)
        {
            InitializeComponent();
        }
        private void AdminPanelForm_Load(object sender, EventArgs e)
        {
            IsMdiContainer = true;
        }

        private void foodStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdminFoodDetailForm form = new AdminFoodDetailForm();
            form.MdiParent = this;
            form.Dock = DockStyle.Fill;
            form.Show();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login form = new Login();
            form.Show();
        }

        private void reportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AdminReportForm form = new AdminReportForm();
            form.MdiParent = this;
            form.Dock = DockStyle.Fill;
            form.Show();
        }
    }
}
