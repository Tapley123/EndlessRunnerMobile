using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour//, IUnityAdsListener
{
    /*
    #region get IOS/ANDROID
#if UNITY_IOS
    private string gameId = "4391038"; //IOS Game ID
    private bool android = false;
#else
    private string gameId = "4391039"; //android game ID
    private bool android = true;
#endif
    #endregion

    #region Variables
    [Header("Tweaks")]
    [SerializeField] private bool useBannerAds = false;
    [SerializeField] private bool rewardDoubleCoins = true;
    [SerializeField] private bool rewardContinueRun = false;

    private bool rewardGiven = false;
    #endregion

    private void Start()
    {
        Advertisement.Initialize(gameId);
        Advertisement.AddListener(this);

        rewardGiven = false;

        if (useBannerAds)
            ShowBanner();
    }

    //intersticial (NO CHOICE NO REWARD)
    public void PlayInterstitialAd()
    {
        if(android)
        {
            if (Advertisement.IsReady("Interstitial_Android"))
                Advertisement.Show("Interstitial_Android");
        }
        else //IOS
        {
            if (Advertisement.IsReady("Interstitial_iOS"))
                Advertisement.Show("Interstitial_iOS");
        }
    }

    public void PlayRewardedAd()
    {
        if (android)
        {
            if (Advertisement.IsReady("Rewarded_Android"))
                Advertisement.Show("Rewarded_Android");
            else
                Debug.Log("Rewarded Ad Not Ready");
        }
        else //IOS
        {
            if (Advertisement.IsReady("Rewarded_iOS"))
                Advertisement.Show("Rewarded_iOS");
            else
                Debug.Log("Rewarded Ad Not Ready");
        }
    }

    public void ShowBanner()
    {
        if (android)
        {
            if (Advertisement.IsReady("Banner_Android"))
            {
                Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER); //put the banner on the bottom center of the screen
                Advertisement.Show("Banner_Android");
            }
            else
                StartCoroutine(RepeatShowBanner());
        }
        else //IOS
        {
            if (Advertisement.IsReady("Banner_iOS"))
            {
                Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER); //put the banner on the bottom center of the screen
                Advertisement.Show("Banner_iOS");
            }
            else
                StartCoroutine(RepeatShowBanner());
        }
    }

    public void HideBanner()
    {
        Advertisement.Banner.Hide();
    }

    private IEnumerator RepeatShowBanner()
    {
        yield return new WaitForSeconds(1);
        ShowBanner();
    }

    #region Rewards
    void Reward_DoubleCoins()
    {
        DeathMenu.Instance.DoubleTheCollectedCoins();
        GameObject.FindObjectOfType<PlayerUI>().DeactivateAdButtons();
    }

    void Reward_KeepRunning()
    {

    }
    #endregion



    #region Ad default Methods
    public void OnUnityAdsReady(string placementId)
    {
        //Debug.Log("Ads are ready");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError("Ads Error: " + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        //Debug.Log("Video Started");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(android)
        {
            if (placementId == "Rewarded_Android" && showResult == ShowResult.Finished && !rewardGiven)
            {
                //Debug.Log("Reward Player Here");
                if (rewardContinueRun)
                {
                    Reward_KeepRunning();
                }

                if (rewardDoubleCoins)
                {
                    Reward_DoubleCoins();
                    //Debug.Log("DOUBLED THE COINS!!");
                }

                rewardGiven = true;
            }
        }
        else //IOS
        {
            if (placementId == "Rewarded_iOS" && showResult == ShowResult.Finished && !rewardGiven)
            {
                //Debug.Log("Reward Player Here");
                if (rewardContinueRun)
                {
                    Reward_KeepRunning();
                }

                if (rewardDoubleCoins)
                {
                    Reward_DoubleCoins();
                }

                rewardGiven = true;
            }
        }
    }
    #endregion
    */
}
