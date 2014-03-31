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
    public class Workout : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private string _name;
        private DateTime _theDateTime;


        public Workout()
        {
            this.LoadActions();
        }
        public Workout(string name, User user)
        {
            this._name = name;
            this._theDateTime = DateTime.Today;
            this.LoadActions();
        }

        private int _id;

        [Column(DbType = "INT NOT NULL IDENTITY", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id
        {
            get { return _id; }
            set
            {
                RaisePropertyChanging("Id");
                _id = value;
                RaisePropertyChanged("Id");
            }
        }

        [Column]
        public string Name
        {
            get { return _name; }
            set
            {
                RaisePropertyChanging("Name");
                _name = value;
                RaisePropertyChanged("Name");
            }
        }

        [Column]
        public string Date
        {
            get { return _theDateTime.ToShortDateString(); }
            private set { }
        }

        [Column] internal int _userId;
        private EntityRef<User> _user;

        [Association(Storage = "_user", OtherKey = "UserId", ThisKey = "_userId", IsForeignKey = true)]
        public User User
        {
            get { return _user.Entity; }
            set
            {
                RaisePropertyChanging("User");
                _user.Entity = value;
                if (value != null)
                {
                    _userId = value.UserId;
                }
                RaisePropertyChanged("User");
            }
        }

        private EntitySet<Exercise> _exercises;

        [Association(Storage = "_exercises", OtherKey = "_workoutId", ThisKey = "Id")]
        public EntitySet<Exercise> Exercises
        {
            get { return this._exercises; }
            set { this._exercises.Assign(value);}
        }

        private void attach_Exercise(Exercise exercise)
        {
           RaisePropertyChanging("Exercises");
            exercise.Workout = this;
        }

        private void detach_exercise(Exercise exercise)
        {
            RaisePropertyChanging("Exercises");
            exercise.Workout = null;
        }

        private void LoadActions()
        {
            _exercises = new EntitySet<Exercise>(
                new Action<Exercise>(this.attach_Exercise),
                new Action<Exercise>(this.detach_exercise)
                );
        }
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
