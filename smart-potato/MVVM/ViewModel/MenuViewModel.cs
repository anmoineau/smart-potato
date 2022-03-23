using SmartPotato.Core;
using SmartPotato.MVVM.Model;
using SmartPotato.MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPotato.MVVM.ViewModel
{
    internal class MenuViewModel : ObservableObject
    {
        public ObservableCollection<Meal> Menu { get; set; }

        /**** Relay Commands ****/
        public RelayCommand? RenewMenuCommand { get; set; }
        public RelayCommand? SelectRecipeCommand { get; set; }

        /*** CanExecute ***/
        private bool isRenewEnabled = true;
        private bool isSelectRecipeEnabled = true;

        /**** Constructor ****/
        public MenuViewModel()
        {
            InitializeViewCommands();
            Menu = MenuHandler.GetInstance.Menu;
        }

        /**** Methods ****/
        private void InitializeViewCommands()
        {
            RenewMenuCommand = new RelayCommand(o =>
            {
                MenuHandler.GetInstance.RenewMenu();
            }, canExecute => isRenewEnabled);

            SelectRecipeCommand = new RelayCommand(o =>
            {
                Debug.WriteLine("Selected : " + o.ToString());
                var win = new PopUpWindow();
                RecipeViewModel recipeVM = new();
                win.Content = recipeVM;
                win.Show();
            }, canExecute => isSelectRecipeEnabled);
        }
    }
}
