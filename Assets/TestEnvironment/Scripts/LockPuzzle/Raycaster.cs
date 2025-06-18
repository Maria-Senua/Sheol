using UnityEngine;
//using UnityEngine.InputSystem;
using UnityEngine.XR;

public class Raycaster : MonoBehaviour
{
    public Transform controllerTransform; 
    public float rayDistance = 5f;

    // Update is called once per frame
    void Update()
    {
        ManageInput();
    }

    private void ManageInput()
    {
        if (IsRightTriggerPressed()) Raycast(); 
    }

    private bool IsRightTriggerPressed()
    {
        Debug.Log("LeftTriggerCheck start");
        InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (!rightHand.isValid)
        {
            return false;
        }

        if (rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed))
        {
            Debug.Log("LeftTriggerCheck pressed");
            return triggerPressed;
        }
        return false;
    }

    private void Raycast()
    {
        Debug.Log("LeftTriggerCheck raycast");
        RaycastHit hit;
        if (Physics.Raycast(controllerTransform.position, controllerTransform.forward, out hit, rayDistance))
        {
            Dial dial = hit.collider.GetComponent<Dial>();
            if (dial != null)
            {
                Debug.Log("LeftTriggerCheck dialrotate");
                dial.Rotate();
            }
        }
    }

    void OnDrawGizmos()
    {
        if (controllerTransform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(controllerTransform.position, controllerTransform.forward * rayDistance);
        }
    }
}
