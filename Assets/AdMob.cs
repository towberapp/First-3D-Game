using GoogleMobileAds.Api;
using GoogleMobileAds.Placement;
using UnityEngine;
using UnityEngine.Events;
using System;


namespace Main {
public class AdMob : MonoBehaviour
{
    //BannerAdGameObject bannerAd;

    private BannerView bannerView;
    private RewardedAd _rewardedAd;   

    public static UnityEvent AdsSeenDone = new UnityEvent();
    public static UnityEvent AdsSeenClose = new UnityEvent();

    private string _bannerdId = "ca-app-pub-1859015684244712/4781533679"; 
    private string _rewardId = "ca-app-pub-1859015684244712/4800417806"; 

    private void Awake() {
        Main.Global.PlayerDeadEvent.AddListener(PlayerDead);
        RunButtonScript.ShowRewardAdsEvent.AddListener(UserChoseToWatchAd);
        RunButtonScript.RemoveBannerEvent.AddListener(RemoveBanner);
    }

    public void Start() 
    {
        MobileAds.Initialize(initStatus => { 
            Debug.Log("ADS INIT OK");
        });

        CreateAndLoadRewardedAd();
    }

    private void RequestBanner()
    {
        if (this.bannerView != null) {
            this.bannerView.Destroy();
        }
        AdSize adaptiveSize =
                AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        this.bannerView = new BannerView(_bannerdId, AdSize.Banner, AdPosition.Center);
        AdRequest adRequest = new AdRequest.Builder().Build();
        this.bannerView.LoadAd(adRequest);
    }

    private void RemoveBanner() {
        bannerView.Destroy();
    }

    private void CreateAndLoadRewardedAd() {
        _rewardedAd = new RewardedAd(_rewardId); 
        _rewardedAd.OnUserEarnedReward += HandleUserErnedReward;
        _rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        AdRequest adRequest = new AdRequest.Builder().Build();
        _rewardedAd.LoadAd(adRequest);
    }

    private void HandleUserErnedReward(object sender, Reward e)
    {
        AdsSeenDone.Invoke();
        Debug.LogWarning("INVOKE!! HandleRewardedAdLoaded SUCESS");
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        CreateAndLoadRewardedAd();
    }


    private void UserChoseToWatchAd()
        {
        if (this._rewardedAd.IsLoaded()) {
            this._rewardedAd.Show();
        }
    }


    private void PlayerDead() {

        Debug.Log("LOAD AND SHOW ADS");
        RequestBanner();
    }


    private void OnDestroy() {
        Main.Global.PlayerDeadEvent.RemoveListener(PlayerDead);
        RunButtonScript.ShowRewardAdsEvent.AddListener(UserChoseToWatchAd);
        RunButtonScript.RemoveBannerEvent.RemoveListener(RemoveBanner);
    }

    public void OnBannerAdFailedToLoad(string reason) {
        Debug.Log("Banner ad failed to load: " + reason);
    }



}
}