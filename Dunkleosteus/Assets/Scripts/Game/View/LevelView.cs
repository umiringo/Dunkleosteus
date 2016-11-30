using UnityEngine;
using System.Collections;
using GlobalDefines;
public class LevelView : MonoBehaviour {

    public string levelName;
    private UISprite levelSprite;
    private GameObject label;
    private GameObject labelUnderline;
    private GameObject shine;
    private UIButton button;
	// Use this for initialization
    void Awake()
    {
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

    public void Init(string name) {
        levelSprite = this.gameObject.GetComponent<UISprite>();
        label = this.gameObject.transform.Find("Label").gameObject;
        labelUnderline = this.gameObject.transform.Find("Label_UnderLine").gameObject;
        shine = this.gameObject.transform.Find("Shine").gameObject;
        levelName = name;
    }
    public void Show(LevelState state)
    {
        switch (state) {
            case LevelState.Unabled:
                levelSprite.gameObject.SetActive(false);
                shine.SetActive(false);
                label.SetActive(false);
                labelUnderline.SetActive(false);
                break;
            case LevelState.Current:
                levelSprite.gameObject.SetActive(true);
                levelSprite.spriteName = PathContainer.YellowCircle;
                //lineSprite.gameObject.SetActive(false);
                shine.SetActive(true);
                label.SetActive(false);
                labelUnderline.SetActive(false);
                break;
            case LevelState.Finished:
                levelSprite.gameObject.SetActive(true);
                levelSprite.spriteName = PathContainer.BlueCircle;
                shine.SetActive(false);
                label.SetActive(false);
                labelUnderline.SetActive(false);
                break;
            case LevelState.Spot:
                levelSprite.gameObject.SetActive(true);
                levelSprite.spriteName = PathContainer.SpotSquare;
                shine.SetActive(false);
                label.SetActive(false);
                labelUnderline.SetActive(false);
                break;
            default:
                break;
        }
    }

}
