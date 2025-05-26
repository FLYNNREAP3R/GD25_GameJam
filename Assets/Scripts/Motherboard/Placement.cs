using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Placement : MonoBehaviour
{
    private Tilemap tilemap;
    [Header("Prefab Turret")]
    public GameObject tower;
    [Header("Layer Colocation")]
    public LayerMask placementLayer;
    [Header("Grid")]
    public Grid grid;
    private Camera cam;
    private Dictionary<Vector3Int, GameObject> placedTurrets = new();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        cam = Camera.main;
    }
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            TryPlaceTower();
        }
    }
    void TryPlaceTower()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorldPos.z = 0f;

        Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);

        if (!tilemap.HasTile(cellPosition))
            return;

        if (placedTurrets.ContainsKey(cellPosition))
        {
            Debug.Log("A turret is already in this cell.");
            return;
        }

        Vector3 placePosition = tilemap.GetCellCenterWorld(cellPosition);
        TurretsSO towerToBuild = BuildManager.main.GetSelectedTower();
        if (towerToBuild.cost > GameManager.instance.money)
        {
            Debug.Log("You do not have enough money!");
            return;
        }

        GameManager.instance.SpendMoney(towerToBuild.cost);

        tower = Instantiate(towerToBuild.prefab, placePosition, Quaternion.identity);
        placedTurrets[cellPosition] = tower;

        //Debug.Log($"Turret in {cellPosition}");
    }
}

