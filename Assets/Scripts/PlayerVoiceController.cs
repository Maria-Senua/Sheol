using UnityEngine;

public class PlayerVoiceController : MonoBehaviour
{
    [SerializeField] private AudioClip[] voiceList;
    [SerializeField] private AudioSource playerVoice;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
