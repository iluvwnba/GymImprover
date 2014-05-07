using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace GymImprover
{
    public partial class UserFoodPage : PhoneApplicationPage
    {
        public UserFoodPage()
        {
            InitializeComponent();
            this.DataContext = App.UserViewModel;
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddFoodPage.xaml", UriKind.Relative)); 
        }
    }
}