using UnityEngine;
using System.Collections;
using GlobalDefines;

public class EventController : MonoBehaviour {

    public LevelPlayModel levelPlayModel;
    public GameDirector gameDirector;
    public CardModel cardModel;
    public OptionModel optionModel;
    public AudioPlayerModel audioPlayer;

	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    #region Trigger by PanelLevel
    // 游戏中的星星点击
    public void OnStarTouched(GameObject go)
    {
        levelPlayModel.TriggerStar(go);
    }

    // 游戏完成卡牌点击
    public void OnCardTouched(GameObject go)
    {
        audioPlayer.PlayFlopCardSound();
        levelPlayModel.TriggerCard(go);
    }

    // 提示点击
    public void OnTouchTips()
    {
        audioPlayer.PlayClickSound();
        levelPlayModel.OnTips();
    }
    #endregion

    // 点击开始
    public void OnStartGame()
    {
        audioPlayer.PlayClickSound();
        gameDirector.StartGame();
    }

    // 选择关卡
    public void OnSelectLevel(string level)
    {
        audioPlayer.PlayClickSound();
        gameDirector.SelectLevel(level);
    }

    // 返回选关
    public void OnBackSelectLevel()
    {
        audioPlayer.PlayClickSound();
        gameDirector.BackSelectLevel();
    }

    // 继续下一关
    public void OnStartNextLevel()
    {
        audioPlayer.PlayClickSound();
        gameDirector.StartNextLevel();  
    }

    // 点击游戏中标题
    public void OnClickTitle()
    {
        audioPlayer.PlayClickSound();
        levelPlayModel.ShowPreview();
    }

    // 点击进入卡牌仓库
    public void OnClickButtonCard()
    {
        audioPlayer.PlayClickSound();
        gameDirector.StartCardView();
    }

    // 点击选择预览卡牌
    public void OnClickCardInfo(string level)
    {
        audioPlayer.PlayClickSound();
        cardModel.ShowCardPreview(level);
    }

    // 关闭卡牌预览
    public void OnClickCloseCardInfo()
    {
        audioPlayer.PlayClickSound();
        cardModel.CloseCardPreview();
    }

    // 点击选择卡牌仓库的类型
    public void OnClickCardMenu(string catagory)
    {
        audioPlayer.PlayClickSound();
        gameDirector.CardViewSwitchCatagory(catagory);
    }

    // 点击卡牌仓库类型显示该类型介绍
    public void OnShowCatagoryNotify()
    {
        audioPlayer.PlayClickSound();
        gameDirector.OnShowCatagoryNotify();
    }

    // 关闭介绍
    public void OnCloseNotify()
    {
        audioPlayer.PlayClickSound();
        gameDirector.CloseNotify();
    }

    // 点击卡牌预览页面的卡牌
    public void OnCardPreviewTouched(GameObject go)
    {
        audioPlayer.PlayFlopCardSound();
        cardModel.TriggerCardPreview(go);
    }

    // 点击进入选项界面
    public void OnClickButtonOption()
    {
        audioPlayer.PlayClickSound();
        gameDirector.StartOptionView();
    }

    // 点选音乐
    public void OnMusicOptionTouched() 
    {
        audioPlayer.PlayClickSound();
        optionModel.MusicOption();
    }

    // 点选音效
    public void OnSoundOptionTouched()
    {
        audioPlayer.PlayClickSound();
        optionModel.SoundOption();
    }

    // 选择语言
    public void OnSelectLanguage(string language)
    {
        audioPlayer.PlayClickSound();
        optionModel.SelectLanguage(language);
    }
}
