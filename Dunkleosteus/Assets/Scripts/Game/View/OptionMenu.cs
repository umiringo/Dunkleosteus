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
            //musicSprite.spriteName = PathContainer.normalBlueSquare;
            //musicMenu.gameObject.SetActive(false);
            musicMenu.key = "LKOn";
           // musicMenu.gameObject.SetActive(true);
        }
        else {
            //musicMenu.gameObject.SetActive(false);
            musicMenu.key = "LKOff";
           // musicMenu.gameObject.SetActive(true);
            labelMusic.color = new Color(0.27f, 0.37f, 0.42f);
            //musicSprite.spriteName = PathContainer.darkBlueSquare;
        }

        int soundOption = PlayerPrefs.GetInt(PlayerPrefsKey.Sound, 1);
        if (soundOption == 1) {
            //soundMenu.gameObject.SetActive(false);
            soundMenu.key = "LKOn";
            //soundMenu.gameObject.SetActive(true);
            labelSound.color = new Color(0.31f, 0.8f, 0.9f);
            //soundSprite.spriteName = PathContainer.normalBlueSquare;
        }
        else {
            //soundMenu.gameObject.SetActive(false);
            soundMenu.key = "LKOff";
           // soundMenu.gameObject.SetActive(true);
            labelSound.color = new Color(0.27f, 0.37f, 0.42f);
            //soundSprite.spriteName = PathContainer.darkBlueSquare;
        }
        gameObject.SetActive(true);
    }
}
