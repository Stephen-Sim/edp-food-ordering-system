using FoodOrderingSystem.models;
using FoodOrderingSystem.views;
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
    public partial class Login : Form
    {
        FoodOrderingSystemEntities ent = new FoodOrderingSystemEntities();
        public Login()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            var user = new User();
            user.Username = UsernameTextBox.Text;
            user.Password = PasswordTextBox.Text;

            var ifFound = ent.Users.Where(x => x.Username == user.Username && x.Password == user.Password).FirstOrDefault();

            if (ifFound != null)
            {
                MessageBox.Show("Successfuly Login !!", "OK");

                if (ifFound.Role_Id == 1)
                {
                    this.Hide();
                    AdminPanelForm form = new AdminPanelForm();
                    form.Show();
                }
                else if (ifFound.Role_Id == 2)
                {
                    var checkCartExist = ent.Orders.FirstOrDefault(x => x.CustomerId == ifFound.Id && x.Status == "pending");
                    
                    if (checkCartExist == null)
                    {
                        Order order = new Order();
                        order.TotalPrice = 0.00m;
                        order.Status = "pending";
                        order.CustomerId = ifFound.Id;

                        ent.Orders.Add(order);
                        ent.SaveChanges();
                    }

                    this.Hide();
                    CustomerPanelForm form = new CustomerPanelForm(ifFound.Id);
                    form.Show();
                }

            }
            else
            {
                MessageBox.Show("Invalid Username or password !!", "OK");
                UsernameTextBox.Text = String.Empty;
                PasswordTextBox.Text = String.Empty;
            }

        }

        private void RegsiterLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Register form = new Register();
            form.Show();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

