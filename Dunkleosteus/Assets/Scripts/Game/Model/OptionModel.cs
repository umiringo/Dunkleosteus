using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using GlobalDefines;

public class OptionModel : MonoBehaviour {
    public OptionMenu optionMenuView;
    public GameObject languageOptionView;
    public AudioPlayerModel audioPlayer;
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
            audioPlayer.SetIsPlayMusic(false);
        } 
        else {
            PlayerPrefs.SetInt(PlayerPrefsKey.Music, 1);
            audioPlayer.SetIsPlayMusic(true);
        }
        optionMenuView.RefreshMenu();
    }

    public void SoundOption()
    {
        int soundOption = PlayerPrefs.GetInt(PlayerPrefsKey.Sound, 1);
        if (soundOption == 1) {
            PlayerPrefs.SetInt(PlayerPrefsKey.Sound, 0);
            audioPlayer.SetIsPlaySound(false);
        }
        else {
            PlayerPrefs.SetInt(PlayerPrefsKey.Sound, 1);
            audioPlayer.SetIsPlaySound(true);
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
        if(language == Localization.language) return;
        audioPlayer.PlaySwitchSound();
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
