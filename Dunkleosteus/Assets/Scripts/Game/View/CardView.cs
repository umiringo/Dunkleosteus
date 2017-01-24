using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GlobalDefines;
using SimpleJSON;

public class CardView : MonoBehaviour {
    private string _currentCatagory;
    private GameObject _cardInfoTemplate;
    private GameObject tableCard;

    public GameObject containerMenu;
    public GameDirector director;
    public GameObject scrollViewGameObject;
    public UILocalize labelTitle;
    public UILabel labelLevel;

    void Awake() {
        _cardInfoTemplate = Resources.Load(PathContainer.CardInfoPrefabPath) as GameObject;
        tableCard = this.gameObject.transform.Find("PanelCardList/PanelCardTable/TableCard").gameObject;
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Show(string catagory)
    {
        _currentCatagory = catagory;

        this.RefreshTableCard();
        this.RefreshMenu();
    }

    public void RefreshTableCard()
    {
        labelTitle.gameObject.SetActive(false);
        labelTitle.key = "LK" + _currentCatagory;
        labelTitle.gameObject.SetActive(true);
        // 清空表中所有对象
        while(tableCard.transform.childCount > 0) {
            DestroyImmediate(tableCard.transform.GetChild(0).gameObject);
        }
        // 构造各个catagory的CatagoryInfo对象
        List<string> levelList = director.GetEnableLevelList(_currentCatagory);
        foreach(var level in levelList) {
            GameObject cardInfo = Instantiate(_cardInfoTemplate);
            CardInfoView cardInfoView = cardInfo.GetComponent<CardInfoView>();
            cardInfoView.Init(level, scrollViewGameObject);  
            cardInfo.transform.parent = tableCard.transform;
            cardInfo.transform.localPosition = new Vector3(0, 0, 0); 
            cardInfo.transform.localScale = new Vector3(1, 1, 1);         
        }
        tableCard.GetComponent<UITable>().Reposition();
        scrollViewGameObject.GetComponent<UIScrollView>().ResetPosition();

        int finishCount = levelList.Count;
        int allCount = director.GetStarAllNum(_currentCatagory);
        if (finishCount >= allCount) {
            labelLevel.text = DefineString.DarkBlueColor + finishCount + " / " + allCount + "[-]";
        }
        else {
            labelLevel.text = DefineString.NormalBlueColor + finishCount + "[-]" + DefineString.DarkBlueColor + " / " + allCount + "[-]";
        }
    }

    public void RefreshMenu()
    {
        foreach(Transform child in containerMenu.transform) {
            MenuView menuView = child.gameObject.GetComponent<MenuView>();
            menuView.Show(_currentCatagory);
        }
    }

    public void BeforeExit()
    {
        while(tableCard.transform.childCount > 0) {
            DestroyImmediate(tableCard.transform.GetChild(0).gameObject);
        }
    }

    public void SwitchCatagory(string catagory)
    {
        if(catagory == _currentCatagory){
            return;
        }
        _currentCatagory = catagory;
        this.RefreshTableCard();
        this.RefreshMenu();
    }

    public string GetCurrentCatagory()
    {
        return _currentCatagory;
    }
}
