using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int gridWidth;
    public int gridLength;
    public float tileSize;
    public float padding;
    public GameObject gridTile;
    public GameObject gridParent;
    public GameObject steamCore;
    public Dictionary<(int, int), GridTile> gridMap;

    void Start()
    {
        gridMap = new Dictionary<(int, int), GridTile>();
        Vector3 gridCorner = gridParent.transform.position;
        Vector3 offSet = gridCorner - new Vector3(tileSize * gridWidth / 2, 0, tileSize * gridLength / 2);
        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridLength; z++)
            {
                // Set the position of this tile on the grid
                Vector3 newPosition = new Vector3(x * tileSize + gridCorner.x, gridCorner.y, z * tileSize + gridCorner.z) + offSet;
                // Instantiate the new tile and add it to the list
                GameObject newTile = Instantiate(gridTile, newPosition, Quaternion.identity, gridParent.transform);
                GridTile newTileComponent = newTile.GetComponent<GridTile>();
                Vector3 size = newTile.GetComponent<Renderer>().bounds.size;
                newTile.transform.localScale = ((tileSize - padding)) * new Vector3(1 / size.x, 1, 1 / size.z);
                newTileComponent.gridPos = new Vector2(x, z);
                gridMap.Add((x, z), newTileComponent);
            }
        }
    }
}
