using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float speed = 10f;
    [SerializeField] float recalculationCooldown = 0.5f;
    
    private GridManager gridManager;
    private Pathfinder pathfinder;
    private Coroutine moveRoutine;
    private bool recalculationPending;
    private float lastRecalculationTime;
    private Vector3 currentTarget;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    void OnEnable()
    {
        ResetTrain();
        StartMovement();
    }

    void Update()
    {
        if (recalculationPending && Time.time > lastRecalculationTime + recalculationCooldown)
        {
            RecalculatePath(false);
            recalculationPending = false;
        }
    }

    void ResetTrain()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.startCoordinates);
        currentTarget = transform.position;
        lastRecalculationTime = Time.time;
    }

    void StartMovement()
    {
        if (moveRoutine != null) StopCoroutine(moveRoutine);
        moveRoutine = StartCoroutine(MovementRoutine());
    }

    public void OnTrackChanged()
    {
        if (!recalculationPending)
        {
            recalculationPending = true;
            lastRecalculationTime = Time.time;
        }
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int currentCoords = resetPath ? 
            pathfinder.startCoordinates : 
            gridManager.GetCoordinatesFromPosition(transform.position);
            
        pathfinder.startCoordinates = currentCoords;
        pathfinder.RecalculatePath();
    }

    IEnumerator MovementRoutine()
    {
        while (true)
        {
            var path = pathfinder.GetCurrentPath();
            if (path == null || path.Count <= 1)
            {
                yield return new WaitForSeconds(0.1f);
                continue;
            }

            // Find closest node in path
            int closestIndex = 0;
            float closestDistance = float.MaxValue;
            for (int i = 0; i < path.Count; i++)
            {
                float dist = Vector3.Distance(transform.position, 
                    gridManager.GetPositionFromCoordinates(path[i].coordinates));
                if (dist < closestDistance)
                {
                    closestDistance = dist;
                    closestIndex = i;
                }
            }

            // Move through the path
            for (int i = closestIndex; i < path.Count; i++)
            {
                currentTarget = gridManager.GetPositionFromCoordinates(path[i].coordinates);
                
                while (Vector3.Distance(transform.position, currentTarget) > 0.05f)
                {
                    // Skip if this node becomes blocked
                    if (path[i].isBlocked)
                    {
                        recalculationPending = true;
                        yield break;
                    }

                    transform.position = Vector3.MoveTowards(
                        transform.position, 
                        currentTarget, 
                        speed * Time.deltaTime);
                    
                    transform.LookAt(currentTarget);
                    yield return null;
                }
            }

            yield return null;
        }
    }

    void OnDisable()
    {
        if (moveRoutine != null) StopCoroutine(moveRoutine);
    }
}