using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController instance;

    public AudioClip[] musicList;
    public AudioClip[] dioramasMusic;

    public GameObject topLimit;
    public GameObject bottomLimit;

    private float spiraleDistance;
    public Transform environmentRoot; 
    private float initialEnvY;


    private AudioSource track2Source;
    private Dictionary<int, AudioSource> trackSources = new Dictionary<int, AudioSource>();

    private Dictionary<int, float> trackTargetVolumes = new Dictionary<int, float>();
    private float fadeSpeed = 1f;

    private AudioSource dioramaSource;
    private bool playingDioramaMusic = false;


    private void Awake()
    {
        instance = this;

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

        foreach (var kvp in trackSources)
        {
            kvp.Value.volume = 0f;
            trackTargetVolumes[kvp.Key] = 0f;
        }
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
        if (playingDioramaMusic) return;

        float topY = topLimit.transform.position.y;
        float bottomY = bottomLimit.transform.position.y;
        float centerY = environmentRoot.position.y; 

        float spiraleDistance = Mathf.Abs(topY - bottomY); 
        float relativeY = topY - centerY;
        float ratio = relativeY / spiraleDistance;


        var keys = new List<int>(trackTargetVolumes.Keys);
        foreach (int key in keys)
        {
            trackTargetVolumes[key] = 0f;
        }


        if (ratio >= 0f && ratio <= 1f / 6f)
        {
            float t = Mathf.InverseLerp(1f / 6f, 0f, ratio);  
            trackTargetVolumes[0] = t;
            Debug.Log("TargetVolume 1/6 " + trackTargetVolumes[0]);
        }

        if (ratio >= 0f && ratio <= 2f / 6f)
        {
            float t = Mathf.InverseLerp(2f / 6f, 0f, ratio); 
            trackTargetVolumes[1] = t;
            Debug.Log("TargetVolume 2/6 " + trackTargetVolumes[1]);
        }

        if (ratio >= 4f / 6f && ratio <= 1f)
        {
            float t = Mathf.InverseLerp(4f / 6f, 1f, ratio); 
            trackTargetVolumes[3] = t;
        }

        if (ratio >= 5f / 6f && ratio <= 1f)
        {
            float t = Mathf.InverseLerp(5f / 6f, 1f, ratio);
            trackTargetVolumes[4] = t;
        }

        UpdateTrackPlayback();

    }

    private void UpdateTrackPlayback()
    {
        foreach (var kvp in trackSources)
        {
            int index = kvp.Key;
            AudioSource source = kvp.Value;
            float targetVolume = trackTargetVolumes[index];

            if (!source.isPlaying && targetVolume > 0f)
                source.Play();

            source.volume = Mathf.MoveTowards(source.volume, targetVolume, fadeSpeed * Time.deltaTime);

            if (source.isPlaying && source.volume <= 0.001f && targetVolume == 0f)
                source.Stop();
        }
    }

    public void PlayDioramaMusic(int trackNum)
    {
        StopSpiralMusic();

        if (dioramaSource == null)
            dioramaSource = gameObject.AddComponent<AudioSource>();

        dioramaSource.clip = dioramasMusic[trackNum];
        dioramaSource.loop = true;
        dioramaSource.volume = 1f;
        dioramaSource.Play();
        playingDioramaMusic = true;
    }

    public void StopDioramaMusic()
    {
        if (dioramaSource != null && dioramaSource.isPlaying)
            dioramaSource.Stop();
        
        playingDioramaMusic = false;
        if (track2Source != null && !track2Source.isPlaying)
        {
            track2Source.volume = 1f;
            track2Source.Play();
            Debug.Log("track2Source resumed");
        }
    }

    private void StopSpiralMusic()
    {
        foreach (var source in trackSources.Values)
        {
            source.Stop();
            source.volume = 0f;
            Debug.Log("stopspiralmusic");
        }
        if (track2Source != null)
        {
            track2Source.Stop();
            track2Source.volume = 0f;
            Debug.Log("track2Source stopped");
        }
    }
}
