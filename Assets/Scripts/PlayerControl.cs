using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerControl : MonoBehaviour
{
    public ActionBasedContinuousMoveProvider motionProvider;

    GameObject leftHand;
    GameObject rightHand;
    ParticleSystem leftHandParticles;
    ParticleSystem rightHandParticles;
    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        leftHand = GameObject.Find("Camera Offset/LeftHand Controller");
        rightHand = GameObject.Find("Camera Offset/RightHand Controller");
        leftHandParticles = GameObject.Find("Camera Offset/LeftHand Controller/Particle System").GetComponent<ParticleSystem>();
        rightHandParticles = GameObject.Find("Camera Offset/RightHand Controller/Particle System").GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.Get(OVRInput.Button.PrimaryThumbstick)) {
            motionProvider.moveSpeed = 10.0f;
        }

        else {
            motionProvider.moveSpeed = 5.0f;
        }

        // If the Button one is pressed apply a small acceleration to the player in the opposite directon that the right hand is pointing
        if(OVRInput.Get(OVRInput.Button.One)) {
            rb.AddForce(rightHand.transform.forward.normalized * -7.0f, ForceMode.Acceleration);
            motionProvider.moveSpeed = 0.0f;
            rightHandParticles.Play();
        }

        else {
            rightHandParticles.Stop();
        }

        // Same thing for button three on the left hand
        if(OVRInput.Get(OVRInput.Button.Three)) {
            rb.AddForce(leftHand.transform.forward.normalized * -7.0f, ForceMode.Acceleration);
            motionProvider.moveSpeed = 0.0f;
            leftHandParticles.Play();
        }

        else {
            leftHandParticles.Stop();
        }
    }
}
