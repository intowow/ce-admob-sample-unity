using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class AdRewardedVideo : MonoBehaviour {
	public string unitId;
	public string appId;

	private static RewardBasedVideoAd rewardVideoAd;
	private static bool rewardBasedEventHandlersSet = false;

	// Use this for initialization
	void Start () {
		#if UNITY_ANDROID
		appId = "ca-app-pub-4766247142778820~7705583257";
		unitId = "ca-app-pub-4766247142778820/5380760094";
		#elif UNITY_IPHONE
		appId = "ca-app-pub-4766247142778820~5904759882";
		unitId = "ca-app-pub-4766247142778820/2141241289";
		#else
		appId = "unexpected_platform";
		unitId = "unexpected_unitId"
		#endif

		rewardVideoAd = RewardBasedVideoAd.Instance;
		if (!rewardBasedEventHandlersSet) {
			rewardVideoAd.OnAdLoaded += HandleRewardBasedVideoLoaded;
			rewardVideoAd.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
			rewardVideoAd.OnAdStarted += HandleRewardBasedVideoStarted;
			rewardVideoAd.OnAdRewarded += HandleRewardBasedVideoRewarded;
			rewardVideoAd.OnAdClosed += HandleRewardBasedVideoClosed;
			rewardBasedEventHandlersSet = true;
		}
	}

	// Update is called once per frame
	void Update () {

	}

	public void RequestRewardVideoAd(){

		if(!string.IsNullOrEmpty(this.unitId)){
			AdRequest request = new AdRequest.Builder ().Build ();
			rewardVideoAd.LoadAd (request, unitId);
		}
	}

	public void ShowRewardVideoAd(){
		if(rewardVideoAd != null && rewardVideoAd.IsLoaded()){
			rewardVideoAd.Show();
		}
	}

	public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
	{
		print ("print-HandleRewardBasedVideoLoaded");
		Debug.Log ("Debug-HandleRewardBasedVideoLoaded");
	}

	public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		print ("print-HandleAdFailedToLoad");
		Debug.Log ("Debug-HandleAdFailedToLoad");
	}

	public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
	{
		print ("print-HandleRewardBasedVideoStarted");
		Debug.Log ("Debug-HandleRewardBasedVideoStarted");
	}

	public void HandleRewardBasedVideoRewarded(object sender, Reward args)
	{
		print ("print-HandleRewardBasedVideoRewarded");
		Debug.Log ("Debug-HandleRewardBasedVideoRewarded");
	}

	public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
	{
		print ("print-HandleRewardBasedVideoClosed");
		Debug.Log ("Debug-HandleRewardBasedVideoClosed");
	}
}
