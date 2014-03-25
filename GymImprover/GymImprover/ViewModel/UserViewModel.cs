using System;
using System.Linq;
using GymImprover.Commands;
using GymImprover.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Phone.Logging;

namespace GymImprover.ViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private readonly ICommand _loadData;
        private ICommand _addUser;
        private readonly UserDataContext _userDb;
        private string _name;
        private string _password;
        private ObservableCollection<User> _allUsers;
        private string _username;
        private int _weight;
        private User _currentUser;

        public UserViewModel(string userDbConnectionString)
        {
            _userDb = new UserDataContext(userDbConnectionString);
            this._addUser = new DelegateCommand(this.AddUser);
        }

        public ICommand LoadData
        {
            get { return _loadData; }
        }

        public ICommand AddUserCommand
        {
            get { return _addUser; }
        }

        public ObservableCollection<User> AllUsers
        {
            get
            {  return _allUsers;}
            set
            {
                _allUsers = value;
                RaisePropertyChanged("AllUsers");
            }
        }

        public string SelectName
        {
            get
            {
                if (CurrentUser != null)
                {
                    return CurrentUser.Name;
                }
                return string.Empty;
            }
            set { _name = value; }
        }

        public string SelectPassword
        {
            get
            {
                if (CurrentUser != null)
                {
                    return CurrentUser.Password;
                }
                return string.Empty;
            }
            set { _password = value; }
        }

        public string SelectUsername
        {
            get
            {
                if (CurrentUser != null)
                {
                    return CurrentUser.Username;
                }
                return string.Empty;
            }
            set { _username = value; }
        }

        public int SelectWeight
        {
            get
            {
                if (CurrentUser != null)
                {
                    return CurrentUser.Weight;
                }
                return 0;
            }
            set { _weight = value; }
        }

        public User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                if (_currentUser != value)
                {
                    _currentUser = value;
                    if (_currentUser != null)
                    {
                        _name = _currentUser.Name;
                        _weight = _currentUser.Weight;
                        _password = _currentUser.Password;
                        _username = _currentUser.Username;
                    }
                    RaisePropertyChanged("SelectWeight");
                    RaisePropertyChanged("SelectName");
                    RaisePropertyChanged("SelectPassword");
                    RaisePropertyChanged("SelectUserName");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public void SaveChangesToDataBase()
        {
            _userDb.SubmitChanges();
        }

        public void LoadAllUsersFromDataBase()
        {
            //queery all users in db
            var usersInDb = from User user in _userDb.Users
                select user;
            AllUsers = new ObservableCollection<User>(usersInDb);
        }

        public void DeleteUserFromDb(User userToDelete)
        {
            //remote from all user observable collection
            AllUsers.Remove(userToDelete);
            //remove from datacontext
            _userDb.Users.DeleteOnSubmit(userToDelete);
            SaveChangesToDataBase();
        }

        private void AddUser(object p)
        {
            _userDb.Users.InsertOnSubmit(new User(SelectName, SelectWeight, SelectUsername, SelectPassword));
            SaveChangesToDataBase();
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}