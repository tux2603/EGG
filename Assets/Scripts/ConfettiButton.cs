using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiButton : MonoBehaviour
{
    ParticleSystem confetti;
    // Collider collider;
    float confettiTimer = 0.0f;

    // Start is called before the first frame update
    void Start() {
        confetti = GetComponent<ParticleSystem>();
        // collider = GetComponent<Collider>();

        // // Add callback for collisions on the button
        // collider.OnCollisionEnter += OnCollision;
    }

    // Update is called once per frame
    void Update() {
        if(confettiTimer > 0.0f) {
            confettiTimer -= Time.deltaTime;
        }

        else {
            confetti.Stop();
        }
    }

    void OnCollisionEnter(Collision collision) {
        confetti.Play();
        confettiTimer = 1.0f;
    }
}
