using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    //List stores each tile which have a waypoint script attached to it
    [SerializeField] List<Waypoint>path = new List<Waypoint>();
    [SerializeField] [Range(0f,5f)]float Speed=1f;
    void Start()
    {
        StartCoroutine (FollowPath());
        //Calling couroutine and passing a function to call
    }

    void FindPath()
    {
        path.Clear();
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Path"); 
        //find each game object with path tag and store them in array ,so we also store them in specific orderin the hierarchy
        foreach(GameObject waypoint in waypoints )
        {
            path.Add(waypoint.GetComponent<Waypoint>());
            //Path is a list storing waypoint script as a marker 
        }
    }

    void Return_To_Start()
    {
        transform.position=path[0].transform.position;
    }

    //Using Coroutine
    IEnumerator FollowPath()
    {
        foreach(Waypoint waypoint in path)
        {
            Vector3 Starting_position= transform.position;
            Vector3 End_position=waypoint.transform.position;
            float travel_percentage=0f;

            transform.LookAt(End_position);//This will make the game obj to look at the next waypoint

            while(travel_percentage<1f)
            {
                //Lerp performa a liner interpolation between two given points in a given fraction
                travel_percentage+=Time.deltaTime*Speed; //this will allow us to move with time and look smooth motion
                transform.position=Vector3.Lerp(Starting_position,End_position,travel_percentage);
                yield return new WaitForEndOfFrame();
                //When the frame ends the coroutine will be re-visited and again start
            }

        }
        Return_To_Start();
    }
}
