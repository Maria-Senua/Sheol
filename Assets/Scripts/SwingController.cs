using UnityEngine;
using System.Collections;

public class SwingController : MonoBehaviour
{
    [Header("Pendulum Settings")]
    [SerializeField] private float maxSwingAngle = 45f; 
    [SerializeField] private float speed = 5f;         
    [SerializeField] private float duration = 5f;      

    private Quaternion originalRotation;
    private Coroutine swingCoroutine;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalRotation = transform.localRotation;
        
    }
    
    public void StartSwinging()
    {
        // Prevent overlapping coroutines if clicked/triggered multiple times
        if (swingCoroutine != null)
        {
            StopCoroutine(swingCoroutine);
        }
        
        swingCoroutine = StartCoroutine(PendulumRoutine());
    }

    private IEnumerator PendulumRoutine()
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            
            // Percentage of completion (0 to 1)
            float normalizedTime = elapsed / duration; 

            // Smoothly decrease the amplitude from 1 down to 0 over the duration
            float amplitudeFade = Mathf.Lerp(1f, 0f, normalizedTime);

            // Calculate the current swing angle using a Sine wave
            // Multiplying by amplitudeFade makes the swing smaller and smaller
            float currentAngle = Mathf.Sin(elapsed * speed) * maxSwingAngle * amplitudeFade;

            // Apply the rotation strictly on the X-axis relative to the original rotation
            transform.localRotation = originalRotation * Quaternion.Euler(currentAngle, 0f, 0f);

            yield return null; // Wait for the next frame
        }

        // Snap precisely back to the original rotation at the end to avoid rounding errors
        transform.localRotation = originalRotation;
        swingCoroutine = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
