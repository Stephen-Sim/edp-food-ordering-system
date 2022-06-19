using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingSystem.models
{
    public class CartTable : FoodOrder
    {
        private string foodName;

        public string FoodName
        {
            get { return foodName; }
            set { foodName = value.Trim(); }
        }

        private string foodType;

        public string FoodType
        {
            get { return foodType; }
            set { foodType = value.Trim(); }
        }
    }
}
