using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using GymImprover.Model;
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

        private void LongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var workout = ((LongListSelector) sender).SelectedItem as Workout;
            App.WorkoutViewModel.CurrentWorkout = workout;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.ExerciseViewModel.AddExerciseCommand.Execute(this);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddExercisePage.xaml", UriKind.Relative)); 
        }

        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            App.ExerciseViewModel.ExerciseName = NameTextBox.Text;
        }

        private void TextBox_SelectionChanged_1(object sender, RoutedEventArgs e)
        {
            if (WeightTextBox.Text != "")
            {
                App.ExerciseViewModel.ExerciseWeight = Convert.ToInt32(WeightTextBox.Text);
            }
        }

        private void TextBox_SelectionChanged_2(object sender, RoutedEventArgs e)
        {
            if (RepsTextBox.Text != "")
            {
                App.ExerciseViewModel.ExerciseReps = Convert.ToInt32(RepsTextBox.Text);
            }
        }

        private void TextBox_SelectionChanged_3(object sender, RoutedEventArgs e)
        {
            if (SetsTextBox.Text != "")
            {
                App.ExerciseViewModel.ExerciseSets = Convert.ToInt32(SetsTextBox.Text);
            }
        }


    }
}