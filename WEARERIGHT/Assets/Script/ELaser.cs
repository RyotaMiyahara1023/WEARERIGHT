using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ELaser : MonoBehaviour
{
    GameObject Boss;
    private float time;
    public Vector3 BossPos;

    void Start()
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        this.Boss = GameObject.Find("Enemy");
        time = 0.0f;
    }

    void Update()
    {
        transform.position = new Vector3(BossPos.x, transform.position.y, transform.position.z);
        time += Time.deltaTime;

        if (time >= 1.5f)
        {
            Destroy(gameObject);
        }
    }
}
