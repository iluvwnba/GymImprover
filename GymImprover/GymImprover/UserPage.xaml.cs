using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GymImprover.ViewModel;

namespace GymImprover
{
    public partial class UserPage : PhoneApplicationPage
    {
        UserViewModel model;
        public UserPage()
        {
            InitializeComponent();
            model = new UserViewModel();
            this.DataContext = model;
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/UserFoodPage.xaml", UriKind.Relative)); 
        }
    }
}