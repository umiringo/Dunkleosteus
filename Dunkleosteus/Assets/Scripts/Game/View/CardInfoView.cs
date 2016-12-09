using UnityEngine;
using System.Collections;

public class CardInfoView : MonoBehaviour {
    public string levelName;
    public UILabel labelTitle;
    public UILocalize labelName;
    private GameDirector director;

    void Awake() {
        labelTitle = this.gameObject.transform.Find("LabelTitle").gameObject.GetComponent<UILabel>();
        labelName = this.gameObject.transform.Find("LabelName").gameObject.GetComponent<UILocalize>();
        director = GameObject.Find("UI Root/GameDirector").GetComponent<GameDirector>();
        
        GameObject controller = GameObject.Find("UI Root/GameController");
        UIEventTrigger trigger = this.gameObject.GetComponent<UIEventTrigger>();
        EventDelegate eventDel = new EventDelegate(controller.GetComponent<EventController>(), "OnClickCardInfo");
        eventDel.parameters[0] = new EventDelegate.Parameter(this, "levelName");
        trigger.onClick.Add(eventDel);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Init(string name, GameObject scrollView)
    {
        levelName = name;
        string catagory = director.GetCatagoryString(levelName);
        labelTitle.text = director.GetAbbreviation(catagory);
        labelName.key = "LK" + levelName + "Title";

        UIDragScrollView dragScroll = this.gameObject.GetComponent<UIDragScrollView>();
        dragScroll.scrollView = scrollView.GetComponent<UIScrollView>();
    }

    public string GetLevelName()
    {
        return levelName;
    }
}
