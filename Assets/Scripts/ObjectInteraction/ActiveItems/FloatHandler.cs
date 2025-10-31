using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class FloatHandler : MonoBehaviour
{
    [SerializeField, TextArea] private string debugString;
    
    [Header("Floating Settings")]
    [SerializeField] private bool isFloating = true;
    [SerializeField] private bool isInsideRoom;
    [SerializeField] private float radius = 2f;
    [SerializeField] private float speed = 1f;

    [Header("Gravity")] 
    [SerializeField] private float distance;
    [SerializeField] private LayerMask roomLayer;
    private Rigidbody rb;

    [Header("References")]
    private Vector3 initialPosition;
    // private XRGrabInteractable xrGrabInteractable;
    private Vector3 targetPosition;
    private Coroutine floatingCoroutine;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if(rb == null)
        {
            rb = GetComponentInParent<Rigidbody>();
        }
        // xrGrabInteractable = GetComponent<XRGrabInteractable>();
        // if(xrGrabInteractable == null)
        // {
        //     xrGrabInteractable = GetComponentInParent<XRGrabInteractable>();
        // }
        isFloating = true;
        
    }

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void FixedUpdate()
    {
        CheckRoomType();
        
        // if (xrGrabInteractable.isSelected) // Object grabbed
        // {
        //     StopFloating();
        // }
        // else if (!isInsideRoom) // Object outside the room
        // {
        //     StartFloating();
        // }
    }

    private IEnumerator FloatingRoutine()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * radius;
        targetPosition = initialPosition + randomDirection;

        while (isFloating)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                randomDirection = UnityEngine.Random.insideUnitSphere * radius;
                targetPosition = initialPosition + randomDirection;
            }

            yield return null;
        }
    }
    
    private void StartFloating()
    {
        if (!isFloating)
        {
            initialPosition = transform.position;
            isFloating = true;
            targetPosition = initialPosition;

            if (floatingCoroutine == null)
            {
                floatingCoroutine = StartCoroutine(FloatingRoutine());
            }
        }
    }

    private void StopFloating()
    {
        isFloating = false;

        if (floatingCoroutine != null)
        {
            StopCoroutine(floatingCoroutine);
            floatingCoroutine = null;
        }
    }
    
    private void CheckRoomType()
    {
        Collider[] nearbyColliders = Physics.OverlapSphere(
            transform.position, 
            distance, 
            roomLayer,
            QueryTriggerInteraction.Collide
        );

        isInsideRoom = nearbyColliders.Length > 0;

        rb.useGravity = isInsideRoom;
        debugString = $"Is Inside Room: {isInsideRoom} | Nearby Colliders: {nearbyColliders.Length}";

        if (!isInsideRoom && rb.linearVelocity.magnitude > 0.1f)
        {
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, 0.1f);
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        
        isInsideRoom = true;
        rb.isKinematic = true;
    }
}