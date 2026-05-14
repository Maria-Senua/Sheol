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
        if (playerVoice.isPlaying) playerVoice.Stop();
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
        StartCoroutine(PlayVoiceSequence(2, 7, true));
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

     public void SecondHerbariumComment()
     {
         StartCoroutine(PlayVoiceSequence(9, 8, true)); //check
     }

     public void DaisyComment()
     {
         PlayVoice(voiceList[9]);
     }

     public void LockedDrawerComment()
     {
         PlayVoice(voiceList[11]);
     }

     public void UnlockedDrawerComment()
     {
         PlayVoice(voiceList[12]);
     }

     public void ReadPoem()
     {
         PlayVoice(voiceList[10]);
     }

     private IEnumerator PlayVoiceSequence(int firstClip, int secondClip, bool saySecond)
     {
         PlayVoice(voiceList[firstClip]);

         yield return new WaitForSeconds(voiceList[firstClip].length);

         if (saySecond) PlayVoice(voiceList[secondClip]);
     }
    
}
