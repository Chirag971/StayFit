namespace StayFitz.Models
{
    public class UserModel
    {
        public string Pin { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string ProfileImage { get; set; }


    }
    public static class UserSession
    {
        public static UserModel CurrentUser { get; set; }
    }
}
