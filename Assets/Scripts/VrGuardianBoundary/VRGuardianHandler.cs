using UnityEngine;

public class VRGuardianHandler : MonoBehaviour
{
    [SerializeField] private Vector3[] boundaryPoints;

    private void OnDrawGizmos()
    {
        if (boundaryPoints == null || boundaryPoints.Length == 0) return;

        Gizmos.color = Color.green;

        for (int i = 0; i < boundaryPoints.Length; i++)
        {
            Vector3 currentPoint = boundaryPoints[i];
            Vector3 nextPoint = boundaryPoints[(i + 1) % boundaryPoints.Length]; // Loop back to the first point

            Gizmos.DrawLine(currentPoint, nextPoint);
        }
    }
}