using Bakery.Core.Contracts;
using Bakery.Models.Drinks;
using Bakery.Models.Tables;
using Bakery.Models.BakedFoods;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Bakery.Core
{
    public class Controller : IController
    {
        private List<BakedFood> bakedFoods;
        private List<Drink> drinks;
        private List<Table> tables;
        private decimal totalIncome = 0;
        public Controller()
        {
            bakedFoods = new List<BakedFood>();
            drinks = new List<Drink>();
            tables = new List<Table>();
        }

        public string AddDrink(string type, string name, int portion, string brand)
        {
            Drink drink;
            if (type == "Water")
            {
                drink = new Water(name, portion, brand);
                drinks.Add(drink);
            }
            else if (type == "Tea")
            {
                drink = new Tea(name, portion, brand);
                drinks.Add(drink);
            }
            return $"Added {name} ({brand}) to the drink menu";
        }

        public string AddFood(string type, string name, decimal price)
        {
            BakedFood food;
            if (type == "Cake")
            {
                food = new Cake(name, price);
                bakedFoods.Add(food);
            }
            else if (type == "Bread")
            {
                food = new Bread(name, price);
                bakedFoods.Add(food);
            }
            return $"Added {name} ({type}) to the menu";
        }

        public string AddTable(string type, int tableNumber, int capacity)
        {
            Table table;
            if (type == "OutsideTable")
            {
                table = new OutsideTable(tableNumber, capacity);
                tables.Add(table);
            }
            else if (type == "InsideTable")
            {
                table = new InsideTable(tableNumber, capacity);
                tables.Add(table);
            }
            return $"Added table number {tableNumber} in the bakery";
        }

        public string GetFreeTablesInfo()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var table in tables)
            {
                if(table.IsReserved == false)
                {
                    sb.AppendLine(table.GetFreeTableInfo());
                }
            }
            return sb.ToString().Trim();
        }

        public string GetTotalIncome()
        {
            return $"Total income: {totalIncome:f2}lv";
        }

        public string LeaveTable(int tableNumber)
        {
            decimal billPrice = tables.First(t => t.TableNumber == tableNumber).GetBill();
            this.totalIncome += billPrice;
            tables.First(t => t.TableNumber == tableNumber).Clear();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Table: {tableNumber}");
            sb.AppendLine($"Bill: {billPrice:f2}");

            return sb.ToString().Trim();
        }

        public string OrderDrink(int tableNumber, string drinkName, string drinkBrand)
        {
            if (tables.FirstOrDefault(t => t.TableNumber == tableNumber) == null)
            {
                return $"Could not find table {tableNumber}";
            }
            else if (drinks.FirstOrDefault(d => d.Name == drinkName) == null ||
                     drinks.FirstOrDefault(d => d.Brand == drinkBrand) == null)
            {
                return $"There is no {drinkName} {drinkBrand} available";
            }
            else
            {
                tables.First(t => t.TableNumber == tableNumber)
                    .OrderDrink(drinks.First(d => d.Name == drinkName));

                return $"Table {tableNumber} ordered {drinkName} {drinkBrand}";
            }
        }

        public string OrderFood(int tableNumber, string foodName)
        {
            if (tables.FirstOrDefault(t => t.TableNumber == tableNumber) == null)
            {
                return $"Could not find table {tableNumber}";
            }
            else if (bakedFoods.FirstOrDefault(f => f.Name == foodName) == null)
            {
                return $"No {foodName} in the menu";
            }
            else
            {
                tables.First(t => t.TableNumber == tableNumber)
                    .OrderFood(bakedFoods.First(f => f.Name == foodName));

                return $"Table {tableNumber} ordered {foodName}";
            }
        }

        public string ReserveTable(int numberOfPeople)
        {
            for (int i = 0; i < tables.Count; i++)
            {
                if (tables[i].IsReserved == false)
                {
                    if (tables[i].Capacity > numberOfPeople)
                    {
                        tables[i].Reserve(numberOfPeople);
                        return $"Table {tables[i].TableNumber} has been reserved for {numberOfPeople} people";
                    }
                }
            }
            return $"No available table for {numberOfPeople} people";
        }
    }
}
