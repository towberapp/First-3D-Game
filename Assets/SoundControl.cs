using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{

    [SerializeField] AudioSource _playerDead;
    [SerializeField] AudioSource _collectAll;
    [SerializeField] AudioSource _collectJeverly;
    [SerializeField] AudioSource _playerGoal;


    private void Awake() {
        Main.Global.PlayerDeadEvent.AddListener(PlayerDead);
        Main.Global.PlayerCollectAllEvent.AddListener(CollectAll);
        Main.Global.CollectJewerlyEvent.AddListener(CollectJeverly); 
        Main.Global.PlayerGoalEvent.AddListener(PlayerGoal);
    }

    private void PlayerDead() {
        _playerDead.volume = Main.Global.SoundFXVolume;
        _playerDead.Play();
    }
    private void CollectAll() {
        _collectAll.volume = Main.Global.SoundFXVolume;
        _collectAll.Play();
    }
    private void CollectJeverly() {
        _collectJeverly.time = 0.4f;
        _collectJeverly.volume = Main.Global.SoundFXVolume;
        _collectJeverly.Play();
    }
    private void PlayerGoal() {
        
        _playerGoal.volume = Main.Global.SoundFXVolume;
        _playerGoal.Play();
    }
 

    private void OnDestroy() {
        Main.Global.PlayerDeadEvent.RemoveListener(PlayerDead);
        Main.Global.PlayerCollectAllEvent.RemoveListener(CollectAll);
        Main.Global.CollectJewerlyEvent.RemoveListener(CollectJeverly); 
        Main.Global.PlayerGoalEvent.RemoveListener(PlayerGoal);
    }
}
