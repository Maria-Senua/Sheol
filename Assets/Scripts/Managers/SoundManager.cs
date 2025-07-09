using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public IEnumerator TypeString(string text, AudioClip audioClip, TextMeshProUGUI TMP, AudioSource audioSource, XRGrabInteractable xrGrabInteractable)
    {
        TMP.text = "";

        audioSource.clip = audioClip;
        audioSource.Play();
        
        float typingSpeed = audioClip.length / text.Length; 

        foreach (char character in text)
        {
            if (xrGrabInteractable.isSelected)
            {
                TMP.text = "";
                audioSource.Stop();
                audioSource.clip = null;
                yield break;
            }
            
            TMP.text += character;
            yield return new WaitForSeconds(typingSpeed);
        }
        
    }
}
