using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{

    [SerializeField] private TileHandler[] tileMap;
    
    [Header("Grid Settings(Depricated)")]
    [SerializeField] private int width = 3;
    [SerializeField] private int height = 3;
    [SerializeField] private float offset = 1;
    [SerializeField] private GameObject tilePrefab;

    private TileHandler[,] tileGrid;
    private GameObject tileObject;
    
    private void Start()
    {
        // GenerateGrid();
        AssignNeighbors();
    }

    private void GenerateGrid()
    {
        tileGrid = new TileHandler[width, height];
        Vector3 startPosition = transform.position;

        for (int z = 0; z < width; z++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 tilePosition = startPosition +
                                       offset * transform.up * y +
                                       offset * transform.right * z;

                tileObject = Instantiate(tilePrefab, tilePosition, Quaternion.identity, transform);
                TileHandler tileHandler = tileObject.GetComponent<TileHandler>();
                tileHandler.GridPosition = new Vector2Int(z, y);
                tileGrid[z, y] = tileHandler;
            }
        }
        
        
        Destroy(tileObject.transform.GetChild(0).gameObject);
        tileObject.GetComponent<TileHandler>().hasPuzzlePiece = false;
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
}