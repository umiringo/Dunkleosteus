using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GlobalDefines;
using SimpleJSON;

public class LevelPlayView : MonoBehaviour {
    private GameObject _lineTemplate; // 线的模版
    private GameObject _lineContainer; // 线的容器
    private GameObject _gameContainer; // 关卡的容器
    private UILocalize _labelLevelTitle; // 关卡标题
    private UILocalize _labelLevelLatin; // 星座拉丁文
    private UILocalize _labelLevelSeason; // 星座季节
    private UILabel _labelLevelPosition; // 关卡的星座坐标
    private UILocalize _labelLevelInfo; // 星座介绍
    public GameObject menu;  // 完成游戏后的菜单
    public GameObject lastMenu; // 最后一关的游戏菜单
    public GameObject levelNameLabel; // 星座名的label
    // public GameObject levelComplete; // 完成游戏的对号
    public UILabel labelCoin;
    public UITexture texturePreview;
    public LevelPlayModel levelPlayModel;
	// Use this for initialization
    void Awake()
    {
        _lineContainer = null;
        _gameContainer = null;
        _labelLevelTitle = null;
        _labelLevelLatin = null;
        _labelLevelSeason = null;
        _labelLevelPosition = null;
        _labelLevelInfo = null;
        _lineTemplate = Resources.Load(PathContainer.LinkedLinePrefabPath) as GameObject;
        // levelComplete.SetActive(false);
        menu.SetActive(false);
        lastMenu.SetActive(false);
    }

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
    ///////////////////////////////////////////////////////////////////////////////////
    /// Interfaces                                                                  ///
    ///////////////////////////////////////////////////////////////////////////////////
    public void ClearUI()
    {
        // clear all data
        DestroyImmediate(_gameContainer); // Try to destroy the gamecontainer
        _lineContainer = null;
        _gameContainer = null;
        menu.SetActive(false);
        lastMenu.SetActive(false);
    }

    public void LoadLevelUI(string name)
    {
        // Load template data
        JSONNode jo = TemplateMgr.Instance.GetTemplateString(ConfigKey.LevelInfo, name);
        // Load name
        string levelName = levelPlayModel.levelName;
        levelNameLabel.GetComponent<UILocalize>().key = "LK" + levelName + "Title";
        levelNameLabel.SetActive(false);
        levelNameLabel.SetActive(true);
        // Load GameContainer
        _gameContainer = Instantiate(Resources.Load(PathContainer.ContainerPath + name + "Container" )) as GameObject;
        _gameContainer.transform.parent = this.gameObject.transform;
        _gameContainer.transform.localPosition = Vector3.zero;
        _gameContainer.transform.localScale = new Vector3(1.3f, 1.3f, 1.0f);
        // Init gameContainer's component
        _labelLevelTitle = _gameContainer.transform.Find("Detail/LabelContainer/LabelName").gameObject.GetComponent<UILocalize>();
        _labelLevelTitle.key = "LK" + levelName + "Title";
        _labelLevelLatin = _gameContainer.transform.Find("Detail/LabelContainer/ContainerInfo/LabelLatin").gameObject.GetComponent<UILocalize>();
        _labelLevelLatin.key = "LK" + levelName + "Latin";
        _labelLevelSeason = _gameContainer.transform.Find("Detail/LabelContainer/ContainerInfo/LabelSeason").gameObject.GetComponent<UILocalize>();
        _labelLevelSeason.key = jo["season"];
        _labelLevelPosition = _gameContainer.transform.Find("Detail/LabelContainer/ContainerInfo/LabelPosition").gameObject.GetComponent<UILabel>();
        _labelLevelPosition.text = jo["position"];
        _labelLevelInfo = _gameContainer.transform.Find("Detail/LabelContainer/LabelInfo").gameObject.GetComponent<UILocalize>();
        _labelLevelInfo.key = "LK" + levelName + "Info";
        _lineContainer = GameObject.Find(_gameContainer.name + "/Sky/LineContainer");
        labelCoin.text = levelPlayModel.GetCoin().ToString();
    }

    // Load Level Data
    public void FlopCard(GameObject go)
    {
        _gameContainer.GetComponent<GameContainer>().BeginToFlop(go);
    }

    // Show complete label 
    public void ShowComplete()
    {
        menu.SetActive(false);
        lastMenu.SetActive(false);
        //levelComplete.SetActive(true);
    }

    // Show menu(next/back)
    public void ShowMenu()
    {
        //levelComplete.GetComponent<TweenAlpha>().Play(true);
        if(levelPlayModel.IsFin()) {
            lastMenu.SetActive(true);
        } else {
            menu.SetActive(true);
        }
    }

    public void ShowGameWin()
    {
        _gameContainer.GetComponent<GameContainer>().GameWin();
    }

    public GameObject AddLine(Transform beginTransform, Transform endTransform)
    {
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
        float angle = this.CaculateAngle(beginTransform, endTransform);
        linkedLine.transform.position = beginTransform.position;
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
        return linkedLine;
    }

    public GameObject GetStarGo(int index)
    {
        return _gameContainer.transform.Find("Sky/StarContainer/Star" + index).gameObject;
    }

    public void UpdateCoinLabel()
    {
        labelCoin.text = levelPlayModel.GetCoin().ToString();
    }

    public void ShowPreview(string levelName)
    {
        _gameContainer.GetComponent<GameContainer>().ShowPreview();
    }
    ///////////////////////////////////////////////////////////////////////////////////
    /// Inner logic function                                                        ///
    ///////////////////////////////////////////////////////////////////////////////////
    #region InnerFuction
    private float CaculateAngle(Transform begin, Transform end)
    {
        float angle = 0.0f;
        angle = Mathf.Rad2Deg * Mathf.Atan((begin.position.y - end.position.y) / (begin.position.x - end.position.x));
        return angle;
    }

    #endregion
}
