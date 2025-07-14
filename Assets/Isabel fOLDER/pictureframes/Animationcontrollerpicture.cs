using UnityEngine;

public class Animationcontrollerpicture : MonoBehaviour
{

    [Tooltip("The Material that uses your Shader Graph.")]
    public Material targetMaterial;

    [Header("Texture Frame Sequence")]
    [Tooltip("Assign all Base Color frame textures in order (from 'crossed' to 'clean').")]
    public Texture2D[] baseColorFrames;

    [Header("Animation Control")]
    [Range(0f, 1f)]
    [Tooltip("Controls the progress of the animation: 0 = fully 'crossed', 1 = fully 'clean'.")]
    public float revealProgress = 0f;


    [SerializeField]
    private int _currentFrameIndex = -1; // To track current frame and optimize updates

    void Start()
    {
        // Debug log to confirm Start is called
        Debug.Log("Script Start method called.", this);

        // Try to auto-assign the material if not set in Inspector
        if (targetMaterial == null)
        {
            Renderer rend = GetComponent<Renderer>();
            if (rend != null && rend.sharedMaterial != null)
            {
                // IMPORTANT: Use .material to get an instanced material
                targetMaterial = rend.material;
                Debug.Log("Material auto-assigned: " + targetMaterial.name, this);
            }
            else
            {
                Debug.LogError("AnimatedTextureRevealController: No targetMaterial assigned and no Renderer found. Disabling script.", this);
                enabled = false;
                return;
            }
        }

        // Calculate the initial frame index based on the 'revealProgress' value from the Inspector.
        // This ensures _currentFrameIndex is always a valid index (0 to max frames)
        // before we attempt to update the texture.
        if (baseColorFrames != null && baseColorFrames.Length > 0)
        {
            _currentFrameIndex = Mathf.FloorToInt(Mathf.Clamp01(revealProgress) * (baseColorFrames.Length - 1));
        }
        else
        {
            _currentFrameIndex = 0; // Default to 0 if array is empty, to avoid negative index
            Debug.LogWarning("Base Color Frames array is empty or null in Start. Defaulting currentFrameIndex to 0.", this);
        }
        Debug.Log($"_currentFrameIndex initialized to {_currentFrameIndex} in Start.", this);

        // Now, perform the initial texture update with this valid index
        UpdateTextureFrame();
        Debug.Log("UpdateTextureFrame called from Start.", this);
    }

    void Update()
    {
        // This log should appear many times per second while in Play Mode.
        Debug.Log("Update method running every frame.", this);

        int desiredFrameIndex = Mathf.FloorToInt(Mathf.Clamp01(revealProgress) * (baseColorFrames.Length - 1));

        // This log shows the values the script is currently working with.
        Debug.Log($"Update: revealProgress={revealProgress:F3}, desiredFrameIndex={desiredFrameIndex}, _currentFrameIndex={_currentFrameIndex}", this);

        // This is the condition that triggers a texture change.
        if (desiredFrameIndex != _currentFrameIndex)
        {
            _currentFrameIndex = desiredFrameIndex;
            // This log should appear ONLY when the frame actually changes.
            Debug.Log($"_currentFrameIndex CHANGED to {_currentFrameIndex}. Calling UpdateTextureFrame.", this);
            UpdateTextureFrame();
        }
    }

    /// <summary>
    /// Updates the Base Color texture property on the material.
    /// </summary>
    void UpdateTextureFrame()
    {
        if (baseColorFrames == null || baseColorFrames.Length == 0)
        {
            Debug.LogWarning("Base Color Frames array is empty or null. Cannot animate.", this);
            return;
        }
        if (_currentFrameIndex < 0 || _currentFrameIndex >= baseColorFrames.Length)
        {
            Debug.LogWarning($"Current frame index ({_currentFrameIndex}) is out of bounds for Base Color Frames array (size {baseColorFrames.Length}).", this);
            return;
        }

        // "_MainTex" or "_BaseColorTex" must match the name of the Texture2D property in your Shader Graph
        targetMaterial.SetTexture("_Front", baseColorFrames[_currentFrameIndex]);
    }

    // Optional Public method to control progress from other scripts or Unity Events
    public void SetRevealProgress(float progress)
    {
        revealProgress = Mathf.Clamp01(progress);
    }
}



