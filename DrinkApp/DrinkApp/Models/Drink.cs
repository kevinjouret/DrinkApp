using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkApp.Models
{
    public class Drink
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
    }
}
