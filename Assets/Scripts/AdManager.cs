using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    public bool isTestAd;
    private string playStoreID = "3615908";
    private string adVideo = "rewardedVideo";
    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(playStoreID, isTestAd);
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
                    FindObjectOfType<GameController>().IncreaseTime();
                    print("Ad Completed");

                }
                break;
        }
    }
}
