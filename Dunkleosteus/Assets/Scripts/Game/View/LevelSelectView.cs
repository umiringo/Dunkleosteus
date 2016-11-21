using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GlobalDefines;

public class LevelSelectView : MonoBehaviour {

    public GameDirector director;
    public UILocalize labelCatagory;
    public UILabel labelCoin;
    public UIScrollView catagoryScrollView;
    public GameObject catagoryTable;

    private Dictionary<string, GameObject> catagoryHash;
    private GameObject levelContainer;
    private UICenterOnChild centerOnChild;
    private List<GameObject> levelList;
    private List<GameObject> levelDuplicateList;

    void Awake() {
        catagoryHash = new Dictionary<string, GameObject>();
        centerOnChild = catagoryTable.GetComponent<UICenterOnChild>();
        centerOnChild.onCenter = this.AfterCenter;
        levelList = new List<GameObject>();
        levelDuplicateList = new List<GameObject>();
        //将占位用的go添加到levelDuplicateList
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
        levelList.Clear();
    }

    public void AfterCenter(GameObject centerGo)
    {
        labelCatagory.gameObject.SetActive(false);
        labelCatagory.key = "LK" + centerGo.GetComponent<CatagoryModel>().catagoryName;
        labelCatagory.gameObject.SetActive(true);
    }

    // Show current catagory and level
    public void Show(string lastestlevel, string currentLevel) 
    {
        // Get next level's catagory
        int lastestIndex = director.GetNextLevelCatagoryIndex(lastestlevel);
        if(lastestIndex == 0) {
            lastestIndex = 1;
        }
        // Add enable catagory into dictionary
        for(int i = 1; i <= lastestIndex; i++) {
            string enableCatagory = director.GetCatagoryString(i);
            GameObject catagoryGameObject = Instantiate(Resources.Load(PathContainer.CatagoryPath + "Container" + enableCatagory)) as GameObject;
            catagoryGameObject.transform.parent = catagoryTable.transform;
            catagoryGameObject.transform.localPosition = new Vector3(0, 0, 0);
            catagoryGameObject.transform.localScale = new Vector3(1, 1, 1);
            catagoryGameObject.SetActive(true); 
            catagoryHash.Add(enableCatagory, catagoryGameObject);
        }
        catagoryTable.GetComponent<UITable>().Reposition();
        // Move scroll to current catagory
        string catagoryName = director.GetCatagoryString(currentLevel);
        int currentIndex = director.GetCatagoryIndex(currentLevel);
        float offset = 0;
        if(catagoryHash.Count > 1) {
            offset = ((float)(currentIndex - 1)) / (catagoryHash.Count - 1);
        }
        catagoryScrollView.SetDragAmount(offset, 0.5f, false);
        catagoryTable.GetComponent<UITable>().Reposition();
        centerOnChild.Recenter();
        // Show catagory name
        labelCatagory.key = "LK" + catagoryName;
        // Circle to set visible
        foreach( var key in catagoryHash.Keys) {
            foreach(Transform child in catagoryHash[key].transform) {
                LevelView levelView = child.gameObject.GetComponent<LevelView>();
                // Check whether level avaliable
                int state = director.GetLevelState(levelView.levelName);
                if(state == -1) {
                    levelView.Show(LevelState.Unabled);
                } else if(state == 1) {
                    levelView.Show(LevelState.Finished);
                } else if(state == 0) {
                    levelView.Show(LevelState.Current);
                }  
            }
        }

        // Coin
        labelCoin.text = director.GetCoin().ToString();
    }

    // Show levels
    public void _Show(string lastestlevel, string currentLevel)
    {
        // 把所有关卡按顺序加入list
        // 循环显示
        // 添加连接线
        // 根据需要来决定占位go的显示
    }

/*
    public void _Show(string lastestlevel, string currentLevel)
    {
        foreach(Transform child in levelContainer) {
            LevelView levelView = child.gameObject.GetComponent<LevelView>();
            int state = director.GetLevelState(levelView.levelName);
            if(state == -1) {
                levelView.Show(LevelState.Unabled);
            } else if(state == 1) {
                levelView.Show(LevelState.Finished);
                levelList.Add(levelView.gameObject);
            } else if(state == 0) {
                levelView.Show(LevelState.Current);
                levelList.Add(levelView.gameObject);
            }
        }

        // Sort list
        levelList.Sort( (x, y) => director.GetLevelIndex(x.GetComponent<LevelView>.levelName).CompareTo(director.GetLevelIndex(y.GetComponent<LevelView>.levelName));
        
        // AddLine
    
    }

    public void AddLevelLine(Transform beginTransform, Transform endTransform)
    {
        GameObject linkedLine = Instantiate(_lineTemplate);
        linkedLine.transform.parent = levelContainer.transform;
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
        float containerScale = _gameContainer.transform.localScale.x;
        int width = (int)(distance / scale / containerScale);

        //modify line width

        linkedLine.SetActive(true)
    }
    */
    
}
