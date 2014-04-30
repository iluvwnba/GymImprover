using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymImprover.Model;
using GymImprover.ViewModel;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace GymImprover.UnitTests
{
    [TestClass]
    public class FoodViewModelTest
    {
        private User makeUser()
        {
            return new User();
        }

        private Food makeFood()
        {
            return new Food();
        }

        [TestMethod]
        public void UserFood_ByDefault_ReturnZero()
        {
            User user = makeUser();
            Food food = makeFood();
            user.Food = food;
            Assert.AreEqual(0, user.Food.Calories);
            Assert.AreEqual(0, user.Food.Carbohydrates);
            Assert.AreEqual(0, user.Food.Protein);
            Assert.AreEqual(0, user.Food.Fats);
        }


        [TestMethod]
        public void AddMealAction_AddedMeal_ReturnNewValues()
        {
            int testAmount = 1;
            User user = makeUser();
            Food food = new Food();
            user.Food = food;
            FoodViewModel foodViewModel = new FoodViewModel(user, new UserDataContext(GymImprover.App.DbConnectionString));
            foodViewModel.SelectedCalories = testAmount;
            foodViewModel.SelectedCarbohydrates = testAmount;
            foodViewModel.SelectedFats = testAmount;
            foodViewModel.SelectedProtein = testAmount;
            foodViewModel.AddMealAction(this);
            Assert.AreEqual(testAmount, user.Food.Calories);
            Assert.AreEqual(testAmount, user.Food.Carbohydrates);
            Assert.AreEqual(testAmount, user.Food.Protein);
            Assert.AreEqual(testAmount, user.Food.Fats);
        }



    }
}
