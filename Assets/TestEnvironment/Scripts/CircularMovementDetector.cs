using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class CircularMovementDetector : MonoBehaviour
{
    public Transform centerPoint;   
    public Transform trackedTransform;
    public GameObject subscenes;
    public GameObject[] dioramas;
    public float speedMultiplier = 0.3f;
    public float distance;

    private Vector3 lastPosition;
    private int direction = 0;
    private bool isInDiorama = false;
    private float playerSpeed;

    private Vector3 previousTrackedPosition;
    private float movementCheckInterval = 0.1f;
    private float movementThreshold = 0.01f;
    private bool isMoving = false;
    private float movementCheckTimer = 0f;
    private List<string> currentSurfaces = new List<string>();
    private string lastSurface = "";

    [SerializeField] private AudioSource stepAudioSource;
    [SerializeField] private AudioClip[] grassStep;
    [SerializeField] private AudioClip[] sandStep;
    [SerializeField] private AudioClip[] floorStep;
    [SerializeField] private AudioClip[] stoneStep;
    [SerializeField] private AudioClip[] boatStep;



    //private bool isNearDiorama = false;

    //public UnityEvent onSpiralConnectorEnter;
    //public UnityEvent onSpiralConnectorExit;

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

        if (!isInDiorama) MoveSpiral();

        ManageDiorama();

        if (isMoving)
        {
            string currentSurface = GetCurrentSurface();

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

            previousTrackedPosition = trackedTransform.position;
            movementCheckTimer = movementCheckInterval;
        }
    }

 



    private void MoveSpiral()
    {
        Vector3 currentPosition = trackedTransform.position;
        float playerSpeed = Vector3.Distance(currentPosition, lastPosition) / Time.deltaTime;

        Vector3 toLast = lastPosition - centerPoint.position;
        Vector3 toCurrent = currentPosition - centerPoint.position;

        Vector2 last2D = new Vector2(toLast.x, toLast.z);
        Vector2 current2D = new Vector2(toCurrent.x, toCurrent.z);

        float cross = last2D.x * current2D.y - last2D.y * current2D.x;

        Debug.DrawLine(centerPoint.position, trackedTransform.position, Color.green);


        if (Mathf.Abs(cross) > 0.001f)
        {
            if (cross > 0)
            {
                direction = -1;
                Debug.Log("Counterclockwise");
            }
            else
            {
                direction = 1;
                Debug.Log("Clockwise");
            }
        }
        else
        {
            direction = 0;
        }

        if (direction != 0)
        {
            float deltaY = direction * playerSpeed * speedMultiplier * Time.deltaTime;

          
                subscenes.transform.position += new Vector3(0, deltaY, 0);
            
        }


        lastPosition = currentPosition;
    }

    private void ManageDiorama()
    {
        foreach (GameObject diorama in dioramas)
        {
            if (Vector3.Distance(diorama.transform.position, trackedTransform.position) < distance)
            {
                diorama.SetActive(true);
            } else
            {
                diorama.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Entrance"))
        {
            Debug.Log("Entered Diorama");
           
            isInDiorama = true;
            
        }

        if (other.CompareTag("Grass") || other.CompareTag("Sand") || other.CompareTag("Floor"))
        {
            if (!currentSurfaces.Contains(other.tag))
                currentSurfaces.Add(other.tag);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Entrance"))
        {
            isInDiorama = true;
            
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Entrance"))
        {
            Debug.Log("Exited Diorama");

            isInDiorama = false;

        }
        if (currentSurfaces.Contains(other.tag))
        {
            currentSurfaces.Remove(other.tag);
        }
    }
}
