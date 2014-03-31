using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using GymImprover.ViewModel;
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
            ExercisePivotItem.DataContext = App.ExerciseViewModel;
        }


        private void Pivot_OnLoadingPivotItem(object sender, PivotItemEventArgs e)
        {
            if (e.Item == AddPivotItem || e.Item == ViewPivotItem)
            {
                this.DataContext = App.WorkoutViewModel;
            }
            if (e.Item == ExercisePivotItem)
            {
                App.ExerciseViewModel = new ExerciseViewModel(App.WorkoutViewModel.CurrentWorkout,
                    App.UserViewModel.UserDB);
                this.DataContext = App.ExerciseViewModel;
            }
        }


    }
}