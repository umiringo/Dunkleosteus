using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using GlobalDefines;

public class GameDirector : MonoBehaviour {

    public GameObject startPanel;
    public GameObject playPanel;
    public LevelPlayMgr levelMgr;
    public List<string> levelList;


    private Dictionary<string, int> level2IndexDic = new Dictionary<string, int>();
    private int currentCatagory;
    private string currentLevel;
    private FiniteStateMachine _fsm;

    public GameObject panelMainMenu;
    public GameObject panelLevelSelect;

    void Awake() {
        // Init template data
        TemplateMgr.Instance.Init();
        // Init finite state machine
        InitFiniteStateMachine();
        // Init level list;
        InitLevelList();
        // Init player prefs
        LoadPlayerPrefs();
    }

    void Start () {
        //StartGame();   
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartGame()
    {
       // startPanelTest.SetActive(false);
        levelMgr.LoadLevel("Scorpius");
        playPanel.SetActive(true);
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
        // Get level info array
        JSONArray ja = TemplateMgr.Instance.GetTemplateArray(ConfigKey.LevelInfo, ConfigKey.LevelSelect);
        foreach (JSONNode levelObject in ja) {
            string tmpLevel = levelObject;
            levelList.Add(tmpLevel);
        }
        for(int i = 0; i < levelList.Count; ++i) {
            level2IndexDic.Add(levelList[i], i);
        }
    }

    private void LoadPlayerPrefs()
    {
        currentLevel = PlayerPrefs.GetString(PlayerPrefsKey.LatestLevel, "");
        if(currentLevel == "") {
            currentLevel = levelList[0];
            PlayerPrefs.SetString(PlayerPrefsKey.LatestLevel, currentLevel);
        }
        Debug.Log("GameDirector.LoadPlayerPrefs: currentLevel = " + currentLevel);
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
        Debug.Log("GameDirector : Enter LevelSelectState.");
    }

    public void ExitLevelSelectState()
    {
        panelLevelSelect.SetActive(false);
        Debug.Log("GameDirector : Exit LevelSelectState.");
    }

    public void EnterGameSceneState()
    {
        playPanel.SetActive(true);
        levelMgr.LoadLevel(currentLevel);
        Debug.Log("GameDirector : Enter GameSceneState.");
    }

    public void ExitGameSceneState()
    {
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
        Debug.Log("GameDirector : OnStarGame");
        // Enter level select state
        _fsm.PerformTransition(StateTransition.PressStart);
    }

    public void OnSelectLevel(string level)
    {
        Debug.Log("GameDirector : OnSelectLevel level = " + level);
        this.currentLevel = level;
        _fsm.PerformTransition(StateTransition.ChoseLevel);
       
    }
    #endregion
}
