using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField][Range(0f, 10f)] float speed = 5f;
    
    Pathfinder pathfinder;
    GridManager gridManager;
    
    List<Node> path = new List<Node>();
    Coroutine movementCoroutine;
    Vector2Int currentCoords;
    bool isOnOriginalPath = true;

    void Awake()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
        gridManager = FindObjectOfType<GridManager>();
    }

    void OnEnable()
    {
        ResetToOriginalPath();
        RecalculatePath(true);
    }

    void OnDisable()
    {
        StopMovementCoroutine();
    }

    void ResetToOriginalPath()
    {
        currentCoords = pathfinder.GetStartCoordinates();
        transform.position = gridManager.GetPositionFromCoordinates(currentCoords);
        isOnOriginalPath = true;
    }

    public void RecalculatePath(bool resetToOriginal)
    {
        Vector2Int coordinates;
        
        if (resetToOriginal || !isOnOriginalPath)
        {
            coordinates = pathfinder.GetStartCoordinates();
            isOnOriginalPath = true;
        }
        else
        {
            coordinates = currentCoords;
        }

        StopMovementCoroutine();
        path.Clear();
        
        // Always calculate path from start to finish
        path = pathfinder.GetNewPathFrom(pathfinder.GetStartCoordinates());
        
        if (path.Count == 0)
        {
            Debug.LogWarning("Pathfinder returned empty path");
            gameObject.SetActive(false);
            return;
        }

        // If we're continuing from current position, find our closest node
        if (!resetToOriginal)
        {
            int closestIndex = FindClosestPathIndex();
            if (closestIndex >= 0)
            {
                path = path.GetRange(closestIndex, path.Count - closestIndex);
                isOnOriginalPath = false;
            }
        }

        movementCoroutine = StartCoroutine(FollowPath());
    }

    int FindClosestPathIndex()
    {
        float minDistance = float.MaxValue;
        int closestIndex = -1;
        
        for (int i = 0; i < path.Count; i++)
        {
            float dist = Vector3.Distance(
                transform.position,
                gridManager.GetPositionFromCoordinates(path[i].coordinates)
            );
            
            if (dist < minDistance)
            {
                minDistance = dist;
                closestIndex = i;
            }
        }
        
        return closestIndex;
    }

    void StopMovementCoroutine()
    {
        if (movementCoroutine != null)
        {
            StopCoroutine(movementCoroutine);
            movementCoroutine = null;
        }
    }

    IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++)
        {
            Node currentNode = path[i-1];
            Node nextNode = path[i];
            
            Vector3 startPos = transform.position;
            Vector3 endPos = gridManager.GetPositionFromCoordinates(nextNode.coordinates);
            float travelPercent = 0f;

            transform.LookAt(endPos);
            currentCoords = currentNode.coordinates;

            while (travelPercent < 1f)
            {
                if (nextNode.isBlocked)
                {
                    RecalculatePath(false);
                    yield break;
                }

                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                yield return null;
            }
        }

        OnPathComplete();
    }

    void OnPathComplete()
    {
        gameObject.SetActive(false);
    }

    public void OnTrackChanged()
    {
        RecalculatePath(false);
    }
}