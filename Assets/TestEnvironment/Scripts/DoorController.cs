using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private enum DoorStates
    {
        LOCKED,
        CLOSED,
        OPEN
    }

    private DoorStates currentState = DoorStates.CLOSED;

    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float animationDuration = 1f;
    [SerializeField] private bool hingeOnLeft = true;

    private bool isAnimating = false;

    private Quaternion closedRotation;
    private float doorWidth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = DoorStates.CLOSED;
        closedRotation = transform.rotation;
        doorWidth = transform.localScale.x;
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
        if (isAnimating) return;

        switch (currentState)
        {
            case DoorStates.LOCKED:
                Debug.Log("DoorState is locked.");
                break;
            case DoorStates.CLOSED:
                StartCoroutine(RotateDoor(true));
                currentState = DoorStates.OPEN;
                Debug.Log("DoorState is opening.");
                break;
            case DoorStates.OPEN:
                StartCoroutine(RotateDoor(false));
                currentState = DoorStates.CLOSED;
                Debug.Log("DoorState is closing.");
                break;
        }
    }

    private IEnumerator RotateDoor(bool opening)
    {
        isAnimating = true;

        // Determine hinge position
        Vector3 hingeOffset = (hingeOnLeft ? -transform.right : transform.right) * (doorWidth / 2f);
        Vector3 hingePosition = transform.position + hingeOffset;

        float elapsed = 0f;
        float totalAngle = opening ? openAngle : -openAngle;

        while (elapsed < animationDuration)
        {
            float deltaAngle = (totalAngle / animationDuration) * Time.deltaTime;
            transform.RotateAround(hingePosition, Vector3.up, deltaAngle);
            elapsed += Time.deltaTime;
            yield return null;
        }

        isAnimating = false;
    }

    void OnDrawGizmosSelected()
    {
        Vector3 hingeOffset = (hingeOnLeft ? -transform.right : transform.right) * (transform.localScale.x / 2f);
        Vector3 hingePosition = transform.position + hingeOffset;

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(hingePosition, 0.02f);
    }

}
