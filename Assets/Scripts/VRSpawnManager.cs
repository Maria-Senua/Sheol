using UnityEngine;
using Unity.XR.CoreUtils;

public class VRSpawnManager : MonoBehaviour
{

    public XROrigin xrOrigin; 
    public Transform spawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MovePlayerTo(spawnPoint.position, spawnPoint.rotation);
    }

    public void MovePlayerTo(Vector3 targetPosition, Quaternion targetRotation)
    {
        var cameraTransform = xrOrigin.Camera.transform;

        Vector3 offset = xrOrigin.transform.position - cameraTransform.position;

        xrOrigin.transform.position = targetPosition + offset;

        xrOrigin.transform.rotation = targetRotation;
    }
}
