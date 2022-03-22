using SmartPotato.Core;
using SmartPotato.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPotato.MVVM.ViewModel
{
    internal class MenuViewModel  :ObservableObject
    {
        private List<Meal>? menu;

        public List<Meal>? Menu
        {
            get { return menu; }
            set { menu = value; OnPropertyChanged(); }
        }

        public MenuViewModel()
        {
            Menu = MenuHandler.Menu;
            MenuHandler.RenewMenu();
        }

    }
}
