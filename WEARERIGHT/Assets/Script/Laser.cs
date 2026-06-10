using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    GameObject player;
    private float time;
    public Vector3 playerPos;

    void Start()
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        this.player = GameObject.Find("Player");
        time = 0.0f;
    }

    void Update()
    {
        transform.position = new Vector3(playerPos.x, transform.position.y, transform.position.z);
        time += Time.deltaTime;

        if (time >= 1.0f)
        {
            Destroy(gameObject);
        }
    }
}
