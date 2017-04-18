using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;
using GlobalDefines;

public class SocialManager : MonoBehaviour {

	public bool isGameCenterSuccess;
	public string userInfo;
	private GameDirector gameDirector;

	void Awake() {
		gameDirector = this.gameObject.GetComponent<GameDirector>();
	}
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	// 初始化gamecenter后的回调
	private void HandleAuthenticated(bool success)
	{
		isGameCenterSuccess = success;
		if(success) {
			gameDirector.UpdateAllGameCenterData();
		}
	}
		
	private void HandleAchievementsLoaded(IAchievement[] achievements)  
	{  
		//Debug.Log("HandleAchievementsLoaded");  
	}  

	private void HandleAchievementDescriptionsLoaded(IAchievementDescription[] achievementDescriptions)  
	{  
		//Debug.Log("HandleAchievementDescriptionsLoaded");  
	} 

	private void HandleFriendsLoaded(bool success)  
	{  
		//Debug.Log("HandleFriendsLoaded: success = " + success);  
	}  

	//上传排行榜分数  
	private void HandleScoreReported(bool success)  
	{  
		//Debug.Log("HandleScoreReported: success = " + success);  
	}  
	//设置 成就  
	private void HandleProgressReported(bool success)  
	{  
		//Debug.Log("HandleProgressReported: success = " + success);  
	}  

	public void Login()
	{
        if (!OCBridge.IsGameCenterAvailable()) return;
		Social.localUser.Authenticate(HandleAuthenticated);
	}

	public void ShowGameCenter()
	{
        if (!OCBridge.IsGameCenterAvailable()) return;
		if(isGameCenterSuccess && Social.localUser.authenticated) {
			Social.ShowAchievementsUI();
		}
	}			

	public void UpdateScore(int score)
	{
        if (!OCBridge.IsGameCenterAvailable()) return;
		if (Social.localUser.authenticated) {  
			Social.ReportScore(score, GameCenterKey.LadderId, HandleScoreReported);   
		}  
	}

	public void UpdateReportProgress(string achieveId, int score, int totalScore)
	{
        if (!OCBridge.IsGameCenterAvailable()) return;
		if (Social.localUser.authenticated) {  
			Social.ReportProgress(achieveId, (double)score/(double)totalScore * 100, HandleProgressReported);
		}  
	}
}
