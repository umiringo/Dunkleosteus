using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using GlobalDefines;
using System.Linq;

public class GameDirector : MonoBehaviour {
    private Dictionary<string, int> levelHash = new Dictionary<string, int>();
    private Dictionary<string, List<string> > catagoryHash = new Dictionary<string, List<string> >();
    private Dictionary<string, string> localPriceHash = new Dictionary<string, string>();
    private int currentCatagory;
    private string currentLevel; // 当前正在进行的关卡
    private int coin;
    private FiniteStateMachine _fsm;
    private Dictionary<string, string> abbrHash = new Dictionary<string, string>();
    private Dictionary<string, int> starNumHash = new Dictionary<string, int>();
	private Dictionary<string, string> achieveHash = new Dictionary<string, string>();

    public LevelPlayModel levelPlayModel;
    public LevelSelectView levelSelectView;
    public CardView cardView;
    public PayView payView;
    
    //public GameObject panelStart; // 开始界面 
    public GameObject panelMainMenu; // 主菜单界面
    public GameObject panelLevelSelect; // 关卡选择界面
    public GameObject panelPlay; // 游戏界面
    public GameObject panelCard; // 卡片浏览界面
    public GameObject panelNotify;
    public GameObject panelOption;
    public GameObject panelPay;
    public GameObject panelLoading;
    public GameObject panelConfirm;

    public AudioPlayerModel audioPlayer;
    private LevelGuideModel _levelGuide;
    private IAPMgr _iapMgr;
	private SocialManager _socialMgr;

    void Awake() {
        _levelGuide = this.gameObject.GetComponent<LevelGuideModel>();
        _iapMgr = this.gameObject.GetComponent<IAPMgr>();
		_socialMgr = this.gameObject.GetComponent<SocialManager>();

        // Init template data
        TemplateMgr.Instance.Init();
        // Init Localize data
        LocalizeMgr.Instance.Init();
        // Init finite state machine
        InitFiniteStateMachine();
        // Init level list;
        InitLevelList();
        // Init player prefs
        LoadPlayerPrefs();
        // Init Localization
        InitLocalization();
        // Init abbreviation
        InitAbbrHash();
		// Init IAP
        _iapMgr.Init();
    }

    void Start () {
        InitSounds();
        panelMainMenu.GetComponent<FadeInOut>().FadeIn();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TriggerTransition(StateTransition trans)
    {
        _fsm.PerformTransition(trans);
    } 

    private void InitFiniteStateMachine()
    {
        // Init all states
        MainMenuState mainMenu = new MainMenuState(this);
        mainMenu.AddTransition(StateTransition.PressStart, StateID.LevelSelect);

        LevelSelectState levelSelect = new LevelSelectState(this);
        levelSelect.AddTransition(StateTransition.ViewCard, StateID.CardView);
        levelSelect.AddTransition(StateTransition.ChoseLevel, StateID.GameScene);
        levelSelect.AddTransition(StateTransition.ViewOption, StateID.OptionView);
        levelSelect.AddTransition(StateTransition.ViewPay, StateID.PayView);

        CardViewState cardView = new CardViewState(this);
        cardView.AddTransition(StateTransition.BackToLevelSelect, StateID.LevelSelect);

        GameSceneState gameScene = new GameSceneState(this);
        gameScene.AddTransition(StateTransition.BackToLevelSelect, StateID.LevelSelect);

        OptionViewState optionView = new OptionViewState(this);
        optionView.AddTransition(StateTransition.BackToLevelSelect, StateID.LevelSelect);

        PayViewState payView = new PayViewState(this);
        payView.AddTransition(StateTransition.BackToLevelSelect, StateID.LevelSelect);

        // Init finite state machine
        _fsm = new FiniteStateMachine();
        _fsm.AddFiniteState(mainMenu);
        _fsm.AddFiniteState(levelSelect);
        _fsm.AddFiniteState(cardView);
        _fsm.AddFiniteState(gameScene);
        _fsm.AddFiniteState(optionView);
        _fsm.AddFiniteState(payView);
    }

    private void InitLevelList()
    {
        // Build levelname to index Hash
        JSONArray jaLevel = TemplateMgr.Instance.GetTemplateArray(ConfigKey.LevelInfo, ConfigKey.LevelSelect);
        for(int i = 0; i < jaLevel.Count; ++i) {
            levelHash.Add(jaLevel[i], i);
        }
    }

    private void LoadPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        // Init latestLevel
        string latestLevel = PlayerPrefs.GetString(PlayerPrefsKey.LatestLevel, "Tucana");
        // first level
        if(latestLevel == "begin") {
            PlayerPrefs.SetString(PlayerPrefsKey.LatestLevel, latestLevel);
            currentLevel = DefineString.FirstLevel;
        }
        else {
            PlayerPrefs.SetString(PlayerPrefsKey.LatestLevel, latestLevel);
            string nextLevel = GetNextLevelByIndexName(latestLevel);
            if(nextLevel == "fin") {
                currentLevel = latestLevel;
            }
            else {
                currentLevel = nextLevel;
            }
        }
        // Init Coin
        coin = PlayerPrefs.GetInt(PlayerPrefsKey.Coin, DefineNumber.DefaultCoin);
        this.InitCatagoryHash(latestLevel);
    }

    private void InitLocalization()
    {
        SystemLanguage localLanguage = OCBridge.GetSystemLanguage();
        string savedLanguage = PlayerPrefs.GetString(PlayerPrefsKey.Language, "notset");
        if(savedLanguage == "notset") {
            // 没有设置过, 则尝试使用系统语言
            if(localLanguage == SystemLanguage.Chinese)
            {
                Localization.language = "SChinese";
            }
            else if (localLanguage == SystemLanguage.ChineseSimplified) {
                Localization.language = "SChinese";
            }
            else if (localLanguage == SystemLanguage.ChineseTraditional) {
                Localization.language = "TChinese";
            }
            else if (localLanguage == SystemLanguage.Japanese) {
                Localization.language = "Japanese";
            }
            else {
                Localization.language = "English";
            }
        }
        else {
            Localization.language = savedLanguage;
        }            
    }

    private void InitCatagoryHash(string latestLevel)
    {
        catagoryHash.Clear();
        // 遍历已经完成的所有关卡，依次插入到对应的list中
        int index = this.GetLevelIndex(latestLevel);
        for(int i = 0; i <= index; i++) {
            // 获取关卡数据
            string levelName = this.GetLevelByIndex(i);
            JSONNode jo = TemplateMgr.Instance.GetTemplateString(ConfigKey.LevelInfo, levelName);
            string catagory = jo["catagory"];
            // 如果找不到key，则初始化一个
            if(!catagoryHash.ContainsKey(catagory)) {
                catagoryHash[catagory] = new List<string>();
            }
            // 添加
            catagoryHash[catagory].Add(levelName);
        }
    }

    private void InitAbbrHash()
    {
        abbrHash["Zodiac"] = "Z";
        abbrHash["Orion"] = "O";
        abbrHash["UrsaMajor"] = "U";
        abbrHash["HeavenlyWaters"] = "W";
        abbrHash["Perseus"] = "P";
        abbrHash["Bayer"] = "B";
        abbrHash["LaCaille"] = "L";
        abbrHash["Hercules"] = "H";

        starNumHash["Zodiac"] = 13;
        starNumHash["Orion"] = 5;
        starNumHash["UrsaMajor"] = 10;
        starNumHash["HeavenlyWaters"] = 9;
        starNumHash["Perseus"] = 9;
        starNumHash["Bayer"] = 11;
        starNumHash["LaCaille"] = 13;
        starNumHash["Hercules"] = 18;

		achieveHash["Zodiac"] = GameCenterKey.AchieveZodiac;
		achieveHash["Orion"] = GameCenterKey.AchieveOrion;
		achieveHash["UrsaMajor"] = GameCenterKey.AchieveUrsaMajor;
		achieveHash["HeavenlyWaters"] = GameCenterKey.AchieveHeavenlyWaters;
		achieveHash["Perseus"] = GameCenterKey.AchievePerseus;
		achieveHash["Bayer"] = GameCenterKey.AchieveBayer;
		achieveHash["LaCaille"] = GameCenterKey.AchieveLaCaille;
		achieveHash["Hercules"] = GameCenterKey.AchieveHercules;
    }

    private void InitSounds()
    {
        int musicOption = PlayerPrefs.GetInt(PlayerPrefsKey.Music, 1);
        if (musicOption > 0) {
            audioPlayer.SetIsPlayMusic(true);
        }
        else {
            audioPlayer.SetIsPlayMusic(false);
        }

        int soundOption = PlayerPrefs.GetInt(PlayerPrefsKey.Sound, 1);
        if (soundOption > 0) {
            audioPlayer.SetIsPlaySound(true);
        }
        else {
            audioPlayer.SetIsPlaySound(false);
        }
        audioPlayer.PlayOpBGM();
    }
		
    #region StateInterface
    public void EnterMainMenuState()
    {
        panelMainMenu.GetComponent<FadeInOut>().FadeIn();
    }

    public void ExitMainMenuState()
    {
        panelMainMenu.GetComponent<FadeInOut>().FadeOut();
    }

    public void EnterLevelSelectState()
    {
        panelLevelSelect.GetComponent<FadeInOut>().FadeIn();
        string latestLevel = PlayerPrefs.GetString(PlayerPrefsKey.LatestLevel);
        levelSelectView.Show(latestLevel, currentLevel);
        if (IsCardGuideEnable()) {
            _levelGuide.TriggerCardGuide();
        }
        audioPlayer.PlaySelectBGM();
		_socialMgr.Login();
		// Update All GameCenter Data
    }

    public void ExitLevelSelectState()
    {
        panelLevelSelect.GetComponent<FadeInOut>().FadeOut();
        levelSelectView.BeforeExit();
    }

    public void EnterGameSceneState()
    {
        panelPlay.GetComponent<FadeInOut>().FadeIn();
        levelPlayModel.LoadLevel(currentLevel);
        audioPlayer.PlayGameBGM();
    }

    public void ExitGameSceneState()
    {
        panelPlay.GetComponent<FadeInOut>().FadeOut();
    }

    public void EnterCardViewState()
    {
        panelCard.GetComponent<FadeInOut>().FadeIn();
        cardView.Show("Zodiac");
    }

    public void ExitCardViewState()
    {
        panelCard.GetComponent<FadeInOut>().FadeOut();
        cardView.BeforeExit();
    }

    public void EnterOptionViewState()
    {
        panelOption.GetComponent<FadeInOut>().FadeIn();
    }

    public void ExitOptionViewState()
    {
        panelOption.GetComponent<FadeInOut>().FadeOut();
    }

    public void EnterPayViewState()
    {
        payView.RefreshView();
        panelPay.GetComponent<FadeInOut>().FadeIn();
    }

    public void ExitPayViewState()
    {
        panelPay.GetComponent<FadeInOut>().FadeOut();
    }

    public void StartGame()
    {
        // Enter level select state
        _fsm.PerformTransition(StateTransition.PressStart);
    }

    public void SelectLevel(string level)
    {
     //   string latestLevel = PlayerPrefs.GetString(PlayerPrefsKey.LatestLevel);
        if (GetLevelState(level) < 0) {
            return;
        }
        // Check the level is availiable
        currentLevel = level;
        _fsm.PerformTransition(StateTransition.ChoseLevel);
    }

    public void BackSelectLevel()
    {
        _fsm.PerformTransition(StateTransition.BackToLevelSelect);
    }

    public void StartNextLevel()
    {
        // currentLevel is already next level
        levelPlayModel.LoadLevel(currentLevel);  
    }

    public void StartCardView()
    {
        _fsm.PerformTransition(StateTransition.ViewCard);
    }

    public void CardViewSwitchCatagory(string catagory)
    {
        cardView.SwitchCatagory(catagory);
    }

    public void OnShowCatagoryNotify()
    {
        string catagory = cardView.GetCurrentCatagory();
        this.ShowNotify("LK" + catagory, "LK" + catagory + "Content");
    }

    public void ShowNotify(string title, string content)
    {
        panelNotify.SetActive(false);
        panelNotify.GetComponent<NotifyView>().Init(title, content);
        panelNotify.GetComponent<FadeInOut>().FadeIn();
    }

    public void CloseNotify()
    {
        panelNotify.GetComponent<FadeInOut>().FadeOut();
    }

    public void StartOptionView()
    {
        _fsm.PerformTransition(StateTransition.ViewOption);
    }

    public void StartPayView()
    {
        _fsm.PerformTransition(StateTransition.ViewPay);
    }
    #endregion

    #region public interface
    // 检查关卡状态：－1，未开放，1，已经完成，0，当前
    public int GetLevelState(string level)
    {
        string latestLevel = PlayerPrefs.GetString(PlayerPrefsKey.LatestLevel, DefineString.FirstLevel);
        if(latestLevel == "begin") {
            if(level == DefineString.FirstLevel) {
                return 0;
            }
            return -1;
        }

        int latestIndex = levelHash[latestLevel];
        int levelIndex = levelHash[level];
        if(levelIndex - latestIndex == 1) {
            return 0;
        }
        if(levelIndex - latestIndex > 1) {
            if(levelIndex - latestIndex > 6) {
                return -1;
            }
            return levelIndex - latestIndex;
        }
        return 1;
    }

    public string GetCatagoryString(string level)
    {
        JSONNode levelInfoJo = TemplateMgr.Instance.GetTemplateString(ConfigKey.LevelInfo, level);
        return levelInfoJo["catagory"];
    }

    public string GetCatagoryString(int index) 
    {
        JSONNode catagoryInfo = TemplateMgr.Instance.GetTemplateString(ConfigKey.LevelInfo, ConfigKey.Catagory);
        return catagoryInfo[index - 1];
    }

    public string GetNextLevelByIndexName(string level)
    {
        int index = levelHash[level];
        return GetLevelByIndex(index + 1);
    }

    public string GetNextLevel()
    {
        int index = levelHash[currentLevel];
        return GetLevelByIndex(index + 1);
    }

    public string GetLevelByIndex(int index)
    {
        JSONArray levelSelectInfo = TemplateMgr.Instance.GetTemplateArray(ConfigKey.LevelInfo, ConfigKey.LevelSelect);
        if(index >= levelSelectInfo.Count) {
            return "fin";
        }
        return levelSelectInfo[index];
    }

    public int GetLevelIndex(string levelName)
    {
        if(levelName == "begin") {
            return -1;
        }
        return levelHash[levelName];
    }

    public bool FinishLevel()
    {
       // string latestLevel = PlayerPrefs.GetString(PlayerPrefsKey.LatestLevel, "begin");
        int ret = GetLevelState(currentLevel);
        if(ret == 0) {
            PlayerPrefs.SetString(PlayerPrefsKey.LatestLevel, currentLevel);
            // 将完成的关卡加入到已经完成的关卡列表中(catagoryHash)
            JSONNode jo = TemplateMgr.Instance.GetTemplateString(ConfigKey.LevelInfo, currentLevel);
            string catagory = jo["catagory"];
            if(!catagoryHash.ContainsKey(catagory)) {
                catagoryHash[catagory] = new List<string>();
            }
            // 添加
            catagoryHash[catagory].Add(currentLevel);
        }
        string nextLevel = GetNextLevel();
        if(nextLevel == "fin") {
            // Game ending
            return true;
        }
        currentLevel = nextLevel;
		UpdateAllGameCenterData();
        return false;
    }

    public int GetCoin()
    {
        return coin;
    }

    public void AddCoin(int num)
    {
        coin += num;
        PlayerPrefs.SetInt(PlayerPrefsKey.Coin, coin);
    }

    public void SubCoin(int num)
    {
        if (coin < num) {
            coin = 0;
        }
        else {
            coin -= num;
        }
        PlayerPrefs.SetInt(PlayerPrefsKey.Coin, coin);
    }

    public List<string> GetEnableLevelList(string catagory)
    {
        if(!catagoryHash.ContainsKey(catagory)) {
            return new List<string>();
        }
        return catagoryHash[catagory];
    }

	public int GetEnableLevelListNum(string catagory)
	{
		if(!catagoryHash.ContainsKey(catagory)) {
			return 0;
		}
		return catagoryHash[catagory].Count;
	}

	public int GetAllEnableLevelListNum()
	{
		int count = 0;
		foreach(string key in abbrHash.Keys){
			count += GetEnableLevelListNum(key);
		}
		return count;
	}

    public string GetAbbreviation(string catagory) 
    {
        return abbrHash[catagory];
    }

    public int GetStarAllNum(string catagory)
    {
        return starNumHash[catagory];
    }

    public void Purchase(string purchaseId)
    {
        panelLoading.GetComponent<LoadingView>().Show();
        _iapMgr.Purchase(purchaseId);
    }

    public void ConfirmPurchase(string purchaseId)
    {
        panelConfirm.SetActive(false);
        this.Purchase(purchaseId);
    }

    public void ShowPurchaseConfirm()
    {
        string localPrice = GetLocalPrice(DefinePurchaseId.PurchaseId10);
        this.ShowPayConfirm("LKPay", "LKPurchaseInGame", "OnOkConfirmPurchase", DefinePurchaseId.PurchaseId10, "10", localPrice);
    }

    public void ShowSale12Confirm()
    {
        string localPrice = GetLocalPrice(DefinePurchaseId.PurchaseIdSale12);
		this.ShowPayConfirm("LKSale", "LKSale12InGame", "OnSale12ConfirmPurchase", DefinePurchaseId.PurchaseIdSale12, "40", localPrice);
    }

	public void ShowCommentConfirm()
	{
		this.ShowConfirm("LKCommentTitle", "LKCommentContent", "OnCommentConfirm", null);
	}

    public void ShowPayConfirm(string title, string content, string delegateName, string param, string num, string price)
    {
        panelConfirm.GetComponent<ConfirmView>().ShowPay(title, content,  delegateName, param, num, price);
    }

	public void ShowConfirm(string title, string content, string delegateName, string param)
	{
		panelConfirm.GetComponent<ConfirmView>().Show(title, content,  delegateName, param);
	}

    public void OnCancelConfirm()
    {
        panelConfirm.SetActive(false);
    }

    public bool IsCardGuideEnable()
    {
        int guideCard = PlayerPrefs.GetInt(PlayerPrefsKey.GuideCard, 0);
        if (currentLevel != "Triangulum" && currentLevel != "Aries" && guideCard <= 0) {
            return true;
        }
        return false;
    }

    public void OnFinishCardGuide()
    {
        audioPlayer.PlayClickSound();
        _levelGuide.StopGuide();
        PlayerPrefs.SetInt(PlayerPrefsKey.GuideCard, 1);
        this.StartCardView();
    }

    public bool IsProductRegister(string productId)
    {
        return localPriceHash.ContainsKey(productId);
    }

    public string GetLocalPrice(string productId)
    {
        if(!localPriceHash.ContainsKey(productId)){
            return "";
        }
        return localPriceHash[productId];
    }

    public void RegisterProduct(string s)
    {
        string[] sArray = s.Split('\t');
        localPriceHash[sArray[3]] = sArray[2];
        if(sArray[3] == DefinePurchaseId.PurchaseIdSale12) {
            if(panelLevelSelect.activeSelf) {
                levelSelectView.RefreshSale();
            }
        }
    }

    public void PurchaseSuccess(string s)
    {
        audioPlayer.PlayPurchaseSound();
        panelLoading.GetComponent<LoadingView>().Hide();
        if(s == DefinePurchaseId.PurchaseId10) {
            AddCoin(10);
        }
        else if(s == DefinePurchaseId.PurchaseId40) {
            AddCoin(35);
        }
        else if(s == DefinePurchaseId.PurchaseId160) {
            AddCoin(60);
        }
        else if(s == DefinePurchaseId.PurchaseIdSale12) {
            AddCoin(40);
            PlayerPrefs.SetInt(PlayerPrefsKey.Sale12, 1);
        }
        // 刷新界面
        if(panelPay.activeSelf) {
            panelPay.GetComponent<PayView>().RefreshCoin();
        }
        if(panelPlay.activeSelf) {
            panelPlay.GetComponent<LevelPlayView>().RefreshCoin();
        }
    }

    public void HideLoading()
    {
        panelLoading.GetComponent<LoadingView>().Hide();
    }
    
    public void OpenAchievement()
    {
		_socialMgr.ShowGameCenter();
    }

	public bool IsGameCenterOK()
	{
		return _socialMgr.isGameCenterSuccess && OCBridge.IsGameCenterAvailable();
	}
		
	public void UpdateAllGameCenterData()
	{
		// Update Ladder
		int finishedLevel = GetAllEnableLevelListNum();
		_socialMgr.UpdateScore(finishedLevel);

		// Update Achieve
		foreach(var item in achieveHash) {
			int count = GetEnableLevelListNum(item.Key);
			_socialMgr.UpdateReportProgress(item.Value, count, starNumHash[item.Key]);
		}
		_socialMgr.UpdateReportProgress(GameCenterKey.AchieveAll, finishedLevel, DefineNumber.MaxLevel);
	}
    #endregion
}
