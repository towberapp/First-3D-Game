using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;
using TMPro;

namespace Main {

    public class LevelBrikController : MonoBehaviour
    {

        public ScriptableMenuConfig scriptablelevelInfo;

        [SerializeField] private TextMesh _recordLevel;
        [SerializeField] private TextMesh _tryCount;
        [SerializeField] private TextMesh _levelName;
        [SerializeField] private GameObject _lockBox;

        [SerializeField] private TextMeshPro _levelIndex;

        [SerializeField] private GameObject _levelElement;



        private ParticleSystem particle;
        private GameObject levelinfo;
        private GameObject buttonStart;
        private TextMesh levelName;

        private bool _isLeveLock;


        private void Awake() {
            particle = GetComponent<ParticleSystem>();
            levelinfo = transform.Find("LevelInfo").gameObject;
            buttonStart = levelinfo.transform.Find("Cube").gameObject;
            
            _levelName.text = scriptablelevelInfo.levelName;  
            _lockBox.SetActive(scriptablelevelInfo.lockLevel);
            _isLeveLock = scriptablelevelInfo.lockLevel;
           

            string json = PlayerPrefs.GetString(scriptablelevelInfo.levelLoadId);
            if (json != "") {
                 PrefsData _prefsData = JsonUtility.FromJson<PrefsData>(json);
                 _recordLevel.text = _prefsData.recordLevel.ToString("F2");
                 _tryCount.text = "Try:" + _prefsData.tryCount.ToString();
                 _lockBox.SetActive(_prefsData.lockLevel);
                 _levelElement.SetActive(true);
                 _isLeveLock = _prefsData.lockLevel;                 
                 
            } else {
                _recordLevel.text = "0.00";
                 _tryCount.text = "Try: 0 ";
            }

            _levelIndex.text = scriptablelevelInfo.levelTextIndex;          
        }


        private void Start() {
            
            if (_isLeveLock) {
                _levelIndex.color = new Color32(255, 0, 0, 255);
                _levelIndex.text = "Lock!";
                _levelIndex.fontSize = 13;
            }  else {
                _levelIndex.color = new Color32(255, 255, 255, 255);                
            }
        }



        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player") && !_isLeveLock) {
                particle.Play();                              
                levelinfo.SetActive(true);
                SetButtonControl();  
                SavePlayerCoord();
            }
        }

        private void SavePlayerCoord() {
            
        }

        private void SetButtonControl()
        {
            // buttonStart
        }

        
        public void StartLevel()
        {
            //Debug.LogWarning("LevelBrikController -> START LEVEL");
        }

        private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            particle.Stop();
            levelinfo.SetActive(false);
        }
    }


}

}

