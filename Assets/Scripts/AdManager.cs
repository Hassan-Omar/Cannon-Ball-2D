using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using GoogleMobileAds.Api;

/*
 * This Class Written by H.Omar 
 * 
 * this class will manage & request ads using AdMob 
 *  ((Rewarded ad not Implmented --Commented-- because the Reference Vedio Doesn't Contains Rewarded)) 
 */

public class AdManager : MonoBehaviour
{  
    // this will be instance of my 
    public static AdManager instance;

    private string appID = "ca-app-pub-3940256099942544~3347511713";

    private BannerView bannerView;
    private string bannerID = "ca-app-pub-3940256099942544/6300978111";

    private InterstitialAd fullScreenAd;
    private string fullScreenAdID = "ca-app-pub-3940256099942544/1033173712";


    //private RewardedAd rewardedAd;
    //private string rewardedAdID = "ca-app-pub-3097712600531288/1556136399";

    //+++++++++++++++++++++++++++++++++++++++++ Initialization +++++++++++++++++++++++++++++++++++++++++++++++++
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }


    private void Start()
    {
        // Initialize MobileAds and Request 
        MobileAds.Initialize(appID);
        RequestBanner();
        RequestFullScreenAd();

        

        //RequestRewardedAd();

        // Called when Intersitial is closed.
        //fullScreenAd.OnAdClosed += HandleOnAdClosed;



        // Called when the user should be rewarded for interacting with the ad.
        //rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;

        // Called when the ad is closed.
        //rewardedAd.OnAdClosed += HandleRewardedAdClosed;


    }

    ///_________________________________ Banner _____________________________________________
    
    public void RequestBanner()
    {
        bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Top);

        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);

        bannerView.Show();
    }

    public void HideBanner()
    {
        bannerView.Hide();
    } 

    ///_________________________________ Intersitial _____________________________________________
    public void RequestFullScreenAd()
    {
        fullScreenAd = new InterstitialAd(fullScreenAdID);

        AdRequest request = new AdRequest.Builder().Build();

        fullScreenAd.LoadAd(request);

    }

    public void ShowFullScreenAd()
    {
        if (fullScreenAd.IsLoaded())
        {
            fullScreenAd.Show();
            RequestFullScreenAd();
        }
        else
        {
            RequestFullScreenAd();
        }
    }

  

    /*
    ///________________________________ Reward Ad ______________________________________________
    public void RequestRewardedAd()
    {
        rewardedAd = new RewardedAd(rewardedAdID);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        rewardedAd.LoadAd(request);

    }

    public void ShowRewardedAd()
    {
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();

        }
        //StartCoroutine("increaseAmmo");
        RequestRewardedAd();

    }

    //+++++++++++++++++++++++++++++  Overrides  ++++++++++++++++++++++++++++++++++++++++++ 
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardBasedVideoRewarded event received for "
                        + amount.ToString() + " " + type);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    { 
        //Increment of bullts or diamonds goes here 
    }
   */
    

}
