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

    public void TriggerLevelGuide(int step)
    {
        switch (step) {
            case 1:
                levelGuideView = gameObject.transform.Find("TriangulumContainer(Clone)/Sky/PanelGuide").gameObject;
                // 提示点击第一颗星星
                GameObject target = gameObject.transform.Find("TriangulumContainer(Clone)/Sky/StarContainer/Star3").gameObject;
                levelGuideView.GetComponent<GuideView>().ShowTap(target, "LKGuideLevel1", "OnLevelGuideStep1", false, false);
                break;
            case 2:
                // 提示点击第二颗星星
                levelGuideView = gameObject.transform.Find("TriangulumContainer(Clone)/Sky/PanelGuide").gameObject;
                target = gameObject.transform.Find("TriangulumContainer(Clone)/Sky/StarContainer/Star1").gameObject;
                levelGuideView.GetComponent<GuideView>().ShowTap(target, "LKGuideLevel2", "OnLevelGuideStep2", true, false);
                break;
            case 3:
                // 提示点击title
                levelGuideView = gameObject.transform.Find("TriangulumContainer(Clone)/Sky/PanelGuide").gameObject;
                levelGuideView.GetComponent<GuideView>().Hide();
                levelGuideView = GameObject.Find("UI Root").transform.Find("PanelGuide").gameObject;
                target = gameObject.transform.Find("Title/LabelLevelName").gameObject;
                levelGuideView.GetComponent<GuideView>().ShowTap(target, "LKGuideLevel3", "OnLevelGuideStep3", false, true);
                break;
            case 4:
                // 提示点击tips
                levelGuideView = GameObject.Find("UI Root").transform.Find("PanelGuide").gameObject;
                target = gameObject.transform.Find("Title/Telescope").gameObject;
                levelGuideView.GetComponent<GuideView>().ShowTap(target, "LKGuideLevel4", "OnLevelGuideStep4", false, false);
                break;
            case 5:
                // 提示连线
                levelGuideView.GetComponent<GuideView>().Hide();
                levelGuideView = gameObject.transform.Find("TriangulumContainer(Clone)/Sky/PanelGuide").gameObject;
                target = gameObject.transform.Find("TriangulumContainer(Clone)/Sky/StarContainer/Star3").gameObject;
                levelGuideView.GetComponent<GuideView>().ShowTap(target, "LKGuideLevel5", "OnLevelGuideStep5", false, false);
                break;
            case 6:
                // 提示连线
                levelGuideView = gameObject.transform.Find("TriangulumContainer(Clone)/Sky/PanelGuide").gameObject;
                target = gameObject.transform.Find("TriangulumContainer(Clone)/Sky/StarContainer/Star2").gameObject;
                levelGuideView.GetComponent<GuideView>().ShowTap(target, "LKGuideLevel6", "OnLevelGuideStep6", true, false);
                break;
            case 7:
                levelGuideView = GameObject.Find("UI Root").transform.Find("PanelGuide").gameObject;
                levelGuideView.GetComponent<GuideView>().ShowIntro("LKGuideLevel7", "OnLevelGuideStep7", true);
                break;
            default:
                break;
        }
    }

    public void TriggerCardGuide()
    {
        levelGuideView = GameObject.Find("UI Root").transform.Find("PanelGuide").gameObject;
        GameObject target = GameObject.Find("UI Root/PanelLevelSelect/PanelLevel/MenuContainer/ButtonCard").gameObject;
        levelGuideView.GetComponent<GuideView>().ShowTap(target, "LKGuideCard", "OnCardGuide", false, true);
    }

    public void StopGuide()
    {
        levelGuideView.GetComponent<GuideView>().Hide();
    }
}
