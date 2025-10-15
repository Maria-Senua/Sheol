using UnityEngine;
using System.Collections;

public class SceneAlignmentManager : MonoBehaviour
{

    [Header("References")]
    public Transform playerRig;       
    public Transform sceneHolder;     
    public Transform centerPoint;     

    [Header("Offsets")]
    public Vector3 desiredOffset = Vector3.zero;   
    public float waitTime = 0.5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(AlignSceneToPlayer());
    }

    IEnumerator AlignSceneToPlayer()
    {
        yield return new WaitForSeconds(waitTime);

        Vector3 playerForward = new Vector3(playerRig.forward.x, 0, playerRig.forward.z).normalized;

        sceneHolder.rotation = Quaternion.Euler(0, playerRig.eulerAngles.y, 0);

        Vector3 centerToScene = sceneHolder.position - centerPoint.position;
        sceneHolder.position = playerRig.position + centerToScene + desiredOffset;

        enabled = false;
    }
}
