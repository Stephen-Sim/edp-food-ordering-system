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
        public CustomerPanelForm(int cusId)
        {
            this.cusId = cusId;
            InitializeComponent();
        }

        int cusId { get; set; }

        private void orderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrderFoodForm form = new OrderFoodForm(this.cusId);
            form.MdiParent = this;
            form.Dock = DockStyle.Fill;
            form.Show();
        }

        private void cartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
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
    }
}
