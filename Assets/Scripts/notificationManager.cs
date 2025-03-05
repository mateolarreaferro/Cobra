using System;
using UnityEngine;
#if UNITY_IOS
using Unity.Notifications.iOS;
#endif

public class NotificationManager : MonoBehaviour
{
    // Set to true in the Inspector for testing (notifications every minute).
    [SerializeField] private bool isDebug = false;

    // Unique identifier for your daily notification.
    private const string dailyNotificationId = "_daily_spin_notification";

    private void Start()
    {
#if UNITY_IOS
        // Remove any existing notification with the same ID to avoid duplicates.
        iOSNotificationCenter.RemoveScheduledNotification(dailyNotificationId);
        iOSNotificationCenter.RemoveDeliveredNotification(dailyNotificationId);

        ScheduleNotification();
#endif
    }

    private void ScheduleNotification()
    {
#if UNITY_IOS
        if (isDebug)
        {
            // DEBUG MODE: schedule a notification every 1 minute (repeating).
            var debugTrigger = new iOSNotificationTimeIntervalTrigger()
            {
                TimeInterval = new TimeSpan(0, 1, 0), // 1 minute
                Repeats = true
            };

            var debugNotification = new iOSNotification()
            {
                Identifier = dailyNotificationId,
                Title = "Spin the Wheel!",
                Body = "Debug: Spin the wheel and win rewards!",
                ShowInForeground = true,
                ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
                Trigger = debugTrigger,
            };

            iOSNotificationCenter.ScheduleNotification(debugNotification);
        }
        else
        {
            // NORMAL MODE: schedule one notification per day between 4:00 PM and 6:00 PM.
            int hour = UnityEngine.Random.Range(16, 18);  // 16 or 17
            int minute = UnityEngine.Random.Range(0, 60); // 0–59

            // Construct the DateTime for today at the random hour/minute.
            DateTime fireTime = new DateTime(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                hour,
                minute,
                0
            );

            // If we've already passed that time today, schedule it for tomorrow.
            if (fireTime < DateTime.Now)
            {
                fireTime = fireTime.AddDays(1);
            }

            // Calendar trigger for iOS, set to repeat daily.
            var calendarTrigger = new iOSNotificationCalendarTrigger()
            {
                Year = fireTime.Year,
                Month = fireTime.Month,
                Day = fireTime.Day,
                Hour = fireTime.Hour,
                Minute = fireTime.Minute,
                Second = fireTime.Second,
                Repeats = true
            };

            var notification = new iOSNotification()
            {
                Identifier = dailyNotificationId,
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
