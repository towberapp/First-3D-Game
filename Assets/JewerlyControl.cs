using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class JewerlyControl : MonoBehaviour
{
    
    public ParticleSystem particle;

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
                particle.Play();  
                gameObject.SetActive(false);                                                            
                //Destroy(gameObject); 
                Main.Global.CollectJewerlyEvent.Invoke();
        }
    }


}
