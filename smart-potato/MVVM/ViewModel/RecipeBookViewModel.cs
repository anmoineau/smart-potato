using SmartPotato.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPotato.MVVM.ViewModel
{
    internal class RecipeBookViewModel
    {
        /**** Properties ****/
        public ObservableCollection<Recipe> RecipeBook { get; set; }
        public ObservableCollection<Tuple<Recipe, Recipe?>> RecipeBookFormat { get; set; } = new();

        /**** Relay Commands ****/

        /*** CanExecute ***/

        /**** Constructor ****/
        public RecipeBookViewModel()
        {
            InitializeViewCommands();
            RecipeBook = MenuHandler.GetInstance.RecipeBook;
            FormatRecipeBook();
        }

        /**** Methods ****/
        private void InitializeViewCommands()
        {
        }

        private void FormatRecipeBook()
        {
            for (int i = 0; i < RecipeBook.Count; i+=2)
            {
                Tuple<Recipe, Recipe?> items;
                if (i+1 >= RecipeBook.Count)
                {
                    items = new(RecipeBook[i], null);
                }
                else
                {
                    items = new(RecipeBook[i], RecipeBook[i + 1]);
                }
                RecipeBookFormat.Add(items);
            }
        }
    }
}
