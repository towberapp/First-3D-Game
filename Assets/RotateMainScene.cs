using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main {
public class RotateMainScene : MonoBehaviour
{
   

    public float speed = 2.0f;

    private Vector3 cameraVelocity = Vector3.zero;
    public float cameraSpeed = 2.0f;
    Transform playerController;


   private void Awake() {
        Global.TwoFingerEvent.AddListener(MoveScene);      
        playerController = FindObjectOfType<PlayerController>().transform;
   }

    public void FixedUpdate() {

        FollowTarget();

        //if (!Main.Global.playerStartStatus) {
        if (!Main.Global.playerStartStatus) {
            transform.Rotate(0, 2.0f * Time.deltaTime, 0);
        }
    }

    private void FollowTarget() {
         Vector3 targetposition = Vector3.SmoothDamp(transform.position, playerController.position, ref cameraVelocity, cameraSpeed);
         transform.position = targetposition;
    }


    private void MoveScene(Vector2 arg0)
    {
        Quaternion rotation = transform.rotation;
        rotation *= Quaternion.Euler(arg0.y*-1 / 2, arg0.x, 0); // this adds a 90 degrees Y rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);
        
        float zAngle= transform.eulerAngles.x;
         if (zAngle > 180 && zAngle < 360)
         {
             zAngle = 0;
         }
         else if (zAngle > 15)
         {
             zAngle = 15;
         }            
         transform.eulerAngles = new Vector3(zAngle, transform.eulerAngles.y, 0);
    }

    private void OnDestroy() {
       Global.TwoFingerEvent.RemoveListener(MoveScene);
    }
}
}
