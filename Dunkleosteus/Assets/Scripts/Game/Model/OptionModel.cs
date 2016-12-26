using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using GlobalDefines;

public class OptionModel : MonoBehaviour {
    public OptionMenu optionMenuView;
    public GameObject languageOptionView;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MusicOption()
    {
        int musicOption = PlayerPrefs.GetInt(PlayerPrefsKey.Music, 1);
        if(musicOption == 1) {
            PlayerPrefs.SetInt(PlayerPrefsKey.Music, 0);
        } 
        else {
            PlayerPrefs.SetInt(PlayerPrefsKey.Music, 1);
        }
        optionMenuView.RefreshMenu();
    }

    public void SoundOption()
    {
        int soundOption = PlayerPrefs.GetInt(PlayerPrefsKey.Sound, 1);
        if (soundOption == 1) {
            PlayerPrefs.SetInt(PlayerPrefsKey.Sound, 0);
        }
        else {
            PlayerPrefs.SetInt(PlayerPrefsKey.Sound, 1);
        }
        optionMenuView.RefreshMenu();
    }

    public void LanguageOption()
    {
        languageOptionView.GetComponent<MoveInOut>().MoveIn();
    }

    public void CloseLanguageOption()
    {
        languageOptionView.GetComponent<MoveInOut>().MoveOut();
    }

    public void SelectLanguage(string language)
    {
        PlayerPrefs.SetString(PlayerPrefsKey.Language, language);
        Localization.language = PlayerPrefs.GetString(PlayerPrefsKey.Language, "English");
        foreach (Transform child in languageOptionView.transform) {
            child.gameObject.GetComponent<LanguageView>().Show();
        }
    }

    public void ResetOption()
    {

    }
}
