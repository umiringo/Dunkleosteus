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
        OCBridge.ClearLocalNotification();
    }

    void OnApplicationPause(bool paused)
    {
        string title = LocalizeMgr.Instance.GetLocalizeStr(LocalizeStringKey.NotificationTitle);
        string message = LocalizeMgr.Instance.GetLocalizeStr(LocalizeStringKey.NotificationMessage);
        if(paused) {
            //OCBridge.LocalRepeatWeekNotificationMessage(title, message);
            OCBridge.LocalIntervalNotificationMessage(title, message, 10);
        }
        else {
            gameObject.GetComponent<GameDirector>().AddCoin(100);
            OCBridge.ClearLocalNotification();
        }
    }

    void OnApplicationQuit()
    {
        string title = LocalizeMgr.Instance.GetLocalizeStr(LocalizeStringKey.NotificationTitle);
        string message = LocalizeMgr.Instance.GetLocalizeStr(LocalizeStringKey.NotificationMessage);
        //OCBridge.LocalRepeatWeekNotificationMessage(title, message);
        OCBridge.LocalIntervalNotificationMessage(title, message, 10);
    }

}
