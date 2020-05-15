using Game.MessageCenter;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UFrame;
using UFrame.Common;
using UnityEngine;
using UnityEngine.UI;

public class AdmobManager : SingletonMono<AdmobManager>
{
    public TagForChildDirectedTreatment tagForChildDirectedTreatment 
        = TagForChildDirectedTreatment.Unspecified;

    public string rewardADUnit;
    public string interstitialADUnit;

    public bool isTestMode = false;

    public List<String> testDevices = new List<String>();

    public Button requestBtn;

    public Text stateTxt;

    public Text rewardTxt;

    RewardedAd rewardedAd;

    Action OnWatchRewardSuccessed;

    [HideInInspector]
    public bool isInit = false;

    public bool isLoaded
    {
        get
        {
            if (!isInit || rewardedAd == null)
            {
                return false;
            }

            return this.rewardedAd.IsLoaded();
        }
    }

    void Start()
    {
        MobileAds.SetiOSAppPauseOnBackground(true);

        List<String> deviceIds = new List<String>();

        RequestConfiguration requestConfiguration;
        if (isTestMode)
        {
            deviceIds.Add(AdRequest.TestDeviceSimulator);
            for (int i = 0; i < testDevices.Count; i++)
            {
                deviceIds.Add(testDevices[i]);
            }

            requestConfiguration = new RequestConfiguration.Builder()
                .SetTagForChildDirectedTreatment(tagForChildDirectedTreatment)
                .SetTestDeviceIds(deviceIds).build();
        }
        else
        {
            requestConfiguration = new RequestConfiguration.Builder()
                .SetTagForChildDirectedTreatment(tagForChildDirectedTreatment).build();
        }

        MobileAds.SetRequestConfiguration(requestConfiguration);

        MobileAds.Initialize(OnInit);
    }

    void OnInit(InitializationStatus initstatus)
    {
        // Callbacks from GoogleMobileAds are not guaranteed to be called on
        // main thread.
        // In this example we use MobileAdsEventExecutor to schedule these calls on
        // the next Update() loop.
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            isInit = true;

            if (this.stateTxt != null)
            {
                this.stateTxt.text = "Initialize OK";
            }
            
            LoadReawrdAD();
        });
    }

    void LoadReawrdAD()
    {
        this.rewardedAd = new RewardedAd(rewardADUnit);

        // Called when an ad request has successfully loaded.
        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }


    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdLoaded event received");
        if (this.stateTxt != null)
        {
            stateTxt.text = "HandleRewardedAdLoaded event received";
        }
        if (requestBtn != null)
        {
            requestBtn.interactable = true;
        }

        MessageManager.GetInstance().Send((int)GameMessageDefine.RewardADLoadSuccess);
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        Debug.Log(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
        if (stateTxt != null)
        {
            stateTxt.text = "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message;
        }

        MessageManager.GetInstance().Send((int)GameMessageDefine.RewardADLoadFail);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdOpening event received");
        if (this.stateTxt != null)
        {
            stateTxt.text = "HandleRewardedAdOpening event received";
        }
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        Debug.Log(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
        if (this.stateTxt != null)
        {
            stateTxt.text = "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message;
        }
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdClosed event received");
        if (this.stateTxt != null)
        {
            stateTxt.text = "HandleRewardedAdClosed event received";
        }
        LoadReawrdAD();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        if (rewardTxt != null)
        {
            rewardTxt.text = "Recv Reward";
        }
        string type = args.Type;
        double amount = args.Amount;
        Debug.Log(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);
        if (this.stateTxt != null)
        {
            stateTxt.text = "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type;
        }

        if (rewardTxt != null)
        {
            rewardTxt.text = amount.ToString() + " " + type;
        }

        OnWatchRewardSuccessed?.Invoke();
    }

    public void UserChoseToWatchAd()
    {
        Debug.Log("UserChoseToWatchAd Checked");
        if (this.rewardedAd.IsLoaded())
        {
            if (requestBtn != null)
            {
                requestBtn.interactable = false;
            }
            this.rewardedAd.Show();
        }
    }
    
    public void UserChoseToWatchAd(Action cbWatchRewardSuccessed)
    {
        if (this.rewardedAd.IsLoaded())
        {
            OnWatchRewardSuccessed = cbWatchRewardSuccessed;
            this.rewardedAd.Show();
        }
    }

}
