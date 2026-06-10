using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back : MonoBehaviour
{
    public int num;
    float time = 0f;
    public GameObject ex;

    void Update()
    {
        time += Time.deltaTime;

        if (num == 1)
        {
            if(time > 25)
            {
                Destroy(gameObject);
                var pos = gameObject.transform.position;
                Instantiate(ex, pos, Quaternion.Euler(transform.forward) * Quaternion.Euler(0f, 0f, 0f));
            }
        }
        else if (num == 2)
        {
            if (time > 40)
            {
                Destroy(gameObject);
                var pos = gameObject.transform.position;
                Instantiate(ex, pos, Quaternion.Euler(transform.forward) * Quaternion.Euler(0f, 0f, 0f));
            }
        }
    }
}
