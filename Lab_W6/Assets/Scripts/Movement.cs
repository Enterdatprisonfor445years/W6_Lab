using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f; // Thrust speed
    [SerializeField] float rotationThrust = 1f; // Rotation speed

    private Rigidbody rb;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // Smoothly increase velocity towards the target speed
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

            // Play engine sound when thrusting
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            // Stop the engine sound when no thrust
            audioSource.Stop();
        }
    }

    void ProcessRotation()
    {
        float rotationThisFrame = rotationThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThisFrame);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        // Temporarily disable physics-driven rotation
        rb.freezeRotation = true;

        // Smoothly rotate the spaceship
        transform.Rotate(Vector3.forward * rotationThisFrame);

        // Re-enable physics-driven rotation
        rb.freezeRotation = false;
    }
}
