using SmartPotato.Core;
using SmartPotato.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPotato.MVVM.ViewModel
{
    internal class ParametersViewModel : ObservableObject
    {
        public DateTime CurrentTime
        {
            get { return TimeProvider.CurrentTime; }
            set { TimeProvider.CurrentTime = value; OnPropertyChanged(); }
        }

        public string AppDataPath { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"SmartPotato\");

        /**** Relay Commands ****/
        public RelayCommand? AddDaysCommand { get; set; }
        public RelayCommand? OpenAppDataCommand { get; set; }

        /*** CanExecute ***/
        private bool isAddDaysEnabled = true;
        private bool isOpenAppDataEnabled = true;

        /**** Constructor ****/
        public ParametersViewModel()
        {
            InitializeViewCommands();
        }

        /**** Methods ****/
        private void InitializeViewCommands()
        {
            AddDaysCommand = new RelayCommand(o =>
            {
                TimeProvider.AddDays(int.Parse(o.ToString()?? "0"));
                OnPropertyChanged("CurrentTime");
            }, canExecute => isAddDaysEnabled);

            OpenAppDataCommand = new RelayCommand(o =>
            {
                Process.Start("explorer.exe", AppDataPath);
            }, canExecute => isOpenAppDataEnabled);
        }
    }
}
