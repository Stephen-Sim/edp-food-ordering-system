using FoodOrderingSystem.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingSystem.controller
{
    public class FoodController : IController
    {
        FoodOrderingSystemEntities ent; 

        public List<FoodTable> getFoodTable()
        {
            ent = new FoodOrderingSystemEntities();
            var foods = ent.Foods.ToList().Select(x => new
            {
                x.Id,
                Name = x.Name.Trim(),
                x.Price,
                FoodType = x.FoodType.Name,
                x.Stock
            });

            List<FoodTable> result = new List<FoodTable>();

            foreach (var food in foods)
            {
                FoodTable ft = new FoodTable();
                ft.Id = food.Id;
                ft.Name = food.Name;
                ft.Price = food.Price;
                ft.FoodTypeName = food.FoodType;
                ft.Stock = food.Stock;
                result.Add(ft);
            }

            return result;
        }

        public List<FoodTable> getSearchFood(string name, int foodType)
        {
            ent = new FoodOrderingSystemEntities();

            var foods = ent.Foods.ToList().Where(x => x.Name.Contains(name) && x.FoodTypeId == foodType).Select(x => new
            {
                x.Id,
                Name = x.Name.Trim(),
                x.Price,
                FoodType = x.FoodType.Name,
                x.Stock
            });

            List<FoodTable> result = new List<FoodTable>();

            foreach (var food in foods)
            {
                FoodTable ft = new FoodTable();
                ft.Id = food.Id;
                ft.Name = food.Name;
                ft.Price = food.Price;
                ft.FoodTypeName = food.FoodType;
                ft.Stock = food.Stock;
                result.Add(ft);
            }

            return result;
        }

        public bool AddFood(Food food)
        {
            ent = new FoodOrderingSystemEntities();

            try
            {
                ent.Foods.Add(food);
                ent.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateFood(int id, Food editedfood)
        {
            ent = new FoodOrderingSystemEntities();

            try
            {
                Food food = ent.Foods.FirstOrDefault(x => x.Id == id);
                food.Name = editedfood.Name;
                food.Price = editedfood.Price;
                food.FoodTypeId = editedfood.FoodTypeId;
                food.Stock = editedfood.Stock;
                ent.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteFood(int id)
        {
            ent = new FoodOrderingSystemEntities();

            try
            {
                ent.Foods.Remove(ent.Foods.FirstOrDefault(x => x.Id == id));
                ent.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Food getFood(int id)
        {
            ent = new FoodOrderingSystemEntities();

            return ent.Foods.FirstOrDefault(x => x.Id == id);
        }

        public bool testConnection()
        {
            throw new NotImplementedException();
        }
    }
}
