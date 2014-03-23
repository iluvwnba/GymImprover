using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace GymImprover.Model
{
    public class User : INotifyPropertyChanged
    {
        private string name;
        private int weight;
        private string username;
        private string password;


        public User(string name, int weight, string userName, string password)
        {
            this.Name = name;
            this.Weight = weight;
            this.Username = userName;
            this.Password = password;
        }

        public string Name {
            get { return name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.RaisePropertyChanged("Name");
                }
            
            }
        }

        public int Weight { 
            get{return weight;}
            set
            {
                if (this.weight != value)
                {
                    this.weight = value;
                    this.RaisePropertyChanged("Weight");
                }
            }
        }

        public string Username { 
            get {return username;}
            set
            {
                if (this.username != value)
                {
                    this.username = value;
                    this.RaisePropertyChanged("Username");
                }
            }
            
        }

        public string Password {
            get { return password; }
            set
            {
                if (this.password != value)
                {
                    this.password = value;
                    this.RaisePropertyChanged("password");
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
