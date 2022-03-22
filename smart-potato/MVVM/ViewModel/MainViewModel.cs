using SmartPotato.Core;
using SmartPotato.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmartPotato.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        /**** Properties ****/
        private object? currentView;
        public object? CurrentView
        {
            get { return currentView; }
            set 
            { 
                currentView = value;
                OnPropertyChanged();
            }
        }

        public string AssemblyVersion { get; set; }

        /**** Relay Commands ****/
        public RelayCommand? MenuViewCommand { get; set; }

        /*** ViewModels ***/
        public MenuViewModel? MenuVM { get; set; }

        /*** CanExecute ***/
        private bool isMenuEnabled = true;

        /**** Constructor ****/
        public MainViewModel()
        {
            Version? version = Assembly.GetExecutingAssembly().GetName().Version;    // Get assembly version.
            AssemblyVersion = $"{version!.Major}.{version!.Minor}.{version!.Build}";   // Format assembly version.
            InitializeViewModels();
            InitializeViewCommands();
            CurrentView = MenuVM!;
        }

        /**** Methods ****/
        private void InitializeViewModels()
        {
            MenuVM = new MenuViewModel();
        }

        private void InitializeViewCommands()
        {
            MenuViewCommand = new RelayCommand(o =>
            {
                CurrentView = MenuVM;
            }, canExecute => isMenuEnabled);
        }
    }
}
