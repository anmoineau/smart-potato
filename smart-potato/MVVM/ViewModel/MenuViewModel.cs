﻿using SmartPotato.Core;
using SmartPotato.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPotato.MVVM.ViewModel
{
    internal class MenuViewModel : ObservableObject
    {
        /**** Properties ****/

        private List<Meal>? menu;
        public List<Meal>? Menu
        {
            get { return menu; }
            set { menu = value; OnPropertyChanged(); }
        }

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
