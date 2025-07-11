using System.Collections;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    public bool fadeOnStart = true;
    public float fadeDuration = 2f;
    public Color fadeColor;
    private Renderer rend;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rend = GetComponent<Renderer>();
        if (fadeOnStart) FadeIn();
    }

    public void FadeIn()
    {
        Debug.Log("FAIDEING IN");
        StartCoroutine(FadeRoutine(1, 0, () => gameObject.SetActive(false)));
    }

    public void FadeOut()
    {
        Debug.Log("FAIDEING OUT");
        gameObject.SetActive(true);
        StartCoroutine(FadeRoutine(0, 1, null));
        
    }

    public void Fade(float alphaIn, float alphaOut, System.Action onComplete = null)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut, onComplete));
    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut, System.Action onComplete)
    {
        float timer = 0;
        while (timer <= fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);
            rend.material.SetColor("_Color", newColor);

            timer += Time.deltaTime;
            yield return null;
        }

        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;
        rend.material.SetColor("_Color", newColor2);
        onComplete?.Invoke();
    }
}
