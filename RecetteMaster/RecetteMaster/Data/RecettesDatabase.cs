using System;
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
            database.CreateTableAsync<AlimentPossible>().Wait();
            database.CreateTableAsync<AlimentRecette>().Wait();
        }
        //----------- Recettes ----------------
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
        
        // ----------- ALIMENTPOSSIBLE --------------
        
        public Task<List<AlimentPossible>> GetAlimentsPossibleAsync()
        {
            //Get all Recettes.
            return database.Table<AlimentPossible>().ToListAsync();
        }

        public Task<AlimentPossible> GetAlimentPossibleAsync(int id)
        {
            // Get a specific Recette.
            return database.Table<AlimentPossible>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveAlimentPossibleAsync(AlimentPossible alimentPossible)
        {
            if (alimentPossible.Id != 0)
            {
                // Update an existing Recette.
                return database.UpdateAsync(alimentPossible);
            }
            else
            {
                // Save a new Recette.
                return database.InsertAsync(alimentPossible);
            }
        }

        public Task<int> DeleteAlimentPossibleAsync(AlimentPossible alimentPossible)
        {
            // Delete a Recette.
            return database.DeleteAsync(alimentPossible);
        }
        
        // ----------- ALIMENTPOSSIBLE --------------
        
        public Task<List<AlimentRecette>> GetAlimentPossibleRecetteAsync(int idRec)
        {
            
            // Get a specific Recette.
            return database.QueryAsync<AlimentRecette>(@"select *  
            from AlimentPossible AP inner join AlimentRecette AR on 
                 AP.Id = AR.AlimentId");
        }
        public Task<AlimentRecette> GetAlimentRecetteAsync(int idRec,int idAli)
        {
            // Get a specific Recette.
            return database.Table<AlimentRecette>()
                .Where(i => i.RecetteId == idRec&&i.AlimentId==idAli)
                .FirstOrDefaultAsync();
        }

        public async Task<int> SaveAlimentRecetteAsync(int idRecette,int idAliment)
        {
            AlimentRecette alimentRecette = new AlimentRecette();
            alimentRecette.AlimentId = idAliment;
            alimentRecette.RecetteId = idRecette;

            if (await database.FindAsync<AlimentRecette>(i =>
                i.AlimentId == alimentRecette.AlimentId && i.RecetteId == alimentRecette.RecetteId) != null)
            {
                // Update an existing Recette.
                return await database.UpdateAsync(alimentRecette);
            }

            return await database.InsertAsync(alimentRecette);
            
        }

        public Task<int> DeleteAlimentPossibleAsync(AlimentRecette alimentPossible)
        {
            // Delete a Recette.
            return database.DeleteAsync(alimentPossible);
        }
        
    }

   
}