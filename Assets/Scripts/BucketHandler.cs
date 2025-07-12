using System;
using UnityEngine;

public class BucketHandler : MonoBehaviour
{
    [SerializeField] private float sphereRadius = 1f;
    [SerializeField] private GameObject water;

    void Update()
    {
        Vector3 origin = transform.position;

        Collider[] hitColliders = Physics.OverlapSphere(origin, sphereRadius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Water"))
            {
                water.SetActive(true);
            }
        }
    }
    
    void OnDrawGizmos()
    {
        Vector3 origin = transform.position;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(origin, sphereRadius);
    }
}
