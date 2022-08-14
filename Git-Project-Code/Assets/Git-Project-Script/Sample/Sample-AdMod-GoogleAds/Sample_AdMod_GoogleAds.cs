//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//using GoogleMobileAds.Api;

//public class Sample_AdMod_GoogleAds : MonoBehaviour
//{
//    private BannerView bannerView;

//    private InterstitialAd interstitial;

//    public void Awake()
//    {
//        DontDestroyOnLoad(this);

//        MobileAds.Initialize(initStatus => { });

//        //this.RequestBanner();

//        this.RequestInterstitial();
//    }

//    private void RequestBanner()
//    {
//#if UNITY_ANDROID
//        string adUnitId = "ca-app-pub-3846025030180027~3260776245";
//#elif UNITY_IPHONE
//            string adUnitId = "";
//#else
//            string adUnitId = "unexpected_platform";
//#endif

//        // Create a 320x50 banner at the top of the screen.
//        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

//        bannerView.Show();
//    }

//    private void RequestInterstitial()
//    {
//#if UNITY_ANDROID
//        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
//#elif UNITY_IPHONE
//        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
//#else
//        string adUnitId = "unexpected_platform";
//#endif

//        // Initialize an InterstitialAd.
//        this.interstitial = new InterstitialAd(adUnitId);

//        // Create an empty ad request.
//        AdRequest request = new AdRequest.Builder().Build();

//        // Load the interstitial with the request.
//        this.interstitial.LoadAd(request);

//        if (this.interstitial.IsLoaded())
//        {
//            this.interstitial.Show();
//        }

//        //interstitial.Destroy();
//    }
//}
