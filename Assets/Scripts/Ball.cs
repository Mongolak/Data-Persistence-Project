using System;
using System.Collections;
using System.Collections.Generic;
using System.Media;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody m_Rigidbody;

    public AudioClip paddleSound;

    public AudioClip brickSound;

    private AudioSource audioSource;

    private bool activeSound;
    
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();

        audioSource = GetComponent<AudioSource>();

        //Asegurar que se encuentra el componente AudioSource:

        // Si no se encuentra, buscar en el objeto padre
        if (audioSource == null)
        {
            audioSource = FindObjectOfType<AudioSource>();
        }

        // Verificar si el AudioSource sigue siendo null
        if (audioSource == null)
        {
            Debug.LogError("No se encontró un AudioSource en la bola o en la escena.");
        }

        activeSound = DataSettingsManager.Instance.isSoundActive;

    }
    
    private void OnCollisionExit(Collision other)
    {
        var velocity = m_Rigidbody.velocity;
        
        //after a collision we accelerate a bit
        velocity += velocity.normalized * 0.01f;
        
        //check if we are not going totally vertically as this would lead to being stuck, we add a little vertical force
        if (Vector3.Dot(velocity.normalized, Vector3.up) < 0.1f)
        {
            velocity += velocity.y > 0 ? Vector3.up * 0.5f : Vector3.down * 0.5f;
        }

        //max velocity
        if (velocity.magnitude > 3.0f)
        {
            velocity = velocity.normalized * 3.0f;
        }

        m_Rigidbody.velocity = velocity;

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.CompareTag("Paddle") && activeSound)
        {

            audioSource.PlayOneShot(paddleSound);

            Debug.Log("Rebota con el paddle");

        }

        if(collision.gameObject.CompareTag("Brick") && activeSound)
        {

            audioSource.PlayOneShot(brickSound);

            Debug.Log("Rebota con el brick");

        }

    }


}
