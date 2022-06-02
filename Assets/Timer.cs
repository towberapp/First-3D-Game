using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Main {
public class Timer : MonoBehaviour
{
  
    [SerializeField] Text _timerText;
    [SerializeField] Text _recordText;
    [SerializeField] Text _tryText;

    private float _mainTimer = 0f;

    private bool _startGame = false;    

    private void Awake() {
        Main.Global.PlayerStartEvent.AddListener(PlayerStart);
        Main.Global.PlayerDeadEvent.AddListener(PlayerDead);
        Main.Global.PlayerGoalEvent.AddListener(PlayerGoal);
    }

    void Start()
    {
       _recordText.text = Main.Global.levelRecord.ToString("F2");
       _tryText.text = "Try: " + Main.Global.levelTry.ToString();
    }

    private void Update() {      
        if (_startGame) { 
            _mainTimer += Time.deltaTime;
            _timerText.text = _mainTimer.ToString("F2");
        }
    }

    public void PlayerStart()
    {    
        _startGame =  true;
    }

    public void PlayerDead()
    {
        PrefsDataController prefsData = new PrefsDataController();
        //Debug.LogWarning("imer ->PlayerDead");
        _startGame =  false;
        prefsData.SetDataDeth();
    }

    public void PlayerGoal()
    {
        PrefsDataController prefsData = new PrefsDataController();
        //Debug.LogWarning("imer ->PlayerGoal");
        _startGame =  false;
        prefsData.SetDataGoal(_mainTimer);
    }


    private void OnDestroy() {
        Main.Global.PlayerStartEvent.RemoveListener(PlayerStart);
        Main.Global.PlayerDeadEvent.RemoveListener(PlayerDead);
        Main.Global.PlayerGoalEvent.RemoveListener(PlayerGoal);
    }

 

}


}
