using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

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

    [SerializeField] public GameObject viewBlocker;
    [SerializeField] private AudioClip[] doorSounds;

    public UnityEvent onPuzzleSolved;
    public UnityEvent onDoorTouched;
    public UnityEvent onDoorUnlocked;
    private bool puzzleSolved = false;

    void Start()
    {
        currentState = DoorStates.LOCKED;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Key"))
        {
            Debug.Log("Key collected, unlocking door.");
            viewBlocker.SetActive(false);
            audioSource.Play();
            currentState = DoorStates.CLOSED;
            StartCoroutine(RotateDoor(new Vector3(0f, -90f, 0f), 3f));
            StartCoroutine(RemoveKey(collision.gameObject));
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            Debug.Log("Key collected, unlocking door.");
            viewBlocker.SetActive(false);
            audioSource.PlayOneShot(doorSounds[1]);
            onDoorUnlocked?.Invoke();
            currentState = DoorStates.CLOSED;
            StartCoroutine(RotateDoor(new Vector3(0f, -90f, 0f), 3f));
            StartCoroutine(RemoveKey(other.gameObject));
        }

        if (other.gameObject.CompareTag("Player") && currentState == DoorStates.LOCKED)
        {
            audioSource.PlayOneShot(doorSounds[0]);
            onDoorTouched?.Invoke();
        }
    }

    private IEnumerator RemoveKey(GameObject key)
    {
        yield return new WaitForSeconds(2f);
        key.SetActive(false);
    }

    public void TouchDoor()
    {
        switch (currentState)
        {
            case DoorStates.LOCKED:
                audioSource.PlayOneShot(doorSounds[0]);
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
        Quaternion startRotation = transform.localRotation;
        Quaternion targetRotation = Quaternion.Euler(targetEulerAngles);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.localRotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = targetRotation;
        isPlayingAnimation = false;

        if (!puzzleSolved)
        {
            onPuzzleSolved?.Invoke();
            puzzleSolved = true;
        }
    }
}
