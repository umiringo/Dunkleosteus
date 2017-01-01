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

        string currentLang = Localization.language;
        if (currentLang == lang) {
            // Dark
            selfLabel.color = new Color(0.447f, 0.416f, 0.239f);
        }
        else {
            // Normal
            selfLabel.color = new Color(0.973f, 0.718f, 0.067f);
        }
    }
}
