using UnityEngine;
using GoogleMobileAds.Api;

public class InitializeAd : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        MobileAds.Initialize(initStatus => { });
    }
}
