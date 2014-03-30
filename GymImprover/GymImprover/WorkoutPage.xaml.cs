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
    public partial class WorkoutPage : PhoneApplicationPage
    {
        public WorkoutPage()
        {
            InitializeComponent();
            this.DataContext = App.WorkoutViewModel;
        }
    }
}