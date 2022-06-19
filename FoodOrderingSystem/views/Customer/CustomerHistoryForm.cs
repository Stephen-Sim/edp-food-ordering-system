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

namespace FoodOrderingSystem.views.Customer
{
    public partial class CustomerHistoryForm : Form
    {
        int cusId { get; set; }
        public CustomerHistoryForm(int cusId)
        {
            InitializeComponent();
            this.cusId = cusId;
        }

        private void loadTable(List<OrderTable> ot)
        {
            DataTable table = new DataTable();
            table.Columns.Add("#", typeof(int));
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Username", typeof(string));
            table.Columns.Add("Transaction Time", typeof(string));
            table.Columns.Add("Total Price", typeof(decimal));

            for (int i = 0; i < ot.Count(); i++)
            {
                OrderTable order = ot[i];
                table.Rows.Add(i + 1, order.Id, order.Username, order.TransactionTime, order.TotalPrice);
            }

            dataGridViewOrder.DataSource = table;
            dataGridViewOrder.Columns["Id"].Visible = false;
        }

        OrderController orderController = new OrderController();

        private void CustomerHistoryForm_Load(object sender, EventArgs e)
        {
            var ot = orderController.getOrderTable(this.cusId);
            loadTable(ot);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            DateTime startdate = dateTimePickerStartDate.Value;
            DateTime enddate = dateTimePickerEndDate.Value;

            if (enddate < startdate)
            {
                MessageBox.Show("End date must be greater than Start Date!!", "Alert");
                return;
            }

            var ot = orderController.getOrderTableDateRange(startdate, enddate);
            loadTable(ot);
        }
    }
}
