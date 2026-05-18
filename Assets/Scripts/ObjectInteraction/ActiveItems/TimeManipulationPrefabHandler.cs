using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Oculus.Interaction;

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

    [Header("Sprite Setup")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] spriteSequence;
    [SerializeField] private InteractableGroupView handGrabGroup;
    private int currentSpriteIndex = 0;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference leftActivateAction;
    [SerializeField] private InputActionReference rightActivateAction;

    public UnityEvent onPagerFinished;
    public UnityEvent onDiaryRead;
    public UnityEvent onPhotoRevealed;

    // MISSING PIECE FIX: Flag to prevent duplicate scene-load invokes
    private bool hasTriggered = false; 

    private void Awake()
    {
        if (bottomLimit != null && player != null)
        {
            previousY = Vector3.Distance(bottomLimit.transform.position, player.transform.position);
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
        if (bottomLimit == null || player == null || handGrabGroup == null) return;

        Vector3 direction = player.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;

        distance = Vector3.Distance(bottomLimit.transform.position, player.transform.position);

        if (meshRenderer != null && handGrabGroup.State == InteractableState.Select) 
        {
            if (!hasTriggered) 
            {
                ChangeMaterialBasedOnDistance();
            }
        }
        else
        {
            previousY = distance;
        }
    }

    private void ChangeMaterialBasedOnDistance()
    {
        xy = Mathf.Abs(distance - previousY);
        Debug.Log("Distance: " + distance + ", PreviousY: " + previousY + ", XY: " + xy);
        
        if (xy >= distanceThreshold)
        {
            if (meshRenderer != null && currentMaterial != null)
            {
                meshRenderer.material = material;
                hasTriggered = true;
                
                Invoke(nameof(ReactToPhoto), 3f); 
            }

            previousY = distance;
        }
    }

    private void ReactToPhoto()
    {
        onPhotoRevealed?.Invoke();
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

        leftActivateAction.action.performed -= ToggleMaterial;
        rightActivateAction.action.performed -= ToggleMaterial;
    }
}