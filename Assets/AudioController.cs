using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{


    [SerializeField] AudioSource _menuAudio;
    [SerializeField] AudioSource _runAudio;
    [SerializeField] AudioSource _gameAudio;


    private void Awake() {
        Main.Global.PlayerStartEvent.AddListener(StartGame);
        Main.Global.PlayerDeadEvent.AddListener(EndGame);
        Main.Global.PlayerGoalEvent.AddListener(WinGame);
    }

    void Start()
    {
        if (Main.Global.MusicFX)
            _menuAudio.Play();
    }

    private void StartGame()
    {
        if (Main.Global.MusicFX && Main.Global.levelId != "MainMenu") {
            _menuAudio.Stop();
            _runAudio.Play();
        }
        Invoke("MainGameAudio", .8f);
    }

    private void MainGameAudio()
    {
        if (Main.Global.MusicFX && Main.Global.levelId != "MainMenu")
            _gameAudio.Play();
    }

    private void EndGame() {
        StartCoroutine (AudioFadeOut.FadeOut (_gameAudio, 0.2f));
    }

    private void WinGame() {
        StartCoroutine (AudioFadeOut.FadeOut (_gameAudio, 0.2f));
    }

    private void OnDestroy() {
        Main.Global.PlayerStartEvent.RemoveListener(StartGame);
        Main.Global.PlayerDeadEvent.RemoveListener(EndGame);
        Main.Global.PlayerGoalEvent.RemoveListener(WinGame);
    }

  

}


 
public static class AudioFadeOut {
 
    public static IEnumerator FadeOut (AudioSource audioSource, float FadeTime) {
        float startVolume = audioSource.volume;
 
        while (audioSource.volume > 0) {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
 
            yield return null;
        }
 
        audioSource.Stop ();
        audioSource.volume = startVolume;
    }
 
}