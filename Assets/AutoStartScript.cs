using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoStartScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject _fadiIn;

    private void Awake() {
        _fadiIn.SetActive(true);
        Invoke("DeactivateFade", 1.0f);
    }

    void Start()
    {
        //Main.Global.MusicFX = false;
        //Main.Global.SoundFXVolume = 0.5f;
       
        Main.Global.playerStartStatus = true;
        Main.Global.PlayerStartEvent.Invoke();
    }

    private void DeactivateFade() {
        _fadiIn.SetActive(false);
    }

}
