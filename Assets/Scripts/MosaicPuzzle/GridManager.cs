using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private int width = 3;
    [SerializeField] private int height = 3;
    [SerializeField] private float offset = 1;
    [SerializeField] private GameObject tilePrefab;

    private TileHandler[,] tileGrid;
    private GameObject tileObject;
    
    private void Start()
    {
        GenerateGrid();
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

    }

    private void AssignNeighbors()
    {
        for (int z = 0; z < width; z++)
        {
            for (int y = 0; y < height; y++)
            {
                TileHandler currentTile = tileGrid[z, y];

                if (z > 0) currentTile.AddNeighbor(tileGrid[z - 1, y]);
                if (z < width - 1) currentTile.AddNeighbor(tileGrid[z + 1, y]);
                if (y > 0) currentTile.AddNeighbor(tileGrid[z, y - 1]);
                if (y < height - 1) currentTile.AddNeighbor(tileGrid[z, y + 1]);
            }
        }
    }
}