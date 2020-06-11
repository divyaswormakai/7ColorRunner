using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    public bool isTestAd;
    private string playStoreID = "3615908";
    private string adVideo = "rewardedVideo";
    private string bannerAd = "banner";

    void Start()
    {
        Advertisement.AddListener(this);
        // Advertisement.Initialize(playStoreID, true);
        Advertisement.Initialize(playStoreID);

        //For banner ads
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            StartCoroutine(ShowBannerAd());
        }
    }

    IEnumerator ShowBannerAd()
    {
        while (!Advertisement.IsReady(bannerAd))
        {
            yield return new WaitForSeconds(0.25f);
        }
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        Advertisement.Banner.Show(bannerAd);

    }
    public void PlayRewardedVideo()
    {
        if (!Advertisement.IsReady(adVideo)) return;
        Advertisement.Show(adVideo);
    }

    public void OnUnityAdsReady(string placementId)
    {
        // throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidError(string message)
    {

        // throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Failed:
                break;
            case ShowResult.Skipped:
                break;
            case ShowResult.Finished:           //this will run for both interstitial ad as well as rewarded video
                if (placementId == adVideo)     // this is unneccesary
                {
                    FindObjectOfType<GameController>().IncreaseTimeFromAd();
                    print("Ad Completed");
                }
                break;
        }
    }
}
