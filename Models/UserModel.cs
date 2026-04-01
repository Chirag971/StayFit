using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayFitz.Models
{
    public class UserModel
    {
        public string Pin { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }



    }
    public static class UserSession
    {
        public static UserModel CurrentUser { get; set; }
    }
}
