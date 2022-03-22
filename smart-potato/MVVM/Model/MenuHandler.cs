using SmartPotato.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPotato.MVVM.Model
{
    internal sealed class MenuHandler : ObservableObject
    {
        /**** Constants ****/

        public static uint MENU_SIZE { get; } = 7;

        /**** Singleton ****/

        private static MenuHandler? instance;
        public static MenuHandler GetInstance {
            get 
            { 
                if(instance == null)
                    instance = new MenuHandler();
                return instance;
            } 
        }

        /**** Properties ****/

        private List<Recipe> recipeBook = new();
        public List<Recipe> RecipeBook
        {
            get { return recipeBook; }
            set { recipeBook = value; }
        }

        private List<Recipe> recipesTodo = new();
        public List<Recipe> RecipesTodo
        {
            get { return recipesTodo; }
            set { recipesTodo = value; }
        }

        private List<Recipe> recipesDone = new();
        public List<Recipe> RecipesDone
        {
            get { return recipesDone; }
            set { recipesDone = value; }
        }

        private List<Meal> menu = new();
        public List<Meal> Menu
        {
            get { return menu; }
            set { menu = value; OnPropertyChanged(); }
        }

        /**** Constructor ****/
        private MenuHandler()
        {
            RecipeBook = RecipeBookParser.ReadRecipeBook();
            RecipesDone = OutputHandler.GetRecipesDone(RecipeBook)!;
            Menu = OutputHandler.GetMenu(RecipeBook)!;
        }

        /**** Methods ****/

        public void ComputeRecipesTodo()
        {
            foreach (var recipe in RecipeBook)
            {
                if (RecipesDone.Exists(r => r.UID == recipe.UID))
                    continue;
                if (Menu.Exists(m => m.Recipe.UID == recipe.UID))
                    continue;
                if (!recipe.IsSeasonal())
                    continue;
                if (!recipe.IsReady())
                    continue;
                RecipesTodo.Add(recipe);
            }
        }

        public void ComputeMenu()
        {
            if (Menu.Count >= MENU_SIZE)
                return;
            // Add one monthly recipe if available.
            var monthlies = RecipesTodo.FindAll(r => r.Frequency == Recipe.Frequencies.MONTHLY);        // TODO : check last made
            if(monthlies.Count > 0 && Menu.Count < MENU_SIZE)
                Menu.Add(new Meal(monthlies.First()));
            // Add one biweekly recipe if available.
            var biweeklies = RecipesTodo.FindAll(r => r.Frequency == Recipe.Frequencies.BIWEEKLY);      // TODO : check last made
            if (biweeklies.Count > 0 && Menu.Count < MENU_SIZE)
                Menu.Add(new Meal(biweeklies.First()));
            // Fill the rest with available weekly recipes.
            if (Menu.Count < MENU_SIZE)
                FillMenu();
            // Check if menu is filled. Reset RecipesDone if not. 
            if (Menu.Count < MENU_SIZE)
            {
                RecipesDone.Clear();
                OutputHandler.ExportRecipesDone(RecipesDone);
                ComputeRecipesTodo();
                FillMenu();
            }
            OutputHandler.ExportMenu(Menu);
            OnPropertyChanged("Menu");
        }

        private void FillMenu()
        {
            var weeklies = new Queue<Recipe>(RecipesTodo.FindAll(r => r.Frequency == Recipe.Frequencies.WEEKLY));
            while (Menu.Count < MENU_SIZE)
            {
                if (weeklies.Count > 0)
                    Menu.Add(new Meal(weeklies.Dequeue()));
                else
                    break;
            }
        }

        public void RenewMenu()
        {
            for (int i = Menu.Count-1; i >=0; i--)
            {
                Meal meal = Menu[i];
                if (meal.IsDone)
                {
                    meal.ArchiveMeal();
                    RecipesDone.Add(meal.Recipe);
                    Menu.RemoveAt(i);
                }
            }
            OutputHandler.ExportRecipesDone(RecipesDone);
            RecipeBookParser.UpdateRecipeBook(RecipeBook);
            ComputeRecipesTodo();
            ComputeMenu();
        }

        public string PrintRecipeBook()
        {
            string format = "";
            format += "Recipe Book :\n\n";
            foreach (var recipe in RecipeBook)
            {
                format += recipe.ToString() + "\n";
            }
            return format;
        }

        public string PrintRecipesTodo()
        {
            string format = "";
            format += "Recipes To Do :\n\n";
            foreach (var recipe in RecipesTodo)
            {
                format += recipe.ToString() + "\n";
            }
            return format;
        }

        public string PrintMenu()
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
