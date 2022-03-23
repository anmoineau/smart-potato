using SmartPotato.Core;
using SmartPotato.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPotato.MVVM.ViewModel
{
    internal class PlanningViewModel
    {
        public ObservableCollection<Recipe> RecipesDone { get; set; }
        public ObservableCollection<Recipe> RecipesTodo { get; set; }

        /**** Relay Commands ****/

        /*** CanExecute ***/

        /**** Constructor ****/
        public PlanningViewModel()
        {
            InitializeViewCommands();
            RecipesDone = MenuHandler.GetInstance.RecipesDone;
            RecipesTodo = MenuHandler.GetInstance.RecipesTodo;
        }

        /**** Methods ****/
        private void InitializeViewCommands()
        {
        }
    }
}
