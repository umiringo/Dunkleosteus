using UnityEngine;
using System.Collections;

public class GameDirector : MonoBehaviour {

	// Use this for initialization
    public GameObject startPanelTest;
    public GameObject playPanel;
    public LevelPlayMgr levelMgr;

	void Start () {
        TemplateMgr.Instance.Init();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartGame()
    {
        Debug.Log("GameDirector.StartGame");
        startPanelTest.SetActive(false);
        levelMgr.LoadLevel("Scorpius");
        playPanel.SetActive(true);
    }
}
