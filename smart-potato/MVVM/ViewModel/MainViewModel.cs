using SmartPotato.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPotato.MVVM.ViewModel
{
    internal class MainViewModel
    {
        public MainViewModel()
        {
            try
            {
                MenuHandler.ComputeRecipesTodo();
                Debug.WriteLine(MenuHandler.PrintRecipesTodo());
                MenuHandler.ComputeMenu();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
