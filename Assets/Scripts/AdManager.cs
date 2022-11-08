
using UnityEngine;
using UnityEngine.Advertisements;


public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static AdManager Instance;

    [SerializeField] private bool testMode;

    private GameOverHandler _gameOverHandler;

#if UNITY_ANDROID
    private readonly string gameId = "5011585";
#elif UNITY_IOS
    private readonly string gameId = "5011584";
#endif

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            Advertisement.Initialize(gameId, testMode, this );
        }
    }

    public void ShowAd(GameOverHandler gameOverHandler)
    {
        _gameOverHandler = gameOverHandler;
        Advertisement.Show("rewardedVideo", this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Inicialization complete and ready");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Inicialization failed {error} - {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Add Loading complete");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Add Loading failed {error} - {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message){}

    public void OnUnityAdsShowStart(string placementId){}

    public void OnUnityAdsShowClick(string placementId)
    {
        
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        switch (showCompletionState)
        {
            case UnityAdsShowCompletionState.COMPLETED:
                _gameOverHandler.ContinueGame();
                break;
            case UnityAdsShowCompletionState.SKIPPED:
                break;
            case UnityAdsShowCompletionState.UNKNOWN:
                Debug.Log("Ad failed miserably");
                break;
        }
    }
}
