using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Debug.LogWarning("EXIT DONE");
            PlayerPrefs.SetInt("savepos", 0);
            Application.Quit();
        }
    }
}
