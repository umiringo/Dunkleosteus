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
    // 游戏中的星星点击
    public void OnStarTouched(GameObject go)
    {
        levelPlayModel.TriggerStar(go);
    }

    // 游戏完成卡牌点击
    public void OnCardTouched(GameObject go)
    {
        levelPlayModel.TriggerCard(go);
    }

    // 提示点击
    public void OnTouchTips()
    {
        levelPlayModel.OnTips();
    }
    #endregion

    // 点击开始
    public void OnStartGame()
    {
        gameDirector.StartGame();
    }

    // 选择关卡
    public void OnSelectLevel(string level)
    {
        gameDirector.SelectLevel(level);
    }

    // 返回选关
    public void OnBackSelectLevel()
    {
        gameDirector.BackSelectLevel();
    }

    // 继续下一关
    public void OnStartNextLevel()
    {
        gameDirector.StartNextLevel();  
    }

    // 点击游戏中标题
    public void OnClickTitle()
    {
        levelPlayModel.ShowPreview();
    }

    // 点击进入卡牌仓库
    public void OnClickButtonCard()
    {
        gameDirector.StartCardView();
    }

    // 点击选择预览卡牌
    public void OnClickCardInfo(string level)
    {
        cardModel.ShowCardPreview(level);
    }

    // 关闭卡牌预览
    public void OnClickCloseCardInfo()
    {
        cardModel.CloseCardPreview();
    }

    // 点击选择卡牌仓库的类型
    public void OnClickCardMenu(string catagory)
    {
        gameDirector.CardViewSwitchCatagory(catagory);
    }

    // 点击卡牌仓库类型显示该类型介绍
    public void OnShowCatagoryNotify()
    {
        gameDirector.OnShowCatagoryNotify();
    }

    // 关闭介绍
    public void OnCloseNotify()
    {
        gameDirector.CloseNotify();
    }

    // 点击卡牌预览页面的卡牌
    public void OnCardPreviewTouched(GameObject go)
    {
        cardModel.TriggerCardPreview(go);
    }

    // 点击进入选项界面
    public void OnClickButtonOption()
    {
        gameDirector.StartOptionView();
    }

    // 点选音乐
    public void OnMusicOptionTouched() 
    {
        optionModel.MusicOption();
    }

    // 点选音效
    public void OnSoundOptionTouched()
    {
        optionModel.SoundOption();
    }

    // 选择语言
    public void OnSelectLanguage(string language)
    {
        optionModel.SelectLanguage(language);
    }
}
