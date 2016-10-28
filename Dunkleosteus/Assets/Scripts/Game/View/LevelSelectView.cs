using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GlobalDefines;
public class LevelSelectView : MonoBehaviour {

    public GameDirector director;
    public UILocalize labelCatagory;
    public UILabel labelCoin;
    public UIScrollView catagoryScrollView;
    public GameObject catagoryTable;

    private Dictionary<string, GameObject> catagoryHash;
    private GameObject levelContainer;
    private UICenterOnChild centerOnChild;

    void Awake() {
        catagoryHash = new Dictionary<string, GameObject>();
        centerOnChild = catagoryTable.GetComponent<UICenterOnChild>();
        centerOnChild.onCenter = this.AfterCenter;
    }

	// Use this for initialization
	void Start () {
	   
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void BeforeExit()
    {
        // Clear containerTable
        foreach(var key in catagoryHash.Keys) {
            Destroy(catagoryHash[key]);
        }
        catagoryHash.Clear();
    }

    public void AfterCenter(GameObject centerGo)
    {
        labelCatagory.gameObject.SetActive(false);
        labelCatagory.key = "LK" + centerGo.GetComponent<CatagoryModel>().catagoryName;
        labelCatagory.gameObject.SetActive(true);
    }

    // Show current catagory and level
    public void Show(string lastestlevel, string currentLevel) 
    {
        // Get next level's catagory
        int lastestIndex = director.GetNextLevelCatagoryIndex(lastestlevel);
        if(lastestIndex == 0) {
            lastestIndex = 1;
        }
        // Add enable catagory into dictionary
        for(int i = 1; i <= lastestIndex; i++) {
            string enableCatagory = director.GetCatagoryString(i);
            GameObject catagoryGameObject = Instantiate(Resources.Load(PathContainer.CatagoryPath + "Container" + enableCatagory)) as GameObject;
            catagoryGameObject.transform.parent = catagoryTable.transform;
            catagoryGameObject.transform.localPosition = new Vector3(0, 0, 0);
            catagoryGameObject.transform.localScale = new Vector3(1, 1, 1);
            catagoryGameObject.SetActive(true); 
            catagoryHash.Add(enableCatagory, catagoryGameObject);
        }
        catagoryTable.GetComponent<UITable>().Reposition();
        // Move scroll to current catagory
        string catagoryName = director.GetCatagoryString(currentLevel);
        int currentIndex = director.GetCatagoryIndex(currentLevel);
        float offset = 0;
        if(catagoryHash.Count > 1) {
            offset = ((float)(currentIndex - 1)) / (catagoryHash.Count - 1);
        }
        catagoryScrollView.SetDragAmount(offset, 0.5f, false);
        catagoryTable.GetComponent<UITable>().Reposition();
        centerOnChild.Recenter();
        // Show catagory name
        labelCatagory.key = "LK" + catagoryName;
        // Circle to set visible
        foreach( var key in catagoryHash.Keys) {
            foreach(Transform child in catagoryHash[key].transform) {
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
        }

        // Coin
        labelCoin.text = director.GetCoin().ToString();
    }
}
