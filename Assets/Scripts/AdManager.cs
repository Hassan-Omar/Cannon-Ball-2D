using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{ 
    public bool flag = false;

    // this will be instance of my 
    public static AdManager instance;

    private string appID = "ca-app-pub-3097712600531288~4560853814";

    // private BannerView bannerView;
    //private string bannerID = "ca-app-pub-3097712600531288/8308527135";

    private InterstitialAd fullScreenAd;
    private string fullScreenAdID = "ca-app-pub-3097712600531288/2840578037";


    private RewardedAd rewardedAd;
    private string rewardedAdID = "ca-app-pub-3097712600531288/4832316550";

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
         appID = "ca-app-pub-3097712600531288~4560853814";
         fullScreenAdID = "ca-app-pub-3097712600531288/2840578037";
         rewardedAdID = "ca-app-pub-3097712600531288/4832316550";

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
        PlayerPrefs.SetInt("Conis", PlayerPrefs.GetInt("Coins")+10);
        StoreHandler.coins += 10;
    }
 

}
