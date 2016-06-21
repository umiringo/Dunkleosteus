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
            Debug.LogError("GamePlayMgr:LinkedPair: Wired situation! indexBegin should euqals indexEnd! " + b.ToString() + e.ToString());
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

    private List<LinkedPair> _linkedLineList;
    private Dictionary<int, int> _starDictionary;
    private List<int> _correctAnswerList;
    private List<int> _answerList;
    private GameObject _readyStar;
    private GameObject _LineTemplate;
    private GameObject _LineContainer;
    private string _levelName;
    public string levelName {
        get {
            return _levelName;
        }
    }

    public GameObject _gameContainer;

	// Use this for initialization
	void Start () {
        _linkedLineList = new List<LinkedPair>();
        _starDictionary = new Dictionary<int, int>();
        _answerList = new List<int>();
        _correctAnswerList = new List<int>();
        _readyStar = null;
        _LineTemplate = Resources.Load(PathContainer.LinkedLinePrefabPath) as GameObject;
        _LineContainer = GameObject.Find("GameContainer/Sky/LineContainer");
        
        //TODO just for test
        TemplateMgr.Instance.Init();

        this.LoadTemplateData("level_info", "Scorpius");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    ///////////////////////////////////////////////////////////////////////////////////
    /// Interfaces                                                                  ///
    ///////////////////////////////////////////////////////////////////////////////////
    public void OnStarClicked(GameObject goStar)
    {
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
    ///////////////////////////////////////////////////////////////////////////////////
    /// Inner logic function                                                        ///
    ///////////////////////////////////////////////////////////////////////////////////
    
    #region InnerFuction
    // Load template data
    private void LoadTemplateData(string fileName, string levelName)
    {
        JSONNode jo = TemplateMgr.Instance.GetTemplateString(fileName, levelName);
            // load name
        _levelName = jo["name"];
            // load answer
        JSONArray answerList = jo["answer"] as JSONArray;
        foreach( JSONNode answerObject in answerList) {
            int tmpAnswer = answerObject.AsInt;
            _correctAnswerList.Add(tmpAnswer);
        }
    }
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
            _gameContainer.GetComponent<GameContainer>().GameWin();
        }
    }

    private void RemoveAnswerFromList(int indexBegin, int indexEnd)
    {
        // just remove from _answerList
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
            _gameContainer.GetComponent<GameContainer>().GameWin();
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

    //Get the reference of line(b,e)
    private int GetLinkedLine(int b, int e)
    {
        for (int i = 0; i < _linkedLineList.Count; ++i) {
            if (_linkedLineList[i].EqualPair(b, e)) {
                return i;
            }
        }
        return -1;
    }

    //Check whether star(s) is already have be connected
    private bool IsStarLinked(int s)
    {
        if (_starDictionary.ContainsKey(s) && _starDictionary[s] > 0) {
            return true;
        }
        return false;
    }

    //Refresh Stars state
    private void RefreshStar(GameObject goStar)
    {
        Star starComponent = goStar.GetComponent<Star>();
        int indexStar = starComponent.index;
        //Check whether is ready
        if (_readyStar != null && _readyStar.GetComponent<Star>().index == indexStar) {
            starComponent.SetChosen();
        }
        else {
            //Not ready
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

    //Add a line, this function is also the key logic of linked
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
            this.RemoveAnswerFromList(indexB, indexE);
        }
        else {
            // Try to link two stars

            Transform beginTransform = goBegin.transform;
            Transform endTransform = goEnd.transform;
            // Instantiate line
            GameObject linkedLine = Instantiate(_LineTemplate);
            linkedLine.transform.parent = _LineContainer.transform;
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
            int width = (int)(distance / scale);

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
            // Draw Line
            //linkedLine.GetComponent<LineDrawer>().Draw(goBegin, goEnd);

            //Add two star to answerList
            this.AddAnswerToList(indexB, indexE);
        }

        // Reset ready starts
        _readyStar = null;
        // Refresh the stars state
        this.RefreshStar(goBegin);
        this.RefreshStar(goEnd);
    }
    #endregion
}
