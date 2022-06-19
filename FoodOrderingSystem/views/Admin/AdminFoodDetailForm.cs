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

namespace FoodOrderingSystem
{
    public partial class AdminFoodDetailForm : Form
    {
        FoodController con = new FoodController();

        int SelectedFoodId { get; set; }
        public AdminFoodDetailForm()
        {
            InitializeComponent();
        }

        private void FoodPanel_Load(object sender, EventArgs e)
        {
            var foods = con.getFoodTable();
            this.loadTable(foods);
        }

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
                table.Rows.Add(i + 1, food.Id, food.Name, food.Price, food.FoodTypeName, food.Stock);
            }

            dataGridViewFood.DataSource = table;
            dataGridViewFood.Columns["Id"].Visible = false;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string foodname = textBoxFoodSearchName.Text;
            int foodtypeid = comboBoxSerachFoodType.SelectedIndex + 1;

            Console.WriteLine(foodtypeid);

            if(!string.IsNullOrEmpty(foodname) && foodtypeid != 0)
            {
                var foods = con.getSearchFood(foodname, foodtypeid);
                this.loadTable(foods);
            }
            else
            {
                MessageBox.Show("All the Fields are required!!", "Alert");
            }
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            var foods = con.getFoodTable();
            this.loadTable(foods);
        }

        private void dataGridViewFood_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SelectedFoodId = (int)dataGridViewFood.Rows[e.RowIndex].Cells["Id"].Value;

                Food food = con.getFood(SelectedFoodId);
                textBoxFoodName.Text = food.Name;
                comboBoxFoodType.SelectedIndex = (int)(food.FoodTypeId - 1);
                textBoxFoodPrice.Text = food.Price.ToString();
                textBoxStockQuantity.Text = food.Stock.ToString();
            }
            catch (Exception)
            {
                return;
            }

        }

        private void clearText()
        {
            textBoxFoodName.Text = String.Empty;
            textBoxFoodPrice.Text = String.Empty;
            comboBoxFoodType.SelectedItem = null;
            textBoxStockQuantity.Text = String.Empty;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                Food food = new Food();
                food.Name = textBoxFoodName.Text;
                food.FoodTypeId = comboBoxFoodType.SelectedIndex + 1;
                food.Price = decimal.Parse(textBoxFoodPrice.Text);
                food.Stock = int.Parse(textBoxStockQuantity.Text);

                bool isAdded = con.AddFood(food);

                if (isAdded)
                {
                    MessageBox.Show("Food is Successfully Added!!!", "Alert");
                    this.clearText();
                    var foods = con.getFoodTable();
                    this.loadTable(foods);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("All the Fields are required with the Valid Input!!", "Alert");
                this.clearText();
                return;
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            this.clearText();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Food food = new Food();
                food.Name = textBoxFoodName.Text;
                food.FoodTypeId = comboBoxFoodType.SelectedIndex + 1;
                food.Price = decimal.Parse(textBoxFoodPrice.Text);
                food.Stock = int.Parse(textBoxStockQuantity.Text);

                bool isAdded = con.UpdateFood(SelectedFoodId, food);

                if (isAdded)
                {
                    MessageBox.Show("Food is Successfully Updated!!!", "Alert");
                    this.clearText();
                    var foods = con.getFoodTable();
                    this.loadTable(foods);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("All the Fields are required with the Valid Input!!", "Alert");
                this.clearText();
                return;
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Are you sure want to delete this Food?", "Alert", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    bool isDeleted = con.DeleteFood(SelectedFoodId);

                    if (isDeleted)
                    {
                        MessageBox.Show("Food is Successfully Deleted!!!", "Alert");
                        this.clearText();
                        var foods = con.getFoodTable();
                        this.loadTable(foods);
                    }
                }

            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
