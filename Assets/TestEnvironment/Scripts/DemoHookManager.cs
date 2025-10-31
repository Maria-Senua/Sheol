using UnityEngine;
using System.Collections;

public class DemoHookManager : MonoBehaviour
{
    // [SerializeField] private GameObject glitchScreen;
    [SerializeField] private GameObject bubbles;
    //[SerializeField] private GameObject safeGrid;
    [SerializeField] private GameObject environment;
    [SerializeField] private GameObject environmentSoundHolder;
    [SerializeField] private Material normalSkyboxMat;
    [SerializeField] private Material weirdSkyboxMat;

    private IEnumerator HookRoutine()
    {
        //glitchScreen.SetActive(true);
        //safeGrid.SetActive(true);
        yield return new WaitForSeconds(0.01f);
        bubbles.SetActive(true);
        
        environmentSoundHolder.SetActive(false);
        RenderSettings.skybox = weirdSkyboxMat;

       
        environment.SetActive(false);
        //Renderer[] rs = environment.GetComponentsInChildren<Renderer>();
        //foreach (Renderer r in rs)
        //    r.enabled = false;

        //glitchScreen.SetActive(false);
    }

    private IEnumerator UnhookRoutine()
    {
        //glitchScreen.SetActive(true);
        yield return new WaitForSeconds(0.01f);
        bubbles.SetActive(false);
        environment.SetActive(true);
      //  Renderer[] rs = environment.GetComponentsInChildren<Renderer>();
      //  foreach (Renderer r in rs)
        //    r.enabled = true;
        environmentSoundHolder.SetActive(true);
        RenderSettings.skybox = normalSkyboxMat;

       

        //safeGrid.SetActive(false);
        //glitchScreen.SetActive(false);
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
