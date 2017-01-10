using UnityEngine;
using System.Collections;

public class LevelGuideModel : MonoBehaviour {
    private GameObject levelGuideView;
    public GameObject guideView;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void InitPanelView()
    {
        levelGuideView = gameObject.transform.Find("TriangulumContainer(Clone)/Sky/PanelGuide").gameObject;
    }

    public void TriggerLevelGuide(int step)
    {
        switch (step) {
            case 1:
                // 提示点击第一颗星星
                Debug.Log("LevelGuideModel.TriggleLevelGuide step = " + step);
                GameObject star = gameObject.transform.Find("TriangulumContainer(Clone)/Sky/StarContainer/Star3").gameObject;
                levelGuideView.GetComponent<GuideView>().ShowTap(star);
                break;
            case 2:
                // 提示点击第二颗星星
                break;
            case 3:
                // 提示划线
                break;
            default:
                break;
        }
    }

    public void TriggerGuide(int step)
    {

    }
}
