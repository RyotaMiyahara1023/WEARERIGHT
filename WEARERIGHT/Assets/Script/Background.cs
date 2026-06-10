using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    //public GameObject background;
    //public GameObject background2;
    public GameObject BBossPrefab;
    float time;
    float time2;
    float time3;
    Renderer _Renderer;

    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        time2 = 0f;
        time3 = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        time2 += Time.deltaTime;
        time3 += Time.deltaTime;
        /*if (time > 1f)
        {
            time = 0f;
            BackEnemy();
        }
        if (time2 > 1.1f)
        {
            time2 = 0f;
            BackShip();
        }*/
        if (time3 > 15f)
        {
            time3 = 0f;
            BackBoss();
        }
    }
    /*
    void BackEnemy()
    {
        var y = UnityEngine.Random.Range(40f, 2f);
        var pos = new Vector3(100, y, 80);
        var obj = Instantiate(background, pos, Quaternion.identity * Quaternion.Euler(0f, -90f, 0f));
        obj.GetComponent<Rigidbody>().velocity = new Vector3(-40f, 0, 0f);
        Destroy(obj, 10f);
        _Renderer = obj.GetComponent<Renderer>();
    }

    void BackShip()
    {
        var y = UnityEngine.Random.Range(40f, 2f);
        var pos = new Vector3(-100, y, 80);
        var obj = Instantiate(background2, pos, Quaternion.identity * Quaternion.Euler(0f, 90f, 0f));
        obj.GetComponent<Rigidbody>().velocity = new Vector3(40f, 0, 0f);
        Destroy(obj, 10f);
        _Renderer = obj.GetComponent<Renderer>();
    }
    */
    void BackBoss()
    {
        var y = UnityEngine.Random.Range(40f, 5f);
        var z = UnityEngine.Random.Range(90f, 150f);
        var pos = new Vector3(175, y, z);
        var obj = Instantiate(BBossPrefab, pos, Quaternion.identity * Quaternion.Euler(0f, -90f, 0f));
        obj.GetComponent<Rigidbody>().velocity = new Vector3(-2.5f, 0, 0f);
        //Destroy(obj, 20f);
        _Renderer = obj.GetComponent<Renderer>();
    }
}
