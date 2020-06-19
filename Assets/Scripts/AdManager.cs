using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    [SerializeField] private StoreHandler handler;
    

    private string appID = "ca-app-pub-3097712600531288~4560853814";



    private InterstitialAd fullScreenAd;
    private string fullScreenAdID = "ca-app-pub-3097712600531288/2840578037";


    private RewardedAd rewardedAd;
    private string rewardedAdID = "ca-app-pub-3097712600531288/4832316550";

    private void Start()
    {
        MobileAds.Initialize(appID);

        //fullScreenAd = new InterstitialAd(fullScreenAdID);
        RequestFullScreenAd();

        rewardedAd = new RewardedAd(rewardedAdID);

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
            if (handler != null)
            {
                //handler.reward(-1);
            }
            RequestFullScreenAd();
        }
        else
        { 
            ShowFullScreenAd();
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
            if (handler != null)
            {
                handler.reward(-1);
            }
        }
        else
        {
            ShowFullScreenAd();
        }
        RequestRewardedAd();
    }


    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
         if(handler!=null)
        {
            PlayerPrefs.SetInt("Coins", -1000);
            handler.updateTxt(PlayerPrefs.GetInt("Coins"));
        }
        PlayerPrefs.SetInt("Coins", -1000);
    }


    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;

        /*GameObject.Find("HSNTST").GetComponent<Text>().text = "Reward Earn "+handler.gameObject.name;
        PlayerPrefs.SetInt("Conis", PlayerPrefs.GetInt("Coins") + 10);
        if(handler!=null)
        {
            handler.updateTxt(PlayerPrefs.GetInt("Coins"));
        }*/
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {

    }

     
}
