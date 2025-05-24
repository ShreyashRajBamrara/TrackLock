using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] public Vector2Int startCoordinates;
    [SerializeField] public Vector2Int destinationCoordinates;

    Node startNode;
    Node destinationNode;
    Node currentSearchNode;

    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
    readonly Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };

    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    List<Node> currentPath = new List<Node>();

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
        {
            grid = gridManager.Grid;
        }
    }

    void Start()
    {
        if (grid.ContainsKey(startCoordinates))
        {
            startNode = grid[startCoordinates];
        }

        if (grid.ContainsKey(destinationCoordinates))
        {
            destinationNode = grid[destinationCoordinates];
        }

        if (startNode == null || destinationNode == null)
        {
            Debug.LogError("Start or Destination node not found in grid");
            return;
        }

        RecalculatePath();
    }

    public void RecalculatePath()
    {
        gridManager.ResetNodes();
        BreadthFirstSearch();
        currentPath = BuildPath();
    }

    void BreadthFirstSearch()
    {
        bool isRunning = true;
        frontier.Clear();
        reached.Clear();

        frontier.Enqueue(startNode);
        reached.Add(startCoordinates, startNode);

        while (frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();

            if (currentSearchNode.coordinates == destinationCoordinates)
            {
                isRunning = false;
            }
        }
    }

    void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCoords = currentSearchNode.coordinates + direction;

            if (grid.ContainsKey(neighborCoords))
            {
                Node neighbor = grid[neighborCoords];

                if (neighbor.isTrack && !neighbor.isBlocked)
                {
                    neighbors.Add(neighbor);
                }
            }
        }

        foreach (Node neighbor in neighbors)
        {
            if (!reached.ContainsKey(neighbor.coordinates))
            {
                neighbor.connectedTo = currentSearchNode;
                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    public List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();

        // Clear previous path visuals
        foreach (var node in grid.Values)
        {
            node.isPath = false;
        }

        Node currentNode = destinationNode;

        while (currentNode != null)
        {
            path.Add(currentNode);
            currentNode.isPath = true;
            currentNode = currentNode.connectedTo;
        }

        path.Reverse();
        return path;
    }
   public List<Node> GetNewPathFrom(Vector2Int customStart)
{
    if (!grid.ContainsKey(customStart) || !grid.ContainsKey(destinationCoordinates))
    {
        Debug.LogWarning("Invalid start or destination coordinates.");
        return new List<Node>();
    }

    gridManager.ResetNodes();

    startCoordinates = customStart;
    startNode = grid[customStart];

    BreadthFirstSearch();

    return BuildPath();
}


    // Public methods to modify coordinates
    public void SetStartCoordinates(Vector2Int newCoordinates)
    {
        if (grid.ContainsKey(newCoordinates))
        {
            startCoordinates = newCoordinates;
            startNode = grid[newCoordinates];
            RecalculatePath();
        }
        else
        {
            Debug.LogWarning($"Coordinates {newCoordinates} not in grid");
        }
    }

    public void SetDestinationCoordinates(Vector2Int newCoordinates)
    {
        if (grid.ContainsKey(newCoordinates))
        {
            destinationCoordinates = newCoordinates;
            destinationNode = grid[newCoordinates];
            RecalculatePath();
        }
        else
        {
            Debug.LogWarning($"Coordinates {newCoordinates} not in grid");
        }
    }

    // Public getters
    public Vector2Int GetStartCoordinates() => startCoordinates;
    public Vector2Int GetDestinationCoordinates() => destinationCoordinates;
    public List<Node> GetCurrentPath() => new List<Node>(currentPath);
}