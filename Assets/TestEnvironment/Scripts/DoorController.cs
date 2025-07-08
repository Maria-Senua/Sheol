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
        switch (currentState)
        {
            case DoorStates.LOCKED:
                Debug.Log("Doorstate locked");
                break;
            case DoorStates.CLOSED:
                currentState = DoorStates.OPEN;
                Debug.Log("Doorstate closed");
                break;
            case DoorStates.OPEN:
                currentState = DoorStates.CLOSED;
                Debug.Log("Doorstate open");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
