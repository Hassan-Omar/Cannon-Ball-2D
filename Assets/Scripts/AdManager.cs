using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    //public GameObject Adprefab;
    private Text adHint;
    private GameObject rewardedAdPanel;
    // public Canvas control;
    public bool flag = false;

    // this will be instance of my 
    public static AdManager instance;

    private string appID = "ca-app-pub-3940256099942544~3347511713";

    // private BannerView bannerView;
    //private string bannerID = "ca-app-pub-3940256099942544/6300978111";

    private InterstitialAd fullScreenAd;
    private string fullScreenAdID = "ca-app-pub-3940256099942544/1033173712";


    private RewardedAd rewardedAd;
    private string rewardedAdID = "ca-app-pub-3940256099942544/5224354917";

    //+++++++++++++++++++++++++++++++++++++++++    Initialization    +++++++++++++++++++++++++++++++++++++++++++++++++
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
         appID = "ca-app-pub-3940256099942544~3347511713";
         fullScreenAdID = "ca-app-pub-3940256099942544/1033173712";
         rewardedAdID = "ca-app-pub-3940256099942544/5224354917";

        MobileAds.Initialize(appID);

        RequestFullScreenAd();

        //rewardedAd = new RewardedAd(rewardedAdID);

        RequestRewardedAd();

        // Called when Intersitial is closed.
        fullScreenAd.OnAdClosed += HandleOnAdClosed;



        // Called when the user should be rewarded for interacting with the ad.
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;

        // Called when the ad is closed.
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;


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
     

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        rewardedAdPanel.SetActive(true);
        adHint.text = "Thank you for Supporting Us";

    }







    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardBasedVideoRewarded event received for "
                        + amount.ToString() + " " + type);

        Time.timeScale = 1;

        flag = true;


    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        
    }
 

}
