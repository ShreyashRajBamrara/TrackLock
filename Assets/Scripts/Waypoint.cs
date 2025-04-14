using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool Is_Placeable;
    void OnMouseDown()
    {
        if(Is_Placeable)
        {
         Debug.Log(transform.name);
        }
    }
}
