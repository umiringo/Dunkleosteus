using UnityEngine;
using System.Collections;

public class ConfirmView : MonoBehaviour {
    public UILocalize labelTitle;
    public UILocalize labelContent;
    public GameObject buttonOk;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Show(string title, string content, string delegateName, string param)
    {
        labelTitle.key = title;
        labelContent.key = content;

        GameObject controller = GameObject.Find("UI Root/GameController");
        UIButton btn = buttonOk.GetComponent<UIButton>();
        EventDelegate eventDel = new EventDelegate(controller.GetComponent<EventController>(), delegateName);
        eventDel.parameters[0] = new EventDelegate.Parameter(param);
        btn.onClick.Clear();
        btn.onClick.Add(eventDel);
        gameObject.SetActive(true);
    }
}
