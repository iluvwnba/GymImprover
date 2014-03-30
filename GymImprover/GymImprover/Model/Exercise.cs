using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymImprover.Model
{
    [Table]
    public class Exercise : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private string _name;
        private int _weight;
        private int _reps;
        private int _sets;

        public Exercise(string name, int reps, int weight, int sets)
        {
            _name = name;
            _reps = reps;
            _weight = weight;
            _sets = sets;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (this._name != value)
                {
                    RaisePropertyChanging("Name");
                    this._name = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }

        public int Weight
        {
            get { return _weight; }
            set
            {
                if (this._weight != value)
                {
                    RaisePropertyChanging("Weight");
                    this._weight = value;
                    this.RaisePropertyChanged("Weight");
                }
            }
        }

        public int Reps
        {
            get { return _reps; }
            set
            {
                if (this._reps != value)
                {
                    RaisePropertyChanging("Reps");
                    this._reps = value;
                    this.RaisePropertyChanged("Reps");
                }
            }
        }

        public int Sets
        {
            get { return _sets; }
            set
            {
                if (this._sets != value)
                {
                    RaisePropertyChanging("Sets");
                    this._sets = value;
                    this.RaisePropertyChanged("Sets");
                }
            }
        }
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify that a property is about to change
        private void RaisePropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }
}
