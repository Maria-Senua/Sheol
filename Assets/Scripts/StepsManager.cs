using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;
using System.Collections.Generic;

public class StepsManager : MonoBehaviour
{
    public Transform trackedTransform;
    
    [SerializeField] private AudioSource stepAudioSource;
    [SerializeField] private AudioClip[] grassStep;
    [SerializeField] private AudioClip[] sandStep;
    [SerializeField] private AudioClip[] floorStep;
    [SerializeField] private AudioClip[] stoneStep;
    [SerializeField] private AudioClip[] boatStep;
    
    private Vector3 lastPosition;
    private Vector3 previousTrackedPosition;
    private bool isMoving = false;
    private float movementCheckTimer = 0f;
    private float movementCheckInterval = 0.1f;
    private float movementThreshold = 0.01f;
    
    private float inactivityTimer = 0f;
    private float inactivityThreshold = 120f; 
    private bool inactivityEventTriggered = false;

    public UnityEvent onPlayerInactive;
    public UnityEvent onFallenDown;
    
    private List<string> currentSurfaces = new List<string>();
    private string lastSurface = "";
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (trackedTransform == null) trackedTransform = Camera.main.transform; ;
        lastPosition = trackedTransform.position;
        previousTrackedPosition = trackedTransform.position;

        currentSurfaces.Clear();
        isMoving = false;
        movementCheckTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovementStatus();
        
        if (isMoving)
        {
            string currentSurface = GetCurrentSurface();
            Debug.Log("Current surface: " + currentSurface);

            if (lastSurface != currentSurface)
            {
                stepAudioSource.clip = GetClipBySurface(currentSurface);
                lastSurface = currentSurface;
                stepAudioSource.Play();
            }
            else if (!stepAudioSource.isPlaying)
            {
                stepAudioSource.Play();
            }
        }
        else
        {
            if (stepAudioSource.isPlaying)
                stepAudioSource.Stop();

            lastSurface = "";
        }
    }
    
    private void UpdateMovementStatus()
    {
        movementCheckTimer -= Time.deltaTime;
        if (movementCheckTimer <= 0f)
        {
            float distanceMoved = Vector3.Distance(trackedTransform.position, previousTrackedPosition);

            if (distanceMoved > movementThreshold)
            {
                
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
            
            if (isMoving)
            {
                inactivityTimer = 0f;
                inactivityEventTriggered = false;
            }
            else
            {
                inactivityTimer += movementCheckInterval;

                if (!inactivityEventTriggered && inactivityTimer >= inactivityThreshold)
                {
                    inactivityEventTriggered = true;
                    onPlayerInactive?.Invoke();
                }
            }

            previousTrackedPosition = trackedTransform.position;
            movementCheckTimer = movementCheckInterval;
        }
    }
    
    private AudioClip GetClipBySurface(string surface)
    {
        switch (surface)
        {
            case "Grass": return GetRandomClip(grassStep);
            case "Sand": return GetRandomClip(sandStep);
            case "Floor": return GetRandomClip(floorStep);
            case "Boat": return GetRandomClip(boatStep);
            case "Stone":
            default: return GetRandomClip(stoneStep);
        }
    }

    private AudioClip GetRandomClip(AudioClip[] clips)
    {
        if (clips == null || clips.Length == 0) return null;
        return clips[Random.Range(0, clips.Length)];
    }

    string GetCurrentSurface()
    {
        if (currentSurfaces.Contains("Grass"))
            return "Grass";
        if (currentSurfaces.Contains("Sand"))
            return "Sand";
        if (currentSurfaces.Contains("Floor"))
            return "Floor";

        return "Stone";
    }
    
    private void PlayDioramaFromTag(string tag)
    {
        switch (tag)
        {
            case "Floor":
                MusicController.instance.PlayDioramaMusic(0);
                break;
            case "Sand":
                MusicController.instance.PlayDioramaMusic(1);
                break;
            case "Grass":
                MusicController.instance.PlayDioramaMusic(2);
                break;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Fallen"))
        {
            Debug.Log("Has fallen");
            onFallenDown?.Invoke();
        }
        if (other.gameObject.CompareTag("Grass") || other.gameObject.CompareTag("Sand") || other.gameObject.CompareTag("Floor"))
        {
            if (!currentSurfaces.Contains(other.gameObject.tag)) currentSurfaces.Add(other.gameObject.tag);
            
            PlayDioramaFromTag(other.gameObject.tag);
            
            Debug.Log("SOUND DIORAMA " + other.gameObject.tag + " " + other.gameObject.name);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Grass") || other.gameObject.CompareTag("Sand") || other.gameObject.CompareTag("Floor"))
        {
            if (!currentSurfaces.Contains(other.gameObject.tag)) currentSurfaces.Add(other.gameObject.tag);
            
            PlayDioramaFromTag(other.gameObject.tag);
            
            Debug.Log("SOUND DIORAMA " + other.gameObject.tag + " " + other.gameObject.name);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (currentSurfaces.Contains(other.gameObject.tag))
        {
            currentSurfaces.Remove(other.gameObject.tag);
            MusicController.instance.StopDioramaMusic();
        }
    }
}
