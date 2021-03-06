using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPotato.MVVM.Model
{
    internal class Recipe
    {
        /**** Properties ****/

        private uint uid;
        public uint UID
        {
            get { return uid; }
            set { uid = value; }
        }

        private string name = "empty";
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string ingredients = "empty";
        public string Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; }
        }

        private string instructions = "empty";
        public string Instructions
        {
            get { return instructions; }
            set { instructions = value; }
        }

        private Frequencies frequency;
        public Frequencies Frequency
        {
            get { return frequency; }
            set { frequency = value; }
        }

        private ExpiryIndexes expiryIndex;
        public ExpiryIndexes ExpiryIndex
        {
            get { return expiryIndex; }
            set { expiryIndex = value; }
        }

        private Seasons season;
        public Seasons Season
        {
            get { return season; }
            set { season = value; }
        }

        private DateTime lastMade;
        public DateTime LastMade
        {
            get { return lastMade; }
            set { lastMade = value; }
        }

        /**** Methods ****/

        public bool IsReady()
        {
            TimeSpan timeDelta = TimeProvider.CurrentTime - LastMade;
            switch (Frequency)
            {
                case Frequencies.WEEKLY:
                    TimeSpan weekSpan = new(7, 0, 0, 0);
                    if (timeDelta >= weekSpan)
                        return true;
                    break;
                case Frequencies.BIWEEKLY:
                    TimeSpan biweekSpan = new(14, 0, 0, 0);
                    if (timeDelta >= biweekSpan)
                        return true;
                    break;
                case Frequencies.MONTHLY:
                    TimeSpan monthSpan = new(28,0,0,0);
                    if (timeDelta >= monthSpan)
                        return true;
                    break;
                default:
                    break;
            };
            return false;
        }

        public bool IsSeasonal()
        {
            return Season.HasFlag(GetSeason(TimeProvider.CurrentTime));
        }

        private static Seasons GetSeason(DateTime date)
        {
            float value = (float)date.Month + date.Day / 100f;
            if (value < 3.21 || value >= 12.22) return Seasons.WINTER;
            if (value < 6.21) return Seasons.SPRING;
            if (value < 9.23) return Seasons.SUMMER;
            return Seasons.FALL;
        }

        public override string ToString()
        {
            string format = "Recipe \t\t\t: {0} \nName \t\t\t: {1} \nIngredients \t: {2} \nInstructions \t: {3} \nFrequency \t\t: {4} \nExpiryIndex \t: {5} \nSeason \t\t\t: {6} \nLastMade \t\t: {7}\n";
            return string.Format(format, UID, Name, ingredients, Instructions, Frequency, ExpiryIndex, Season, LastMade);
        }

        /**** Enums ****/

        internal enum Frequencies : short
        {
            WEEKLY   = 0,
            BIWEEKLY = 1,
            MONTHLY  = 2
        }

        internal enum ExpiryIndexes : short
        {
            LOW      = 0,
            NORMAL   = 1,
            CRITICAL = 2
        }

        [Flags]
        internal enum Seasons : short
        {
            NONE   = 0,
            WINTER = 1,
            SPRING = 2,
            SUMMER = 4,
            FALL   = 8,
            ANY    = 15,
        }
    }
}
