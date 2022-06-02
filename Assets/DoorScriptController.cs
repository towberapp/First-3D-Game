using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DoorScriptController : MonoBehaviour
{
        
    [SerializeField] private GameObject doorOpen;
    [SerializeField] private GameObject doorClose;
    [SerializeField] private GameObject AnimationChancgeDoor;

    private void Awake() {
        Main.Global.PlayerCollectAllEvent.AddListener(PlayerCollectAll);   
        Main.Global.PlayerDeadEvent.AddListener(PlayerDead);
        Main.Global.PlayerGoalEvent.AddListener(PlayerWin);   
    }


    private void Start() {
        doorOpen.SetActive(false);
        doorClose.SetActive(true);
    }

    private void OnCollisionEnter(Collision other) {
        Collider tag = other.collider;
    }

    private void PlayerCollectAll() {
        doorOpen.SetActive(true);
        doorClose.SetActive(false);
        AnimationChancgeDoor.GetComponent<ParticleSystem>().Play();
    }

    private void PlayerDead() {
        GetComponent<Collider>().enabled = false;
    }

    private void PlayerWin()
    {
        GetComponent<Collider>().enabled = false;
        AnimationChancgeDoor.GetComponent<ParticleSystem>().Play();
    }

    private void OnDestroy() {
        Main.Global.PlayerCollectAllEvent.RemoveListener(PlayerCollectAll);
        Main.Global.PlayerDeadEvent.RemoveListener(PlayerDead);      
        Main.Global.PlayerGoalEvent.RemoveListener(PlayerWin);  
    }
}
