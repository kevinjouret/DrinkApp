using DrinkApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DrinkApp.Data
{
    public class Database
    {
        readonly SQLiteAsyncConnection database;

        public Database(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Drink>().Wait();
        }
        public Task<List<Drink>> GetDrinksAsync()
        {
            //Get all drinks.
            return database.Table<Drink>().ToListAsync();
        }

        public Task<Drink> GetDrinkAsync(int id)
        {
            // Get a specific drink.
            return database.Table<Drink>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveDrinkAsync(Drink drink)
        {
            // Save a new drink.
            return database.InsertAsync(drink);
        }

        public Task<int> UpdateDrinkAsync(Drink drink)
        {
            // Update an existing drink.
            return database.UpdateAsync(drink);
        }

        public Task<int> DeleteDrinkAsync(Drink drink)
        {
            // Delete a drink.
            return database.DeleteAsync(drink);
        }
    }
}
