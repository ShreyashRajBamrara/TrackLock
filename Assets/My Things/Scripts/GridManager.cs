using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] public Vector2Int gridSize;
    [SerializeField] int unityGridSize = 10;
    
    public int UnityGridSize => unityGridSize;
    public Dictionary<Vector2Int, Node> Grid => grid;

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    void Awake() => CreateGrid();

    public Node GetNode(Vector2Int coordinates)
    {
        return grid.ContainsKey(coordinates) ? grid[coordinates] : null;
    }

    public void ResetNodes()
    {
        foreach (var node in grid.Values)
        {
            node.ResetPathfindingData();
        }
    }

    public void ToggleBlock(Vector2Int coordinates, bool shouldBlock)
    {
        if (grid.ContainsKey(coordinates))
        {
            grid[coordinates].isBlocked = shouldBlock;
            grid[coordinates].isTrack = !shouldBlock; // Blocked nodes can't be tracks
        }
    }

    public void lever_node(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            grid[coordinates].isLever = true;
            grid[coordinates].isTrack = false;
            grid[coordinates].isPath = false;
            grid[coordinates].isBlocked = false;
        }
    }

    public void Track_node(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            grid[coordinates].isTrack = true;
            grid[coordinates].isLever = false;
            grid[coordinates].isBlocked = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int
        {
            x = Mathf.RoundToInt(position.x / unityGridSize),
            y = Mathf.RoundToInt(position.z / unityGridSize)
        };
        
        coordinates.x = Mathf.Clamp(coordinates.x, 0, gridSize.x - 1);
        coordinates.y = Mathf.Clamp(coordinates.y, 0, gridSize.y - 1);
        
        return coordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        return new Vector3
        {
            x = coordinates.x * unityGridSize,
            z = coordinates.y * unityGridSize
        };
    }

    void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                grid.Add(coordinates, new Node(coordinates, true));
            }
        }
        
        Debug.Log($"Grid created with size {gridSize}, containing {grid.Count} nodes");
    }
}