using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using StayFitz.Models;

namespace StayFitz.Services
{
    public class NotificationService
    {
        //public void ScheduleNotification(Reminder reminder)
        //{
        //    var request = new NotificationRequest
        //    {
        //        NotificationId = reminder.Id,
        //        Title = $"{reminder.Type} Reminder ⏰",
        //        Description = reminder.Message,

        //        ReturningData = reminder.Id.ToString(),

        //        Android = new AndroidOptions
        //        {
        //            Priority = AndroidPriority.Max,
        //            AutoCancel = false,
        //            Ongoing = true // 🔥 makes it like alarm
        //        },

        //        Schedule = new NotificationRequestSchedule
        //        {
        //            NotifyTime = reminder.ReminderDateTime
        //        }
        //    };

        //    LocalNotificationCenter.Current.Show(request);
        //}
        public void ScheduleNotification(Reminder reminder)
        {
            try
            {
                var request = new NotificationRequest
                {
                    NotificationId = reminder.Id,
                    Title = reminder.Type,
                    Description = reminder.Message,
                    ReturningData = reminder.Id.ToString(),
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = reminder.ReminderDateTime
                    }
                };

                LocalNotificationCenter.Current.Show(request);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        public void CancelNotification(int id)
        {
            LocalNotificationCenter.Current.Cancel(id);
        }




    }


}
