using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GymImprover.Commands;
using GymImprover.Model;

namespace GymImprover.ViewModel
{
    public class WorkoutViewModel : INotifyPropertyChanged
    {
        private UserDataContext _userDb;
        private User _currentUser;
        private ICommand _addWorkout;
        private string _workoutName;

        public WorkoutViewModel(User currentUser, UserDataContext context)
        {
            Workouts = new ObservableCollection<Workout>();
            _userDb = context;
            _currentUser = currentUser;
            LoadWorkouts();
            _addWorkout = new DelegateCommand(this.NewWorkout);
        }

        public void LoadWorkouts()
        {
            var workoutsForUser = from Workout workout in _userDb.Workouts
                where workout._userId == _currentUser.UserId
                select workout;
            if (workoutsForUser.Count() != 0)
            {        
                Workouts.Clear();
                foreach (Workout workout in workoutsForUser)
                {
                    Debug.WriteLine(workout.Id);
                    Workouts.Add(workout);
                }
            }
        }

        public ICommand AddWorkout
        {
            get { return _addWorkout; }
        }

        public void NewWorkout(object p)
        {
            Workout workout = new Workout("NEW WORKOUT", _currentUser);
            workout.User = _currentUser;
            _userDb.Workouts.InsertOnSubmit(workout);
            _userDb.SubmitChanges();
            LoadWorkouts();
        }

        public string WorkoutName
        {
            get { return _workoutName; }
            set
            {
                _workoutName = value;
                RaisePropertyChanged("WorkoutName");
            }
        }

        private ObservableCollection<Workout> _workouts;
        public ObservableCollection<Workout> Workouts
        {
            get { return _workouts; }
            set
            {
                _workouts = value;
                RaisePropertyChanged("Workouts");
            }
        } 


        public event PropertyChangedEventHandler PropertyChanged;
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
