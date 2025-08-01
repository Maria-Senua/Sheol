using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

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

    [Header("Sprite Setup")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] spriteSequence;
    [SerializeField] private XRGrabInteractable xrGrabInteractable;
    private int currentSpriteIndex = 0;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference leftActivateAction;
    [SerializeField] private InputActionReference rightActivateAction;

    public UnityEvent onPagerFinished;
    public UnityEvent onDiaryRead;

    private void Awake()
    {
        previousY = Vector3.Distance(bottomLimit.transform.position, player.transform.position);
        xrGrabInteractable = GetComponent<XRGrabInteractable>();
        if(xrGrabInteractable == null)
        {
            xrGrabInteractable = GetComponentInChildren<XRGrabInteractable>();
        }
        
        leftActivateAction.action.performed += ToggleMaterial;
        rightActivateAction.action.performed += ToggleMaterial;

        if (spriteRenderer != null && spriteSequence.Length > 0)
        {
            spriteRenderer.sprite = spriteSequence[0];
        }
    }

    private void Update()
    {
        distance = Vector3.Distance(bottomLimit.transform.position, player.transform.position);

        if (meshRenderer != null && xrGrabInteractable.isSelected) ChangeMaterialBasedOnDistance();
        if (spriteRenderer != null && xrGrabInteractable.isSelected) UpdateSpriteBasedOnDistance();
        
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
                meshRenderer.material = material;
                onDiaryRead?.Invoke();
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

    private void UpdateSpriteBasedOnDistance()
    {
        xy = Mathf.Abs(distance - previousY);

        if (xy >= distanceThreshold && currentSpriteIndex < spriteSequence.Length - 1)
        {
            currentSpriteIndex++;
            spriteRenderer.sprite = spriteSequence[currentSpriteIndex];

            Debug.Log("Sprite changed to index: " + currentSpriteIndex);
            if (currentSpriteIndex == spriteSequence.Length - 1) onPagerFinished?.Invoke();
            previousY = distance;
        }
    }

    private void ResetSpriteSequence(InputAction.CallbackContext context)
    {
        currentSpriteIndex = 0;
        if (spriteRenderer != null && spriteSequence.Length > 0)
        {
            spriteRenderer.sprite = spriteSequence[0];
            Debug.Log("Sprite sequence reset.");
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
