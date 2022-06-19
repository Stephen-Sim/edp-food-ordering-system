using FoodOrderingSystem.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingSystem.controller
{
    public class OrderController : IController
    {
        FoodOrderingSystemEntities ent;
        public List<CartTable> getCartTable()
        {
            ent = new FoodOrderingSystemEntities();
            
            var orders = ent.FoodOrders.ToList().Select(x => new
            {
                FoodName = ent.Foods.First(y => y.Id == x.FoodId).Name,
                FoodType = ent.Foods.First(y => y.Id == x.FoodId).FoodType.Name,
                x.TotalPrice,
                x.Quantity
            });

            List<CartTable> result = new List<CartTable>();

            foreach (var order in orders)
            {
                CartTable cartTable = new CartTable();
                cartTable.FoodName = order.FoodName;
                cartTable.FoodType = order.FoodType;
                cartTable.TotalPrice = order.TotalPrice;
                cartTable.Quantity = order.Quantity;
                result.Add(cartTable);
            }

            return result;
        }

        public bool AddOrder(int customerId, int foodId, int foodQuantity)
        {
            ent = new FoodOrderingSystemEntities();

            try
            {
                Order order = ent.Orders.FirstOrDefault(x => x.Status == "Pending" && x.CustomerId == customerId);
                Food food = ent.Foods.FirstOrDefault(x => x.Id == foodId);

                food.Stock -= foodQuantity;

                var ifExist = ent.FoodOrders.FirstOrDefault(x => x.OrderId == order.Id && x.FoodId == foodId);

                if (ifExist != null)
                {
                   ifExist.Quantity += foodQuantity;
                   ifExist.TotalPrice = ifExist.Quantity * food.Price;
                }
                else
                {
                    FoodOrder foodOrder = new FoodOrder();
                    foodOrder.FoodId = foodId;
                    foodOrder.OrderId = order.Id;
                    foodOrder.Quantity = foodQuantity;
                    foodOrder.TotalPrice = foodOrder.Quantity * food.Price;

                    ent.FoodOrders.Add(foodOrder);
                }

                order.TotalPrice = ent.FoodOrders.Where(x => x.OrderId == order.Id).Sum(x => x.TotalPrice);

                ent.SaveChanges();
                return true;
            }
            catch (Exception err)
            {
                return false;
            }

        }

        public bool testConnection()
        {
            throw new NotImplementedException();
        }
    }
}
