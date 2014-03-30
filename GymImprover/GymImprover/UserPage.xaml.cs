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
        public UserPage()
        {
            InitializeComponent();
            this.DataContext = App.UserViewModel;
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            App.FoodViewModel = new FoodViewModel(App.UserViewModel.CurrentUser, App.UserViewModel.UserDB);
            NavigationService.Navigate(new Uri("/UserFoodPage.xaml", UriKind.Relative)); 
        }

        private void Workout(object sender, EventArgs e)
        {
            App.WorkoutViewModel = new WorkoutViewModel(App.UserViewModel.CurrentUser ,App.UserViewModel.UserDB);
            NavigationService.Navigate(new Uri("/WorkoutPage.xaml", UriKind.Relative));
        }
    }
}