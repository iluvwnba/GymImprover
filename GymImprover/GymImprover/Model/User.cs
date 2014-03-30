using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace GymImprover.Model{

    [Table]
    public class User : INotifyPropertyChanged, INotifyPropertyChanging
    {
        //Constructor
        public User(string name, int weight, string userName, string password)
        {
            this.Name = name;
            this.Weight = weight;
            this.Username = userName;
            this.Password = password;
            this.loadActions();
        }
        public User() { this.loadActions(); }

        private int _userId;
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int UserId
        {
            get { return _userId; }
            set
            {
                if (_userId != value)
                {
                    RaisePropertyChanging("UserId");
                    this._userId = value;
                    RaisePropertyChanged("UserId");
                }
            }
        }

        private string _name;
        [Column]
        public string Name {
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

        private int _weight;
        [Column]
        public int Weight { 
            get{return _weight;}
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

        private string _username;
        [Column]
        public string Username
        { 
            get {return _username;}
            set
            {
                if (this._username != value)
                {
                    RaisePropertyChanging("Username");
                    this._username = value;
                    this.RaisePropertyChanged("Username");
                }
            }
            
        }

        private string _password;
        [Column]
        public string Password {
            get { return _password; }
            set
            {
                if (this._password != value)
                {
                    RaisePropertyChanging("Password");
                    this._password = value;
                    this.RaisePropertyChanged("Password");
                }
            } 
        }



        // Link to Food
        [Column] 
        internal int _foodId;

        private EntityRef<Food> _food;

        [Association(Storage = "_food", ThisKey = "_foodId", OtherKey = "Id", IsForeignKey = true)]
        public Food Food
        {
            get { return _food.Entity; }
            set
            {
                RaisePropertyChanging("Food");
                _food.Entity = value;
                if (value != null)
                {
                    _foodId = value.Id;
                }
                RaisePropertyChanged("Food");
            }
        }

        // Link to Workouts
        private EntitySet<Workout> _workouts;

        [Association(Storage = "_workouts", OtherKey = "_userId", ThisKey = "UserId")]
        public EntitySet<Workout> Workouts
        {
            get { return this._workouts; }
            set { this._workouts.Assign(value);}
        }

        private void attach_Workout(Workout workout)
        {
            RaisePropertyChanging("Workout");
            workout.User = this;
        }

        private void detach_Workout(Workout workout)
        {
            RaisePropertyChanging("Workout");
            workout.User = null;
        }

        private void loadActions()
        {
            _workouts = new EntitySet<Workout>(
                new Action<Workout>(this.attach_Workout),
                new Action<Workout>(this.detach_Workout)
                );
        }
            /*
        [Column]
        internal int _workoutId;

        private EntityRef<Workout> _workout;

        [Association(Storage = "_workout", ThisKey = "_workoutId", OtherKey = "Id", IsForeignKey = true)]
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
        */

        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;


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
