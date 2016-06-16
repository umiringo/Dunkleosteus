using UnityEngine;
using System.Collections;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour 
{
    private static T _instance;
    private static bool applicationIsQutting = false;
    private static object _lock = new object();

    public static T Instance
    {
        get {
            Debug.Log("Get Instantce");
            if (applicationIsQutting) {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                                "' already destroyed on application quit." +
                                " Won't create again - returning null.");
                return null;
            }
            lock(_lock)
            {
                if(_instance == null) {
                    Debug.Log("Begin to create Instantce");
                    _instance = (T)FindObjectOfType(typeof(T));
                    if(FindObjectsOfType(typeof(T)).Length > 1) {
                        Debug.LogError("[Singleton] Something went really wrong " +
                                " - there should never be more than 1 singleton!" +
                                " Reopening the scene might fix it. " + typeof(T));
                        return _instance;
                    }
                    if (_instance == null) {
                        //创建一个新的gameobject来放置此单例
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "Singleton_" + typeof(T).ToString();
                        DontDestroyOnLoad(singleton);

                        Debug.Log("[Singleton] An instance of " + typeof(T) +
                                    " is needed in the scene, so '" + singleton +
                                    "' was created with DontDestroyOnLoad.");
                    }
                    else {
                        Debug.Log("[Singleton] Using instance already created: " + _instance.gameObject.name);
                    }
                }
                Debug.Log("after create Instance");
                return _instance;
            }
        }
       
    }
    public void OnDestroy()
    {
        applicationIsQutting = true;
    }
}
