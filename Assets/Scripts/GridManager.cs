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
    private GridTile _selectedTile;
    public GridTile selectedTile
    {
        get
        {
            return _selectedTile;
        }
        set
        {
            if (_selectedTile != null)
            {
                _selectedTile.isSelected = false;
                _selectedTile.GetComponent<Renderer>().material = _selectedTile.defaultMaterial;
            }
            this._selectedTile = value;
            if (_selectedTile != null)
            {
                _selectedTile.isSelected = true;
                _selectedTile.GetComponent<Renderer>().material = _selectedTile.hoverMaterial;
            }
        }
    }

    public void PlaceTurret(GameObject turret)
    {
        if (selectedTile == null || GameManager.Instance.turretCost > GameManager.Instance.currency)
        {
            return;
        }
        GameObject newTurret = Instantiate(turret, selectedTile.transform.position + selectedTile.turretOffset, Quaternion.identity, selectedTile.transform.parent);
        selectedTile.turret = newTurret;
        selectedTile.isOccupied = true;
        selectedTile.GetComponent<Renderer>().enabled = false;
        Physics.IgnoreCollision(selectedTile.GetComponent<Collider>(), turret.GetComponentInChildren<Collider>());
        GameManager.Instance.currency -= GameManager.Instance.turretCost;
    }

    void Start()
    {
        gridMap = new Dictionary<(int, int), GridTile>();
        if (gridParent.transform.childCount > 0)
        {
            foreach (GridTile t in gridParent.transform.GetComponentsInChildren<GridTile>())
            {
                gridMap.Add(((int)t.gridPos.x, (int)t.gridPos.y), t);
                if (t.turret)
                {
                    t.turret.transform.position = t.transform.position + t.turretOffset;
                    Physics.IgnoreCollision(t.GetComponent<Collider>(), t.turret.GetComponentInChildren<Collider>());
                }
            }
            return;
        }
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
                newTile.transform.localScale = new Vector3((tileSize - padding) / size.x, 1, (tileSize - padding) / size.z);
                newTileComponent.gridPos = new Vector2(x, z);
                gridMap.Add((x, z), newTileComponent);
            }
        }
    }
}
