using System;
using UnityEngine;

public class ClosedVoiceTrigger : MonoBehaviour
{
    //private AudioSource audioSource;
    private MeshRenderer meshRenderer;
    [SerializeField] private FadeScreen fadeScreen;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        //meshRenderer = GetComponent<MeshRenderer>();
        //meshRenderer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //audioSource.Play();
            //meshRenderer.enabled = true;
            fadeScreen.FadeOut();
        }
            
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //meshRenderer.enabled = false;
            fadeScreen.FadeIn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
