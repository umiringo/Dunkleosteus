using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GlobalDefines;
using SimpleJSON;

public class LevelSelectView : MonoBehaviour {

    public GameDirector director;
    public UILocalize labelCatagory;
    public UILabel labelCoin;
    public UIScrollView catagoryScrollView;
    //public GameObject catagoryTable;

    private Dictionary<string, GameObject> catagoryHash;
    private GameObject _lineTemplate;
    private GameObject levelContainer;
    private UICenterOnChild centerOnChild;
    private List<GameObject> levelList;
    private List<GameObject> levelDuplicateList;

    void Awake() {
        catagoryHash = new Dictionary<string, GameObject>();
        //centerOnChild = catagoryTable.GetComponent<UICenterOnChild>();
        //centerOnChild.onCenter = this.AfterCenter;
        levelList = new List<GameObject>();
        levelDuplicateList = new List<GameObject>();
        _lineTemplate = Resources.Load(PathContainer.LinkedLinePrefabPath) as GameObject;
        // 把所有关卡按顺序加入list
       foreach (Transform child in catagoryScrollView.gameObject.transform)
       {
           Debug.Log("child name = " + child.gameObject.name);
       }
        JSONArray jaLevel = TemplateMgr.Instance.GetTemplateArray(ConfigKey.LevelInfo, ConfigKey.LevelSelect);
        for(int i = 1; i <= jaLevel.Count; ++i) {
            GameObject go = catagoryScrollView.gameObject.transform.Find("Level" + i).gameObject;
            levelList.Add(go);
        }
        //将占位用的go添加到levelDuplicateList
        for(int i = 1; i <= 4; ++i) {
            GameObject go = catagoryScrollView.gameObject.transform.Find("LevelDuplicate" + i).gameObject;
            levelDuplicateList.Add(go);
        }
        this.HideAllStar();
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void BeforeExit()
    {
        // Clear containerTable
        foreach(var key in catagoryHash.Keys) {
            Destroy(catagoryHash[key]);
        }
        catagoryHash.Clear();
        HideAllStar();
    }

    public void AfterCenter(GameObject centerGo)
    {
        labelCatagory.gameObject.SetActive(false);
        labelCatagory.key = "LK" + centerGo.GetComponent<CatagoryModel>().catagoryName;
        labelCatagory.gameObject.SetActive(true);
    }

    // Show levels
    public void Show(string lastestlevel, string currentLevel)
    {
        // 循环显示
        for(int i = 0; i < levelList.Count; i++) {
            int state = director.GetLevelState(levelList[i].GetComponent<LevelView>().levelName);
            LevelView levelView = levelList[i].GetComponent<LevelView>();
            if(state == -1) {
                levelView.Show(LevelState.Unabled);
            } else if(state == 1) {
                levelView.Show(LevelState.Finished);
            } else if(state == 0) {
                levelView.Show(LevelState.Current);
            } 
        }

        int index = director.GetLevelIndex(lastestlevel);
        // 根据需要来决定占位go的显示
        int duplicateNum = index + levelDuplicateList.Count - levelList.Count;
        if(duplicateNum < 0) duplicateNum = 0;
        for(int i = 0; i < duplicateNum; i++) {
            levelDuplicateList[i].SetActive(true);
        }
        
        // 添加连接线
        for(int i = 0; i < index-1; i++) {
            AddLevelLine(levelList[i].transform, levelList[i+1].transform);
        }
    }

    private void HideAllStar()
    {
        foreach(var go in levelList) {
            go.SetActive(false);
        }
        foreach(var go in levelDuplicateList) {
            go.SetActive(false);
        }
    }

    private void AddLevelLine(Transform beginTransform, Transform endTransform)
    {
        GameObject linkedLine = beginTransform.FindChild("Line").gameObject;
        UISprite lineSprite = linkedLine.GetComponent<UISprite>();
        lineSprite.pivot = UIWidget.Pivot.Center;
        lineSprite.depth = DefineNumber.LineDepth;
        // Modify pivot
        if (beginTransform.position.x > endTransform.position.x) {
            if (beginTransform.position.y > endTransform.position.y) {
                lineSprite.pivot = UIWidget.Pivot.TopRight;
            }
            else if (beginTransform.position.y < endTransform.position.y) {
                lineSprite.pivot = UIWidget.Pivot.BottomRight;
            }
            else {
                lineSprite.pivot = UIWidget.Pivot.Right;
            }
        }
        else if (beginTransform.position.x < endTransform.position.x) {
            if (beginTransform.position.y > endTransform.position.y) {
                lineSprite.pivot = UIWidget.Pivot.TopLeft;
            }
            else if (beginTransform.position.y < endTransform.position.y) {
                lineSprite.pivot = UIWidget.Pivot.BottomLeft;
            }
            else {
                lineSprite.pivot = UIWidget.Pivot.Left;
            }
        }
        else {
            if (beginTransform.position.y > endTransform.position.y) {
                lineSprite.pivot = UIWidget.Pivot.Top;
            }
            else if (beginTransform.position.y < endTransform.position.y) {
                lineSprite.pivot = UIWidget.Pivot.Bottom;
            }
            else {
                lineSprite.pivot = UIWidget.Pivot.Center;
            }
        }
        float angle = this.CaculateAngle(beginTransform, endTransform);
        linkedLine.transform.position = beginTransform.position;
        linkedLine.transform.rotation = Quaternion.Euler(0, 0, angle);
        linkedLine.transform.localScale = new Vector3(1, 1, 1);

        float distance = Vector3.Distance(beginTransform.position, endTransform.position);
        float scale = GameObject.Find(PathContainer.UIRootPath).transform.localScale.x;
        //float containerScale = _gameContainer.transform.localScale.x;
        int width = (int)(distance / scale);
        lineSprite.width = width;
        linkedLine.SetActive(true);
    }
    
    private float CaculateAngle(Transform begin, Transform end)
    {
        float angle = 0.0f;
        angle = Mathf.Rad2Deg * Mathf.Atan((begin.position.y - end.position.y) / (begin.position.x - end.position.x));
        return angle;
    }
}
