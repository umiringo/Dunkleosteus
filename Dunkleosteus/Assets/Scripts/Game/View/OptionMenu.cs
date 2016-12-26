using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GlobalDefines;


public class OptionMenu : MonoBehaviour {
    public UILocalize musicMenu;
    public UILabel labelMusic;
    //public UISprite musicSprite;
    public UILocalize soundMenu;
    public UILabel labelSound;
    //public UISprite soundSprite;

	// Use this for initialization
	void Start () {
        RefreshMenu();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void RefreshMenu()
    {
        gameObject.SetActive(false);

        int musicOption = PlayerPrefs.GetInt(PlayerPrefsKey.Music, 1);
        if (musicOption == 1) {
            labelMusic.color = new Color(0.31f, 0.8f, 0.9f);
            musicMenu.key = "LKOn";
        }
        else {
            labelMusic.color = new Color(0.27f, 0.37f, 0.42f);
            musicMenu.key = "LKOff";
        }

        int soundOption = PlayerPrefs.GetInt(PlayerPrefsKey.Sound, 1);
        if (soundOption == 1) {
            labelSound.color = new Color(0.31f, 0.8f, 0.9f);
            soundMenu.key = "LKOn";
        }
        else {
            labelSound.color = new Color(0.27f, 0.37f, 0.42f);
            soundMenu.key = "LKOff";
        }
        
        gameObject.SetActive(true);
    }
}
