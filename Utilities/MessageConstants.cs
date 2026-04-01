using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace StayFitz.Utilities
{
    public static class MessageConstants
    {
        public const string Ok = "OK";
        public const string Info = "Info";
        public const string LoginSuccessTitle = "Login Successful";
        public const string Progress = "Progress";
        public const string ViewProgressMessage = "View Progress Clicked";
        public const string SnackLogged = "Snack Logged";
        public const string InvalidCredential = "Invalid Pin and Password";
        public const string OnlyAdminAccess = "Only Admin can open Product Page";
        public const string CartTitle = "Cart";
        public const string ItemAddedToCart = "{0} added!";







        // Logout
        public const string LogoutTitle = "Logout";
        public const string LogoutMessage = "Are you sure you want to logout?";
        // General
        public const string Error = "Error";
        public const string Warning = "Warning";
        public const string Success = "Success";
        public const string Yes = "Yes";
        public const string No = "No";
      

        // Profile Messages
        public const string EditProfileTitle = "Edit";
        public const string EditProfileMessage = "Edit Profile Clicked";

        //Admin
        public const string AdminOnlyAccessMessage = "Only Admin can access Product Page.";

        // Buttons
        public const string Start = "Start";
        public const string Cancel = "Cancel";


        // Workout Start Alert
        public const string StartWorkout= "Start Workout";
        public const string StartWorkoutMessage = "Are you ready for a  your workout?";
        public const string Workout = "Workout Check";

        // Workout Completed
        public const string WorkoutCompleted = "Workout Completed";
        public const string WorkoutCompletedMessage = "Great Job!";
    }
}
