using SmartPotato.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPotato.MVVM.Model
{
    internal class Meal : ObservableObject
    {
        /**** Properties ****/

        private Recipe recipe = new();
        public Recipe Recipe
        {
            get { return recipe; }
            set { recipe = value; }
        }

        private bool isDone;
        public bool IsDone
        {
            get { return isDone; }
            set {
                isDone = value;
                if(isDone)
                    DoneDate = TimeProvider.CurrentTime;
                OnPropertyChanged();
            }
        }

        private DateTime doneDate;
        public DateTime DoneDate
        {
            get { return doneDate; }
            set { doneDate = value; }
        }

        /**** Constructors ****/

        public Meal(Recipe recipe)
        {
            this.recipe = recipe;
        }

        /**** Methods ****/

        public void ArchiveMeal()
        {
            recipe.LastMade = doneDate;
        }

        public override string ToString()
        {
            return recipe.ToString() + "Done \t\t\t: " + IsDone + "\nDoneDate \t\t: " + DoneDate + "\n";
        }
    }
}
