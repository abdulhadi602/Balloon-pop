using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    // Start is called before the first frame update
    string gameId = "3499169";
    bool testMode = false;
    string rewardedAd = "rewardedVideo";
    string banner = "Banner";
    private GameObject Manager;
    private Manager manager;
    public GameObject Adavailable;
    private static int Retries;
    void Start()
    {
       
        Manager = GameObject.FindGameObjectWithTag("GameManager");
        manager = Manager.GetComponent<Manager>();
        DontDestroyOnLoad(gameObject);
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);
        StartCoroutine(ShowBannerWhenReady());
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
    }
    public void IncreaseRetryCount()
    {
        Retries++;
        if (Retries == 3)
        {
            ShowAd();
            Retries = 0;
        }
    }
    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(banner))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Show(banner);
    }
 
    public void OnUnityAdsDidError(string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
            manager.ContinueLevel();
          
        }
        else if (showResult == ShowResult.Skipped)
        {
           
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        manager.PauseGame();
    }

    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, show the ad:
        if(placementId == rewardedAd)
        {
            Adavailable.SetActive(true);
        }
      
    }
    public void ShowAd()
    {
        Advertisement.Show();
    }
}
