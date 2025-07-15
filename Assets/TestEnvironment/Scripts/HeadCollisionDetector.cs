using System.Collections.Generic;
using UnityEngine;

public class HeadCollisionDetector : MonoBehaviour
{
    [SerializeField, Range(0, 0.5f)]
    private float _detectionDelay = 0.05f;

    [SerializeField]
    private float _detectionDistance = 0.2f;

    [SerializeField]
    private List<string> detectableTags;

    public List<RaycastHit> DetectedColliderHits { get; private set; }

    private float _currentTime = 0;

    [HideInInspector] public AudioSource audioSource;

    private List<RaycastHit> PerformDetection(Vector3 position, float distance)
    {
        List<RaycastHit> detectedHits = new();
        List<Vector3> directions = new() { transform.forward, transform.right, -transform.right };

        RaycastHit hit;
        foreach (var dir in directions)
        {
            if (Physics.Raycast(position, dir, out hit, distance))
            {
                if (detectableTags.Contains(hit.collider.tag))
                {
                    detectedHits.Add(hit);
                }
            }
        }

        return detectedHits;
    }

    private void Start()
    {
        DetectedColliderHits = PerformDetection(transform.position, _detectionDistance);
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime > _detectionDelay)
        {
            _currentTime = 0;
            DetectedColliderHits = PerformDetection(transform.position, _detectionDistance);
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        Gizmos.color = DetectedColliderHits.Count > 0 ? new Color(1, 0, 0, 0.5f) : new Color(0, 1, 0, 0.5f);
        Gizmos.DrawWireSphere(transform.position, _detectionDistance);

        List<Vector3> directions = new() { transform.forward, transform.right, -transform.right };
        Gizmos.color = Color.magenta;
        foreach (var dir in directions)
        {
            Gizmos.DrawRay(transform.position, dir * _detectionDistance);
        }
    }
}
