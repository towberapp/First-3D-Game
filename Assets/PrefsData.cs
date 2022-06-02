using UnityEngine;

namespace Main {

    public class PrefsData
    {        
        public float recordLevel;
        public int tryCount;
        public int successCount;
        public bool lockLevel;        
    }
    


    public class PrefsDataController {

        private string _levelid = Global.levelId;

        public void CreatePrefData(string levelId) {
            
            //Debug.LogWarning("PrefsData -> CreatePref");

            string json = PlayerPrefs.GetString(levelId, "");
            if (json != "") {
                SetGlobalPref(levelId);               
            }
            else {
                RecEmptyData(levelId);
                SetGlobalPref(levelId); 
            }
        }        

        private PrefsData GetPref(string levelId) {
            //Debug.LogWarning("PrefsData -> getPref");
             string json = PlayerPrefs.GetString(levelId);
             PrefsData _prefsData = JsonUtility.FromJson<PrefsData>(json);
             return _prefsData;
        }
        
        private void SetGlobalPref(string levelId) {
            //Debug.LogWarning("PrefsData -> SetGlobalPref");
            PrefsData _prefsData = GetPref(levelId);
            Global.levelRecord = _prefsData.recordLevel;
            Global.levelTry =  _prefsData.tryCount;    
            Global.levelHasRecord = true;           
        }

        private void RecEmptyData(string levelId) {
            //Debug.LogWarning("PrefsData -> RecEmptyData");
             PrefsData obj = new PrefsData();
             obj.lockLevel = false;
             obj.successCount = 0;
             obj.tryCount = 0;
             obj.recordLevel = 0f;
             RecPrefDataObject(obj, levelId);
        }

        private void RecPrefDataObject(PrefsData obj, string levelid) {
            //Debug.LogWarning("PrefsData -> RecPrefDataObject: " + obj.ToString());
            string json = JsonUtility.ToJson(obj);
            PlayerPrefs.SetString(levelid, json);
        }


        public void SetDataDeth() {            
            PrefsData _prefsData = GetPref(_levelid);
            _prefsData.tryCount++;
            RecPrefDataObject(_prefsData, _levelid);
            //Debug.LogWarning("PrefsData -> SetDataDeth: " + _prefsData.ToString());
        }

        public void SetDataGoal(float rec) {
            PrefsData _prefsData = GetPref(_levelid);
            _prefsData.tryCount++;    
            _prefsData.successCount++;        
            if (rec < _prefsData.recordLevel  || _prefsData.recordLevel == 0) {
                //new rec
                Global.NewRecod.Invoke();
                _prefsData.recordLevel = rec;
            }   
            RecPrefDataObject(_prefsData, _levelid);  
            // разблокировать следующий уровень
            if (Global.nextLevelId != "") {
                RecEmptyData(Global.nextLevelId);
            }
            
        }

    }

}
