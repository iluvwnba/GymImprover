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

        public Workout()
        {
        }
        public Workout(string name, User user)
        {
            this._name = name;
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

            /*
             * 
             * 
             * 
             *         private EntitySet<User> _user;

        [Association(Storage = "_user", OtherKey = "_workoutId", ThisKey = "Id")]
        public EntitySet<User> User
        {
            get { return _user; }
            set { this._user.Assign(value); }
        }

        private void attach_User(User user)
        {
            RaisePropertyChanging("User");
            user.Workout = this;
        }

        private void detatch_User(User user)
        {
            RaisePropertyChanging("User");
            user.Workout = null;
        }
             * 
             * 
        [Column]
        internal int _exerciseId;

        private EntityRef<Exercise> _exercise;

        [Association(Storage = "_workout", ThisKey = "_exerciseId", OtherKey = "ExerciseId", IsForeignKey = true)]
        public Exercise Exercise
        {
            get { return _exercise.Entity; }
            set
            {
                RaisePropertyChanging("Exercise");
                _exercise.Entity = value;
                if (value != null)
                {
                    _exercise = value.Id;
                }
                RaisePropertyChanged("Exercise");
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
