using StayFitz.Models;

namespace StayFitz.Services
{
    public static class UserService
    {
     
        public static List<UserModel> Users = new List<UserModel>();
        public static void Register(UserModel user)
        {
            Users.Add(user);
        }
        public static UserModel Login(string pin, string password)
        {
            return Users.FirstOrDefault(u => u.Pin == pin && u.Password == password);
        }
    }
}
