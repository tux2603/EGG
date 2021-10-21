using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GravityInteractable : MonoBehaviour
{
    bool leftHandSelected = false;
    bool rightHandSelected = false;
    bool isActivated = false;

    Color c = Color.black;
    Renderer rend;
    Rigidbody rb;
    XRBaseInteractable interactable;
    XRBaseInteractor rightHandInteractor, leftHandInteractor;
    GameObject rightHand, leftHand;
    XRController rightHandController, leftHandController;




    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
        interactable = GetComponent<XRBaseInteractable>();
        rend.material.color = c;

        // Get the left and right hand controllers
        rightHand = GameObject.Find("Camera Offset/RightHand Controller");
        leftHand = GameObject.Find("Camera Offset/LeftHand Controller");
        rightHandInteractor = rightHand.GetComponent<XRBaseInteractor>();
        leftHandInteractor = leftHand.GetComponent<XRBaseInteractor>();
        rightHandController = rightHand.GetComponent<XRController>();
        leftHandController = leftHand.GetComponent<XRController>();
    }

    // Update is called once per frame
    void Update() {

        if(leftHandSelected) {
            rend.material.color = Color.blue;

            if (isActivated) {
                // Get the direction that the left hand is pointing
                Vector3 leftHandDirection = leftHand.transform.forward;
                leftHandDirection = leftHandDirection.normalized * 10.8f;

                // Apply an acceleration to the object
                rb.AddForce(leftHandDirection, ForceMode.Acceleration);

                // make the left hand controller vibrate
                leftHandController.SendHapticImpulse(0.5f, 0.1f);
            }
        }

        else if(rightHandSelected) {
            rend.material.color = Color.green;
            
            if(isActivated) {
                // Get the direction that the right hand is facing
                Vector3 rightHandDirection = rightHand.transform.forward;
                rightHandDirection = rightHandDirection.normalized * 10.8f;

                // Apply an acceleration in the firectoin that the controller is pointing
                rb.AddForce(rightHandDirection, ForceMode.Acceleration);

                // make the right hand controller vibrate
                rightHandController.SendHapticImpulse(0.5f, 0.1f);
            }
        }

        else {
            rend.material.color = Color.black;
        }


        if(rightHandInteractor == null || leftHandInteractor == null) {
            rend.material.color = Color.red;
        }
    }

    public void setSelected() {
        if (rightHandInteractor.selectTarget == interactable) {
            rightHandSelected = true;
        }
        else if (leftHandInteractor.selectTarget == interactable) {
            leftHandSelected = true;
        }
    }

    public void setUnselected() {
        rightHandSelected = false;
        leftHandSelected = false;
    }

    public void setActivated() {
        isActivated = true;
    }

    public void setDeactivated() {
        isActivated = false;
    }
}
