using UnityEngine;
using UnityEngine.Advertisements;
public class InitializeAds : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private string androidGameId;
    [SerializeField] private string iosGameId;
    [SerializeField] private bool isTesting= true;

    private string gameID;

    public void OnInitializationComplete()
    {
        Debug.Log("Ads Intialized .... ");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)   {    }

    private void Awake()
    {
        #if UNITY_IOS
            gameID=iosGameId;
        #elif UNITY_ANDROID
            gameID=androidGameId;
        #elif UNITY_EDITOR
            gameId=androidGameId;
        #endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(gameID, isTesting, this);
        }
    }
}
