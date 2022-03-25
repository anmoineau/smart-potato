using CsvHelper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPotato.MVVM.Model
{
    internal static class OutputHandler
    {
        /**** Constants ****/
        public static string MenuRecordPath { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"SmartPotato\Menu.csv");
        public static string DoneRecordPath { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"SmartPotato\RecipesDone.csv");

        /**** Methods ****/
        public static ObservableCollection<Recipe> GetRecipesDone(ObservableCollection<Recipe> recipeBook)
        {
            ObservableCollection<Recipe> recipes = new();
            try
            {
                using (var reader = new StreamReader(DoneRecordPath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<DoneRecord>();
                    foreach (var record in records)
                    {
                        recipes.Add(recipeBook.Where(r => r.UID == record.UID).First());
                    }
                }
            }
            catch (Exception)
            {
            }
            return recipes;
        }

        public static void ExportRecipesDone(ObservableCollection<Recipe> recipes)
        {
            List<DoneRecord> DoneRecords = new();
            foreach (var recipe in recipes)
            {
                DoneRecord record = new()
                {
                    UID = recipe.UID,
                    Name = recipe.Name
                };
                DoneRecords.Add(record);
            }
            using (var writer = new StreamWriter(DoneRecordPath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(DoneRecords);
            }
        }

        private class DoneRecord
        {
            public uint UID { get; set; }
            public string? Name { get; set; }
        }

        public static ObservableCollection<Meal> GetMenu(ObservableCollection<Recipe> recipeBook)
        {
            ObservableCollection<Meal> menu = new();
            try
            {
                using (var reader = new StreamReader(MenuRecordPath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<MealRecord>();
                    foreach (var record in records)
                    {
                        Meal meal = new(recipeBook.Where(r => r.UID == record.UID).First(), record.IsDone, record.DoneDate);
                        menu.Add(meal);
                    }
                }
            }
            catch (Exception)
            {

            }
            return menu;
        }

        public static void ExportMenu(ObservableCollection<Meal> menu)
        {
            List<MealRecord> MenuRecord = new();
            foreach (var meal in menu)
            {
                MealRecord record = new()
                {
                    UID = meal.Recipe.UID,
                    Name = meal.Recipe.Name,
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
            public string? Name { get; set; }
            public bool IsDone { get; set; }
            public DateTime DoneDate { get; set; }
        }
    }
}
