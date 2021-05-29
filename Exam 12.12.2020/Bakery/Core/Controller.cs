using Bakery.Core.Contracts;
using Bakery.Models.BakedFoods;
using Bakery.Models.BakedFoods.Contracts;
using Bakery.Models.Drinks;
using Bakery.Models.Drinks.Contracts;
using Bakery.Models.Tables;
using Bakery.Models.Tables.Contracts;
using Bakery.Utilities.Messages;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bakery.Core
{
    public class Controller : IController
    {
        private ICollection<ITable> tables;
        private ICollection<IDrink> drinks;
        private ICollection<IBakedFood> foods;
        private decimal TotalIncome;

        public Controller()
        {
            tables = new List<ITable>();
            drinks = new List<IDrink>();
            foods = new List<IBakedFood>();
        }

        public string AddDrink(string type, string name, int portion, string brand)
        {
            IDrink drink = DrinkFactory(type, name, portion, brand);

            drinks.Add(drink);

            return string.Format(OutputMessages.DrinkAdded, drink.Name, drink.Brand);
        }

        

        public string AddFood(string type, string name, decimal price)
        {
            IBakedFood food = FoodFactory(type, name, price);

            foods.Add(food);

            return string.Format(OutputMessages.FoodAdded, food.Name, type);
        }

        

        public string AddTable(string type, int tableNumber, int capacity)
        {
            ITable table = TableFactory(type, tableNumber, capacity);

            tables.Add(table);

            return string.Format(OutputMessages.TableAdded, tableNumber);
        }

        
        public string GetFreeTablesInfo()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var table in this.tables.Where(t => t.IsReserved == false))
            {
                sb.AppendLine(table.GetFreeTableInfo());
            }

            return sb.ToString().TrimEnd();
        }

        public string GetTotalIncome()
        {
            
            return $"Total income: {this.TotalIncome:f2}lv";
        }

        public string LeaveTable(int tableNumber)
        {
            ITable table = tables.FirstOrDefault(t => t.TableNumber == tableNumber);
            decimal tableBill = table.GetBill();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Table: {tableNumber}");
            sb.AppendLine($"Bill: {tableBill:f2}");

            this.TotalIncome += tableBill;

            table.Clear();
            return sb.ToString().TrimEnd();
        }

        public string OrderDrink(int tableNumber, string drinkName, string drinkBrand)
        {
            ITable table = tables.FirstOrDefault(t => t.TableNumber == tableNumber);

            if (table == null)
            {
                return string.Format(OutputMessages.WrongTableNumber, tableNumber);
            }

            IDrink drink = drinks.FirstOrDefault(d => d.Name == drinkName);

            if (drink == null)
            {
                return string.Format(OutputMessages.NonExistentDrink, drinkName, drinkBrand);
            }

            table.OrderDrink(drink);
            return $"Table {tableNumber} ordered {drinkName} {drinkBrand}".TrimEnd();
        }

        public string OrderFood(int tableNumber, string foodName)
        {
            ITable table = tables.FirstOrDefault(t => t.TableNumber == tableNumber);

            if (table == null)
            {
                return string.Format(OutputMessages.WrongTableNumber, tableNumber);
            }

            IBakedFood food = foods.FirstOrDefault(d => d.Name == foodName);

            if (food == null)
            {
                return string.Format(OutputMessages.NonExistentFood, foodName);
            }

            table.OrderFood(food);
            return $"Table {tableNumber} ordered {foodName}".TrimEnd();
        }

        public string ReserveTable(int numberOfPeople)
        {
            ITable[] tablesWhichAreNotReserved = tables.Where(t => t.IsReserved == false).ToArray();
            ITable table = tablesWhichAreNotReserved.FirstOrDefault(t => t.Capacity > numberOfPeople);

            if (table == null)
            {
                return $"No available table for {numberOfPeople} people";
            }

            table.Reserve(numberOfPeople);
            this.TotalIncome += table.Price;

            return $"Table {table.TableNumber} has been reserved for {numberOfPeople} people".TrimEnd();
        }

        private IDrink DrinkFactory(string type, string name, int portion, string brand)
        {
            IDrink drink = null;
            switch (type.ToLower())
            {
                case "tea":
                    return drink = new Tea(name, portion, brand);
                case "water":
                    return drink = new Water(name, portion, brand);
            }

            return drink;
        }
        private ITable TableFactory(string type, int tableNumber, int capacity)
        {
            ITable table = null;
            switch (type.ToLower())
            {
                case "insidetable":
                    return table = new InsideTable(tableNumber, capacity);
                case "outsidetable":
                    return table = new OutsideTable(tableNumber, capacity);
            }

            return table;
        }
        private IBakedFood FoodFactory(string type, string name, decimal price)
        {
            IBakedFood food = null;
            switch (type.ToLower())
            {
                case "bread":
                    return food = new Bread(name, price);
                case "cake":
                    return food = new Cake(name, price);
            }

            return food;
        }
    }
}
