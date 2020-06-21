using UnityEngine;
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

        RequestFullScreenAd();
        RequestRewardedAd();
    }
     

    ///_________________________________ Intersitial _____________________________________________
    public void RequestFullScreenAd()
    {
        fullScreenAd = new InterstitialAd(fullScreenAdID);
        // Called when Intersitial is closed.
        fullScreenAd.OnAdClosed += HandleOnAdClosed;

        AdRequest request = new AdRequest.Builder().Build();

        fullScreenAd.LoadAd(request);

    }

    public void ShowFullScreenAd()
    {
        if (fullScreenAd.IsLoaded())
        {
            fullScreenAd.Show();
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
        
        // Called when the user should be rewarded for interacting with the ad.
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;

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
        else
        {
            ShowFullScreenAd();
        }
        RequestRewardedAd();
    }


    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public void HandleOnAdClosed(object sender, System.EventArgs args)
    {
        incCoins();
    }


    public void HandleUserEarnedReward(object sender, Reward args)
    {
        incCoins();
    }

    private void incCoins()
    {
        PlayerPrefs.SetInt("Conis", PlayerPrefs.GetInt("Coins") + 10);
        if (handler != null)
        {
            handler.updateTxt(PlayerPrefs.GetInt("Coins"));
        }
    }
}
