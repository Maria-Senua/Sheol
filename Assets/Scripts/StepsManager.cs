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
    
    private string currentSurfaceTag = "Stone"; // Default
    private string lastSurface = "";
    
    // RAYCAST SETTINGS
    [Header("Raycast Settings")]
    [Tooltip("How far down to check for the floor. Increase if your player floats.")]
    public float raycastDistance = 2.0f; 
    // It's highly recommended to assign a LayerMask so the raycast ignores the player's own collider
    public LayerMask groundLayerMask = ~0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (trackedTransform == null) trackedTransform = Camera.main.transform;
        previousTrackedPosition = trackedTransform.position;
        isMoving = false;
        movementCheckTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovementStatus();
        
        if (isMoving)
        {
            CheckSurfaceBelow();
            
            if (lastSurface != currentSurfaceTag)
            {
                stepAudioSource.clip = GetClipBySurface(currentSurfaceTag);
                lastSurface = currentSurfaceTag;
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
            isMoving = distanceMoved > movementThreshold;
            
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

    // --- NEW RAYCAST LOGIC ---
    private void CheckSurfaceBelow()
    {
        // Start the ray slightly above the tracked transform
        Vector3 rayStart = trackedTransform.position + (Vector3.up * 0.5f);
        
        Debug.DrawRay(rayStart, Vector3.down * raycastDistance, Color.red);

        // RaycastAll shoots THROUGH everything and returns an array of all colliders it touched
        RaycastHit[] hits = Physics.RaycastAll(rayStart, Vector3.down, raycastDistance);

        bool foundGround = false;

        // Check every single thing the laser passed through
        foreach (RaycastHit hit in hits)
        {
            string hitTag = hit.collider.tag;

            // Optional: Print what we are hitting in the console to help you debug
            // Debug.Log("Laser passed through: " + hit.collider.gameObject.name);

            if (hitTag == "Fallen")
            {
                onFallenDown?.Invoke();
                return; // Stop immediately if we hit the death zone
            }

            // If we find our ground tags, update the audio and stop looking
            if (hitTag == "Grass" || hitTag == "Sand" || hitTag == "Floor")
            {
                if (currentSurfaceTag != hitTag)
                {
                    currentSurfaceTag = hitTag;
                    PlayDioramaFromTag(hitTag);
                    Debug.Log("Raycast successfully pierced through and found surface: " + hitTag + " on object: " + hit.collider.gameObject.name);
                }
                foundGround = true;
                break; // Ground found, break out of the loop
            }
        }

        // If the laser went through everything and never found a valid ground tag
        if (!foundGround)
        {
            if (currentSurfaceTag != "Stone")
            {
                currentSurfaceTag = "Stone";
                MusicController.instance.StopDioramaMusic();
            }
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

    private void PlayDioramaFromTag(string tag)
    {
        switch (tag)
        {
            case "Floor": MusicController.instance.PlayDioramaMusic(0); break;
            case "Sand": MusicController.instance.PlayDioramaMusic(1); break;
            case "Grass": MusicController.instance.PlayDioramaMusic(2); break;
        }
    }
}
