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

        /**** Constructors ****/

        public Recipe(uint _UID)
        {
            UID = _UID;
        }

        /**** Methods ****/

        public override string ToString()
        {
            string format = "Recipe \t\t\t: {0} \nName \t\t\t: {1} \nIngredients \t: {2} \nInstructions \t: {3} \nExpiryIndex \t: {4} \nSeason \t\t\t: {5} \nLastMade \t\t: {6} \n";
            return string.Format(format, UID, Name, ingredients, Instructions, ExpiryIndex, Season, LastMade);
        }

    }

    internal enum ExpiryIndexes : short
    {
        LOW = 0,
        NORMAL = 1,
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
