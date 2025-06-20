using System;
using System.Collections.Generic;
using UnityEngine;

public class TileHandler : MonoBehaviour
{
    [Header("Neighbors")]
    public Vector2Int GridPosition { get; set; }
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private List<TileHandler> neighbors = new List<TileHandler>();
    public bool hasPuzzlePiece = true;

    private TileHandler movedToTile;
    
    
    private void Awake()
    {
        if(this.transform.GetChild(0).gameObject.activeSelf)
        {
            hasPuzzlePiece = true;
        }
        else
        {
            hasPuzzlePiece = false;
        }
    }

    public void AddNeighbor(TileHandler neighbor)
    {
        neighbors.Add(neighbor);
    }

    public void MoveToEmptyTile()
    {
        foreach (TileHandler neighbor in neighbors)
        {
            if (!neighbor.hasPuzzlePiece)
            {
                Transform puzzlePiece = this.transform.GetChild(0);
                
                neighbor.hasPuzzlePiece = true;
                hasPuzzlePiece = false;
                
                StartCoroutine(SmoothMove(puzzlePiece, neighbor.transform));
                
                break;
            }
        }
    }

    private System.Collections.IEnumerator SmoothMove(Transform puzzlePiece, Transform targetTransform)
    {
        Vector3 startPosition = puzzlePiece.position;
        Vector3 endPosition = targetTransform.position;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            puzzlePiece.position = Vector3.Lerp(startPosition, endPosition, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed;
            yield return null;
        }

        puzzlePiece.position = endPosition;
        puzzlePiece.SetParent(targetTransform);
        hasPuzzlePiece = false;
    }
    
    public void VerifyPuzzlePieceState()
    {
        if (this.transform.childCount > 0 && this.transform.GetChild(0).gameObject.activeSelf)
        {
            hasPuzzlePiece = true;
        }
        else
        {
            hasPuzzlePiece = false;
        }
    }
}
    