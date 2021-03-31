using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using RecetteMaster.Models;

namespace RecetteMaster.Data
{
    public class RecettesDatabase
    {
        readonly SQLiteAsyncConnection database;

        public RecettesDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Recette>().Wait();
        }

        public Task<List<Recette>> GetRecettesAsync()
        {
            //Get all Recettes.
            return database.Table<Recette>().ToListAsync();
        }

        public Task<Recette> GetRecetteAsync(int id)
        {
            // Get a specific Recette.
            return database.Table<Recette>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveRecetteAsync(Recette recette)
        {
            if (recette.Id != 0)
            {
                // Update an existing Recette.
                return database.UpdateAsync(recette);
            }
            else
            {
                // Save a new Recette.
                return database.InsertAsync(recette);
            }
        }

        public Task<int> DeleteRecetteAsync(Recette recette)
        {
            // Delete a Recette.
            return database.DeleteAsync(recette);
        }
    }
}