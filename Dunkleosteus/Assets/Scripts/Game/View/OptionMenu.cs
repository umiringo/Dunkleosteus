using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GlobalDefines;


public class OptionMenu : MonoBehaviour {
    public UILocalize musicMenu;
    public UILabel labelMusic;
    public UILabel labelMusicTitle;
    public UILocalize soundMenu;
    public UILabel labelSound;
    public UILabel labelSoundTitle;
	public GameObject buttonAchieve;
	public GameDirector gameDirector;

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
            // Normal
            labelMusic.color = new Color(0.973f, 0.718f, 0.067f);
            labelMusicTitle.color = new Color(0.973f, 0.718f, 0.067f);
            musicMenu.key = "LKOn";
        }
        else {
            // Dark
            labelMusic.color = new Color(0.447f, 0.416f, 0.239f);
            labelMusicTitle.color = new Color(0.447f, 0.416f, 0.239f);
            musicMenu.key = "LKOff";
        }

        int soundOption = PlayerPrefs.GetInt(PlayerPrefsKey.Sound, 1);
        if (soundOption == 1) {
            // Normal
            labelSound.color = new Color(0.973f, 0.718f, 0.067f);
            labelSoundTitle.color = new Color(0.973f, 0.718f, 0.067f);
            soundMenu.key = "LKOn";
        }
        else {
            // Dark
            labelSound.color = new Color(0.447f, 0.416f, 0.239f);
            labelSoundTitle.color = new Color(0.447f, 0.416f, 0.239f);
            soundMenu.key = "LKOff";
        }

        /*
		if(gameDirector.IsGameCenterOK()) {
			buttonAchieve.SetActive(true);
		}
		else {
			buttonAchieve.SetActive(false);
		}
        */
        
        gameObject.SetActive(true);
    }
}
