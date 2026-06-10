using UnityEngine;

public class AutoObjectController : MonoBehaviour
{
    Renderer rend;

    void Start ()
    {
        rend = GetComponent<Renderer>();
    }
    void Update()
    {       
        var pos_c1 = transform.GetChild(0).position;
        var pos_c2 = transform.GetChild(1).position;

        transform.position += new Vector3(-20f*Time.deltaTime, 0f, 0f);
                 
        var camera_left_x = 0f;   
        var width = 79.5f;  
        if( pos_c1.x < pos_c2.x )
        {
            //Debug.Log( "C1.x = "+ pos_c1.x );
            if( pos_c1.x + width < camera_left_x )
            {
                pos_c1.x = pos_c2.x + width;
                transform.GetChild(0).position = pos_c1;
            }
        }
        else
        {
            //Debug.Log( "C2.x = "+ pos_c2.x );
            if( pos_c2.x + width < camera_left_x )
            {
                pos_c2.x = pos_c1.x + width;
                transform.GetChild(1).position = pos_c2;
            }
        }
    }
}