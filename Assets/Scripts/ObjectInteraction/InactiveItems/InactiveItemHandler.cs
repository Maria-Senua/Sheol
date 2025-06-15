using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class InactiveItemHandler : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField, TextArea] private string subtitles;
    [SerializeField] private TextMeshProUGUI subtitleTMP;
    [SerializeField] private bool isHovering = false;

    [Header("Reference")]
    private AudioSource audioSource;
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
        }
    }

    void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrab);
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        InActiveItem();
    }

    public void InActiveItem()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }

        subtitleTMP.text = subtitles;
    }
}