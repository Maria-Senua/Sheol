using System.Collections;
using TMPro;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private bool hasTyped = false;
    private bool hasPlayedAudio = false;
    
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
    
    public IEnumerator TypeString(string text, AudioClip audioClip, TextMeshProUGUI TMP)
    {
        if (hasTyped) yield break;
        TMP.text = "";
        if (!hasPlayedAudio)
        {
            AudioSource.PlayClipAtPoint(audioClip, transform.position);
            hasPlayedAudio = true;
        }        
        
        float typingSpeed = audioClip.length / text.Length; 

        foreach (char character in text)
        {
            TMP.text += character;
            yield return new WaitForSeconds(typingSpeed);
        }
        
        hasTyped = true;
    }
}
