using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using GlobalDefines;

public class GameDirector : MonoBehaviour {
    public LevelPlayMgr levelPlayMgr;
    public LevelSelectMgr levelSelectMgr;
    private Dictionary<string, int> levelHash = new Dictionary<string, int>();
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
        JSONArray ja = TemplateMgr.Instance.GetTemplateArray(ConfigKey.LevelInfo, ConfigKey.LevelSelect);
        for(int i = 0; i < ja.Count; ++i) {
            levelHash.Add(ja[i], i);
        }
    }

    private void LoadPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        // Init lastestLevel
        string lastestLevel = PlayerPrefs.GetString(PlayerPrefsKey.LatestLevel);
        if(lastestLevel == "") {
            lastestLevel = DefineString.FirstLevel;
            PlayerPrefs.SetString(PlayerPrefsKey.LatestLevel, currentLevel);
        }
        string nextLevel = GetNextLevelByIndexName(lastestLevel);
        if(nextLevel == "fin") {
            currentLevel = lastestLevel;
        } else {
            currentLevel = nextLevel;
        }
        currentCatagory = this.GetCatagoryIndex(currentLevel);
        
        // Init Coin
        coin = PlayerPrefs.GetInt(PlayerPrefsKey.Coin, 0);
        
        Debug.Log("GameDirector.LoadPlayerPrefs: currentLevel = " + currentLevel + " | currentCatagory = " + currentCatagory);
    }

    private void InitLocalization()
    {
        Localization.language = "Japanese";
    }

    #region StateInterface
    public void EnterMainMenuState()
    {
        panelMainMenu.SetActive(true);
        Debug.Log("GameDirector : Enter MainMenuState.");
    }

    public void ExitMainMenuState()
    {
        panelMainMenu.SetActive(false);
        Debug.Log("GameDirector : Exit MainMenuState.");
    }

    public void EnterLevelSelectState()
    {
        panelLevelSelect.SetActive(true);
        string lastestLevel = PlayerPrefs.GetString(PlayerPrefsKey.LatestLevel);
        levelSelectMgr.Show(lastestLevel, currentLevel);
        Debug.Log("GameDirector : Enter LevelSelectState.");
    }

    public void ExitLevelSelectState()
    {
        panelLevelSelect.SetActive(false);
        Debug.Log("GameDirector : Exit LevelSelectState.");
    }

    public void EnterGameSceneState()
    {
        panelPlay.SetActive(true);
        levelPlayMgr.LoadLevel(currentLevel);
        Debug.Log("GameDirector : Enter GameSceneState.");
    }

    public void ExitGameSceneState()
    {
        panelPlay.SetActive(false);
        Debug.Log("GameDirector : Exit GameSceneState.");
    }

    public void EnterCardViewState()
    {
        Debug.Log("GameDirector : Enter CardViewState.");
    }

    public void ExitCardViewState()
    {
        Debug.Log("GameDirector : Exit CardViewState.");
    }

    #endregion


    #region Callback

    public void OnStarGame()
    {
        // Enter level select state
        _fsm.PerformTransition(StateTransition.PressStart);
    }

    public void OnSelectLevel(string level)
    {
        string latestLevel = PlayerPrefs.GetString(PlayerPrefsKey.LatestLevel);
        if (CompareLevel(level, latestLevel) > 0) {
            return;
        }
        // Check the level is availiable
        currentLevel = level;
        _fsm.PerformTransition(StateTransition.ChoseLevel);
    }

    public void OnBackSelectLevel()
    {
        _fsm.PerformTransition(StateTransition.BackToLevelSelect);
    }

    public void OnStartNextLevel()
    {
        // currentLevel is already next level
        levelPlayMgr.LoadLevel(currentLevel);  
    }
    #endregion

    #region public interface
    public int CompareLevel(string level1, string level2) 
    {
        return levelHash[level1] - levelHash[level2];
    }

    public int GetCatagoryIndex(string level)
    {
        JSONNode levelInfoJo = TemplateMgr.Instance.GetTemplateString(ConfigKey.LevelInfo, level);
        return levelInfoJo["catagory"].AsInt;
    }

    public string GetCatagoryString(string level)
    {
        JSONNode levelInfoJo = TemplateMgr.Instance.GetTemplateString(ConfigKey.LevelInfo, level);
        int catagory = levelInfoJo["catagory"].AsInt;

        JSONNode catagoryInfo = TemplateMgr.Instance.GetTemplateString(ConfigKey.LevelInfo, ConfigKey.Catagory);
        return catagoryInfo[catagory - 1];
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

    public void FinishLevel(string levelName)
    {
        string nextLevel = GetNextLevel();
        if(nextLevel == "fin") {
            // Game ending
            return;
        }
        PlayerPrefs.SetString(PlayerPrefsKey.LatestLevel, currentLevel);
        currentLevel = nextLevel;
        currentCatagory = GetCatagoryIndex(currentLevel);
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
