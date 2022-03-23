using SmartPotato.Core;
using SmartPotato.MVVM.Model;
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
        public RelayCommand? RenewViewCommand { get; set; }

        /*** CanExecute ***/
        private bool isRenewEnabled = true;

        /**** Constructor ****/
        public MenuViewModel()
        {
            InitializeViewCommands();
            Menu = MenuHandler.GetInstance.Menu;
        }

        /**** Methods ****/
        private void InitializeViewCommands()
        {
            RenewViewCommand = new RelayCommand(o =>
            {
                MenuHandler.GetInstance.RenewMenu();
            }, canExecute => isRenewEnabled);
        }
    }
}
