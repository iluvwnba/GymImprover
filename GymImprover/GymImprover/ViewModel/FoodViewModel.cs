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
    class FoodViewModel : INotifyPropertyChanged
    {
        private int calories;
        private int protein;
        private int fat;
        private int carbohydrates;
        private ObservableCollection<Food> userFood;
        private ICommand loadFood;
        private ICommand addMeal;

        public FoodViewModel()
        {
            this.loadFood = new DelegateCommand(this.LoadFoodAction);
            this.addMeal = new DelegateCommand(this.AddMealAction);
        }

        public void LoadFoodAction(object p)
        {
            this.DataSource.Add(new Food());
        }

        public void AddMealAction(object p)
        {
            this.DaysFood.Calories += this.calories;
            this.DaysFood.Carbohydrates += this.carbohydrates;
            this.DaysFood.Fats += this.fat;
            this.DaysFood.Protein += this.protein;
        }

        public ICommand LoadFood
        {
            get { return this.loadFood; }
        }

        public ICommand AddMeal
        {
            get { return this.addMeal; }
        }

        public int SelectedCalories {
            get {
                if (this.DaysFood != null)
                {
                    return this.DaysFood.Calories;
                }
                return 0;
            }
            set { this.calories = value; } 
        }

        public int SelectedProtein {
            get
            {
                if (this.DaysFood != null)
                {
                    return this.DaysFood.Protein;
                }
                return 0;
            }
            set { this.protein = value; }
        }
        public int SelectedFats {
            get
            {
                if (this.DaysFood != null)
                {
                    return this.DaysFood.Fats;
                }
                return 0;
            }
            set { this.fat = value; } 
        }
        public int SelectedCarbohydrates {
            get
            {
                if (this.DaysFood != null)
                {
                    return this.DaysFood.Carbohydrates;
                }
                return 0;
            }
            set { this.carbohydrates = value; } 
        }

        public ObservableCollection<Food> DataSource
        {
            get
            {
                if (this.userFood == null)
                {
                    this.userFood = new ObservableCollection<Food>();
                }
                return this.userFood;
            }
        }


        private Food daysFood;
        public Food DaysFood
        {
            get { return this.daysFood; }
            set
            {
                {
                    if (this.daysFood != value)
                    {
                        this.daysFood = value;
                        if (this.daysFood != null)
                        {
                            this.calories = this.daysFood.Calories;
                            this.protein = this.daysFood.Protein;
                            this.fat = this.daysFood.Fats;
                            this.carbohydrates = this.daysFood.Carbohydrates;
                        }
                        this.RaisePropertyChanged("SelectedCarbohydrates");
                        this.RaisePropertyChanged("SelectedCalories");
                        this.RaisePropertyChanged("SelectedProtein");
                        this.RaisePropertyChanged("SelectedFats");
                    }
                }
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
