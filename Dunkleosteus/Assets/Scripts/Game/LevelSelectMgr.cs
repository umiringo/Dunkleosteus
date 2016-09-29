using UnityEngine;
using System.Collections;
using GlobalDefines;
public class LevelSelectMgr : MonoBehaviour {

    public GameDirector director;
    public GameObject levelContainer;
    public UILocalize labelCatagory;
    public UILabel labelCoin;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Show current catagory and level
    public void Show(string lastestlevel, string currentLevel) {
        string catagoryName = director.GetCatagoryString(currentLevel);
        labelCatagory.key = "LK" + catagoryName;

        // 显示特定的界面，还需要可以切换 TODO
        
        // Circle to set visible
        foreach (Transform child in levelContainer.transform) {
            LevelSelect levelSelect = child.gameObject.GetComponent<LevelSelect>();
            // Check whether level avaliable
            int state = director.GetLevelState(levelSelect.levelName);
            if(state == -1) {
                levelSelect.Show(LevelState.Unabled);
            } else if(state == 1) {
                levelSelect.Show(LevelState.Finished);
            } else if(state == 0) {
                levelSelect.Show(LevelState.Current);
            }
        }

        // Coin
        labelCoin.text = director.GetCoin().ToString();

    }
}
