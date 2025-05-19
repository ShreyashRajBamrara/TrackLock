using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Node
{
    public Vector2Int coordinates;
    public bool isTrack;
    public bool isPath;
    public bool isBlocked;
    public bool isLever;
    public bool isExplored;
    public Node connectedTo;

    public Node(Vector2Int coordinates, bool isTrack)
    {
        this.coordinates = coordinates;
        this.isTrack = isTrack;
        // Initialize all other states to false
        isPath = false;
        isBlocked = false;
        isLever = false;
        isExplored = false;
        connectedTo = null;
    }

    // Helper method to reset pathfinding data
    public void ResetPathfindingData()
    {
        isPath = false;
        isExplored = false;
        connectedTo = null;
    }
}