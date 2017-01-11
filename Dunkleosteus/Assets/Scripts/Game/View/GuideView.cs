using UnityEngine;
using System.Collections;

public class GuideView : MonoBehaviour {
    public GameObject spriteTap;
    public GameObject spriteTarget;
    public UILocalize labelGuide;
    private EventController controller;
    void Awake() {
        controller = GameObject.Find("UI Root/GameController");
    }
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowTap(GameObject star, string content, string callback)
    {
        if (!gameObject.activeSelf) {
            gameObject.SetActive(true);
        }
        Vector3 starPosition = star.transform.localPosition;
        spriteTap.transform.localPosition = new Vector3(starPosition.x + 20, starPosition.y - 65, starPosition.z);
        spriteTarget.transform.localPosition = new Vector3(starPosition.x, starPosition.y, starPosition.z);
        labelGuide.key = content;
        // Delegate TODO
        UIEventTrigger trigger = this.GameObject.GetComponent<UIEventTrigger>();
        EventDelegate del = new EventDelegate(controller, callback);
        trigger.onClick.Set(del);
    }
}
