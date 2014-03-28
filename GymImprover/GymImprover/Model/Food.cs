using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace GymImprover.Model
{
    [Table]
    public class Food : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private int _calories = 0;
        private int _protein = 0;
        private int _fat = 0;
        private int _carbohydrates = 0;
        private int _id;

        public Food()
        {
            _users = new EntitySet<User>(
                this.attach_User,
                this.detatch_User
                );
        }

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
        public int Calories
        {
            get { return _calories; }
            set
            {
                if (this._calories != value)
                {
                    RaisePropertyChanging("Calories");
                    this._calories = value;
                    this.RaisePropertyChanged("Calories");
                }
            }
        }

        [Column]
        public int Fats
        {
            get { return _fat; }
            set
            {
                if (this._fat != value)
                {
                    RaisePropertyChanging("Fats");
                    this._fat = value;
                    this.RaisePropertyChanged("Fats");
                }
            }
        }

        [Column]
        public int Protein
        {
            get { return _protein; }
            set
            {
                if (this._protein != value)
                {
                    RaisePropertyChanging("Protein");
                    this._protein = value;
                    this.RaisePropertyChanged("Protein");
                }
            }
        }

        [Column]
        public int Carbohydrates
        {
            get { return _carbohydrates; }
            set
            {
                if (this._carbohydrates != value)
                {
                    RaisePropertyChanging("Carbohydrates");
                    this._carbohydrates = value;
                    this.RaisePropertyChanged("Carbohydrates");
                }
            }
        }


        private EntitySet<User> _users;

        [Association(Storage = "_users", OtherKey = "_foodId", ThisKey = "Id")]
        public EntitySet<User> Users
        {
            get { return _users; }
            set { this._users.Assign(value);}
        }

        private void attach_User(User user)
        {
            RaisePropertyChanging("User");
            user.Food = this;
        }

        private void detatch_User(User user)
        {
            RaisePropertyChanging("User");
            user.Food = null;
        }

    // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;

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

        #endregion INotifyPropertyChanging Members

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

        #endregion INotifyPropertyChanged Members
    }
}