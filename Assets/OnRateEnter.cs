using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRateEnter : MonoBehaviour
{
    // Start is called before the first frame update

  private void Start() {

  }


   private void OnTriggerEnter(Collider other) {
    if (other.CompareTag("Player")) {    
       // to rate
        Application.OpenURL("https://play.google.com/store/apps/details?id=ua.com.iber.catchmeifucan");
    }
 }
}
