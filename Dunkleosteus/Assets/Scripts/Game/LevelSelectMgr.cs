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

    void Show(string catagory, string level) {
        // Show current catagory and level
        // 根据类型设定levelContainer TODO
        // Test, igonre catagory

        // Circle to set visible
        foreach (Transform child in _levelContainer.transform) {
            LevelSelect levelSelect = child.gameObject:GetComponent<LevelSelect>()
            levelSelect.Show(lastestLevel, _director);
        }
    }
}
