using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main {
public class ClickController : MonoBehaviour
{

    public Camera cam;
    public GameObject clickAnimation;


    private Vector3 position;
    private float width;
    private float height;

    void Update()
    { 
        if (Input.touchCount > 0) {     
            Touch touch = Input.GetTouch(0);

            if (Input.touchCount == 1 && Global.playerStartStatus) {

                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;    
                if (Physics.Raycast(ray, out hit)) {          
                    Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green);

                    Global.ClickOnMapEvent.Invoke(hit.point);
                    ClickOnGameFild(hit.point);
                }                  
            }

            if (Input.touchCount == 2) {
                
                touch = Input.GetTouch(1);

                if (touch.phase == TouchPhase.Began) {
                }

                if (touch.phase == TouchPhase.Moved) {
                    Global.TwoFingerEvent.Invoke(touch.deltaPosition);
                }

                if (touch.phase == TouchPhase.Ended) {
                }
            }
        }        
    }


    private void ClickOnGameFild(Vector3 position) {
        clickAnimation.transform.position = position;
        clickAnimation.GetComponent<ParticleSystem>().Play();
    }
}
}




