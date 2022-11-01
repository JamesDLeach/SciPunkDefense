using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    public Vector2 gridPos; // position of this tile on the grid
    public GameObject turret;
    public GameObject floorTile;
    public Vector3 turretOffset;
    public Vector3 floorOffset;
    public bool isHovered;
    public bool isSelected;
    public bool isOccupied; //Used to check if there is a building in grid tile
    public Material defaultMaterial;
    public Material hoverMaterial;
    private Renderer _renderer;

    private void SetTurretPos()
    {
        if (turret == null)
        {
            return;
        }
        turret.transform.position = gameObject.transform.position + turretOffset;
    }

    private void InstantiateFloorTile()
    {
        if (floorTile == null)
        {
            return;
        }
        GameObject tempTile = Instantiate(floorTile, transform.position + floorOffset, transform.rotation, transform.parent);
        Vector3 size = _renderer.bounds.size;
        //Think scale should be tile size + 2 * padding
        tempTile.transform.localScale = new Vector3(5 / size.x, 1, 5 / size.z);
    }

    private void Start()
    {
        SetTurretPos();
        isHovered = false;
        isSelected = false;
        _renderer = GetComponent<Renderer>();
        defaultMaterial = defaultMaterial ?? _renderer.material;

        _renderer.enabled = !isOccupied;
        InstantiateFloorTile();
    }

    private void UpdateMaterial(Material material)
    {
        if (isSelected)
        {
            return;
        }
        _renderer.material = material;
    }

    private void OnMouseEnter()
    {
        isHovered = true;
        UpdateMaterial(hoverMaterial);
    }

    private void OnMouseExit()
    {
        isHovered = false;
        UpdateMaterial(defaultMaterial);
    }

    private void OnMouseDown()
    {
        if (isSelected || isOccupied)
        {
            GameManager.GridManager.selectedTile = null;
        }
        else
        {
            GameManager.GridManager.selectedTile = this;
        }
    }
}
