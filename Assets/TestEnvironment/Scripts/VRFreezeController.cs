using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.SpatialTracking;

public class VRFreezeController : MonoBehaviour
{
    [Header("XR Rig Parts")]
    public Transform xrCamera;
    public GameObject leftController;
    public GameObject rightController;

    [Header("Input System")]
    public InputActionAsset inputActions;

    private Vector3 frozenCameraPos;
    private Quaternion frozenCameraRot;
    private bool isFrozen = false;

    private TrackedPoseDriver cameraPoseDriver;
    private TrackedPoseDriver leftPoseDriver;
    private TrackedPoseDriver rightPoseDriver;

    void Awake()
    {
        if (xrCamera != null)
        {
            cameraPoseDriver = xrCamera.GetComponent<TrackedPoseDriver>();
            frozenCameraPos = xrCamera.position;
            frozenCameraRot = xrCamera.rotation;
        }

        if (leftController != null)
            leftPoseDriver = leftController.GetComponent<TrackedPoseDriver>();

        if (rightController != null)
            rightPoseDriver = rightController.GetComponent<TrackedPoseDriver>();

        FreezePlayer(); 
    }

    void Update()
    {
        if (isFrozen && xrCamera != null)
        {
            xrCamera.position = frozenCameraPos;
            xrCamera.rotation = frozenCameraRot;
        }
    }

    public void FreezePlayer()
    {
        isFrozen = true;

        if (cameraPoseDriver != null)
            cameraPoseDriver.enabled = false;

        if (leftPoseDriver != null) leftPoseDriver.enabled = false;
        if (rightPoseDriver != null) rightPoseDriver.enabled = false;

        if (leftController != null) leftController.SetActive(false);
        if (rightController != null) rightController.SetActive(false);

        if (inputActions != null)
            inputActions.Disable();
    }

    public void UnfreezePlayer()
    {
        isFrozen = false;

        if (cameraPoseDriver != null) cameraPoseDriver.enabled = true;
        if (leftPoseDriver != null) leftPoseDriver.enabled = true;
        if (rightPoseDriver != null) rightPoseDriver.enabled = true;

        if (leftController != null) leftController.SetActive(true);
        if (rightController != null) rightController.SetActive(true);

        if (inputActions != null)
            inputActions.Enable();
    }
}
