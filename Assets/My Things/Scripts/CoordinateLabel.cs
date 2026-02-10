using UnityEngine;
using TMPro;

[ExecuteAlways]
public class TileTypeLabel : MonoBehaviour
{
    [Header("Color Settings")]
    [SerializeField] Color blockedColor = Color.red;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, 0.5f, 0f); // Orange
    [SerializeField] Color leverColor = Color.blue;
    [SerializeField] Color trackColor = Color.green;
    [SerializeField] Color defaultColor = Color.white;

    [Header("Visibility")]
    [SerializeField] private KeyCode toggleKey = KeyCode.C;
    private bool showCoordinates = false;

    private TextMeshPro label;
    private GridManager gridManager;
    private Pathfinder pathfinder;
    private Vector2Int coordinates;

    void Awake()
    {
        label = GetComponent<TextMeshPro>();
        if (label == null)
        {
            Debug.LogError("TextMeshPro component not found ");
            return;
        }

        InitializeReferences();
        UpdateVisuals();
    }

    void Update()
    {
        if (Application.isPlaying && Input.GetKeyDown(toggleKey))
        {
            showCoordinates = !showCoordinates;
            UpdateVisuals();
        }

        if (!Application.isPlaying || showCoordinates)
        {
            UpdateCoordinates();
            UpdateTileColor();
            UpdateTileName();
        }
    }

    void InitializeReferences()
    {
        if (gridManager == null)
        {
            gridManager = FindObjectOfType<GridManager>();
        }
        if (pathfinder == null)
        {
            pathfinder = FindObjectOfType<Pathfinder>();
        }
    }

    void UpdateCoordinates()
    {
        if (gridManager == null) return;

        coordinates = new Vector2Int(
            Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize),
            Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize)
        );
    }

    void UpdateVisuals()
    {
        label.enabled = showCoordinates;
        label.text = showCoordinates ? $"({coordinates.x},{coordinates.y})" : "";
    }

    void UpdateTileName()
    {
        transform.parent.name = $"Tile {coordinates}";
    }

    void UpdateTileColor()
    {
        if (gridManager == null || gridManager.Grid == null) return;

        Node node = gridManager.GetNode(coordinates);
        if (node == null) return;

        if (node.isBlocked) label.color = blockedColor;
        else if (node.isLever) label.color = leverColor;
        else if (node.isPath && pathfinder != null && pathfinder.GetCurrentPath().Contains(node)) label.color = pathColor;
        else if (node.isExplored) label.color = exploredColor;
        else if (node.isTrack) label.color = trackColor;
        else label.color = defaultColor;
    }
}