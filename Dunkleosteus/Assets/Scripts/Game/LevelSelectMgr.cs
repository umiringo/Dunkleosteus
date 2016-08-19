using UnityEngine;
using System.Collections;

public class LevelSelectMgr : MonoBehaviour {

    public GameDirector _director;
    public GameObject _levelContainer;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Show current catagory and level
    public void Show(string lastestlevel) {
        // Test, igonre catagory
        // Circle to set visible
        foreach (Transform child in _levelContainer.transform) {
            LevelSelect levelSelect = child.gameObject.GetComponent<LevelSelect>();
            // Check whether level avaliable
            if(_diretor.CompareLevel(levelSelect.levelName, lastestlevel) >= 0) {
                levelSelect.Show(true);
            }
            else {
                levelSelect.Show(false);
            }
        }
    }
}
