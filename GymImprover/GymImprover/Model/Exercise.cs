using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
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

        public Exercise(){ }

        public Exercise(string name, int reps, int weight, int sets)
        {
            _name = name;
            _reps = reps;
            _weight = weight;
            _sets = sets;
        }

        private int _id;

        [Column(DbType = "INT NOT NULL IDENTITY", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id
        {
            get { return _id; }
            set
            {
                RaisePropertyChanging("Id");
                this._id = value;
                RaisePropertyChanged("Id");
            }
        }

        [Column]
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
        [Column]
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
        [Column]
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
        [Column]
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

        [Column] internal int _workoutId;
        private EntityRef<Workout> _workout;
        [Association(Storage = "_workout", OtherKey = "Id", ThisKey = "Id", IsForeignKey = true)]

        public Workout Workout
        {
            get { return _workout.Entity; }
            set
            {
                RaisePropertyChanging("Workout");
                _workout.Entity = value;
                if (value != null)
                {
                    _workoutId = value.Id;
                }
                RaisePropertyChanged("Workout");
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
