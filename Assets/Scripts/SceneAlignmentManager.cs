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
    public Vector3 pointOffset = Vector3.zero;
    public float waitTime = 0.5f;

    private Vector3 centerPointRelativePos;


    void Awake()
    {
        if (playerRig != null && centerPoint != null)
        {
            centerPointRelativePos = centerPoint.position - playerRig.position;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(AlignSceneToPlayer());
    }

    IEnumerator AlignSceneToPlayer()
    {
        yield return new WaitForSeconds(waitTime);

        sceneHolder.rotation = Quaternion.Euler(0, playerRig.eulerAngles.y, 0);

        Vector3 centerToScene = sceneHolder.position - centerPoint.position;
        sceneHolder.position = playerRig.position + centerToScene + desiredOffset;

        centerPoint.position = playerRig.position + centerPointRelativePos + pointOffset;


        enabled = false;
        //Destroy(gameObject);
    }
}