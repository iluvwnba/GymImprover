using System;
using System.Diagnostics;
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
        private ICommand _login;
        private ICommand _addUser;
        private readonly UserDataContext _userDb;
        private string _name;
        private string _password;
        private ObservableCollection<User> _allUsers;
        private string _username;
        private int _weight;
        private User _currentUser;
        private ObservableCollection<User> _loggedInUser;

        public UserViewModel(string userDbConnectionString)
        {
            _userDb = new UserDataContext(userDbConnectionString);
            this._addUser = new DelegateCommand(this.AddUser);
            this._login = new DelegateCommand(this.Login);
        }


        public ICommand AddUserCommand
        {
            get { return _addUser; }
        }

        public ICommand LoginCommand
        {
            get { return _login; }
        }

        public ObservableCollection<User> AllUsers
        {
            get { return _allUsers; }
            set
            {
                _allUsers = value;
                RaisePropertyChanged("AllUsers");
            }
        }

        public ObservableCollection<User> LoggedInUser
        {
            get { return _loggedInUser; }
            set
            {
                _loggedInUser = value;
                RaisePropertyChanged("LoggedInUser");
            }
        }

        private string _loginUsername;
        public string LoginUsername
        {
            get {
                if (_loginUsername != null)
                {
                    return _loginUsername;
                }
                return string.Empty;
            }
            set
            {
                _loginUsername = value;
                RaisePropertyChanged("LoginUsername");
            }
        }

        private string _loginPassword;
        public string LoginPassword
        {
            get
            {
                if (_loginPassword != null)
                {
                    return _loginPassword;
                }
                return string.Empty;
            }
            set
            {
                _loginPassword = value;
                RaisePropertyChanged("LoginPassword");
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
            _userDb.Users.InsertOnSubmit(new User("Martin", 10, "user1", "pass"));
            _userDb.Users.InsertOnSubmit(new User("Joe", 10, "user2", "pass"));
            SaveChangesToDataBase();
            LoadAllUsersFromDataBase();
        }

        private void Login(object p)
        {
            var loggingInUser = from User user in _userDb.Users
                                where user.Username == LoginUsername
                                && user.Password == LoginPassword
                                select user;
            User tempLoginUser = null;
            foreach (User user in loggingInUser)
            {
                tempLoginUser = user;
            }
            if (tempLoginUser != null)
            {
                CurrentUser = tempLoginUser;
                LoggedInUser = new ObservableCollection<User> {CurrentUser};
                RaisePropertyChanged("LoggedInUser");
                foreach (User user in LoggedInUser)
                {
                    Debug.WriteLine("Debug Login " + user.Name);
                }
            }
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