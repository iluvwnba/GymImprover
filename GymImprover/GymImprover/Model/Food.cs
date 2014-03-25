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

        private int _calories = 0;
        private int _protein = 0;
        private int _fat = 0;
        private int _carbohydrates = 0;

        public int Calories {
            get { return _calories; }
            set
            {
                if (this._calories != value)
                {
                    this._calories = value;
                    this.RaisePropertyChanged("Calories");
                }
            }
        }
        public int Fats {
            get { return _fat; }
            set 
            {
                if (this._fat != value)
                {
                    this._fat = value;
                    this.RaisePropertyChanged("Fats");
                }
            } 
        }
        public int Protein {
            get { return _protein; }
            set   
            {
                if (this._protein != value)
                {
                    this._protein = value;
                    this.RaisePropertyChanged("Protein");
                }
            } 
        }

        public int Carbohydrates {
            get { return _carbohydrates; } 
            set
            {
                if (this._carbohydrates != value)
                {
                    this._carbohydrates = value;
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
