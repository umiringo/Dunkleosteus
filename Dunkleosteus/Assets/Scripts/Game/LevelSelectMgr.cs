using UnityEngine;
using System.Collections;
using GlobalDefines;
public class LevelSelectMgr : MonoBehaviour {

    public GameDirector _director;
    public GameObject _levelContainer;
    public UILocalize _labelCatagory;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Show current catagory and level
    public void Show(string lastestlevel, string currentLevel) {
        string catagoryName = _director.GetCatagoryString(currentLevel);
        _labelCatagory.key = "LK" + catagoryName;

        // 显示特定的界面，还需要可以切换 TODO
        
        // Circle to set visible
        foreach (Transform child in _levelContainer.transform) {
            LevelSelect levelSelect = child.gameObject.GetComponent<LevelSelect>();
            // Check whether level avaliable
            int ret = _director.CompareLevel(levelSelect.levelName, lastestlevel);
            if(ret > 1) {
                levelSelect.Show(LevelState.Unabled);
                // 设置一下不可点击
            } else if(ret < 1) {
                levelSelect.Show(LevelState.Finished);
            } else {
                levelSelect.Show(LevelState.Current);
            }
        }

    }
}
