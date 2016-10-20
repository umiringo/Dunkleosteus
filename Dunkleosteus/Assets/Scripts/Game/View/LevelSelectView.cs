using UnityEngine;
using System.Collections;
using GlobalDefines;
public class LevelSelectView : MonoBehaviour {

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
            LevelView levelView = child.gameObject.GetComponent<LevelView>();
            // Check whether level avaliable
            int state = director.GetLevelState(levelView.levelName);
            if(state == -1) {
                levelView.Show(LevelState.Unabled);
            } else if(state == 1) {
                levelView.Show(LevelState.Finished);
            } else if(state == 0) {
                levelView.Show(LevelState.Current);
            }
        }

        // Coin
        labelCoin.text = director.GetCoin().ToString();
    }
}
