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
    XRInteractorLineVisual leftHandLine;
    XRInteractorLineVisual rightHandLine;
    AudioSource leftHandAudio;
    AudioSource rightHandAudio;
    Rigidbody rb;

    bool leftRocketWasRunning = false;
    bool rightRocketWasRunning = false;

    bool buttonFourWasPressed = false;
    bool rotationLocked = true;


    // Start is called before the first frame update
    void Start()
    {
        leftHand = GameObject.Find("Camera Offset/LeftHand Controller");
        rightHand = GameObject.Find("Camera Offset/RightHand Controller");
        leftHandParticles = GameObject.Find("Camera Offset/LeftHand Controller/Particle System").GetComponent<ParticleSystem>();
        rightHandParticles = GameObject.Find("Camera Offset/RightHand Controller/Particle System").GetComponent<ParticleSystem>();
        leftHandLine = leftHand.GetComponent<XRInteractorLineVisual>();
        rightHandLine = rightHand.GetComponent<XRInteractorLineVisual>();
        leftHandAudio = leftHand.GetComponent<AudioSource>();
        rightHandAudio = rightHand.GetComponent<AudioSource>();
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
            rb.AddForce(rightHand.transform.right.normalized * 500.0f * Time.deltaTime, ForceMode.Acceleration);
            motionProvider.moveSpeed = 0.0f;
            rightHandParticles.Play();
            rightHandLine.lineLength = 0.0f;
            

            if(!rightRocketWasRunning) {
                rightHandAudio.Play();
                rightRocketWasRunning = true;
            }
        }

        else {
            rightHandParticles.Stop();
            rightHandLine.lineLength = 10.0f;
            rightHandAudio.Stop();
            rightRocketWasRunning = false;
        }


        // Same thing for button three on the left hand
        if(OVRInput.Get(OVRInput.Button.Three)) {
            rb.AddForce(leftHand.transform.right.normalized * -500.0f * Time.deltaTime, ForceMode.Acceleration);
            motionProvider.moveSpeed = 0.0f;
            leftHandParticles.Play();
            leftHandLine.lineLength = 0.0f;
            
            if(!leftRocketWasRunning) {
                leftHandAudio.Play();
                leftRocketWasRunning = true;
            }
        }

        else {
            leftHandParticles.Stop();
            leftHandLine.lineLength = 10.0f;
            leftHandAudio.Stop();
            leftRocketWasRunning = false;
        }

        // If button four was pressed, toggle whether or not the rigid body rotation is locked
        if(OVRInput.Get(OVRInput.Button.Four) && !buttonFourWasPressed) {
            rotationLocked = !rotationLocked;

            // If rotation is locked, lock the rotation of the rigid body and reset all rotations to zero
            if(rotationLocked) {
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                transform.rotation = Quaternion.identity;
            }

            // If rotation is not locked, unlock the rotation of the rigid body
            else {
                rb.constraints = RigidbodyConstraints.None;
            }
        }

        buttonFourWasPressed = OVRInput.Get(OVRInput.Button.Four);
    }
}
