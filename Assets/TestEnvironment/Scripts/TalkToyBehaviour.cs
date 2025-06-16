using UnityEngine;

public class TalkToyBehaviour : MonoBehaviour
{
    public GameObject hiddenObject;
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
        }
        else
        {
            if (currentDistance < previousDistance)
            {
                Debug.Log("DistanceCheck WARM");
                if (currentDistance < hotDistance)
                {
                    Debug.Log("DistanceCheck HOT");
                }
            }
            else if (currentDistance > previousDistance)
            {
                Debug.Log("DistanceCheck COLD");
            }
        }

        previousDistance = currentDistance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        isCarried = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        isCarried = true;
    }
}
