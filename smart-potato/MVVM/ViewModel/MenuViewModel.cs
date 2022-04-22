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
        public RelayCommand? SoftEraseCommand { get; set; }
        public RelayCommand? HardEraseCommand { get; set; }

        /*** CanExecute ***/
        private bool isRenewEnabled = true;
        private bool isSoftEraseEnabled = true;
        private bool isHardEraseEnabled = true;

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

            SoftEraseCommand = new RelayCommand(o =>
            {
                MenuHandler.GetInstance.MoveMealToToDo((uint)o);
            }, canExecute => isSoftEraseEnabled);

            HardEraseCommand = new RelayCommand(o =>
            {
                MenuHandler.GetInstance.MoveMealToDone((uint)o);
            }, canExecute => isHardEraseEnabled);
        }
    }
}
