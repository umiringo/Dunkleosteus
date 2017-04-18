using UnityEngine;
using System.Collections;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;
using GlobalDefines;

public class AdsManager : MonoBehaviour {
    private GameDirector gameDirector;
    private bool rewardBasedEventHandlersSet = false;

    void Awake() {
        gameDirector = this.gameObject.GetComponent<GameDirector>();
    }

	// Use this for initialization
	void Start () {
	   gameDirector.ShowAdsButton(false);
       RequestRewardBasedVideo();
       RegisterEventHandler();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void RegisterEventHandler()
    {
        if(!rewardBasedEventHandlersSet) {
            RewardBasedVideoAd rewardBasedVideo = RewardBasedVideoAd.Instance;

            rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
            rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
            rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
            rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
            rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
            rewardBasedEventHandlersSet = true;
        }
    }

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        gameDirector.ShowAdsButton(true);
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        gameDirector.ShowAdsButton(false);
        RequestRewardBasedVideo();
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        gameDirector.ShowAdsButton(false);
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        gameDirector.AddCoin(1);
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        RequestRewardBasedVideo();
    }

    public void RequestRewardBasedVideo()
    {
    #if UNITY_EDITOR
        string adUnitId = "unused";
    #elif UNITY_ANDROID
        string adUnitId = "unused";
    #elif UNITY_IOS
        string adUnitId = "ca-app-pub-9686954417419070/6402318149";
    #else
        string adUnitId = "unexpected_platform";
    #endif
        RewardBasedVideoAd rewardBasedVideo = RewardBasedVideoAd.Instance;
        AdRequest request = new AdRequest.Builder().Build();
        rewardBasedVideo.LoadAd(request, adUnitId);
    }

    public void PlayRewardBasedVideo()
    {
        RewardBasedVideoAd rewardBasedVideo = RewardBasedVideoAd.Instance;
        if(rewardBasedVideo.IsLoaded()) {
            rewardBasedVideo.Show();
        }
    }

    public bool IsAdsLoaded()
    {
        RewardBasedVideoAd rewardBasedVideo = RewardBasedVideoAd.Instance;
        return rewardBasedVideo.IsLoaded();
    }
}
