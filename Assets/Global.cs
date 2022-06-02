using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Main {


public class Global : MonoBehaviour
{

    public ScriptableMenuConfig sceneConfig;

   
    #region STATIC GLOBAL VAR

        public static bool deadPlayerStatus;
        public static bool winPlayerStatus;
        public static bool playerStartStatus;
        public static int jewerlyStartCount;
        public static int jewerlyPlayerCollect;
        public static bool playerCollectAllEvent;
        public static bool MusicFX;
        public static bool SoundFX;
        public static float SoundFXVolume;
        public static bool mainMenuStatus;

        // level data
        public static string levelId;
        public static float levelRecord;
        public static int levelTry;
        public static bool levelHasRecord;
        public static string nextLevelId;

        
    #endregion


    #region STATIC EVENT
        public static UnityEvent PlayerDeadEvent = new UnityEvent();
        public static UnityEvent<Vector3> ClickOnMapEvent = new UnityEvent<Vector3>();
        public static UnityEvent PlayerCollectAllEvent = new UnityEvent();
        public static UnityEvent CollectJewerlyEvent = new UnityEvent();
        public static UnityEvent PlayerGoalEvent = new UnityEvent();
        public static UnityEvent PlayerStartEvent = new UnityEvent();
        public static UnityEvent NewRecod = new UnityEvent();

        public static UnityEvent<Vector2> TwoFingerEvent = new UnityEvent<Vector2>();

    #endregion



    private void Awake() {

        // CODE IT DELETE PUBLISHED
        //PlayerPrefs.DeleteAll();

        Global.levelId = sceneConfig.levelLoadId;
        Global.nextLevelId = sceneConfig.nextLevelId;
       
        PrefsDataController prefsData = new PrefsDataController();
        prefsData.CreatePrefData(Global.levelId);   

        //Debug.LogWarning("NEXT LEV ID: " + Global.nextLevelId);     
    
        CollectJewerlyEvent.AddListener(CollectJewelyEvent);
    
        deadPlayerStatus = false;
        winPlayerStatus = false;
        playerStartStatus = false;
        jewerlyPlayerCollect = 0;
        playerCollectAllEvent = false;
        MusicFX = true;
        SoundFX = true;
        SoundFXVolume = 1.0f;
        mainMenuStatus = sceneConfig.mainMenuLevel;
        
    }


    void Start() {      
        jewerlyStartCount = GameObject.FindGameObjectsWithTag("Jewerly").Length;
    }


    private void CollectJewelyEvent() {
        jewerlyPlayerCollect++;
        if (Global.jewerlyPlayerCollect == Global.jewerlyStartCount) {
            playerCollectAllEvent = true;
            PlayerCollectAllEvent.Invoke();
        }
    }


    private void OnDestroy() {
        CollectJewerlyEvent.RemoveListener(CollectJewelyEvent);
    }

}
}
