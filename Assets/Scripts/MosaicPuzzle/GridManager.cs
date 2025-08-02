using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

[System.Serializable]
public class TileAssignment
{
    public GameObject tile;
    public GameObject assignedObject;
}

public class GridManager : MonoBehaviour
{
    public static GridManager instance;
    
    [SerializeField] private TileHandler[] tileMap;
    
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private bool ispuzzleInitated = false;
        
    private TileHandler[,] tileGrid;
    private GameObject tileObject;
    
    [Header("Corrected Grid Settings")]
    public List<TileAssignment> correctOrientation = new List<TileAssignment>();
    private Dictionary<GameObject, GameObject> currentAssignments = new Dictionary<GameObject, GameObject>();

    public UnityEvent onBoxOpening;
    public UnityEvent onBoxClosed;

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

    private void Start()
    {
        foreach (var correctPair in correctOrientation)
        {
            Transform child = correctPair.tile.transform.childCount > 0 ? 
                correctPair.tile.transform.GetChild(0) : null;
        
            currentAssignments.Add(correctPair.tile, child != null ? child.gameObject : null);
        }

        onBoxClosed?.Invoke();
        // GenerateGrid();
        AssignNeighbors();
    }
    
    void Update()
    {
        DetectTiles();
    }
    
    private void AssignNeighbors()
    {
        int gridSize = Mathf.RoundToInt(Mathf.Sqrt(tileMap.Length));

        for (int i = 0; i < tileMap.Length; i++)
        {
            TileHandler currentTile = tileMap[i];
            int x = i % gridSize;
            int y = i / gridSize;

            if (x > 0) currentTile.AddNeighbor(tileMap[i - 1]); 
            if (x < gridSize - 1) currentTile.AddNeighbor(tileMap[i + 1]);
            if (y > 0) currentTile.AddNeighbor(tileMap[i - gridSize]);
            if (y < gridSize - 1) currentTile.AddNeighbor(tileMap[i + gridSize]);
        }
    }
    
    public void UpdateAssignment(GameObject tile, GameObject assignedObject)
    {
        if (assignedObject == null)
        {
            currentAssignments.Remove(tile);
        }
        else
        {
            if (currentAssignments.ContainsKey(tile))
            {
                currentAssignments[tile] = assignedObject;
            }
            else
            {
                currentAssignments.Add(tile, assignedObject);
            }
        }
    
        CheckOrientation();
    }
    
    private void CheckOrientation()
    {
        foreach (var correctPair in correctOrientation)
        {
            bool hasAssignment = currentAssignments.TryGetValue(correctPair.tile, out var currentObj);
            
            if (correctPair.assignedObject == null)
            {
                if (hasAssignment)
                {
                    return;
                }
                continue;
            }
            
            if (!hasAssignment)
            {
                return;
            }
            
            if (currentObj != correctPair.assignedObject)
            {
                return;
            }
        }

        PuzzleSolved();
    }

    private void PuzzleSolved()
    {
        if (boxPrefab != null && ispuzzleInitated)
        {
            StartCoroutine(RotateBoxAfterDelay(2f));
        }
    }
    
    private IEnumerator RotateBoxAfterDelay(float duration)
    {
        Quaternion startRotation = boxPrefab.transform.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(180f, 0f, 0f);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            boxPrefab.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        boxPrefab.transform.rotation = targetRotation;
        //trigger pager drag
        onBoxOpening?.Invoke();
 
    }

    private void DetectTiles()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Tile"))
            {
                meshRenderer.enabled = true;
                ispuzzleInitated = true;
            }
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
