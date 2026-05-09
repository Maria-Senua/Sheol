using System;
using UnityEngine;

public class BucketHandler : MonoBehaviour
{
    [SerializeField] private float sphereRadius = 1f;
    public GameObject water;
    public bool hasWater = false;

    void Update()
    {
        Vector3 origin = transform.position;

        Collider[] hitColliders = Physics.OverlapSphere(origin, sphereRadius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Sand")) //Should be water but changed to sand because the plane is tagged as Sand because of Audio
            {
                water.SetActive(true);
                hasWater = true;
            }
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, sphereRadius);

        Gizmos.color = new Color(Gizmos.color.r, Gizmos.color.g, Gizmos.color.b, 0.1f);
        Gizmos.DrawSphere(transform.position, sphereRadius);
    }
}
