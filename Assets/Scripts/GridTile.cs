using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    public Vector2 gridPos; // position of this tile on the grid
    public GameObject turret;
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
