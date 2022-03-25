using SmartPotato.Core;
using SmartPotato.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SmartPotato.MVVM.ViewModel
{
    internal class RecipeViewModel
    {
        /**** Properties ****/
        public Recipe CurrentRecipe { get; set; }

        /**** RelayCommand ****/
        public RelayCommand? CloseWindowCommand { get; set; }

        /*** CanExecute ***/
        private bool isCloseWindowEnabled = true;

        /**** Constructor ****/
        public RecipeViewModel(uint UID)
        {
            InitializeViewCommands();
            List<Recipe> recipes = MenuHandler.GetInstance.RecipeBook.ToList();
            CurrentRecipe = recipes.Find(recipe => recipe.UID == UID) ?? new();
        }

        /**** Methods ****/
        private void InitializeViewCommands()
        {
            CloseWindowCommand = new RelayCommand(o =>
            {
                if (o != null)
                    ((Window)o).Close();
            }, canExecute => isCloseWindowEnabled);
        }
    }
}
