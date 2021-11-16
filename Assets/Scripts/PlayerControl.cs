using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerControl : MonoBehaviour
{
    public ActionBasedContinuousMoveProvider motionProvider;

    GameObject leftHand;
    GameObject rightHand;
    bool wasButtonPressed = false;


    // Start is called before the first frame update
    void Start()
    {
        leftHand = GameObject.Find("Camera Offset/LeftHand Controller");
        rightHand = GameObject.Find("Camera Offset/RightHand Controller");
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.Get(OVRInput.Button.PrimaryThumbstick)) {
            motionProvider.moveSpeed = 50.0f;
        }

        else {
            motionProvider.moveSpeed = 5.0f;
        }

        // Set the global gravity to the opposite direction of the left hand when button.three is pressed
        if(OVRInput.Get(OVRInput.Button.Three) && !wasButtonPressed) {
            Physics.gravity = leftHand.transform.forward.normalized * 9.81f;
            
            // // Move the player up a bit so they don't clip through things too badly
            // transform.position += transform.up * 2.0f;

            // // set the upvector of the player to the left hand
            // transform.up = -leftHand.transform.forward.normalized;

            // transform.position += transform.up;

        }

        wasButtonPressed = OVRInput.Get(OVRInput.Button.Three);
    }
}
