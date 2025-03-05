using System;
using UnityEngine;
#if UNITY_IOS
using Unity.Notifications.iOS;
#endif

public class NotificationManager : MonoBehaviour
{
    // Set to true in the Inspector for testing (notifications every minute)
    [SerializeField] private bool isDebug = false;

    private void Start()
    {
        ScheduleNotification();
    }

    private void ScheduleNotification()
    {
#if UNITY_IOS
        if (isDebug)
        {
            // Debug mode: schedule a notification every minute.
            iOSNotificationTimeIntervalTrigger timeTrigger = new iOSNotificationTimeIntervalTrigger()
            {
                TimeInterval = new TimeSpan(0, 1, 0), // 1 minute interval
                Repeats = true
            };

            iOSNotification debugNotification = new iOSNotification()
            {
                Identifier = "_debug_notification",
                Title = "Spin the Wheel!",
                Body = "Debug: Spin the wheel and win rewards!",
                ShowInForeground = true,
                ForegroundPresentationOption = PresentationOption.Alert | PresentationOption.Sound,
                Trigger = timeTrigger,
            };

            iOSNotificationCenter.ScheduleNotification(debugNotification);
        }
        else
        {
            // Normal mode: choose a random time between 4:00 PM and 6:00 PM.
            int hour = UnityEngine.Random.Range(16, 18);
            int minute = UnityEngine.Random.Range(0, 60);

            // Set the notification fire time for today.
            DateTime fireTime = new DateTime(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                hour,
                minute,
                0);

            // If the time has already passed today, schedule for tomorrow.
            if (fireTime < DateTime.Now)
            {
                fireTime = fireTime.AddDays(1);
            }

            // Create an iOS calendar trigger that repeats daily.
            iOSNotificationCalendarTrigger calendarTrigger = new iOSNotificationCalendarTrigger()
            {
                Hour = fireTime.Hour,
                Minute = fireTime.Minute,
                Second = fireTime.Second,
                Repeats = true
            };

            iOSNotification notification = new iOSNotification()
            {
                Identifier = "_daily_spin_notification",
                Title = "Spin the Wheel!",
                Body = "It's time to spin the wheel and win rewards!",
                ShowInForeground = true,
                ForegroundPresentationOption = PresentationOption.Alert | PresentationOption.Sound,
                Trigger = calendarTrigger,
            };

            iOSNotificationCenter.ScheduleNotification(notification);
        }
#endif
    }
}
