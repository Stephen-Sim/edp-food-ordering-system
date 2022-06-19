using FoodOrderingSystem.controller;
using FoodOrderingSystem.models;
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
    public partial class CustomerCartForm : Form
    {
        int cusId;
        public CustomerCartForm(int cusId)
        {
            this.cusId = cusId;
            InitializeComponent();
        }

        OrderController orderController = new OrderController();

        private void CustomerCartForm_Load(object sender, EventArgs e)
        {
            var orders = orderController.getCartTable(this.cusId);
            this.loadTable(orders);
        }

        FoodOrder SelectedFoodorder { get; set; } = null;
        decimal? sum = 0.00m;
        private void loadTable(List<CartTable> orders)
        {
            DataTable table = new DataTable();
            table.Columns.Add("#", typeof(int));
            table.Columns.Add("FoodId", typeof(int));
            table.Columns.Add("OrderId", typeof(int));
            table.Columns.Add("Food Name", typeof(string));
            table.Columns.Add("Food Type", typeof(string));
            table.Columns.Add("Quantity", typeof(int));
            table.Columns.Add("Food Price", typeof(decimal));

            

            for (int i = 0; i < orders.Count(); i++)
            {
                CartTable order = orders[i];
                table.Rows.Add(i + 1, order.FoodId, order.OrderId, order.FoodName, order.FoodType, order.Quantity, order.TotalPrice);

                sum += order.TotalPrice;
            }

            dataGridViewCart.DataSource = table;
            dataGridViewCart.Columns["FoodId"].Visible = false;
            dataGridViewCart.Columns["OrderId"].Visible = false;

            labelTotalAmount.Text = sum.ToString();
        }

        private void dataGridViewCart_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SelectedFoodorder = new FoodOrder();

                SelectedFoodorder.FoodId = (int)dataGridViewCart.Rows[e.RowIndex].Cells["FoodId"].Value;
                SelectedFoodorder.OrderId = (int)dataGridViewCart.Rows[e.RowIndex].Cells["OrderId"].Value;

            }
            catch (Exception)
            {
                SelectedFoodorder = null;
                return;
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (SelectedFoodorder == null)
            {
                MessageBox.Show("Please Select an order", "Alert");
                return;
            }

            DialogResult result = MessageBox.Show("Do you confirm to delete order??", "Alert", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                if (orderController.DeleteOrder(SelectedFoodorder, this.cusId))
                {
                    MessageBox.Show("Order is successfully deleted!!!", "Alert");
                    var orders = orderController.getCartTable(this.cusId);
                    this.loadTable(orders);
                }
                else
                {
                    MessageBox.Show("Order is failed to delete!!!", "Alert");
                }
            }
        }

        private void buttonConfirmOrder_Click(object sender, EventArgs e)
        {
            if (sum == 0.00m)
            {
                MessageBox.Show("There is item in the cart!!", "Alert");
                return;
            }

            DialogResult result = MessageBox.Show("Do you sure to Confirm order??", "Alert", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                if (orderController.ConfrimOrder(this.cusId))
                {
                    MessageBox.Show("Thank You!!!", "Alert");
                    var orders = orderController.getCartTable(this.cusId);
                    this.loadTable(orders);
                    sum = 0.00m;
                    labelTotalAmount.Text = sum.ToString();
                }
                else
                {
                    MessageBox.Show("Order is failed to delete!!!", "Alert");
                }
            }
        }
    }
}
