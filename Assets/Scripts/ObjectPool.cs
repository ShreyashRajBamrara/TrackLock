using System.Collections;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject Hostile_Train;
    [SerializeField] float spawn_time=1f;
    // [SerializeField] Transform spawn_point;
    void Start()
    {
        StartCoroutine(SpawnTrain());
    }

    IEnumerator SpawnTrain()
    {
        while(true)
        {
            Instantiate(Hostile_Train,transform);
            yield return new WaitForSeconds(spawn_time);
        }
    }
}
