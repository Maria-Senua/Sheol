using System;
using UnityEngine;
using UnityEngine.Events;

public class FallDown : MonoBehaviour
{
    
    public UnityEvent onFallingDown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Down"))
        {
            onFallingDown.Invoke();
        }
    }
}
