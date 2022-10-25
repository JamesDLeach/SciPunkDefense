using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    public Vector2 gridPos; // position of this tile on the grid
    public GameObject turret;
    public GameObject floorTile;
    public Vector3 offset;
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
        turret.transform.position = gameObject.transform.position + offset;
    }

    private void Start()
    {
        SetTurretPos();
        isHovered = false;
        _renderer = GetComponent<Renderer>();
        if (!defaultMaterial)
        {
            defaultMaterial = _renderer.material;
        }
        if(floorTile)
        {
            GameObject tempTile = Instantiate(floorTile, new Vector3(0, 0, 0), gameObject.transform.rotation);
            Vector3 gScale = gameObject.transform.localScale;
            gScale.y += 9;
            tempTile.transform.localScale = gScale;
            tempTile.transform.position = gameObject.transform.position + new Vector3(0, -1, 0); //Have to reset position after scaling
        }
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
