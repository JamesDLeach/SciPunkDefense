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
        GameObject tempTile = Instantiate(floorTile, transform.position, transform.rotation, transform);
        Vector3 size = tempTile.GetComponent<Renderer>().bounds.size;
        tempTile.transform.localScale = ((GameManager.GridManager.tileSize)) * new Vector3(1 / size.x, 0, 1 / size.z);
        //tempTile.transform.position = transform.position + new Vector3(0, -1, 0); //Have to reset position after scaling
    }

    private void Start()
    {
        SetTurretPos();
        isHovered = false;
        _renderer = GetComponent<Renderer>();
        defaultMaterial = defaultMaterial ?? _renderer.material;
        InstantiateFloorTile();
    }

    private void OnMouseEnter()
    {
        isHovered = true;
        _renderer.material = hoverMaterial;
    }

    private void OnMouseExit()
    {
        isHovered = false;
        _renderer.material = defaultMaterial;
    }
}
