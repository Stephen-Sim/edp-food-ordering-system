using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingSystem.models
{
    public class FoodTable : Food
    {
        private string _foodTypeName;

        public string FoodTypeName
        {
            get { return _foodTypeName; }
            set { _foodTypeName = value.Trim(); }
        }
    }
}
