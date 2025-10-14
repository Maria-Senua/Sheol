using UnityEngine;
using Unity.XR.CoreUtils;
using System.Collections;

public class VRSpawnManager : MonoBehaviour
{

    //public XROrigin xrOrigin; 
    //public Transform spawnPoint;
    public Transform playerRig;
    public Transform boat;
    public Transform bubbles;
    public float distanceInFront = 5f;
    public Vector3 boatOffset = Vector3.zero;
    public Vector3 bubblesOffset = new Vector3(0, -0.5f, 0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //MovePlayerTo(spawnPoint.position, spawnPoint.rotation);
        StartCoroutine(PositionObjectsAfterXR());
    }

    IEnumerator PositionObjectsAfterXR()
    {
        yield return new WaitForSeconds(0.5f);

        Vector3 forward = new Vector3(playerRig.forward.x, 0, playerRig.forward.z).normalized;

        boat.position = playerRig.position + forward * distanceInFront + boatOffset;

        boat.rotation = Quaternion.Euler(0, 0, 0);

        bubbles.position = boat.position + bubblesOffset;

        bubbles.rotation = boat.rotation;
    }

    //public void MovePlayerTo(Vector3 targetPosition, Quaternion targetRotation)
    //{
    //    var cameraTransform = xrOrigin.Camera.transform;

    //    Vector3 offset = xrOrigin.transform.position - cameraTransform.position;

    //    xrOrigin.transform.position = targetPosition + offset;

    //    xrOrigin.transform.rotation = targetRotation;
    //}
}
