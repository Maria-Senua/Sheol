using System;
using System.Collections;
using UnityEngine;

public class PlayerVoiceController : MonoBehaviour
{
    [SerializeField] private AudioClip[] voiceList;
    [SerializeField] private AudioSource playerVoice;
    
    private void Awake()
    {
        CommentOnStart();
    }

    private void PlayVoice(AudioClip clip)
    {
        playerVoice.PlayOneShot(clip);
    }

    public void CommentOnLockedDoor()
    {
        PlayVoice(voiceList[0]);
    }

    public void CommmentOnOpenedDoor()
    {
        PlayVoice(voiceList[1]);
    }
    
    public void CommentOnBucket()
    {
        PlayVoice(voiceList[2]);
    }
    
     public void CommentOnFlowerPot()
    {
        PlayVoice(voiceList[3]);
    }
     
     public void CommentOnStart() 
     {
         PlayVoice(voiceList[4]);
     }

     public void FirstHerbariumComment()
     {
         StartCoroutine(PlayVoiceSequence(5, 6, true));
     }

     private IEnumerator PlayVoiceSequence(int firstClip, int secondClip, bool saySecond)
     {
         PlayVoice(voiceList[firstClip]);

         // Wait until the first clip finishes
         yield return new WaitForSeconds(voiceList[firstClip].length);

         if (saySecond) PlayVoice(voiceList[secondClip]);
     }
    
}
