using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BLaser : MonoBehaviour
{
    public GameObject line;
    public Vector3 Pos;
    float time1;
    float time2;
    float time3;
    float time4;

    // Start is called before the first frame update
    void Start()
    {
        time1 = 3f;
        time2 = 1.8f;
        time3 = 0.7f;
        time4 = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        time1 += Time.deltaTime;
        if (time1 >= 3f)
        {
            time1 = 0f;
            Shot_BLaser1();
        }

        time2 += Time.deltaTime;
        if (time2 > 2.3f)
        {
            time2 = 0f;
            Shot_BLaser2();
        }

        time3 += Time.deltaTime;
        if (time3 > 1.7f)
        {
            time3 = 0f;
            Shot_BLaser3();
        }

        time4 += Time.deltaTime;
        if (time4 > 2f)
        {
            time4 = 0f;
            Shot_BLaser4();
        }
    }

    void Shot_BLaser1()
    {
        Pos = new Vector3(-41.2f, -2, 12);
        var a = Instantiate(line);
        a.transform.position = Pos;
        var r = a.GetComponent<LineRenderer>();
        r.SetPosition(0, new Vector3(30, 15, 0));
        r.SetPosition(1, new Vector3(50, 0, 0));
        Destroy(a, 0.1f);
    }

    void Shot_BLaser2()
    {
        Pos = new Vector3(-40, 12.25f, 10);
        var a = Instantiate(line);
        a.transform.position = Pos;
        var r = a.GetComponent<LineRenderer>();
        r.SetPosition(0, new Vector3(-45, -35, 0));
        r.SetPosition(1, new Vector3(80, 0, 0));
        Destroy(a, 0.1f);
    }

    void Shot_BLaser3()
    {
        Pos = new Vector3(-33.5f, 12, 10);
        var a = Instantiate(line);
        a.transform.position = Pos;
        var r = a.GetComponent<LineRenderer>();
        r.SetPosition(0, new Vector3(40, -30, 0));
        r.SetPosition(1, new Vector3(50, 0, 0));
        Destroy(a, 0.1f);
    }

    void Shot_BLaser4()
    {
        Pos = new Vector3(-60.4f, -2.7f, 10);
        var a = Instantiate(line);
        a.transform.position = Pos;
        var r = a.GetComponent<LineRenderer>();
        r.SetPosition(0, new Vector3(30, 75, 0));
        r.SetPosition(1, new Vector3(50, 0, 0));
        Destroy(a, 0.1f);
    }

}