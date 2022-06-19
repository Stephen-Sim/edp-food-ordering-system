﻿using FoodOrderingSystem.controller;
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
    public partial class CustomerOrderFoodForm : Form
    {
        public CustomerOrderFoodForm(int cusId)
        {
            this.cusId = cusId;
            InitializeComponent();
        }

        FoodController foodController = new FoodController();
        OrderController orderController = new OrderController();
        int cusId { get; set; }
        int SelectedFoodId { get; set; } = 0;
        int MaxQuantity { get; set; } = 0;

        decimal? selectedFoodPrice { get; set; } = 0;

        private void loadTable(List<FoodTable> foods)
        {
            DataTable table = new DataTable();
            table.Columns.Add("#", typeof(int));
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Food Name", typeof(string));
            table.Columns.Add("Food Price", typeof(decimal));
            table.Columns.Add("Food Type", typeof(string));
            table.Columns.Add("Quantity", typeof(int));

            for (int i = 0; i < foods.Count(); i++)
            {
                FoodTable food = foods[i];
                
                if (food.Stock == 0)
                    continue;

                table.Rows.Add(i + 1, food.Id, food.Name, food.Price, food.FoodTypeName, food.Stock);
            }

            dataGridViewFood.DataSource = table;
            dataGridViewFood.Columns["Id"].Visible = false;
        }

        private void OrderFoodForm_Load(object sender, EventArgs e)
        {
            var foods = foodController.getFoodTable();
            this.loadTable(foods);
            textBoxQuantity.Text = 0.ToString();
        }

        private void dataGridViewFood_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SelectedFoodId = (int)dataGridViewFood.Rows[e.RowIndex].Cells["Id"].Value;
                Food food = foodController.getFood(SelectedFoodId);
                MaxQuantity = (int) food.Stock;
                selectedFoodPrice = food.Price;
                labelFoodName.Text = food.Name;
            }
            catch (Exception)
            {
                return;
            }

        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            SelectedFoodId = 0;
            MaxQuantity = 0;
            textBoxQuantity.Text = 0.ToString();
            labelFoodName.Text = "No Food Selected";
        }

        delegate bool DelegateOneInt (int expression);

        DelegateOneInt checkIfEqualZero = delegate (int expression)
        {
            return expression == 0;
        };

        DelegateOneInt checkIfSamllerOrEqualToZero = quantity =>
        {
            return quantity <= 0;
        };

        private void buttonAddCart_Click(object sender, EventArgs e)
        {
            try
            {
                int quantity = int.Parse(textBoxQuantity.Text);

                if (checkIfEqualZero(SelectedFoodId))
                {
                    MessageBox.Show("You must select a food!", "Alert");
                    return;
                }

                if (checkIfSamllerOrEqualToZero(quantity))
                {
                    MessageBox.Show("Invalid quantity value!! Input must be greater than 0.", "Alert");
                    textBoxQuantity.Text = 0.ToString();
                    return;
                }

                if (quantity > MaxQuantity)
                {
                    MessageBox.Show("Stock Quantity is limited!!", "Alert");
                    textBoxQuantity.Text = MaxQuantity.ToString();
                    return;
                }

                if (orderController.AddOrder(this.cusId, SelectedFoodId, quantity))
                {
                    decimal? price = quantity * selectedFoodPrice;
                    MessageBox.Show($"Order is Successfully Added to Cart!!\nTotal Price : RM {price}", "Alert");
                    var foods = foodController.getFoodTable();
                    this.loadTable(foods);
                    buttonReset_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Failed to add!!", "Alert");
                }
            }
            catch
            {
                MessageBox.Show("Invalid Input, Quanity must be an number!!", "Alert");
                return;
            }
        }
    }
}
