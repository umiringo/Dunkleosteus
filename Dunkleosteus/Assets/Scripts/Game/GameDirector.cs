using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using GlobalDefines;

public class GameDirector : MonoBehaviour {

    public GameObject startPanelTest;
    public GameObject playPanel;
    public LevelPlayMgr levelMgr;
    public List<string> levelList;

    private FiniteStateMachine _fsm;
	
    
    void Awake() {
        // Init template data
        TemplateMgr.Instance.Init();
        // Init finite state machine
        InitFiniteStateMachine();
        // Init level list;
        InitLevelList();
    }

    void Start () {
        //StartGame();   
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartGame()
    {
        startPanelTest.SetActive(false);
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
    }

    #region StateInterface
    public void EnterMainMenuState()
    {
        Debug.Log("GameDirector : Enter MainMenuState.");
    }

    public void ExitMainMenuState()
    {
        Debug.Log("GameDirector : Exit MainMenuState.");
    }

    public void EnterLevelSelectState()
    {
        Debug.Log("GameDirector : Enter LevelSelectState.");
    }

    public void ExitLevelSelectState()
    {
        Debug.Log("GameDirector : Exit LevelSelectState.");
    }

    public void EnterGameSceneState()
    {
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
}
