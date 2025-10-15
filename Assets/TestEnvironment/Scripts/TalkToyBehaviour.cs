using System;
using UnityEngine;

public class TalkToyBehaviour : MonoBehaviour
{
    public GameObject hiddenObject;
    private AudioSource audioSource;
    public AudioClip[] voiceLines;
    public float freezeDistance;
    public float hotDistance;
    private float previousDistance;
    public float checkInterval = 5f; 
    private float timer;
    private bool isCarried = false;
    private Rigidbody rb;

    public AudioClip commentVoice;
    public float commentTimer = 4f;
    private bool hasCommented = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        previousDistance = Vector3.Distance(hiddenObject.transform.position, gameObject.transform.position);
        timer = checkInterval;
        audioSource = gameObject.GetComponent<AudioSource>();
        rb = gameObject.GetComponent<Rigidbody>();
        if(rb == null)
        {
            rb = gameObject.GetComponentInParent<Rigidbody>();
        }
        rb.useGravity = false;
        rb.isKinematic = true;
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

            if (!hasCommented)
            {
                commentTimer -= Time.deltaTime;
                if (commentTimer <= 0f)
                {
                    audioSource.PlayOneShot(commentVoice);
                    hasCommented = true;
                }
            }
        }
    }

    void ManageVoice()
    {
        float currentDistance = Vector3.Distance(hiddenObject.transform.position, gameObject.transform.position);

        if (currentDistance > freezeDistance)
        {
            Debug.Log("DistanceCheck FREEZING " + currentDistance);
            audioSource.PlayOneShot(voiceLines[0]);
        }
        else
        {
            if (currentDistance < previousDistance && currentDistance > hotDistance)
            {
                Debug.Log("DistanceCheck WARM " + currentDistance);
                audioSource.PlayOneShot(voiceLines[2]);
               
            }
            else if (currentDistance < hotDistance)
            {
                Debug.Log("DistanceCheck HOT " + currentDistance);
                audioSource.PlayOneShot(voiceLines[3]);
            }
            else if (currentDistance > previousDistance && (currentDistance - previousDistance > 0.5))
            {
                Debug.Log("DistanceCheck COLD " + currentDistance);
                audioSource.PlayOneShot(voiceLines[1]);
            }
        }

        previousDistance = currentDistance;
    }

    public void DetectCarry()
    {
        isCarried = true;
        rb.useGravity = true;
        rb.isKinematic = false;
    }
    
    public void StopCarry()
    {
        isCarried = false;
        rb.useGravity = true;
        rb.isKinematic = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        rb.isKinematic = true;
     }

    // private void OnTriggerEnter(Collider other)
    // {
    //     isCarried = false;
    // }
    //
    // private void OnTriggerExit(Collider other)
    // {
    //     isCarried = true;
    // }

    // private void OnCollisionExit(Collision collision)
    // {
    //     isCarried = true;
    // }
}
