using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CoordinateLabel : MonoBehaviour
{
    [Header("Color Settings")]
    [SerializeField] Color blockedColor = Color.red;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, 0.5f, 0f); // Orange
    [SerializeField] Color leverColor = Color.blue;
    [SerializeField] Color trackColor = Color.green;
    [SerializeField] Color defaultColor = Color.white;

    [Header("Visibility")]
    [SerializeField] private bool isVisible = true;
    [SerializeField] private KeyCode toggleKey = KeyCode.C;

    private TextMeshPro label;
    private Vector2Int coordinates;
    private GridManager gridManager;
    private Pathfinder pathfinder;

    void Awake()
    {
        label = GetComponent<TextMeshPro>();
        if (label == null)
        {
            Debug.LogError("TextMeshPro component not found on CoordinateLabel");
            return;
        }

        label.enabled = isVisible;
        InitializeReferences();
        DisplayCoordinates();
    }

    void Update()
    {
        if (Application.isPlaying && Input.GetKeyDown(toggleKey))
        {
            ToggleVisibility();
        }

        // Only update these in Editor or if visible
        if (!Application.isPlaying || isVisible)
        {
            DisplayCoordinates();
            UpdateTileName();
            ColorCoordinates();
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

    void ToggleVisibility()
    {
        isVisible = !isVisible;
        label.enabled = isVisible;
    }

    void DisplayCoordinates()
    {
        if (gridManager == null) return;

        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);
        label.text = $"({coordinates.x},{coordinates.y})";
    }

    void UpdateTileName()
    {
        transform.parent.name = $"Tile {coordinates}";
    }

    void ColorCoordinates()
    {
        if (gridManager == null || gridManager.Grid == null)
        {
            InitializeReferences();
            if (gridManager == null || gridManager.Grid == null) return;
        }

        Node node = gridManager.GetNode(coordinates);
        if (node == null) return;

        // Priority order for coloring
        if (node.isBlocked)
        {
            label.color = blockedColor;
        }
        else if (node.isLever)
        {
            label.color = leverColor;
        }
        else if (node.isPath && pathfinder != null && pathfinder.GetCurrentPath().Contains(node))
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else if (node.isTrack)
        {
            label.color = trackColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }
}