using FoodOrderingSystem.views.Customer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodOrderingSystem.views
{
    public partial class CustomerPanelForm : Form
    {
        public CustomerPanelForm(int cusId, string username)
        {
            this.cusId = cusId;
            this.username = username;
            InitializeComponent();

            labelusername.Text = "Welcome !!! " + username;
        }

        int cusId { get; set; }
        string username { get; set; }

        private void orderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerOrderFoodForm form = new CustomerOrderFoodForm(this.cusId);
            form.MdiParent = this;
            form.Dock = DockStyle.Fill;
            form.Show();
        }

        private void cartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerCartForm form = new CustomerCartForm(this.cusId);
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

        private void CustomerPanelForm_Load(object sender, EventArgs e)
        {
            IsMdiContainer = true;
        }

        private void histroyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerHistoryForm form = new CustomerHistoryForm(this.cusId);
            form.MdiParent = this;
            form.Dock = DockStyle.Fill;
            form.Show();
        }
    }
}
