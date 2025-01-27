using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mission03
{
    public class FoodItem
    {
        // Properties of a food item
        public string foodName { get; set; }
        public string foodCategory { get; set; }
        public int foodQuantity { get; set; }
        public DateTime foodExpireDate { get; set; }

        // Constructor for creating a FoodItem object
        public FoodItem(string name, string category, int quantity, DateTime expireDate)
        {
            foodName = name;
            foodCategory = category;
            foodQuantity = quantity;
            foodExpireDate = expireDate;
        }

        public override string ToString()
        {
            return $"{foodName} - {foodCategory} - {foodQuantity} - {foodExpireDate.ToShortDateString()}";
        }
    }
}
