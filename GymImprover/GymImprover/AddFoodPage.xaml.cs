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
    public partial class AddFoodPage : PhoneApplicationPage
    {
        public AddFoodPage()
        {
            InitializeComponent();
            this.DataContext = this.DataContext = App.FoodViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}