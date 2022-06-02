using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main {
public class RunLevelButton : MonoBehaviour
    {

        private GameObject level;

        private string levelName;
        
        public GameObject _fadeOut;


        private void Awake() {
            _fadeOut.SetActive(false);
        }

        void Start()
        {
            level = transform.parent.parent.gameObject;                 
            LevelBrikController leveldata = level.GetComponent<LevelBrikController>();
            levelName = leveldata.scriptablelevelInfo.levelLoadId;
        }

        private void OnTriggerEnter(Collider other) {
                if (other.CompareTag("Player")) {    
                    _fadeOut.SetActive(true);
                    Invoke("RunLevel", 1.0f);                    
                }
        }


        private void RunLevel() {
            SceneManager.LoadScene(levelName);
        }

    }
}
