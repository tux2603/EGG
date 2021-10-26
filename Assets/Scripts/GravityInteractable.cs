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

    Vector3 gravity = new Vector3(0, -9.81f, 0);
    bool isGravityEnabled = true;

    Vector3 preSelectVelocity = Vector3.zero;

    // Start is called before the first frame update
    void Start() {
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

        Vector3 handDirection = Vector3.zero;

        if(leftHandSelected) {
            rend.material.color = Color.blue;
            handDirection = leftHand.transform.forward;

            // Slow down the object when it's selected
            rb.velocity = rb.velocity * 0.8f;
        }

        else if(rightHandSelected) {
            rend.material.color = Color.green;
            handDirection = rightHand.transform.forward;
            
            // Slow down the object when it's selected
            rb.velocity = rb.velocity * 0.8f;
        }

        else {
            rend.material.color = Color.black;

            // accelerate the object in the direction of its gravity vector
            if(isGravityEnabled) {
                rb.AddForce(gravity, ForceMode.Acceleration);
            }
        }


        if (isActivated) {
            rend.material.color = Color.red;

            // Set the gravity vector to be in the direction that the hand is pointing
            gravity = handDirection.normalized * 9.81f;
        }
    }

    public void setSelected() {
        preSelectVelocity = rb.velocity;

        if (rightHandInteractor.selectTarget == interactable) {
            rightHandSelected = true;
        }
        else if (leftHandInteractor.selectTarget == interactable) {
            leftHandSelected = true;
        }
    }

    public void setUnselected() {
        rb.velocity = preSelectVelocity;

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
