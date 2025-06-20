using System;
using UnityEngine;

public class TalkToyBehaviour : MonoBehaviour
{
    public GameObject hiddenObject;
    private AudioSource audioSource;
    public AudioClip[] voiceLines;
    public float distance;
    public float hotDistance;
    private float previousDistance;
    public float checkInterval = 5f; 
    private float timer;
    private bool isCarried = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        previousDistance = Vector3.Distance(hiddenObject.transform.position, gameObject.transform.position);
        timer = checkInterval;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCarried)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                ManageVoice();
                timer = checkInterval;
            }
        }
    }

    void ManageVoice()
    {
        float currentDistance = Vector3.Distance(hiddenObject.transform.position, gameObject.transform.position);

        if (currentDistance > distance)
        {
            Debug.Log("DistanceCheck FREEZING");
            audioSource.PlayOneShot(voiceLines[0]);
        }
        else
        {
            if (currentDistance < previousDistance)
            {
                Debug.Log("DistanceCheck WARM");
                audioSource.PlayOneShot(voiceLines[2]);
                if (currentDistance < hotDistance)
                {
                    Debug.Log("DistanceCheck HOT");
                    audioSource.PlayOneShot(voiceLines[3]);
                }
            }
            else if (currentDistance > previousDistance)
            {
                Debug.Log("DistanceCheck COLD");
                audioSource.PlayOneShot(voiceLines[1]);
            }
        }

        previousDistance = currentDistance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        isCarried = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        isCarried = false;
    }

    private void OnTriggerExit(Collider other)
    {
        isCarried = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isCarried = true;
    }
}
