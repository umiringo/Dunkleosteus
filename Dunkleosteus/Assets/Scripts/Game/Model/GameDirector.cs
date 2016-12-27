using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using GlobalDefines;
using System.Linq;

public class GameDirector : MonoBehaviour {
    private Dictionary<string, int> levelHash = new Dictionary<string, int>();
    private Dictionary<string, List<string> > catagoryHash = new Dictionary<string, List<string> >();
    private int currentCatagory;
    private string currentLevel; // 当前正在进行的关卡
    private int coin;
    private FiniteStateMachine _fsm;
    private Dictionary<string, string> abbrHash = new Dictionary<string, string>();

    public LevelPlayModel levelPlayModel;
    public LevelSelectView levelSelectView;
    public CardView cardView;
    
    //public GameObject panelStart; // 开始界面 
    public GameObject panelMainMenu; // 主菜单界面
    public GameObject panelLevelSelect; // 关卡选择界面
    public GameObject panelPlay; // 游戏界面
    public GameObject panelCard; // 卡片浏览界面
    public GameObject panelNotify;
    public GameObject panelOption;

    public AudioPlayerModel audioplayer;

    void Awake() {
        // Init template data
        TemplateMgr.Instance.Init();
        // Init finite state machine
        InitFiniteStateMachine();
        // Init level list;
        InitLevelList();
        // Init player prefs
        LoadPlayerPrefs();
        // Init Localization
        InitLocalization();
        // Init abbreviation
        InitAbbrHash();
    }

    void Start () {
        audioplayer.PlayOpBGM();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TriggerTransition(StateTransition trans)
    {
        _fsm.PerformTransition(trans);
    } 

    private void InitFiniteStateMachine()
    {
        // Init all states
        MainMenuState mainMenu = new MainMenuState(this);
        mainMenu.AddTransition(StateTransition.PressStart, StateID.LevelSelect);

        LevelSelectState levelSelect = new LevelSelectState(this);
        levelSelect.AddTransition(StateTransition.ViewCard, StateID.CardView);
        levelSelect.AddTransition(StateTransition.ChoseLevel, StateID.GameScene);
        levelSelect.AddTransition(StateTransition.ViewOption, StateID.OptionView);
        levelSelect.AddTransition(StateTransition.ViewPay, StateID.PayView);

        CardViewState cardView = new CardViewState(this);
        cardView.AddTransition(StateTransition.BackToLevelSelect, StateID.LevelSelect);

        GameSceneState gameScene = new GameSceneState(this);
        gameScene.AddTransition(StateTransition.BackToLevelSelect, StateID.LevelSelect);

        OptionViewState optionView = new OptionViewState(this);
        optionView.AddTransition(StateTransition.BackToLevelSelect, StateID.LevelSelect);

        PayViewState payView = new PayViewState(this);
        payView.AddTransition(StateTransition.BackToLevelSelect, StateID.LevelSelect);

        // Init finite state machine
        _fsm = new FiniteStateMachine();
        _fsm.AddFiniteState(mainMenu);
        _fsm.AddFiniteState(levelSelect);
        _fsm.AddFiniteState(cardView);
        _fsm.AddFiniteState(gameScene);
        _fsm.AddFiniteState(optionView);
        _fsm.AddFiniteState(payView);
    }

    private void InitLevelList()
    {
        // Build levelname to index Hash
        JSONArray jaLevel = TemplateMgr.Instance.GetTemplateArray(ConfigKey.LevelInfo, ConfigKey.LevelSelect);
        for(int i = 0; i < jaLevel.Count; ++i) {
            levelHash.Add(jaLevel[i], i);
        }
    }

    private void LoadPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        // Init latestLevel
        string latestLevel = PlayerPrefs.GetString(PlayerPrefsKey.LatestLevel, "Crater");
        // first level
        if(latestLevel == "begin") {
            PlayerPrefs.SetString(PlayerPrefsKey.LatestLevel, latestLevel);
            currentLevel = DefineString.FirstLevel;
        }
        else {
            PlayerPrefs.SetString(PlayerPrefsKey.LatestLevel, latestLevel);
            string nextLevel = GetNextLevelByIndexName(latestLevel);
            if(nextLevel == "fin") {
                currentLevel = latestLevel;
            }
            else {
                currentLevel = nextLevel;
            }
        }
        // Init Coin
        coin = PlayerPrefs.GetInt(PlayerPrefsKey.Coin, 10000);
        this.InitCatagoryHash(latestLevel);
    }

    private void InitLocalization()
    {
        // TODO 需要修改成本机的语言设置
        Localization.language = PlayerPrefs.GetString(PlayerPrefsKey.Language, "English");       
    }

    private void InitCatagoryHash(string latestLevel)
    {
        catagoryHash.Clear();
        // 遍历已经完成的所有关卡，依次插入到对应的list中
        int index = this.GetLevelIndex(latestLevel);
        for(int i = 0; i <= index; i++) {
            // 获取关卡数据
            string levelName = this.GetLevelByIndex(i);
            JSONNode jo = TemplateMgr.Instance.GetTemplateString(ConfigKey.LevelInfo, levelName);
            string catagory = jo["catagory"];
            // 如果找不到key，则初始化一个
            if(!catagoryHash.ContainsKey(catagory)) {
                catagoryHash[catagory] = new List<string>();
            }
            // 添加
            catagoryHash[catagory].Add(levelName);
        }
    }

    private void InitAbbrHash()
    {
        abbrHash["Zodiac"] = "Z";
        abbrHash["Orion"] = "O";
        abbrHash["UrsaMajor"] = "U";
        abbrHash["HeavenlyWaters"] = "W";
        abbrHash["Perseus"] = "P";
        abbrHash["Bayer"] = "B";
        abbrHash["LaCaille"] = "L";
        abbrHash["Hercules"] = "H";
    }

    #region StateInterface
    public void EnterMainMenuState()
    {
        Debug.Log("EnterMainMenuState");
        panelMainMenu.SetActive(true);
    }

    public void ExitMainMenuState()
    {
        panelMainMenu.GetComponent<FadeInOut>().FadeOut();
    }

    public void EnterLevelSelectState()
    {
        panelLevelSelect.GetComponent<FadeInOut>().FadeIn();
        string latestLevel = PlayerPrefs.GetString(PlayerPrefsKey.LatestLevel);
        levelSelectView.Show(latestLevel, currentLevel);
        audioplayer.PlaySelectBGM();
        
    }

    public void ExitLevelSelectState()
    {
        // panelLevelSelect.SetActive(false);
        panelLevelSelect.GetComponent<FadeInOut>().FadeOut();
        levelSelectView.BeforeExit();
    }

    public void EnterGameSceneState()
    {
        //panelPlay.SetActive(true);
        panelPlay.GetComponent<FadeInOut>().FadeIn();
        levelPlayModel.LoadLevel(currentLevel);
        audioplayer.PlayGameBGM();
    }

    public void ExitGameSceneState()
    {
       // panelPlay.SetActive(false);
        panelPlay.GetComponent<FadeInOut>().FadeOut();
    }

    public void EnterCardViewState()
    {
       // panelCard.SetActive(true);
        panelCard.GetComponent<FadeInOut>().FadeIn();
        cardView.Show("Zodiac");
    }

    public void ExitCardViewState()
    {
        panelCard.GetComponent<FadeInOut>().FadeOut();
        cardView.BeforeExit();
       // panelCard.SetActive(false);
    }

    public void EnterOptionViewState()
    {
       // panelOption.SetActive(true);
        panelOption.GetComponent<FadeInOut>().FadeIn();
    }

    public void ExitOptionViewState()
    {
        panelOption.GetComponent<FadeInOut>().FadeOut();
    }

    public void EnterPayViewState()
    {

    }

    public void ExitPayViewState()
    {

    }

    public void StartGame()
    {
        // Enter level select state
        _fsm.PerformTransition(StateTransition.PressStart);
    }

    public void SelectLevel(string level)
    {
     //   string latestLevel = PlayerPrefs.GetString(PlayerPrefsKey.LatestLevel);
        if (GetLevelState(level) < 0) {
            return;
        }
        // Check the level is availiable
        currentLevel = level;
        _fsm.PerformTransition(StateTransition.ChoseLevel);
    }

    public void BackSelectLevel()
    {
        _fsm.PerformTransition(StateTransition.BackToLevelSelect);
    }

    public void StartNextLevel()
    {
        // currentLevel is already next level
        levelPlayModel.LoadLevel(currentLevel);  
    }

    public void StartCardView()
    {
        _fsm.PerformTransition(StateTransition.ViewCard);
    }

    public void CardViewSwitchCatagory(string catagory)
    {
        cardView.SwitchCatagory(catagory);
    }

    public void OnShowCatagoryNotify()
    {
        string catagory = cardView.GetCurrentCatagory();
        this.ShowNotify("LK" + catagory, "LK" + catagory + "Content");
    }

    public void ShowNotify(string title, string content)
    {
        panelNotify.SetActive(false);
        panelNotify.GetComponent<NotifyView>().Init(title, content);
        panelNotify.GetComponent<FadeInOut>().FadeIn();
    }

    public void CloseNotify()
    {
        panelNotify.GetComponent<FadeInOut>().FadeOut();
    }

    public void StartOptionView()
    {
        _fsm.PerformTransition(StateTransition.ViewOption);
    }
    #endregion

    #region public interface
    // 检查关卡状态：－1，未开放，1，已经完成，0，当前
    public int GetLevelState(string level)
    {
        string latestLevel = PlayerPrefs.GetString(PlayerPrefsKey.LatestLevel, DefineString.FirstLevel);
        if(latestLevel == "begin") {
            if(level == DefineString.FirstLevel) {
                return 0;
            }
            return -1;
        }

        int latestIndex = levelHash[latestLevel];
        int levelIndex = levelHash[level];
        if(levelIndex - latestIndex == 1) {
            return 0;
        }
        if(levelIndex - latestIndex > 1) {
            if(levelIndex - latestIndex > 4) {
                return -1;
            }
            return levelIndex - latestIndex;
        }
        return 1;
    }

    public string GetCatagoryString(string level)
    {
        JSONNode levelInfoJo = TemplateMgr.Instance.GetTemplateString(ConfigKey.LevelInfo, level);
        return levelInfoJo["catagory"];
    }

    public string GetCatagoryString(int index) 
    {
        JSONNode catagoryInfo = TemplateMgr.Instance.GetTemplateString(ConfigKey.LevelInfo, ConfigKey.Catagory);
        return catagoryInfo[index - 1];
    }

    public string GetNextLevelByIndexName(string level)
    {
        int index = levelHash[level];
        return GetLevelByIndex(index + 1);
    }

    public string GetNextLevel()
    {
        int index = levelHash[currentLevel];
        return GetLevelByIndex(index + 1);
    }

    public string GetLevelByIndex(int index)
    {
        JSONArray levelSelectInfo = TemplateMgr.Instance.GetTemplateArray(ConfigKey.LevelInfo, ConfigKey.LevelSelect);
        if(index >= levelSelectInfo.Count) {
            return "fin";
        }
        return levelSelectInfo[index];
    }

    public int GetLevelIndex(string levelName)
    {
        if(levelName == "begin") {
            return -1;
        }
        return levelHash[levelName];
    }

    public bool FinishLevel()
    {
       // string latestLevel = PlayerPrefs.GetString(PlayerPrefsKey.LatestLevel, "begin");
        int ret = GetLevelState(currentLevel);
        if(ret == 0) {
            PlayerPrefs.SetString(PlayerPrefsKey.LatestLevel, currentLevel);
            // 将完成的关卡加入到已经完成的关卡列表中(catagoryHash)
            JSONNode jo = TemplateMgr.Instance.GetTemplateString(ConfigKey.LevelInfo, currentLevel);
            string catagory = jo["catagory"];
            if(!catagoryHash.ContainsKey(catagory)) {
                catagoryHash[catagory] = new List<string>();
            }
            // 添加
            catagoryHash[catagory].Add(currentLevel);
        }
        string nextLevel = GetNextLevel();
        if(nextLevel == "fin") {
            // Game ending
            return true;
        }
        currentLevel = nextLevel;
        return false;
    }

    public int GetCoin()
    {
        return coin;
    }

    public void AddCoin(int num)
    {
        coin += num;
        PlayerPrefs.SetInt(PlayerPrefsKey.Coin, coin);
    }

    public void SubCoin(int num)
    {
        if (coin < num) {
            coin = 0;
        }
        else {
            coin -= num;
        }
        PlayerPrefs.SetInt(PlayerPrefsKey.Coin, coin);
    }

    public List<string> GetEnableLevelList(string catagory)
    {
        if(!catagoryHash.ContainsKey(catagory)) {
            return new List<string>();
        }
        return catagoryHash[catagory];
    }

    public string GetAbbreviation(string catagory) 
    {
        return abbrHash[catagory];
    }
    #endregion
}
