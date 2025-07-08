using UnityEngine;

public class DoorController : MonoBehaviour
{
    private enum DoorStates
    {
        LOCKED,
        CLOSED,
        OPEN
    }

    private DoorStates currentState = DoorStates.LOCKED;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = DoorStates.LOCKED;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            currentState = DoorStates.CLOSED;
        }
    }

    public void TouchDoor()
    {
        if (currentState == DoorStates.CLOSED)
        {
            currentState = DoorStates.OPEN;
            //add anim
        } else if (currentState == DoorStates.OPEN)
        {
            currentState = DoorStates.CLOSED;
            //add anim
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
