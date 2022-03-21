using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPotato.MVVM.Model
{
    internal static class OutputHandler
    {
        public static string MenuRecordPath { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"SmartPotato\Menu.csv");

        public static List<Meal> GetMenu(List<Recipe> recipeBook)
        {
            List<Meal> menu = new();
            try
            {
                using (var reader = new StreamReader(MenuRecordPath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<MealRecord>();
                    foreach (var record in records)
                    {
                        Meal meal = new(recipeBook.Find(r => r.UID == record.UID)!)
                        {
                            IsDone = record.IsDone,
                            DoneDate = record.DoneDate
                        };
                        menu.Add(meal);
                    }
                }
            }
            catch (Exception)
            {

            }
            return menu;
        }

        public static void ExportMenu(List<Meal> menu)
        {
            List<MealRecord> MenuRecord = new();
            foreach (var meal in menu)
            {
                MealRecord record = new()
                {
                    UID = meal.Recipe.UID,
                    IsDone = meal.IsDone,
                    DoneDate = meal.DoneDate
                };
                MenuRecord.Add(record);
            }
            using (var writer = new StreamWriter(MenuRecordPath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(MenuRecord);
            }
        }

        private class MealRecord
        {
            public uint UID { get; set; }
            public bool IsDone { get; set; }
            public DateTime DoneDate { get; set; }
        }
    }
}
