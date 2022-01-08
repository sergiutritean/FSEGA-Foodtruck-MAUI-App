using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foodtrucks_final.Models;

namespace Foodtrucks_final.Data
{
    public class FoodtruckDatabase
    {
        static SQLiteAsyncConnection Database;
        public static readonly AsyncLazy<FoodtruckDatabase> Instance =
            new AsyncLazy<FoodtruckDatabase>(async () =>
            {
                var instance = new FoodtruckDatabase();
                try
                {
                    CreateTableResult result = await Database.CreateTableAsync<Foodtruck>();
                }
                catch (Exception ex)
                {
                    throw;
                }
                return instance;
            });

        public FoodtruckDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public Task<List<Foodtruck>> GetItemsAysnc()
        {
            return Database.Table<Foodtruck>().ToListAsync();
        }

        public Task<Foodtruck> GetItemAsync(int id)
        {
            return Database.Table<Foodtruck>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Foodtruck item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Foodtruck item)
        {
            return Database.DeleteAsync(item);
        }
    
    }
}

