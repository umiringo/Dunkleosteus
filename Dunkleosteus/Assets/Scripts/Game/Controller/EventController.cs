using UnityEngine;
using System.Collections;
using GlobalDefines;

public class EventController : MonoBehaviour {

    public LevelPlayModel levelPlayModel;
    public GameDirector gameDirector;
    public CardModel cardModel;
    public OptionModel optionModel;

	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    #region Trigger by PanelLevel
    public void OnStarTouched(GameObject go)
    {
        levelPlayModel.TriggerStar(go);
    }

    public void OnCardTouched(GameObject go)
    {
        levelPlayModel.TriggerCard(go);
    }

    public void OnTouchTips()
    {
        levelPlayModel.OnTips();
    }
    #endregion

    public void OnStartGame()
    {
        gameDirector.StartGame();
    }

    public void OnSelectLevel(string level)
    {
        gameDirector.SelectLevel(level);
    }

    public void OnBackSelectLevel()
    {
        gameDirector.BackSelectLevel();
    }

    public void OnStartNextLevel()
    {
        gameDirector.StartNextLevel();  
    }

    public void OnClickTitle()
    {
        levelPlayModel.ShowPreview();
    }

    public void OnClickButtonCard()
    {
        gameDirector.StartCardView();
    }

    public void OnClickCardInfo(string level)
    {
        cardModel.ShowCardPreview(level);
    }

    public void OnClickCloseCardInfo()
    {
        cardModel.CloseCardPreview();
    }

    public void OnClickCardMenu(string catagory)
    {
        gameDirector.CardViewSwitchCatagory(catagory);
    }

    public void OnShowCatagoryNotify()
    {
        gameDirector.OnShowCatagoryNotify();
    }

    public void OnCloseNotify()
    {
        gameDirector.CloseNotify();
    }

    public void OnClickButtonOption()
    {
        gameDirector.StartOptionView();
    }

    public void OnCardPreviewTouched(GameObject go)
    {
        cardModel.TriggerCardPreview(go);
    }

    public void OnMusicOptionTouched() {
        optionModel.MusicOption();
    }

    public void OnSoundOptionTouched()
    {
        optionModel.SoundOption();
    }

    public void OnLanguageOptionTouched()
    {
        optionModel.LanguageOption();
    }

    public void OnSelectLanguage(string language)
    {
        optionModel.SelectLanguage(language);
    }

    public void OnCloseLanguage()
    {
        optionModel.CloseLanguageOption();
    }

    public void OnCreditorOptionTouched()
    {

    }

    public void OnResetOptionTouched()
    {
        optionModel.ResetOption();
    }


}
