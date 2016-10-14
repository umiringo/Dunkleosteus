using UnityEngine;
using System.Collections;
using GlobalDefines;
public class LevelView : MonoBehaviour {

    public string levelName;
    private UISprite levelSprite;
    private UISprite lineSprite;
    private GameObject shine;
    private UIButton button;
	// Use this for initialization
    void Awake()
    {
        levelSprite = this.gameObject.GetComponent<UISprite>();
        Transform lineChild = this.gameObject.transform.FindChild("Line");
        if (lineChild == null) {
            lineSprite = null;
        }
        else {
            lineSprite = lineChild.gameObject.GetComponent<UISprite>();
        }
        shine = this.gameObject.transform.FindChild("Shine").gameObject;
    }
    
	void Start () {
        // Set listener
        GameObject controller = GameObject.Find("UI Root/GameController");
        UIEventTrigger trigger = this.gameObject.GetComponent<UIEventTrigger>();
        EventDelegate eventDel = new EventDelegate(controller.GetComponent<EventController>(), "OnSelectLevel");
        eventDel.parameters[0] = new EventDelegate.Parameter(this, "levelName");
        trigger.onClick.Add(eventDel);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Show(LevelState state)
    {
        switch (state) {
            case LevelState.Unabled:
                levelSprite.spriteName = PathContainer.BlueCircle;
                if (lineSprite != null) {
                    lineSprite.spriteName = PathContainer.BlueSmallSquare;
                }
                shine.SetActive(false);
                break;
            case LevelState.Current:
                levelSprite.spriteName = PathContainer.YellowCircle;
                if (lineSprite != null) {
                    lineSprite.spriteName = PathContainer.BlueSmallSquare;
                }
                shine.SetActive(true);
                break;
            case LevelState.Finished:
                levelSprite.spriteName = PathContainer.GreenCircle;
                if (lineSprite != null) {
                    lineSprite.spriteName = PathContainer.GreenSmallSquare;
                }
                shine.SetActive(false);
                break;
            default:
                break;
        }
    }
}
