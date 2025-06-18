using System;
using System.Collections.Generic;
using UnityEngine;

public class TileHandler : MonoBehaviour
{
    [Header("Neighbors")]
    public Vector2Int GridPosition { get; set; }
    [SerializeField] private List<TileHandler> neighbors = new List<TileHandler>();
    public bool hasPuzzlePiece = true;

    private TileHandler movedToTile;
    
    private void Start()
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
                puzzlePiece.SetParent(neighbor.transform);
                puzzlePiece.localPosition = Vector3.zero;

                neighbor.hasPuzzlePiece = true;
                hasPuzzlePiece = false; 

                break;
            }
        }
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
    