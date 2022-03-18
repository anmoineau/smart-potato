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
                    break;
                if (menu.Exists(m => m.Recipe.UID == recipe.UID))
                    break;
                if (!recipe.IsSeasonal())
                    break;
                recipesTodo.Add(recipe);
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
    }
}
