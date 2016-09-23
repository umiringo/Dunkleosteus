using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GlobalDefines;
using SimpleJSON;

class LinkedPair
{
    public int indexBegin;
    public int indexEnd;
    public GameObject line;
    public LinkedPair(int b, int e, GameObject l)
    {
        if (b > e) {
            indexBegin = b;
            indexEnd = e;
        }
        else if (e > b) {
            indexBegin = e;
            indexEnd = b;
        }
        else {
            Debug.LogError("LevelPlayMgr:LinkedPair: Weird situation! indexBegin should euqals indexEnd! " + b.ToString() + e.ToString());
            return;
        }
        line = l;
    }
    public bool EqualPair(int s, int e)
    {
        if ((indexBegin == s && indexEnd == e) || (indexBegin == e && indexEnd == s)) {
            return true;
        }
        return false;
    }
};

public class LevelPlayMgr : MonoBehaviour {

    private List<LinkedPair> _linkedLineList;  // 已经连接的线表
    private Dictionary<int, int> _starDictionary; // 星星所连接线的hash表，之前用于显示颜色用
    private List<int> _correctAnswerList; // 正确答案
    private List<int> _answerList; // 当前答案
    private GameObject _readyStar; // 选中的星星
    private GameObject _lineTemplate; // 线的模版
    private GameObject _lineContainer; // 线的容器
    private GameObject _gameContainer; // 关卡的容器
    private UILocalize _labelLevelTitle; // 关卡标题
    private UILocalize _labelLevelLatin; // 星座拉丁文
    private UILocalize _labelLevelSeason; // 星座季节
    private UILabel _labelLevelPosition; // 关卡的星座坐标
    private UILocalize _labelLevelInfo; // 星座介绍
    public GameObject _completeLabel; // 完成游戏的说明
    public GameObject _menu;  // 完成游戏后的菜单
    public GameObject _lastMenu; // 最后一关的游戏菜单
    public GameObject _levelNameLabel; // 星座名的label
    public GameObject _levelComplete; // 完成游戏的对号
    public GameDirector director;

    private string _levelName;
    public string levelName {
        get {
            return _levelName;
        }
    }

	// Use this for initialization
    void Awake()
    {
        _linkedLineList = new List<LinkedPair>();
        _starDictionary = new Dictionary<int, int>();
        _correctAnswerList = new List<int>();
        _answerList = new List<int>();
        _readyStar = null;
        _lineTemplate = null;
        _lineContainer = null;
        _gameContainer = null;
        _labelLevelTitle = null;
        _labelLevelLatin = null;
        _labelLevelSeason = null;
        _labelLevelPosition = null;
        _labelLevelInfo = null;
        _lineTemplate = Resources.Load(PathContainer.LinkedLinePrefabPath) as GameObject;
        _completeLabel.SetActive(false);
        _levelComplete.SetActive(false);
        _menu.SetActive(false);
        _lastMenu.SetActive(false);
    }

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
    ///////////////////////////////////////////////////////////////////////////////////
    /// Interfaces                                                                  ///
    ///////////////////////////////////////////////////////////////////////////////////
    // Load Level Data
    public void LoadLevel(string name)
    {
        // clear all data
        _linkedLineList.Clear();
        _starDictionary.Clear();
        _correctAnswerList.Clear();
        _answerList.Clear();
        _readyStar = null;
        DestroyImmediate(_gameContainer); // Try to destroy the gamecontainer
        _lineContainer = null;
        _gameContainer = null;
        _completeLabel.SetActive(false);
        _levelComplete.SetActive(false);
        _levelComplete.GetComponent<TweenAlpha>().ResetToBeginning();
        _menu.SetActive(false);
        _lastMenu.SetActive(false);

        // Load template data
        JSONNode jo = TemplateMgr.Instance.GetTemplateString(ConfigKey.LevelInfo, name);
        // Load name
        _levelName = jo["name"];
        _levelNameLabel.GetComponent<UILocalize>().key = "LK" + _levelName + "Title";
        _levelNameLabel.SetActive(false);
        _levelNameLabel.SetActive(true);
        // Load answer
        JSONArray answerList = jo["answer"] as JSONArray;
        foreach (JSONNode answerObject in answerList) {
            int tmpAnswer = answerObject.AsInt;
            _correctAnswerList.Add(tmpAnswer);
        }
        // Load GameContainer
        _gameContainer = Instantiate(Resources.Load(PathContainer.ContainerPath + name + "Container" )) as GameObject;
        _gameContainer.transform.parent = this.gameObject.transform;
        _gameContainer.transform.localPosition = Vector3.zero;
        _gameContainer.transform.localScale = new Vector3(1.3f, 1.3f, 0.0f);
        // Init gameContainer's component
        _labelLevelTitle = _gameContainer.transform.Find("Detail/LabelContainer/LabelName").gameObject.GetComponent<UILocalize>();
        _labelLevelTitle.key = "LK" + _levelName + "Title";
        _labelLevelLatin = _gameContainer.transform.Find("Detail/LabelContainer/ContainerInfo/LabelLatin").gameObject.GetComponent<UILocalize>();
        _labelLevelLatin.key = "LK" + _levelName + "Latin";
        _labelLevelSeason = _gameContainer.transform.Find("Detail/LabelContainer/ContainerInfo/LabelSeason").gameObject.GetComponent<UILocalize>();
        _labelLevelSeason.key = jo["season"];
        _labelLevelPosition = _gameContainer.transform.Find("Detail/LabelContainer/ContainerInfo/LabelPosition").gameObject.GetComponent<UILabel>();
        _labelLevelPosition.text = jo["position"];
        _labelLevelInfo = _gameContainer.transform.Find("Detail/LabelContainer/LabelInfo").gameObject.GetComponent<UILocalize>();
        _labelLevelInfo.key = "LK" + _levelName + "Info";
        _lineContainer = GameObject.Find(_gameContainer.name + "/Sky/LineContainer");
    }

    // Click star
    public void TriggerStar(GameObject goStar)
    {
        Debug.Log("LevelPlayMgr:TriggerStar : goStar_name = " + goStar.name);
        int index = goStar.GetComponent<Star>().index;
        //Check whether ready
        if (_readyStar == null) {
            //first one
            _readyStar = goStar;
            RefreshStar(goStar);
        }
        else {
            if (_readyStar.GetComponent<Star>().index == index) {
                _readyStar = null;
                RefreshStar(goStar);
            }
            else {
                TryLinkStar(_readyStar, goStar);
            }
        }
    }
    
    // Click Card
    public void TriggerCard(GameObject go)
    {
       _gameContainer.GetComponent<GameContainer>().BeginToFlop(go);
    }

    // Show complete label 
    public void ShowComplete()
    {
        _menu.SetActive(false);
        _lastMenu.SetActive(false);
        _completeLabel.SetActive(true);
        _levelComplete.SetActive(true);
    }

    // Show menu(next/back)
    public void ShowMenu()
    {
        _completeLabel.SetActive(false);
        _levelComplete.GetComponent<TweenAlpha>().Play(true);
        if(director.GetNextLevel() == "fin") {
            _lastMenu.SetActive(true);
        } else {
            _menu.SetActive(true);
        }
        
    }

    // Click back
    public void OnBackToLevelSelect()
    {
        director.OnBackSelectLevel();
    }

    // Click next
    public void OnNextLevel()
    {
        string nextLevel = director.GetNextLevel();
        if(nextLevel == "fin") {
            return;
        } else {
            director.OnStartNextLevel();  
        }
    }

    // Win
    public void OnGameWin()
    {
        director.FinishLevel(_levelName);
        _gameContainer.GetComponent<GameContainer>().GameWin();
    }

    // Click tips
    public void OnTips()
    {
        // Check if have coin
        if(director.GetCoin() >= DefineNumber.TipCost) {
            this.DoTip();
        } 
        else {
            // Do charge
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////
    /// Inner logic function                                                        ///
    ///////////////////////////////////////////////////////////////////////////////////
    #region InnerFuction
    // Check whether answer is correct
    private bool CheckAnswer()
    {
        return _correctAnswerList.TrueForAll(_answerList.Contains) && _answerList.TrueForAll(_correctAnswerList.Contains);
    }
    // Add Answer to list
    private void AddAnswerToList(int indexBegin, int indexEnd)
    {
        // Add Answer to list
        int answerMark = 0;
        if (indexBegin > indexEnd) {
            answerMark = indexEnd * 100 + indexBegin;
        }
        else {
            answerMark = indexBegin * 100 + indexEnd;
        }
        if (_answerList.Contains(answerMark)) {
            Debug.Log("GamePlayMgr.AddAnswerToList: Some is rong, answerMark exist. mark = " + answerMark);
            return;
        }
        _answerList.Add(answerMark);

        // Check whether sucess
        if (CheckAnswer()) {
            // Win
            this.OnGameWin();
        }
    }

    // Remove Answer from list
    private void RemoveAnswerFromList(int indexBegin, int indexEnd)
    {
        // Just remove from _answerList
        int answerMark = 0;
        if (indexBegin > indexEnd) {
            answerMark = indexEnd * 100 + indexBegin;
        }
        else {
            answerMark = indexBegin * 100 + indexEnd;
        }
        _answerList.Remove(answerMark);

        // Check whether sucess
        if (CheckAnswer()) {
            // Win
            this.OnGameWin();
        }
    }

    // Check whether line(b,e) is already existed
    private bool HaveLinkedLine(int b, int e)
    {
        foreach (LinkedPair lp in _linkedLineList) {
            if (lp.EqualPair(b, e)) {
                return true;
            }
        }
        return false;
    }

    // Get the reference of line(b,e)
    private int GetLinkedLine(int b, int e)
    {
        for (int i = 0; i < _linkedLineList.Count; ++i) {
            if (_linkedLineList[i].EqualPair(b, e)) {
                return i;
            }
        }
        return -1;
    }

    // Check whether star(s) is already have be connected
    private bool IsStarLinked(int s)
    {
        if (_starDictionary.ContainsKey(s) && _starDictionary[s] > 0) {
            return true;
        }
        return false;
    }

    // Refresh Stars state
    private void RefreshStar(GameObject goStar)
    {
        Star starComponent = goStar.GetComponent<Star>();
        int indexStar = starComponent.index;
        // Check whether is ready
        if (_readyStar != null && _readyStar.GetComponent<Star>().index == indexStar) {
            starComponent.SetChosen();
        }
        else {
            // Not ready
            if (IsStarLinked(indexStar)) {
                starComponent.SetLinked();
            }
            else {
                starComponent.SetNormal();
            }
        }
    }

    private float CaculateAngle(Transform begin, Transform end)
    {
        float angle = 0.0f;
        angle = Mathf.Rad2Deg * Mathf.Atan((begin.position.y - end.position.y) / (begin.position.x - end.position.x));

        return angle;
    }

    // Add a line, this function is also the key logic of linked
    private void TryLinkStar(GameObject goBegin, GameObject goEnd)
    {
        if (goBegin == null || goEnd == null) {
            return;
        }
        int indexB = goBegin.GetComponent<Star>().index;
        int indexE = goEnd.GetComponent<Star>().index;

        //Check whether already linke
        int index = this.GetLinkedLine(indexB, indexE);
        if (index >= 0) {
            //Already linked, try to delete it
            GameObject goLine = _linkedLineList[index].line;
            //delte from list
            _linkedLineList.RemoveAt(index);
            //modify hash table
            if (IsStarLinked(indexB)) {
                _starDictionary[indexB]--;
            }
            if (IsStarLinked(indexE)) {
                _starDictionary[indexE]--;
            }
            //delete the line
            Destroy(goLine);
            //remove two star from anwser
            RemoveAnswerFromList(indexB, indexE);
        }
        else {
            // Try to link two stars
            Transform beginTransform = goBegin.transform;
            Transform endTransform = goEnd.transform;
            // Instantiate line
            GameObject linkedLine = Instantiate(_lineTemplate);
            linkedLine.transform.parent = _lineContainer.transform;
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

            float angle = CaculateAngle(goBegin.transform, goEnd.transform);
            linkedLine.transform.position = goBegin.transform.position;
            linkedLine.transform.rotation = Quaternion.Euler(0, 0, angle);
            linkedLine.transform.localScale = new Vector3(1, 1, 1);

            float distance = Vector3.Distance(beginTransform.position, endTransform.position);
            float scale = GameObject.Find(PathContainer.UIRootPath).transform.localScale.x;
            float containerScale = _gameContainer.transform.localScale.x;
            int width = (int)(distance / scale / containerScale);
            TweenWidth lineTweenWidth = linkedLine.GetComponent<TweenWidth>();
            lineTweenWidth.from = 0;
            lineTweenWidth.to = width;
            linkedLine.SetActive(true);
            // Add record to List
            LinkedPair lp = new LinkedPair(indexB, indexE, linkedLine);
            _linkedLineList.Add(lp);
            // Add record to Hash
            if (_starDictionary.ContainsKey(indexB)) {
                _starDictionary[indexB]++;
            }
            else {
                _starDictionary[indexB] = 1;
            }
            if (_starDictionary.ContainsKey(indexE)) {
                _starDictionary[indexE]++;
            }
            else {
                _starDictionary[indexE] = 1;
            }
            //Add two star to answerList
            this.AddAnswerToList(indexB, indexE);
        }

        // Reset ready starts
        _readyStar = null;
        // Refresh the stars state
        this.RefreshStar(goBegin);
        this.RefreshStar(goEnd);
    }

    private void TipLinkLine(int indexBegin, int indexEnd) {
        GameObject goBegin = _gameContainer.transform.Find("Sky/SkyContainer/Star" + indexBegin).gameObject;
        GameObject goEnd = _gameContainer.transform.Find("Sky/SkyContainer/Star" + indexEnd).gameObject;
        TryLinkStar(goBegin, goEnd);
        director.SubCoin(DefineNumber.TipCost);
    }

    private void DoTip()
    {
        // Check if already
        if(CheckAnswer()) {
            return;
        }
        // Traverse the answer list for error one
        foreach(int ans in _answerList) {
            // Check ans whether is correct
            if(!_correctAnswerList.Contains(ans)) {
                // Wrong ans, try to remove it 
                TipLinkLine(ans % 100, ans / 100);
                return;
            }
        }

        // Traverse correct answer to find a new line
        foreach(int cans in _correctAnswerList) {
            if(!_answerList.Contains(cans)) {
                // Right ans, try to link it 
                TipLinkLine(cans % 100, cans / 100);
                return;
            }
        }
    }
    #endregion
}
