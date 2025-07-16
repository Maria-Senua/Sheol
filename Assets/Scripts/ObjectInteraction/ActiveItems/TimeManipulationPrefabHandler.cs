using UnityEngine;
using UnityEngine.InputSystem;

public class TimeManipulationPrefabHandler : MonoBehaviour
{
[Header("Setup")]
    [SerializeField] private GameObject bottomLimit;
    [SerializeField] private GameObject player;
    [SerializeField] private float distanceThreshold = 1f;
    private float previousY;
    private float distance;
    private float xy;

    [Header("Mesh Renderer & Materials")]
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material material;
    [SerializeField] private Material currentMaterial;

    private bool isMaterial1Active = true;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference leftActivateAction;
    [SerializeField] private InputActionReference rightActivateAction;

    private void Awake()
    {
        previousY = Vector3.Distance(bottomLimit.transform.position, player.transform.position);

        leftActivateAction.action.performed += ToggleMaterial;
        rightActivateAction.action.performed += ToggleMaterial;
    }

    private void Update()
    {
        distance = Vector3.Distance(bottomLimit.transform.position, player.transform.position);

        ChangeMaterialBasedOnDistance();

        Vector3 direction = player.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }

    private void ChangeMaterialBasedOnDistance()
    {
        xy = Mathf.Abs(distance - previousY);
        
        if (xy >= distanceThreshold)
        {
            if (meshRenderer != null && currentMaterial != null)
            {
                Debug.Log("Changing material based on distance");
                meshRenderer.material = material;
            }

            previousY = distance;
        }
    }

    private void ToggleMaterial(InputAction.CallbackContext context)
    {
        if (meshRenderer != null && currentMaterial != null)
        {
            Debug.Log("Toggling material");
            meshRenderer.material = currentMaterial;
        }
    }

    private void OnEnable()
    {
        leftActivateAction.action.Enable();
        rightActivateAction.action.Enable();
    }

    private void OnDisable()
    {
        leftActivateAction.action.Disable();
        rightActivateAction.action.Disable();
    }
}
