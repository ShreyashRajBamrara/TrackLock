using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientObjectMover : MonoBehaviour
{
    [SerializeField] List<GameObject> vehiclePrefabs;
    [SerializeField] List<Waypoint> nodesToTraverse;
    [SerializeField] float constantSpeed = 2.5f;
    [SerializeField] float minSpawnInterval = 3f;
    [SerializeField] float maxSpawnInterval = 6f;
    [SerializeField] int maxActiveVehicles = 4;

    private List<GameObject> activeVehicles = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnObjectsRoutine());
    }

    IEnumerator SpawnObjectsRoutine()
    {
        while (true)
        {
            // Remove null entries (destroyed vehicles)
            activeVehicles.RemoveAll(item => item == null);

            if (nodesToTraverse.Count > 1 && activeVehicles.Count < maxActiveVehicles)
            {
                SpawnRandomVehicle();
                yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
            }
            else
            {
                yield return new WaitForSeconds(1f); // Wait before checking again
            }
        }
    }

    void SpawnRandomVehicle()
    {
        if (vehiclePrefabs.Count == 0 || nodesToTraverse.Count == 0) return;

        // Select random vehicle
        GameObject randomPrefab = vehiclePrefabs[Random.Range(0, vehiclePrefabs.Count)];
        GameObject newVehicle = Instantiate(randomPrefab, nodesToTraverse[0].transform.position, Quaternion.identity);
        
        activeVehicles.Add(newVehicle);
        StartCoroutine(MoveObjectAlongPath(newVehicle, constantSpeed));
    }

    IEnumerator MoveObjectAlongPath(GameObject movingObject, float speed)
    {
        for (int i = 1; i < nodesToTraverse.Count; i++)
        {
            Waypoint startNode = nodesToTraverse[i-1];
            Waypoint endNode = nodesToTraverse[i];
            
            Vector3 startPos = startNode.transform.position;
            Vector3 endPos = endNode.transform.position;
            float travelPercent = 0f;

            movingObject.transform.LookAt(endPos);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                movingObject.transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                yield return null;
            }
        }

        // Remove from active list before destroying
        activeVehicles.Remove(movingObject);
        Destroy(movingObject);
    }
}