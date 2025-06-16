using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class InactiveItemHandler : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField, TextArea] private string subtitles;
    [SerializeField] private TextMeshProUGUI subtitleTMP;

    [Header("Reference")]
    private AudioSource audioSource;

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
}