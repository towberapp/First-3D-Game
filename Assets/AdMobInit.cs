using GoogleMobileAds.Api;
using UnityEngine;

public class AdMobInit : MonoBehaviour
{

    public void Start()
    {
        MobileAds.Initialize((initStatus) =>
        {
            Debug.Log("AD MOB INIT");
        });
      
    }
}