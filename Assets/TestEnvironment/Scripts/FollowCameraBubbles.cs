using UnityEngine;

public class FollowCameraBubbles : MonoBehaviour
{
    public Transform cameraTransform;
    public Vector3 offset = new Vector3(0, 0, 1f);

    void LateUpdate()
    {
        if (cameraTransform != null)
        {
            transform.position = cameraTransform.position + cameraTransform.forward * offset.z;
            transform.rotation = cameraTransform.rotation;
        }
    }
}
