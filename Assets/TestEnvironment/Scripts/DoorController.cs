using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField, TextArea] private string debugString;
    private enum DoorStates
    {
        LOCKED,
        CLOSED,
        OPEN
    }

    private DoorStates currentState = DoorStates.LOCKED;
    private bool isPlayingAnimation = false;
    private AudioSource audioSource;
    
    void Start()
    {
        currentState = DoorStates.LOCKED;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            audioSource.Play();
            currentState = DoorStates.CLOSED;
            StartCoroutine(RotateDoor(new Vector3(0f, -90f, 0f), 3f));
            debugString = "Door is now closed and unlocked.";
            
        }
    }

    public void TouchDoor()
    {
        switch (currentState)
        {
            case DoorStates.LOCKED:
                debugString = "Door is locked. Find the key to unlock it.";
                break;
            case DoorStates.CLOSED:
                currentState = DoorStates.OPEN;
                break;
            case DoorStates.OPEN:
                currentState = DoorStates.CLOSED;
                break;
        }
    }

    private IEnumerator RotateDoor(Vector3 targetEulerAngles, float duration)
    {
        isPlayingAnimation = true;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(targetEulerAngles);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
        isPlayingAnimation = false;
    }
}
