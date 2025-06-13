using System;
using UnityEngine;

public class RandomFloatHandler : MonoBehaviour
{
    [SerializeField, TextArea] private string debugString;
    
    [Header("Floating Settings")]
    [SerializeField] private bool isFloating = true;
    [SerializeField] private float radius = 2f;
    [SerializeField] private float speed = 1f;

    [Header("Gravity")] 
    [SerializeField] private float distance;
    [SerializeField] private LayerMask roomLayer;
    private Rigidbody rb;

    private Vector3 initialPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void FixedUpdate()
    {
        CheckRoomType();
    }

    private void Update()
    {
        Floating();
    }

    private void Floating()
    {
        if (!isFloating) return;

        float offsetX = Mathf.PerlinNoise(Time.time * speed, 0f) * 2f - 1f;
        float offsetY = Mathf.PerlinNoise(0f, Time.time * speed) * 2f - 1f;
        float offsetZ = Mathf.PerlinNoise(Time.time * speed, Time.time * speed) * 2f - 1f;

        Vector3 randomOffset = new Vector3(offsetX, offsetY, offsetZ) * radius;

        transform.position = initialPosition + randomOffset;
    }

    private void CheckRoomType()
    {
        bool isInsideRoom = Physics.Raycast(transform.position, Vector3.down, distance, roomLayer);
        
        rb.useGravity = isInsideRoom;
        isFloating = !isInsideRoom;
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