using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Rotate : MonoBehaviour
{
    [SerializeField] float rotateX;
    [SerializeField] float rotateY;
    [SerializeField] float rotateZ;
     
    void Update ()
    {
        transform.Rotate(new Vector3(rotateX, rotateY, rotateZ) * Time.deltaTime, Space.World);
    }
}