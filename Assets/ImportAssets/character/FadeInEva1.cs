using UnityEngine;

public class FadeAlphaParameter : MonoBehaviour
{
    public Material material2;
    public float fadeDuration2 = 2f;

    private float currentAlpha2;
    private float fadeSpeed2;

    void Start()
    {
        // Start at max alpha (15)
        currentAlpha2 = 0f;
        fadeSpeed2 = currentAlpha2 / fadeDuration2;
    }

    void Update()
    {
        if (material2 != null && currentAlpha2 > 0f)
        {
            currentAlpha2 -= fadeSpeed2 * Time.deltaTime;
            currentAlpha2 = Mathf.Max(currentAlpha2, 15f);

            // Set the ALPHA parameter in the material
            material2.SetFloat("_ALPHA", currentAlpha2);
        }
    }
}
