using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPotato.MVVM.Model
{
    internal static class MenuHandler
    {
        /**** Constants ****/

        public static uint MENU_SIZE { get; } = 7;

        /**** Properties ****/

        private static List<Recipe> recipeBook = new();
        public static List<Recipe> RecipeBook
        {
            get { return recipeBook; }
            set { recipeBook = value; }
        }

        private static List<Recipe> recipesTodo = new();
        public static List<Recipe> RecipesTodo
        {
            get { return recipesTodo; }
            set { recipesTodo = value; }
        }

        private static List<Recipe> recipesDone = new();
        public static List<Recipe> RecipesDone
        {
            get { return recipesDone; }
            set { recipesDone = value; }
        }

        private static List<Meal> menu = new();
        public static List<Meal> Menu
        {
            get { return menu; }
            set { menu = value; }
        }

        /**** Methods ****/

        public static void ComputeRecipesTodo()
        {
            foreach (var recipe in RecipeBook)
            {
                if (recipesDone.Exists(r => r.UID == recipe.UID))
                    continue;
                if (Menu.Exists(m => m.Recipe.UID == recipe.UID))
                    continue;
                if (!recipe.IsSeasonal())
                    continue;
                recipesTodo.Add(recipe);
            }
        }

        public static void ComputeMenu()
        {
            // Add one monthly recipe if available.
            var monthlies = recipesTodo.FindAll(r => r.Frequency == Recipe.Frequencies.MONTHLY);
            if(monthlies.Count > 0 && Menu.Count < MENU_SIZE)
                Menu.Add(new Meal(monthlies.First()));
            // Add one biweekly recipe if available.
            var biweeklies = recipesTodo.FindAll(r => r.Frequency == Recipe.Frequencies.BIWEEKLY);
            if(biweeklies.Count > 0 && Menu.Count < MENU_SIZE)
                Menu.Add(new Meal(biweeklies.First()));
            // Fill the rest with available weekly recipes.
            var weeklies = new Queue<Recipe> (recipesTodo.FindAll(r => r.Frequency == Recipe.Frequencies.WEEKLY));
            while (Menu.Count < MENU_SIZE)
            {
                if (weeklies.Count > 0)
                    Menu.Add(new Meal(weeklies.Dequeue()));
                else
                    break;
            }
        }

        public static string PrintRecipeBook()
        {
            string format = "";
            format += "Recipe Book :\n\n";
            foreach (var recipe in RecipeBook)
            {
                format += recipe.ToString() + "\n";
            }
            return format;
        }

        public static string PrintRecipesTodo()
        {
            string format = "";
            format += "Recipes To Do :\n\n";
            foreach (var recipe in RecipesTodo)
            {
                format += recipe.ToString() + "\n";
            }
            return format;
        }

        public static string PrintMenu()
        {
            string format = "";
            format += "Menu :\n\n";
            foreach (var meal in Menu)
            {
                format += meal.ToString() + "\n";
            }
            return format;
        }
    }
}
