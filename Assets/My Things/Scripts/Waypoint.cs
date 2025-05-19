using UnityEngine;
using System.Collections.Generic;

public class Waypoint : MonoBehaviour
{
    [Header("Node Types")]
    [SerializeField] private bool isTrack;
    [SerializeField] private bool isLever;
    
    [Header("Linked Nodes to Toggle")]
    [SerializeField] private List<Waypoint> nodes_to_block;

    [Header("State")]
    [SerializeField] private bool isBlocked = false;
    private bool isBlocking = false;

    public bool IsRailwayTrack => isTrack;
    public bool IsLever => isLever;
    public bool IsBlocked => isBlocked;

    private Vector2Int coordinates;
    private GridManager grid_manager;
    private Pathfinder pathfinder;

    void Awake()
    {
        grid_manager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    void Start()
    {
        coordinates = grid_manager.GetCoordinatesFromPosition(transform.position);

        if (isTrack)
        {
            grid_manager.Track_node(coordinates);
        }

        if (isLever)
        {
            grid_manager.lever_node(coordinates);
        }
    }

    void OnMouseDown()
    {
        if (isTrack)
        {
            Debug.Log($"Clicked railway track: {transform.name}");
        }

        if (isLever)
        {
            Debug.Log($"Clicked lever: {transform.name}");
            isBlocking = !isBlocking;

            foreach (Waypoint node in nodes_to_block)
            {
                node.SetBlocked(isBlocking);
                Debug.Log($"{(isBlocking ? "Blocked" : "Unblocked")} node: {node.transform.name}");
            }

            // Recalculate path when lever is toggled
            if (pathfinder != null)
            {
                pathfinder.RecalculatePath();
            }
        }
    }

    public void SetBlocked(bool block)
    {
        isBlocked = block;

        if (grid_manager != null)
        {
            grid_manager.ToggleBlock(coordinates, isBlocked);
        }
    }

    void OnValidate()
    {
        if (isLever && isTrack)
        {
            Debug.LogWarning($"'{name}' can't be both Lever and Track. Defaulting to Track.");
            isLever = false;
        }
    }
}