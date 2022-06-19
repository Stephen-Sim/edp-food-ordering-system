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
    public partial class Register : Form
    {
        FoodOrderingSystemEntities ent = new FoodOrderingSystemEntities();
        public Register()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            if (PasswordTextBox.Text != ConfirmPasswordTextBox.Text)
            {
                MessageBox.Show("The password doesn't match !!", "OK");
                PasswordTextBox.Text = String.Empty;
                ConfirmPasswordTextBox.Text = String.Empty;
                return;
            }

            try
            {
                var user = new User();
                user.Username = UsernameTextBox.Text;
                user.Password = PasswordTextBox.Text;
                user.Role_Id = 2; // register as customer

                ent.Users.Add(user);
                ent.SaveChanges();

                MessageBox.Show("User Successfully Created", "OK");

            }
            catch
            {
                MessageBox.Show("Soemthing Went Wrong!! Please Try Again.", "OK");
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
            var form = new Login();
            form.Show();
        }
    }
}
