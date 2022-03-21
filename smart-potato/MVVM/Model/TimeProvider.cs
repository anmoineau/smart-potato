using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPotato.MVVM.Model
{
    internal static class TimeProvider
    {
        public static DateTime CurrentTime
        {
            get { return Properties.Settings.Default.Time; }
            set {
                Properties.Settings.Default.Time = value;
                Properties.Settings.Default.Save();
            }
        }
    }
}
