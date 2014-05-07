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
        private Workout _currentWorkout;

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
            Workouts.Clear();
            var workoutsForUser = from Workout workout in _userDb.Workouts
                where workout._userId == _currentUser.UserId
                select workout;
            if (workoutsForUser.Count() != 0)
            {        
                foreach (Workout workout in workoutsForUser)
                {
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
            Workout workout = new Workout(WorkoutName, _currentUser);
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

        public Workout CurrentWorkout
        {
            get { return _currentWorkout; }
            set
            {
                if (_currentWorkout != value)
                {
                    _currentWorkout = value;
                    if (_currentWorkout != null)
                    {
                        _workoutName = _currentWorkout.Name;
                    }
                    RaisePropertyChanged("WorkoutName");
                }
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
