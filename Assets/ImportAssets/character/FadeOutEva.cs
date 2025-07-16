using UnityEngine;

public class FadeAlphaParameter2 : MonoBehaviour
{
    public Material material;
    public float fadeDuration = 2f;

    private float currentAlpha;
    private float fadeSpeed;

    void Start()
    {
        // Start at max alpha (15)
        currentAlpha = 15f;
        fadeSpeed = currentAlpha / fadeDuration;
    }

    void Update()
    {
        if (material != null && currentAlpha > 0f)
        {
            currentAlpha -= fadeSpeed * Time.deltaTime;
            currentAlpha = Mathf.Max(currentAlpha, 0f);

            // Set the ALPHA parameter in the material
            material.SetFloat("_ALPHA", currentAlpha);
        }
    }
}
