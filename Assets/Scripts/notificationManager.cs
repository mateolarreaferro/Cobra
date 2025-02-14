using UnityEngine;
using Unity.Notifications.iOS;

public class NotificationController : MonoBehaviour
{
    public AudioSource audioSource; // AudioSource to play sounds
    public AudioClip[] soundClips; // Array to store sound options

    private float notificationInterval = 300f; // Interval in seconds (e.g., 5 minutes)
    private float timer = 0f;
    private int soundIndex = 0; // Index for the selected sound

    void Start()
    {
        // Request notification permissions
        RequestNotificationPermission();

        // Set the initial sound clip
        if (soundClips.Length > 0)
        {
            audioSource.clip = soundClips[soundIndex];
        }
        else
        {
            Debug.LogError("No sound clips assigned!");
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= notificationInterval)
        {
            TriggerNotification();
            PlaySound();
            timer = 0f; // Reset timer
        }
    }

    private void RequestNotificationPermission()
    {
        // Request permissions for iOS notifications
        var authorizationOption = AuthorizationOption.Alert | AuthorizationOption.Sound;
        iOSNotificationCenter.ScheduleNotification(new iOSNotification
        {
            Identifier = "permission_request",
            Title = "Notification Permissions",
            Body = "Enable notifications to stay reminded!",
            ShowInForeground = true
        });
    }

    private void TriggerNotification()
    {
        var notification = new iOSNotification
        {
            Identifier = System.Guid.NewGuid().ToString(),
            Title = "Aquí y Ahora",
            Body = "Un recordatorio para estar presente.",
            ShowInForeground = true,
            ForegroundPresentationOption = PresentationOption.Alert | PresentationOption.Sound,
            CategoryIdentifier = "reminder",
            ThreadIdentifier = "reminder-thread",
            Trigger = new iOSNotificationTimeIntervalTrigger
            {
                TimeInterval = new System.TimeSpan(0, 0, (int)notificationInterval),
                Repeats = false
            }
        };

        iOSNotificationCenter.ScheduleNotification(notification);
    }

    private void PlaySound()
    {
        if (audioSource.clip != null)
        {
            audioSource.Play();
        }
    }
}
