using UnityEngine;
using System.Collections;

public class Sky : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject controller = GameObject.Find("UI Root/GameController");
        UIEventTrigger trigger = this.gameObject.GetComponent<UIEventTrigger>();
        EventDelegate eventDel = new EventDelegate(controller.GetComponent<EventController>(), "OnCardTouched");
        eventDel.parameters[0] = new EventDelegate.Parameter(this, "gameObject");
        trigger.onClick.Add(eventDel);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
