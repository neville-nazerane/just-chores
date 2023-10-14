using Android.Widget;
using CommunityToolkit.Maui.Storage;
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

        public Chore GetChore(int id)
        {
            using var db = new LiteDatabase(FilePath);
            var collection = db.GetCollection<Chore>();
            var item = collection.FindById(id);
            return item;
        }

        public Chore Completed(int id, DateTime? completedOn = null)
        {
            using var db = new LiteDatabase(FilePath);
            var collection = db.GetCollection<Chore>();
            var item = collection.FindById(id);
            completedOn ??= item.DueOn;

            int day = completedOn.Value.Day;
            int month = completedOn.Value.Month;
            int year = completedOn.Value.Year;

            switch (item.FrequencyType)
            {
                case FrequencyType.Day:
                    item.DueOn = completedOn.Value.AddDays(1);
                    break;
                case FrequencyType.Week:
                    item.DueOn = completedOn.Value.AddDays(7);
                    break;
                case FrequencyType.Month:
                    var monthDate = new DateTime(year, month, 1).AddMonths(1);

                    item.DueOn = new DateTime(
                        monthDate.Year,
                        monthDate.Month,
                        Math.Min(DateTime.DaysInMonth(monthDate.Year, monthDate.Month), day)
                    );
                    break;
                case FrequencyType.Year:
                    var yearDate = new DateTime(year, month, 1).AddYears(1);

                    item.DueOn = new DateTime(
                        yearDate.Year,
                        yearDate.Month,
                        Math.Min(DateTime.DaysInMonth(yearDate.Year, yearDate.Month), day)
                    );
                    break;
            }

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
    
        public async Task BackupAsync()
        {
            //await FileSaver.save
            await using var stream = File.OpenRead(FilePath);

            var permRead = await Permissions.RequestAsync<Permissions.StorageWrite>();

            if (permRead == PermissionStatus.Granted)
            {
                var permWrite = await Permissions.RequestAsync<Permissions.StorageWrite>();
                if (permWrite == PermissionStatus.Granted)
                {
                    var res = await FileSaver.Default.SaveAsync("data.db", stream, CancellationToken.None);

                }
            }
        }
    
    }

}
