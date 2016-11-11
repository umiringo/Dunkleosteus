using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using GlobalDefines;

public class GameDirector : MonoBehaviour {
    public LevelPlayModel levelPlayModel;
    public LevelSelectView levelSelectView;
    private Dictionary<string, int> levelHash = new Dictionary<string, int>();
    private Dictionary<string, int> catagoryHash = new Dictionary<string, int>();
    private int currentCatagory;
    private string currentLevel; // 当前正在进行的关卡
    private int coin;
    private FiniteStateMachine _fsm;

    public GameObject panelStart; // 开始界面 
    public GameObject panelMainMenu; // 主菜单界面
    public GameObject panelLevelSelect; // 关卡选择界面
    public GameObject panelPlay; // 游戏界面

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
    }

    void Start () {   
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

        CardViewState cardView = new CardViewState(this);
        cardView.AddTransition(StateTransition.BackToLevelSelect, StateID.LevelSelect);

        GameSceneState gameScene = new GameSceneState(this);
        gameScene.AddTransition(StateTransition.BackToLevelSelect, StateID.LevelSelect);

        // Init finite state machine
        _fsm = new FiniteStateMachine();
        _fsm.AddFiniteState(mainMenu);
        _fsm.AddFiniteState(levelSelect);
        _fsm.AddFiniteState(cardView);
        _fsm.AddFiniteState(gameScene);
    }

    private void InitLevelList()
    {
        // Build levelname to index Hash
        JSONArray jaLevel = TemplateMgr.Instance.GetTemplateArray(ConfigKey.LevelInfo, ConfigKey.LevelSelect);
        for(int i = 0; i < jaLevel.Count; ++i) {
            levelHash.Add(jaLevel[i], i);
        }

        JSONArray jaCatagory = TemplateMgr.Instance.GetTemplateArray(ConfigKey.LevelInfo, ConfigKey.Catagory);
        for(int i = 0; i < jaCatagory.Count; ++i) {
            catagoryHash.Add(jaCatagory[i], i + 1);
        }
    }

    private void LoadPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        // Init latestLevel
        string latestLevel = PlayerPrefs.GetString(PlayerPrefsKey.LatestLevel, "begin");
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
        currentCatagory = this.GetCatagoryIndex(currentLevel);
        // Init Coin
        coin = PlayerPrefs.GetInt(PlayerPrefsKey.Coin, 10000);
    }

    private void InitLocalization()
    {
        Localization.language = "SChinese";
    }

    #region StateInterface
    public void EnterMainMenuState()
    {
        panelMainMenu.SetActive(true);
        //Debug.Log("GameDirector : Enter MainMenuState.");
    }

    public void ExitMainMenuState()
    {
        panelMainMenu.SetActive(false);
        //Debug.Log("GameDirector : Exit MainMenuState.");
    }

    public void EnterLevelSelectState()
    {
        panelLevelSelect.SetActive(true);
        string latestLevel = PlayerPrefs.GetString(PlayerPrefsKey.LatestLevel);
        levelSelectView.Show(latestLevel, currentLevel);
        //Debug.Log("GameDirector : Enter LevelSelectState.");
    }

    public void ExitLevelSelectState()
    {
        panelLevelSelect.SetActive(false);
        levelSelectView.BeforeExit();
        //Debug.Log("GameDirector : Exit LevelSelectState.");
    }

    public void EnterGameSceneState()
    {
        panelPlay.SetActive(true);
        levelPlayModel.LoadLevel(currentLevel);
        //Debug.Log("GameDirector : Enter GameSceneState.");
    }

    public void ExitGameSceneState()
    {
        panelPlay.SetActive(false);
        //Debug.Log("GameDirector : Exit GameSceneState.");
    }

    public void EnterCardViewState()
    {
        //Debug.Log("GameDirector : Enter CardViewState.");
    }

    public void ExitCardViewState()
    {
        //Debug.Log("GameDirector : Exit CardViewState.");
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
            return -1;
        }
        return 1;
    }

    public int GetCatagoryIndex(string level)
    {
        JSONNode levelInfoJo = TemplateMgr.Instance.GetTemplateString(ConfigKey.LevelInfo, level);
        string catagory = levelInfoJo["catagory"];
        return catagoryHash[catagory];
    }

    public int GetNextLevelCatagoryIndex(string level)
    {
        if(level == "begin") {
            return 0;
        }
        int index = levelHash[level];
        string nextLevel = GetLevelByIndex(index + 1);
        if(nextLevel == "fin") {
            return GetCatagoryIndex(level);
        } else {
            return GetCatagoryIndex(nextLevel);
        }
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

    public bool FinishLevel(string levelName)
    {
       // string latestLevel = PlayerPrefs.GetString(PlayerPrefsKey.LatestLevel, "begin");
        int ret = GetLevelState(currentLevel);
        if(ret == 0) {
            PlayerPrefs.SetString(PlayerPrefsKey.LatestLevel, currentLevel);
        }
        string nextLevel = GetNextLevel();
        if(nextLevel == "fin") {
            // Game ending
            return true;
        }
        currentLevel = nextLevel;
        currentCatagory = GetCatagoryIndex(currentLevel);
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
    #endregion
}
