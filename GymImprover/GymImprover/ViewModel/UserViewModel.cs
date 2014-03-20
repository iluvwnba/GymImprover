using GymImprover.Model;
using GymImprover.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GymImprover.ViewModel
{
    class UserViewModel : INotifyPropertyChanged
    {
        private string name;
        private int weight;
        private string username;
        private string password;
        private ObservableCollection<User> userData;
        private ICommand loadData;

        public UserViewModel()
        {
            this.loadData = new DelegateCommand(this.LoadDataAction);
        }

        private void LoadDataAction(object p)
        {
            this.DataSource.Add(new User("Martin", 100, "Martin", "password"));
            this.DataSource.Add(new User("Paul", 10, "Paul", "password"));
        }

        public ICommand LoadData {
            get { return this.loadData; }
        }


        public ObservableCollection<User> DataSource
        {
            get 
            {
                if (this.userData == null)
                {
                    this.userData = new ObservableCollection<User>();
                }
                return this.userData;
            }
        }

        public string SelectName
        {
            get
            {
                if (this.CurrentUser != null)
                {
                    return this.CurrentUser.Name;
                }
                return string.Empty;
            }
            set { this.name = value; }
        }


        public string SelectPassword
        {
            get
            {
                if (this.CurrentUser != null)
                {
                    return this.CurrentUser.Password;
                }
                return string.Empty;
            }
            set { this.password = value; }
        }

        public string SelectUserName
        {
            get
            {
                if (this.CurrentUser != null)
                {
                    return this.CurrentUser.Username;
                }
                return string.Empty;
            }
            set { this.username = value; }
        }

        public int SelectWeight { 
            get 
            {
                if(this.CurrentUser != null)
                {
                    return this.CurrentUser.Weight;
                }
                return 0;
            }
            set { this.weight = value; }
        }

        private User currentUser;
        public User CurrentUser {
            get { return this.currentUser; }
            set
            {
                if (this.currentUser != value)
                {
                    this.currentUser = value;
                    if (this.currentUser != null)
                    {
                            this.name = this.currentUser.Name;
                            this.weight = this.currentUser.Weight;
                            this.password = this.currentUser.Password;
                            this.username = this.currentUser.Username;
                    }
                    this.RaisePropertyChanged("SelectWeight");
                    this.RaisePropertyChanged("SelectName");
                    this.RaisePropertyChanged("SelectPassword");
                    this.RaisePropertyChanged("SelectUserName");
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
