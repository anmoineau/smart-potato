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
        public RelayCommand? PlanningViewCommand { get; set; }
        public RelayCommand? RecipeBookViewCommand { get; set; }
        public RelayCommand? ParametersViewCommand { get; set; }
        public RelayCommand? AboutViewCommand { get; set; }

        /*** ViewModels ***/
        public MenuViewModel? MenuVM { get; set; }
        public PlanningViewModel? PlanningVM { get; set; }
        public RecipeBookViewModel? RecipeBookVM { get; set; }
        public ParametersViewModel? ParametersVM { get; set; }
        public AboutViewModel? AboutVM { get; set; }

        /*** CanExecute ***/
        private bool isMenuEnabled = true;
        private bool isPlanningEnabled = true;
        private bool isRecipeBookEnabled = true;
        private bool isParametersEnabled = true;
        private bool isAboutEnabled = true;

        /**** Constructor ****/
        public MainViewModel()
        {
            Version? version = Assembly.GetExecutingAssembly().GetName().Version;    // Get assembly version.
            AssemblyVersion = $"{version!.Major}.{version!.Minor}.{version!.Build}";   // Format assembly version.
            InitializeViewModels();
            InitializeViewCommands();
            CurrentView = AboutVM!;
        }

        /**** Methods ****/
        private void InitializeViewModels()
        {
            MenuVM = new MenuViewModel();
            PlanningVM = new PlanningViewModel();
            RecipeBookVM = new RecipeBookViewModel();
            ParametersVM = new ParametersViewModel();
            AboutVM = new AboutViewModel();
        }

        private void InitializeViewCommands()
        {
            MenuViewCommand = new RelayCommand(o =>
            {
                CurrentView = MenuVM;
            }, canExecute => isMenuEnabled);

            PlanningViewCommand = new RelayCommand(o =>
            {
                CurrentView = PlanningVM;
            }, canExecute => isPlanningEnabled);

            RecipeBookViewCommand = new RelayCommand(o =>
            {
                CurrentView = RecipeBookVM;
            }, canExecute => isRecipeBookEnabled);

            ParametersViewCommand= new RelayCommand(o =>
            {
                CurrentView = ParametersVM;
            }, canExecute => isParametersEnabled);

            AboutViewCommand= new RelayCommand(o =>
            {
                CurrentView = AboutVM;
            }, canExecute => isAboutEnabled);
        }
    }
}
