using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class AdRewardVideo : MonoBehaviour {

	#if UNITY_IOS && !UNITY_EDITOR
	public string unitId = "ca-app-pub-4766247142778820/2141241289";
	#else
	public string unitId = "ca-app-pub-4766247142778820/5380760094";
	#endif
	
	

	private static RewardBasedVideoAd rewardVideoAd;
	private static bool rewardBasedEventHandlersSet = false;

	// Use this for initialization
	void Start () {
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

			//			rewardVideoAd = RewardBasedVideoAd.Instance;
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
