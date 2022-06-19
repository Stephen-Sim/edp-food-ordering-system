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
        public List<CartTable> getCartTable(int cusId)
        {
            ent = new FoodOrderingSystemEntities();
            
            var orders = ent.Orders.First(x => x.CustomerId == cusId && x.Status == "Pending").FoodOrders.ToList().Select(x => new
            {
                x.OrderId,
                x.FoodId,
                FoodName = ent.Foods.First(y => y.Id == x.FoodId).Name,
                FoodType = ent.Foods.First(y => y.Id == x.FoodId).FoodType.Name,
                x.TotalPrice,
                x.Quantity
            });

            List<CartTable> result = new List<CartTable>();

            foreach (var order in orders)
            {
                CartTable cartTable = new CartTable();
                cartTable.OrderId = order.OrderId;
                cartTable.FoodId = order.FoodId;
                cartTable.FoodName = order.FoodName;
                cartTable.FoodType = order.FoodType;
                cartTable.TotalPrice = order.TotalPrice;
                cartTable.Quantity = order.Quantity;
                result.Add(cartTable);
            }

            return result;
        }

        public List<OrderTable> getOrderTable()
        {
            ent = new FoodOrderingSystemEntities();

            var orders = ent.Orders.Where(x => x.Status == "Paid").ToList().Select(x => new
            {
                x.Id,
                Username = ent.Users.First(y => y.Id == x.CustomerId).Username,
                x.TransactionTime,
                x.TotalPrice
            });

            List<OrderTable> result = new List<OrderTable>();

            foreach (var order in orders)
            {
                OrderTable orderTable = new OrderTable();
                orderTable.Id = order.Id;
                orderTable.Username = order.Username;
                orderTable.TransactionTime = order.TransactionTime;
                orderTable.TotalPrice = order.TotalPrice;
                result.Add(orderTable);
            }

            return result;
        }

        public List<OrderTable> getOrderTable(int cusId)
        {
            ent = new FoodOrderingSystemEntities();

            var orders = ent.Orders.Where(x => x.Status == "Paid" && x.CustomerId == cusId).ToList().Select(x => new
            {
                x.Id,
                Username = ent.Users.First(y => y.Id == x.CustomerId).Username,
                x.TransactionTime,
                x.TotalPrice
            });

            List<OrderTable> result = new List<OrderTable>();

            foreach (var order in orders)
            {
                OrderTable orderTable = new OrderTable();
                orderTable.Id = order.Id;
                orderTable.Username = order.Username;
                orderTable.TransactionTime = order.TransactionTime;
                orderTable.TotalPrice = order.TotalPrice;
                result.Add(orderTable);
            }

            return result;
        }

        public List<OrderTable> getOrderTableDateRange(DateTime startdate, DateTime enddate)
        {
            ent = new FoodOrderingSystemEntities();

            var orders = ent.Orders.Where(x => x.Status == "Paid" && x.TransactionTime >= startdate && x.TransactionTime <= enddate).ToList().Select(x => new
            {
                x.Id,
                Username = ent.Users.First(y => y.Id == x.CustomerId).Username,
                x.TransactionTime,
                x.TotalPrice
            });

            List<OrderTable> result = new List<OrderTable>();

            foreach (var order in orders)
            {
                OrderTable orderTable = new OrderTable();
                orderTable.Id = order.Id;
                orderTable.Username = order.Username;
                orderTable.TransactionTime = order.TransactionTime;
                orderTable.TotalPrice = order.TotalPrice;
                result.Add(orderTable);
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
                ent.SaveChanges();

                order.TotalPrice = ent.FoodOrders.Where(x => x.OrderId == order.Id).Sum(x => x.TotalPrice);

                ent.SaveChanges();
                return true;
            }
            catch (Exception err)
            {
                return false;
            }

        }

        public bool DeleteOrder(FoodOrder fo, int customerId)
        {
            ent = new FoodOrderingSystemEntities();

            try
            {
                FoodOrder foodorder = ent.FoodOrders.FirstOrDefault(x => x.OrderId == fo.OrderId && x.FoodId == fo.FoodId);
                Food food = ent.Foods.FirstOrDefault(x => x.Id == fo.FoodId);

                food.Stock += foodorder.Quantity;

                ent.FoodOrders.Remove(foodorder);
                ent.SaveChanges();

                Order order = ent.Orders.FirstOrDefault(x => x.Status == "Pending" && x.CustomerId == customerId);
                order.TotalPrice = ent.FoodOrders.Where(x => x.OrderId == order.Id).Sum(x => x.TotalPrice);
                ent.SaveChanges();
                return true;
            }
            catch (Exception err)
            {
                return false;
            }

        }

        public bool ConfrimOrder(int customerId)
        {
            ent = new FoodOrderingSystemEntities();

            try
            {
                Order order = ent.Orders.FirstOrDefault(x => x.Status == "Pending" && x.CustomerId == customerId);
                order.Status = "Paid";
                order.TransactionTime = DateTime.Now;

                ent.Orders.Append(order);
                ent.SaveChanges();

                Order neworder = new Order();
                neworder.TotalPrice = 0.00m;
                neworder.Status = "pending";
                neworder.CustomerId = customerId;

                ent.Orders.Add(neworder);

                ent.SaveChanges();
                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }

        public List<OrderMessage> getSelectedOrder(int id)
        {
            var fo = ent.FoodOrders.Where(x => x.OrderId == id).ToList().Select(x => new
            {
                FoodName = ent.Foods.First(y => y.Id == x.FoodId).Name,
                TotalPrice = x.TotalPrice,
            });

            List<OrderMessage> result = new List<OrderMessage>();

            foreach (var order in fo)
            {
                OrderMessage ordermsg = new OrderMessage();
                ordermsg.FoodName = order.FoodName;
                ordermsg.TotalPrice = order.TotalPrice;
                result.Add(ordermsg);
            }

            return result;
        }

        public bool testConnection()
        {
            throw new NotImplementedException();
        }
    }
}
