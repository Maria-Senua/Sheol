using System;
using UnityEngine;

public class ClosedVoiceTrigger : MonoBehaviour
{
    private AudioSource audioSource;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Play();
        }
            
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
