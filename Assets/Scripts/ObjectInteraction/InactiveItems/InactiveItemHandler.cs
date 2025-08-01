using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class InactiveItemHandler : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField, TextArea] private string subtitles;
    [SerializeField] private TextMeshProUGUI subtitleTMP;
    
    [Header("Reference")]
    public AudioClip audioclip;
    private AudioSource audioSource;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference leftActivateAction;
    [SerializeField] private InputActionReference rightActivateAction;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    public void InActiveItem()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }

        subtitleTMP.text = subtitles;
    }

    public void SubtitleCall(InputAction.CallbackContext context)
    {
        SoundManager.instance.StartCoroutine(SoundManager.instance.TypeStringInactive(subtitles, audioclip, subtitleTMP, audioSource));
    }
    
    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        rightActivateAction.action.performed += SubtitleCall;
        leftActivateAction.action.performed += SubtitleCall;
    }

    private void OnHoverExited(HoverExitEventArgs args)
    {
        rightActivateAction.action.performed -= SubtitleCall;
        leftActivateAction.action.performed -= SubtitleCall;
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