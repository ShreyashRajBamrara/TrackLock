using UnityEngine;
using TMPro;

[ExecuteAlways] // This Script will run always
public class CoordinateLabel : MonoBehaviour
{
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();

    void Awake()
    {
        label =GetComponent<TextMeshPro>();
        DisplayCoordinates();
    }

    void Update()
    {
        if(!Application.isPlaying)
        {
        DisplayCoordinates();
        Update_Tile_Name_To_Its_Postition();
        }
    }

    void DisplayCoordinates()
    {
        // coordinates.x=transform.parent.rotation.y=90;
        //Coordinates is vector2Int and thus store x and y coordinates
        //Transform.parent.postion.x == stores the x position of the parent which is the Tile in form of float value
        coordinates.x=Mathf.RoundToInt(transform.parent.position.x); 
        coordinates.y=Mathf.RoundToInt(transform.parent.position.z); //We working in 2D x and Z Axis and thus using position.z
        label.text="("+coordinates.x/10+","+coordinates.y/10+")";
    }

    void Update_Tile_Name_To_Its_Postition()
    {
        // coordinates.x-=coordinates.x/10;
        // coordinates.x-=2;
        // coordinates.y-=coordinates.y/10;
        transform.parent.name="Tile "+(coordinates/10).ToString();
    }

    

}  
