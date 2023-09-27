using JustChores.MobileApp.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustChores.MobileApp.Services
{
    public class MainRepository
    {
        static string FilePath => $"{FileSystem.Current.AppDataDirectory}/main.db";

        public int? InsertChore(Chore newChore)
        {
            newChore.CreatedOn = DateTime.UtcNow;
            using var db = new LiteDatabase(FilePath);
            var collection = db.GetCollection<Chore>();
            return collection.Insert(newChore);
        }

        public IEnumerable<Chore> GetChores()
        {
            using var db = new LiteDatabase(FilePath);
            return db.GetCollection<Chore>().FindAll().ToArray();
        }

        public Chore Completed(int id)
        {
            using var db = new LiteDatabase(FilePath);
            var collection = db.GetCollection<Chore>();
            var item = collection.FindById(id);

            collection.Update(item);
            return item;
        }

        public bool UpdateChore(Chore updatedChore)
        {
            updatedChore.UpdatedOn = DateTime.UtcNow;
            using var db = new LiteDatabase(FilePath);
            var collection = db.GetCollection<Chore>();
            return collection.Update(updatedChore);
        }

        public bool DeleteChore(int choreId)
        {
            using var db = new LiteDatabase(FilePath);
            var collection = db.GetCollection<Chore>();
            return collection.Delete(choreId);
        }
    }

}
