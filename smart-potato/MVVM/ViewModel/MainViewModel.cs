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
                MenuHandler.RenewMenu();
                TimeProvider.CurrentTime = TimeProvider.CurrentTime.AddDays(7);

                Debug.WriteLine(MenuHandler.PrintRecipesTodo());
                MenuHandler.ComputeMenu();
                for(int i = 0; i < 4; i++)
                {
                    MenuHandler.Menu[i].IsDone = true;
                }
                OutputHandler.ExportMenu(MenuHandler.Menu);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
