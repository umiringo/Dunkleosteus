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
        levelPlayModel.OnTips();
    }
    #endregion

    //定义 WaitAndPrint（）方法  
    IEnumerator StarGameCoroutine(float waitTime)  
    {  
        yield return new WaitForSeconds(waitTime);  
        gameDirector.StartGame();
    }    
    // 点击开始
    public void OnStartGame()
    {
        audioPlayer.PlayClickSound();
        StartCoroutine(StarGameCoroutine(0.1f));   
    }

    IEnumerator SelectLevelCoroutine(float waitTime, string level)
    {
        yield return new WaitForSeconds(waitTime);
        gameDirector.SelectLevel(level);
    }    
    // 选择关卡
    public void OnSelectLevel(string level)
    {
        audioPlayer.PlayClickSound();
        StartCoroutine(SelectLevelCoroutine(0.1f, level));
    }

    IEnumerator BackSelectLevelCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        gameDirector.BackSelectLevel();
    }    
    // 返回选关
    public void OnBackSelectLevel()
    {
        audioPlayer.PlayClickSound();
        StartCoroutine(BackSelectLevelCoroutine(0.1f));
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
        audioPlayer.PlayUnChoseSound();
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

    IEnumerator MusicOptionCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        optionModel.MusicOption();
    }  
    // 点选音乐
    public void OnMusicOptionTouched() 
    {
        audioPlayer.PlaySwitchSound();
        StartCoroutine(MusicOptionCoroutine(0.25f));
    }

    // 点选音效
    public void OnSoundOptionTouched()
    {
        audioPlayer.PlaySwitchSound();
        optionModel.SoundOption();
    }

    // 选择语言
    public void OnSelectLanguage(string language)
    {
        optionModel.SelectLanguage(language);
    }

    // 点击进入内购界面
    public void OnClickButtonPay()
    {
        audioPlayer.PlayClickSound();
        gameDirector.StartPayView();
    }

    // 支付
    public void OnClickPurchase()
    {
        gameDirector.Purchase();
    }
}
