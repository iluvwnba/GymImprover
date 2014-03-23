using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymImprover.Model
{
    public class Food : INotifyPropertyChanged
    { 

        private int calories = 0;
        private int protein = 0;
        private int fat = 0;
        private int carbohydrates = 0;

        public int Calories {
            get { return calories; }
            set
            {
                if (this.calories != value)
                {
                    this.calories = value;
                    this.RaisePropertyChanged("Calories");
                }
            }
        }
        public int Fats {
            get { return fat; }
            set 
            {
                if (this.fat != value)
                {
                    this.fat = value;
                    this.RaisePropertyChanged("Fats");
                }
            } 
        }
        public int Protein {
            get { return protein; }
            set   
            {
                if (this.protein != value)
                {
                    this.protein = value;
                    this.RaisePropertyChanged("Protein");
                }
            } 
        }

        public int Carbohydrates {
            get { return carbohydrates; } 
            set
            {
                if (this.carbohydrates != value)
                {
                    this.carbohydrates = value;
                    this.RaisePropertyChanged("Carbohydrates");
                }
            } 
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
