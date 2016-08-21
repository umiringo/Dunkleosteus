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
        // Test, igonre catagory
        // Circle to set visible
        foreach (Transform child in _levelContainer.transform) {
            LevelSelect levelSelect = child.gameObject.GetComponent<LevelSelect>();
            // Check whether level avaliable
            int ret = _director.CompareLevel(levelSelect.levelName, lastestlevel);
            if(ret > 0) {
                //unabled
                levelSelect.Show(LevelState.Unabled);
            }
            else if(ret < 0) {
                levelSelect.Show(LevelState.Finished);
            }
            else {
                levelSelect.Show(LevelState.Current);
            }
        }

        string catagoryName = _director.GetCatagoryString(currentLevel);
        //show label by catagory name
        _labelCatagory.key = "LK" + catagoryName;
    }
}
