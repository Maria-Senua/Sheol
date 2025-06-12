using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip[] musicList;

    public GameObject topLimit;
    public GameObject bottomLimit;

    private float spiraleDistance;
    public Transform environmentRoot; 
    private float initialEnvY;


    private AudioSource track2Source;
    private Dictionary<int, AudioSource> trackSources = new Dictionary<int, AudioSource>();
    private HashSet<int> activeTracks = new HashSet<int>();

    private void Awake()
    {
        initialEnvY = environmentRoot.position.y;

        float topY = topLimit.transform.position.y;
        float bottomY = bottomLimit.transform.position.y;
        spiraleDistance = Mathf.Abs(topY - bottomY);

        Debug.Log("spiraleDistance " + spiraleDistance);

        track2Source = CreateAndPlayLoopingSource(musicList[2]);

        trackSources[0] = CreateLoopingSource(musicList[0]);
        trackSources[1] = CreateLoopingSource(musicList[1]);
        trackSources[3] = CreateLoopingSource(musicList[3]);
        trackSources[4] = CreateLoopingSource(musicList[4]);

    }

    private AudioSource CreateLoopingSource(AudioClip clip)
    {
        AudioSource src = gameObject.AddComponent<AudioSource>();
        src.clip = clip;
        src.loop = true;
        src.playOnAwake = false;
        return src;
    }

    private AudioSource CreateAndPlayLoopingSource(AudioClip clip)
    {
        var src = CreateLoopingSource(clip);
        src.Play();
        return src;
    }

    // Update is called once per frame
    void Update()
    {
        float topY = topLimit.transform.position.y;
        float bottomY = bottomLimit.transform.position.y;
        float centerY = environmentRoot.position.y; 

        float spiraleDistance = Mathf.Abs(topY - bottomY); 
        float relativeY = topY - centerY;
        float ratio = relativeY / spiraleDistance;

        HashSet<int> tracksToPlay = new HashSet<int>();

        if (ratio <= 1f / 6f)
        {
            Debug.Log("AddTrack 1/6");
            tracksToPlay.Add(0);
            tracksToPlay.Add(1);
        }
        else if (ratio <= 2f / 6f)
        {
            Debug.Log("AddTrack 2/6");
            tracksToPlay.Add(1);
        }
        else if (ratio >= 5f / 6f)
        {
            Debug.Log("AddTrack 5/6");
            tracksToPlay.Add(3);
            tracksToPlay.Add(4);
        }
        else if (ratio >= 4f / 6f)
        {
            Debug.Log("AddTrack 4/6");
            tracksToPlay.Add(3);
        }

        UpdateTrackPlayback(tracksToPlay);

    }

    private void UpdateTrackPlayback(HashSet<int> desiredTracks)
    {
        foreach (int trackIndex in activeTracks)
        {
            if (!desiredTracks.Contains(trackIndex))
            {
                if (trackSources[trackIndex].isPlaying)
                    trackSources[trackIndex].Stop();
            }
        }

        foreach (int trackIndex in desiredTracks)
        {
            if (!trackSources[trackIndex].isPlaying)
                trackSources[trackIndex].Play();
        }

        activeTracks = desiredTracks;
    }
}
