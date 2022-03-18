using SmartPotato.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPotato.MVVM.ViewModel
{
    internal class MainViewModel
    {
        public MainViewModel()
        {
            try
            {
                List<Recipe> recipes = RecipeBookParser.ReadRecipeBook();
                foreach (var _recipe in recipes)
                {
                    Debug.WriteLine(_recipe.ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
