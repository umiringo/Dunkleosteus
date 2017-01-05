using UnityEngine;
using System.Collections;
using GlobalDefines;
using NotificationServices = UnityEngine.iOS.NotificationServices;
using NotificationType = UnityEngine.iOS.NotificationType;
using LocalNotification = UnityEngine.iOS.LocalNotification;

public class NotificationMgr : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Awake()
    {
        NotificationServices.RegisterForNotifications(NotificationType.Alert | NotificationType.Badge | NotificationType.Sound);
        ClearNotification();
    }

    // 本地推送
    public static void NotificationMessage(string message, int hour, bool isRepeatDay, int delayDays)
    {
        System.DateTime notificationDate = System.DateTime.Now.AddDays(delayDays);
        int year = notificationDate.Year;
        int month = notificationDate.Month;
        int day = notificationDate.Day;
        System.DateTime newDate = new System.DateTime(year, month, day, hour, 0, 0);
        NotificationMessage(message, newDate, isRepeatDay);
    }

    public static void NotificationMessage(string message, System.DateTime newDate, bool isRepeatDay)
    {
        if(newDate > System.DateTime.Now) {
            LocalNotification localNotification = new LocalNotification();
            localNotification.fireDate = newDate;
            localNotification.alertBody = message;
            localNotification.applicationIconBadgeNumber = 1;
            localNotification.hasAction = true;
            if(isRepeatDay)
            {
                localNotification.repeatCalendar = UnityEngine.iOS.CalendarIdentifier.GregorianCalendar;
                localNotification.repeatInterval = UnityEngine.iOS.CalendarUnit.Day;
            }
            localNotification.soundName = LocalNotification.defaultSoundName;
            NotificationServices.ScheduleLocalNotification(localNotification);
        }
    }

    void OnApplicationPause(bool paused)
    {
        string message = LocalizeMgr.Instance.GetLocalizeStr(LocalizeStringKey.NotificationMessage);
        if(paused) {
            NotificationMessage(message, System.DateTime.Now.AddSeconds(10), false);
        }
        else {
            gameObject.GetComponent<GameDirector>().AddCoin(100);
            ClearNotification();
        }
    }

    void OnApplicationQuit()
    {
        string message = LocalizeMgr.Instance.GetLocalizeStr(LocalizeStringKey.NotificationMessage);
        NotificationMessage(message, System.DateTime.Now.AddSeconds(10), false);
    }

    void ClearNotification()
    {
        LocalNotification tmp = new LocalNotification();
        tmp.applicationIconBadgeNumber = -1;
        tmp.hasAction = false;
        NotificationServices.PresentLocalNotificationNow(tmp);
        NotificationServices.CancelAllLocalNotifications();
        NotificationServices.ClearLocalNotifications();
    }

}
