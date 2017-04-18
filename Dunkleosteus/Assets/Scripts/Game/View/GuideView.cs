using UnityEngine;
using System.Collections;

public class GuideView : MonoBehaviour {
    public GameObject spriteTap;
    public GameObject spriteTarget;
    public UILocalize labelGuide;
    private EventController controller;
    private Vector3 prePosition = Vector3.zero;
    public GameObject spriteBg;
    public GameObject spriteContent;
    void Awake() {
        controller = GameObject.Find("UI Root/GameController").GetComponent<EventController>();
    }
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowTap(GameObject star, string content, string callback, bool isMove, bool isMiddle)
    {
        if (!gameObject.activeSelf) {
            gameObject.SetActive(true);
        }
        spriteTap.SetActive(true);
        spriteTarget.SetActive(true);
        if (spriteBg) {
            spriteBg.GetComponent<UIEventTrigger>().onClick.Clear();
        }
        TweenPosition tapTween = spriteTap.GetComponent<TweenPosition>();
        tapTween.enabled = false;
        Vector3 starPosition = star.transform.localPosition;
        if(isMiddle) {
            spriteContent.transform.localPosition = Vector3.zero;
        } else {
            spriteContent.transform.localPosition = new Vector3(0, -500, 0);
        }
        spriteTarget.transform.localPosition = new Vector3(starPosition.x, starPosition.y, starPosition.z);
        labelGuide.gameObject.SetActive(false);
        labelGuide.key = content;
        labelGuide.gameObject.SetActive(true);
        UIEventTrigger trigger = spriteTarget.GetComponent<UIEventTrigger>();
        EventDelegate del = new EventDelegate(controller, callback);
        trigger.onClick.Clear();
        trigger.onClick.Add(del);

        if (isMove) {
            tapTween.ResetToBeginning();
            tapTween.from = prePosition;
            tapTween.to = new Vector3(starPosition.x + 20, starPosition.y - 65, starPosition.z);
            tapTween.PlayForward();
        } else {
            spriteTap.transform.localPosition = new Vector3(starPosition.x + 20, starPosition.y - 65, starPosition.z);
        }
        prePosition = spriteTap.transform.localPosition;
    }

    public void ShowIntro(GameObject star, string content, string callback, bool isMiddle)
    {
        if (!gameObject.activeSelf) {
            gameObject.SetActive(true);
        }
        if (spriteBg) {
            spriteBg.GetComponent<BoxCollider>().enabled = true;
            UIEventTrigger trigger = spriteBg.GetComponent<UIEventTrigger>();
            EventDelegate del = new EventDelegate(controller, callback);
            trigger.onClick.Clear();
            trigger.onClick.Add(del);
        }
        labelGuide.gameObject.SetActive(false);
        labelGuide.key = content;
        labelGuide.gameObject.SetActive(true);
        spriteTap.SetActive(false);
        spriteTarget.SetActive(true);
        Vector3 starPosition = star.transform.localPosition;
        spriteTarget.transform.localPosition = new Vector3(starPosition.x, starPosition.y, starPosition.z);
        UIEventTrigger targetTrigger = spriteTarget.GetComponent<UIEventTrigger>();
        EventDelegate targetDel = new EventDelegate(controller, callback);
        targetTrigger.onClick.Clear();
        targetTrigger.onClick.Add(targetDel);
        if(isMiddle) {
            spriteContent.transform.localPosition = Vector3.zero;
        } else {
            spriteContent.transform.localPosition = new Vector3(0, -500, 0);
        }
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
