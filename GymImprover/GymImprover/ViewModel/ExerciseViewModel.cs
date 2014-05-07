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
    public class ExerciseViewModel : INotifyPropertyChanged
    {
        private Workout _currentWorkout;
        private UserDataContext _userDB;
        private ICommand _addExercise;
        private string _exerciseName;
        private int _exerciseWeight;
        private int _exerciseReps;
        private int _exerciseSets;

        public ExerciseViewModel(Workout currentWorkout, UserDataContext dataContext)
        {
            this.Exercises = new ObservableCollection<Exercise>();
            this._currentWorkout = currentWorkout;
            this._userDB = dataContext;
            this._addExercise = new DelegateCommand(this.AddExercise);
            LoadExercises();
        }

        public ICommand AddExerciseCommand
        {
            get { return _addExercise; }
        }

        public void LoadExercises()
        {
            var exercisesForWorkout = from Exercise exercise in _userDB.Exercises
                where exercise._workoutId == _currentWorkout.Id
                select exercise;
            if (exercisesForWorkout.Count() != 0)
            {
                foreach (Exercise exercise in exercisesForWorkout)
                {
                    Exercises.Add(exercise);
                }
            }
            
        }

        private void AddExercise(object p)
        {
            Exercise exercise = new Exercise(ExerciseName, ExerciseReps, ExerciseWeight, ExerciseSets);
            exercise.Workout = _currentWorkout;
            _userDB.Exercises.InsertOnSubmit(exercise);
            _userDB.SubmitChanges();
            LoadExercises();
        }

        public string ExerciseName
        {
            get { return _exerciseName; }
            set
            {
                _exerciseName = value;
                RaisePropertyChanged("ExerciseName");
            }
        }
        public int ExerciseWeight
        {
            get { return _exerciseWeight; }
            set
            {
                _exerciseWeight = value;
                RaisePropertyChanged("ExerciseWeight");
            }
        }

        public int ExerciseReps
        {
            get { return _exerciseReps; }
            set
            {
                _exerciseReps = value;
                RaisePropertyChanged("ExerciseReps");
            }
        }

        public int ExerciseSets
        {
            get { return _exerciseSets; }
            set
            {
                _exerciseSets = value;
                RaisePropertyChanged("ExerciseSets");
            }
        }

        
        private ObservableCollection<Exercise> _exercises;

        public ObservableCollection<Exercise> Exercises
        {
            get { return _exercises; }
            set
            {
                _exercises = value;
                RaisePropertyChanged("Exercises");
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
