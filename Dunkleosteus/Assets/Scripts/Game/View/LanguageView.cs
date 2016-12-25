using UnityEngine;
using System.Collections;
using GlobalDefines;

public class LanguageView : MonoBehaviour {
    public string lang;
    private UILabel selfLabel;
    private UISprite selfSprite;
	// Use this for initialization
	void Start () {
        selfLabel = gameObject.transform.Find("Label").GetComponent<UILabel>();
        selfSprite = gameObject.GetComponent<UISprite>();
        GameObject controller = GameObject.Find("UI Root/GameController");
        UIEventTrigger trigger = gameObject.GetComponent<UIEventTrigger>();
        EventDelegate eventDel = new EventDelegate(controller.GetComponent<EventController>(), "OnSelectLanguage");
        eventDel.parameters[0] = new EventDelegate.Parameter(this, "lang");
        trigger.onClick.Add(eventDel);
        Show();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Show()
    {
 
        string currentLang = PlayerPrefs.GetString(PlayerPrefsKey.Language, "English");
        if (currentLang == lang) {
            selfLabel.color = new Color(0.27f, 0.37f, 0.42f);
            selfSprite.spriteName = PathContainer.darkBlueSquare;
        }
        else {
            selfLabel.color = new Color(0.31f, 0.8f, 0.9f);
            selfSprite.spriteName = PathContainer.normalBlueSquare;
        }
    }
}
