using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSFX : MonoBehaviour
{
    public AudioClip audioClip;

    private Rigidbody rb;
    private AudioSource audioController;
    private static bool readyToPlay = false;

    IEnumerator Start()
    {
        audioController = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        yield return new WaitForSeconds(0.5f);
        readyToPlay = true;
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Table") && readyToPlay)
        {
            audioController.PlayOneShot(audioClip);
            print("D:");
        }
    }

}
