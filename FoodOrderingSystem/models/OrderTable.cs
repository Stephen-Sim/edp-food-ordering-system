using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingSystem.models
{
    public class OrderTable : Order
    {
        private string username;

        public string Username
        {
            get { return username; }
            set { username = value.Trim(); }
        }

    }
}
