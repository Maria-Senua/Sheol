using UnityEngine;
using System.Collections;

public class SceneAlignmentManager : MonoBehaviour
{

    [Header("References")]
    public Transform playerRig;       // XR Origin
    public Transform sceneHolder;     // The root of your scene (or main object group)
    public Transform centerPoint;     // The reference point in your scene

    [Header("Offsets")]
    public Vector3 desiredOffset = Vector3.zero;   // Optional manual offset
    public float waitTime = 0.5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(AlignSceneToPlayer());
    }

    IEnumerator AlignSceneToPlayer()
    {
        // Wait for XR rig to initialize properly
        yield return new WaitForSeconds(waitTime);

        // Flatten player forward vector (ignore tilt)
        Vector3 playerForward = new Vector3(playerRig.forward.x, 0, playerRig.forward.z).normalized;

        // Rotate the scene to face same direction as player
        sceneHolder.rotation = Quaternion.Euler(0, playerRig.eulerAngles.y, 0);

        // Move the scene so that centerPoint aligns with player position (plus optional offset)
        Vector3 centerToScene = sceneHolder.position - centerPoint.position;
        sceneHolder.position = playerRig.position + centerToScene + desiredOffset;
    }
}
