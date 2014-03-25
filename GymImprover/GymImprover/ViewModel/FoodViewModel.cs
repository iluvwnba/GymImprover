using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GymImprover.Model;
using GymImprover.Commands;

namespace GymImprover.ViewModel
{
    public class FoodViewModel : INotifyPropertyChanged
    {
        private int _calories = 0;
        private int _protein = 0;
        private int _fat = 0;
        private int _carbohydrates = 0;
        private ObservableCollection<Food> _userFood;
        private ICommand loadFood;
        private ICommand addMeal;
        public ICommand resetFood;
        private Food theDaysFood;

        public FoodViewModel()
        {
            this.loadFood = new DelegateCommand(this.LoadFoodAction);
            this.addMeal = new DelegateCommand(this.AddMealAction);
            this.resetFood = new DelegateCommand(this.ResetFoodAction);
            theDaysFood = new Food();
            this.LoadFoodAction(this);
        }

        //Actions

        public void LoadFoodAction(object p)
        {
            if (DataSource.Count == 0)
            {
                this.DataSource.Add(theDaysFood);
            }
        }

        public void AddMealAction(object p)
        {
                theDaysFood.Calories += this._calories;
                theDaysFood.Carbohydrates += this._carbohydrates;
                theDaysFood.Fats += this._fat;
                theDaysFood.Protein += this._protein;
        }

        public void ResetFoodAction(object p)
        {
            theDaysFood.Calories = 0;
            theDaysFood.Carbohydrates = 0;
            theDaysFood.Fats = 0;
            theDaysFood.Protein = 0;
        }

        //Commands

        public ICommand ResetFood
        {
            get { return this.resetFood; }
        }
        public ICommand LoadFood
        {
            get { return this.loadFood; }
        }

        public ICommand AddMeal
        {
            get { return this.addMeal; }
        }

        //Select Properties

        public int SelectedCalories {
            get {
                return 0;
            }
            set { this._calories = value; } 
        }

        public int SelectedProtein {
            get
            {
                return 0;
            }
            set { this._protein = value; }
        }
        public int SelectedFats {
            get
            {
                return 0;
            }
            set { this._fat = value; } 
        }
        public int SelectedCarbohydrates {
            get
            {
                return 0;
            }
            set { this._carbohydrates = value; } 
        }



        public ObservableCollection<Food> DataSource
        {
            get
            {
                if (this._userFood == null)
                {
                    this._userFood = new ObservableCollection<Food>();
                }
                return this._userFood;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
