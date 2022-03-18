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
            Recipe recipe = new(1)
            {
                Name = "Smart potatoes",
                Ingredients = "Potatoes",
                Instructions = "Boil them.",
                ExpiryIndex = ExpiryIndexes.NORMAL,
                Season = Seasons.ANY,
                LastMade = DateTime.Now
            };
            Seasons HotSeasons = Seasons.SUMMER | Seasons.SPRING;
            Debug.WriteLine(recipe.ToString());
            Debug.WriteLine(recipe.Season.HasFlag(HotSeasons));
        }
    }
}
