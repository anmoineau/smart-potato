using SmartPotato.Core;
using SmartPotato.MVVM.Model;
using System;
using System.Collections.Generic;
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


        /**** Relay Commands ****/
        public RelayCommand? AddDaysCommand { get; set; }

        /*** CanExecute ***/
        private bool isAddDaysEnabled = true;

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
        }
    }
}
