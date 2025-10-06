using UnityEngine;
using System.Collections;

public class DemoHookManager : MonoBehaviour
{
    [SerializeField] private GameObject glitchScreen;
    [SerializeField] private GameObject bubbles;
    [SerializeField] private GameObject safeGrid;
    [SerializeField] private GameObject environment;
    [SerializeField] private GameObject environmentSoundHolder;
    [SerializeField] private Material normalSkyboxMat;
    [SerializeField] private Material weirdSkyboxMat;

    private IEnumerator HookRoutine()
    {
        glitchScreen.SetActive(true);
        safeGrid.SetActive(true);
        bubbles.SetActive(true);
        
        environmentSoundHolder.SetActive(false);
        RenderSettings.skybox = weirdSkyboxMat;

        yield return new WaitForSeconds(1f);
        //environment.SetActive(false);
        Renderer[] rs = environment.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs)
            r.enabled = false;

        glitchScreen.SetActive(false);
    }

    private IEnumerator UnhookRoutine()
    {
        glitchScreen.SetActive(true);
        bubbles.SetActive(false);
        //environment.SetActive(true);
        Renderer[] rs = environment.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs)
            r.enabled = true;
        environmentSoundHolder.SetActive(true);
        RenderSettings.skybox = normalSkyboxMat;

        yield return new WaitForSeconds(1f);

        safeGrid.SetActive(false);
        glitchScreen.SetActive(false);
    }

    public void HookPlayer()
    {
        StartCoroutine(HookRoutine());
    }

    public void UnhookPlayer()
    {
        StartCoroutine(UnhookRoutine());
    }

    public void HookThenUnhook()
    {
        StartCoroutine(HookThenUnhookRoutine());
    }

    private IEnumerator HookThenUnhookRoutine()
    {
        yield return new WaitForSeconds(2f);
        HookPlayer();
        yield return new WaitForSeconds(5f);
        UnhookPlayer();
    }
}
