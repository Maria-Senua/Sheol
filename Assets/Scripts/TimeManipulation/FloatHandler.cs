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
    private XRGrabInteractable xrGrabInteractable;
    private Vector3 targetPosition;
    private Coroutine floatingCoroutine;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        xrGrabInteractable = GetComponent<XRGrabInteractable>();
        isFloating = true;
        
    }

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void FixedUpdate()
    {
        CheckRoomType();
        Floating();
    }

    private void Floating()
    {
        if (xrGrabInteractable.isSelected) //Might need in future 
        {
            isFloating = false;
            if (floatingCoroutine != null)
            {
                StopCoroutine(floatingCoroutine);
                floatingCoroutine = null;
            }
        }
        else
        {
            if (!isFloating)
            {
                initialPosition = transform.position;
                isFloating = true;
                targetPosition = initialPosition;
            }

            if (floatingCoroutine == null)
            {
                floatingCoroutine = StartCoroutine(FloatingRoutine());
            }
        }
    }

    private IEnumerator FloatingRoutine()
    {
        while (isFloating)
        {
            targetPosition = initialPosition + new Vector3(
                UnityEngine.Random.Range(-radius, radius),
                UnityEngine.Random.Range(-radius, radius),
                UnityEngine.Random.Range(-radius, radius)
            );

            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
    
    private void CheckRoomType()
    {
        isInsideRoom = Physics.Raycast(transform.position, Vector3.down, distance, roomLayer);
        isFloating = !isInsideRoom;

        rb.useGravity = isInsideRoom;
        debugString = $"Is Inside Room: {isInsideRoom}";
        
        if (!isInsideRoom && rb.linearVelocity.magnitude > 0.1f)
        {
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, 0.1f);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}