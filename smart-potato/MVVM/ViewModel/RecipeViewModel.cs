using SmartPotato.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPotato.MVVM.ViewModel
{
    internal class RecipeViewModel
    {
        public Recipe CurrentRecipe { get; set; }
        public RecipeViewModel(uint UID)
        {
            List<Recipe> recipes = MenuHandler.GetInstance.RecipeBook;
            CurrentRecipe = recipes.Find(recipe => recipe.UID == UID) ?? new();
        }
    }
}
