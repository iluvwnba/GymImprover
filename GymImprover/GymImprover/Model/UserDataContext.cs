using System.Data.Linq;

namespace GymImprover.Model
{
    public class UserDataContext : DataContext
    {
        public UserDataContext(string connectionString) : base(connectionString) { }

        public Table<User> Users;
        public Table<Food> Foods;
        public Table<Workout> Workouts;
        public Table<Exercise> Exercises;
    }
}