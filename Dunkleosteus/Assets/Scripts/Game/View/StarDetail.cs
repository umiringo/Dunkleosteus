using UnityEngine;
using System.Collections;

public class StarDetail : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    // Set EventController
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
