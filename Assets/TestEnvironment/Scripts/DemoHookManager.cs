using System;
using UnityEngine;
using System.Collections;

public class DemoHookManager : MonoBehaviour
{
    // [SerializeField] private GameObject glitchScreen;
    [SerializeField] private GameObject bubbles;
    //[SerializeField] private GameObject safeGrid;
    // [SerializeField] private GameObject environment;
    [SerializeField] private GameObject environmentSoundHolder;
    // [SerializeField] private Material normalSkyboxMat;
    // [SerializeField] private Material weirdSkyboxMat;
    [SerializeField] private GameObject underwaterVolume;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private IEnumerator HookRoutine(AudioClip clip)
    {
        //glitchScreen.SetActive(true);
        //safeGrid.SetActive(true);
        yield return new WaitForSeconds(0.01f);
        bubbles.SetActive(true);
        audioSource.PlayOneShot(clip);
        underwaterVolume.SetActive(true);
        environmentSoundHolder.SetActive(false);
        //RenderSettings.skybox = weirdSkyboxMat;

       
        //environment.SetActive(false);
        //Renderer[] rs = environment.GetComponentsInChildren<Renderer>();
        //foreach (Renderer r in rs)
          //  r.enabled = false;

        //glitchScreen.SetActive(false);
    }

    private IEnumerator UnhookRoutine()
    {
        //glitchScreen.SetActive(true);
        yield return new WaitForSeconds(0.01f);
        bubbles.SetActive(false);
        //environment.SetActive(true);
        
        //Renderer[] rs = environment.GetComponentsInChildren<Renderer>();
       // foreach (Renderer r in rs)
       //     r.enabled = true;
        
        underwaterVolume.SetActive(false);
        environmentSoundHolder.SetActive(true);
        //RenderSettings.skybox = normalSkyboxMat;

       

        //safeGrid.SetActive(false);
        //glitchScreen.SetActive(false);
    }

    public void HookPlayer(AudioClip clip)
    {
        StartCoroutine(HookRoutine(clip));
    }

    public void UnhookPlayer()
    {
        StartCoroutine(UnhookRoutine());
    }

    public void HookThenUnhook(AudioClip clip)
    {
        StartCoroutine(HookThenUnhookRoutine(clip));
    }

    private IEnumerator HookThenUnhookRoutine(AudioClip clip)
    {
        yield return new WaitForSeconds(2f);
        HookPlayer(clip);
        yield return new WaitForSeconds(5f);
        UnhookPlayer();
    }
}
