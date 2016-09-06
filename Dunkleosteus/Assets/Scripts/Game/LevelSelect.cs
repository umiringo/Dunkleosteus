using UnityEngine;
using System.Collections;
using GlobalDefines;
public class LevelSelect : MonoBehaviour {

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
        button = this.gameObject.GetComponent<UIButton>();
    }
	void Start () {

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
                button.enabled = false;
                break;
            case LevelState.Current:
                levelSprite.spriteName = PathContainer.YellowCircle;
                if (lineSprite != null) {
                    lineSprite.spriteName = PathContainer.BlueSmallSquare;
                }
                shine.SetActive(true);
                button.enabled = true;
                break;
            case LevelState.Finished:
                levelSprite.spriteName = PathContainer.GreenCircle;
                if (lineSprite != null) {
                    lineSprite.spriteName = PathContainer.GreenSmallSquare;
                }
                shine.SetActive(false);
                button.enabled = true;
                break;
            default:
                break;
        }
    }
}
