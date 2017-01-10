using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class OCBridge {
#if UNITY_IOS
    [DllImport("__Internal")]  
    private static extern string CurIOSLang();

    [DllImport("__Internal")]
    private static extern void RegisterNotification();

    [DllImport("__Internal")]
    private static extern void ClearNotification();

    [DllImport("__Internal")]
    private static extern void NotificationMessageRepeatWeek(string title, string message);

    [DllImport("__Internal")]
    private static extern void NotificationMessageInterval(string title, string message, int interval);
#endif

    public static SystemLanguage GetSystemLanguage()
    {
        SystemLanguage lang = Application.systemLanguage;
#if UNITY_EDITOR || UNITY_STANDALONE
#elif UNITY_IOS || UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer) {
            if (lang == SystemLanguage.Chinese) {
                string name = CurIOSLang();
                if (name.StartsWith("zh-Hans")) {
                    return SystemLanguage.ChineseSimplified;
                }  
            return SystemLanguage.ChineseTraditional;
            }
        }
#elif UNITY_ANDROID
#endif

        return lang;
    }

    public static void RegisterLocalNotification()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
#elif UNITY_IOS || UNITY_IPHONE
        RegisterNotification();
#elif UNITY_ANDROID

#endif
    }

    public static void ClearLocalNotification()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
#elif UNITY_IOS || UNITY_IPHONE
        ClearNotification();
#elif UNITY_ANDROID
#endif
    }

    public static void LocalRepeatWeekNotificationMessage(string title, string message)
    {
#if UNITY_EDITOR || UNITY_STANDALONE
#elif UNITY_IOS || UNITY_IPHONE
        NotificationMessageRepeatWeek(title, message);
#elif UNITY_ANDROID
#endif
    }

    public static void LocalIntervalNotificationMessage(string title, string message, int interval)
    {
#if UNITY_EDITOR || UNITY_STANDALONE
#elif UNITY_IOS || UNITY_IPHONE
        NotificationMessageInterval(title, message, interval);
#elif UNITY_ANDROID
#endif
    }
}