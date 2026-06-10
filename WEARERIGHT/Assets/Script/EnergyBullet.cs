using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBullet : MonoBehaviour
{
    void Update()
    {
        Material mat = this.GetComponent<MeshRenderer>().material;
        mat.color = Color.Lerp(Color.white, Color.yellow, Mathf.PingPong(Time.time, 1));
    }
}
