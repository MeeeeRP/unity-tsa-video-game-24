using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
        //This is Main Camera in the Scene
    public Camera Camera_1;
    //This is the second Camera and is assigned in inspector
    public Camera Camera_2;
    public int Manager;

    public PlayerInteractS playerInteract;


    public void Start() {
        //This gets the Main Camera from the Scene
        Camera_1 = Camera.main;
        //This enables Main Camera
        Camera_1.enabled = true;
        //Use this to disable secondary Camera
        Camera_2.enabled = false;
        PlayerInteractS.fairyTalk += ChangeCamera;
    }

    public void ManagerCamera() {
        print("Manager run");
        if (Manager == 0) {
            Cam_2();
            Manager = 1;
        } else {
            Cam_1();
            Manager = 0;
        }
    }
    void Cam_1() {
        Camera_1.enabled = true;
        Camera_2.enabled = false;
    }

        void Cam_2() {
        Camera_2.enabled = true;
        Camera_1.enabled = false;
    }

    public void ChangeCamera() {
        GetComponent<Animator>().SetTrigger("Change");
        PlayerInteractS.fairyTalk -= ChangeCamera;
        print("change trigger");
    }
    
}
