using UnityEngine;
using System.Collections;

public class MenuAlignmentManager : MonoBehaviour
{
    [Header("References")]
    public Transform playerRig;     
    public Transform sceneHolder;   
    public Transform canvas;        

    [Header("Settings")]
    public float waitTime = 0.5f;   

    private Vector3 sceneHolderRelativePos;
    private Quaternion sceneHolderRelativeRot;

    private Vector3 canvasRelativePos;
    private Quaternion canvasRelativeRot;

    void Awake()
    {
        sceneHolderRelativePos = Quaternion.Inverse(playerRig.rotation) * (sceneHolder.position - playerRig.position);
        sceneHolderRelativeRot = Quaternion.Inverse(playerRig.rotation) * sceneHolder.rotation;

        canvasRelativePos = Quaternion.Inverse(playerRig.rotation) * (canvas.position - playerRig.position);
        canvasRelativeRot = Quaternion.Inverse(playerRig.rotation) * canvas.rotation;
    }

    void Start()
    {
        StartCoroutine(AlignSceneToPlayer());
    }

    IEnumerator AlignSceneToPlayer()
    {
        yield return new WaitForSeconds(waitTime);

        sceneHolder.position = playerRig.position + playerRig.rotation * sceneHolderRelativePos;
        sceneHolder.rotation = playerRig.rotation * sceneHolderRelativeRot;

        if (canvas != null)
        {
            canvas.position = playerRig.position + playerRig.rotation * canvasRelativePos;
            canvas.rotation = playerRig.rotation * canvasRelativeRot;
        }
    }
}
