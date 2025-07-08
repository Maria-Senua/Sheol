using System.Collections;
using TMPro;
using UnityEngine;

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
    
    public IEnumerator TypeString(string text, AudioClip audioClip, TextMeshProUGUI TMP, AudioSource audioSource)
    {
        TMP.text = "";

        audioSource.clip = audioClip;
        audioSource.Play();
        
        float typingSpeed = audioClip.length / text.Length; 

        foreach (char character in text)
        {
            TMP.text += character;
            yield return new WaitForSeconds(typingSpeed);
        }
        
    }
}
