using UnityEngine;

public class TalkToyBehaviour : MonoBehaviour
{
    public GameObject hiddenObject;
    public float distance;
    private float previousDistance;
    public float checkInterval = 5f; 
    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        previousDistance = Vector3.Distance(hiddenObject.transform.position, gameObject.transform.position);
        timer = checkInterval;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            ManageVoice(); 
            timer = checkInterval; 
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
            }
            else if (currentDistance > previousDistance)
            {
                Debug.Log("DistanceCheck COLD");
            }
        }

        previousDistance = currentDistance;
    }
}
