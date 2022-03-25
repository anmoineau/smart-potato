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

        /**** Relay Commands ****/

        /*** CanExecute ***/

        /**** Constructor ****/
        public RecipeBookViewModel()
        {
            InitializeViewCommands();
            RecipeBook = MenuHandler.GetInstance.RecipeBook;
        }

        /**** Methods ****/
        private void InitializeViewCommands()
        {
        }
    }
}
