using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private UserDataContext _userdb;
        private User foodUser;

        public FoodViewModel(User user, UserDataContext context)
        {
            this.loadFood = new DelegateCommand(this.LoadFoodAction);
            this.addMeal = new DelegateCommand(this.AddMealAction);
            this.resetFood = new DelegateCommand(this.ResetFoodAction);
            this.LoadFoodAction(this);
            _userdb = context;
            foodUser = user;
        }


        //Actions

        private Food getUsersFood(UserDataContext context)
        {
            var foodItem = from Food food in context.Users
                where food.Id == foodUser.UserId
                select food;
            Food tempFood = null;
            foreach (Food food in foodItem)
            {
                tempFood = food;
            }
            return tempFood;
        }

        public void LoadFoodAction(object p)
        {

        }

        public void AddMealAction(object p)
        {
            foodUser.Food.Calories += this._calories;
            foodUser.Food.Carbohydrates += this._carbohydrates;
            foodUser.Food.Fats += this._fat;
            foodUser.Food.Protein += this._protein;
            _userdb.SubmitChanges();
        }

        public void ResetFoodAction(object p)
        {
            foodUser.Food.Calories = 0;
            foodUser.Food.Carbohydrates = 0;
            foodUser.Food.Fats = 0;
            foodUser.Food.Protein = 0;
            _userdb.SubmitChanges();
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
