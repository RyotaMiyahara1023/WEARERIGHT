using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    private float step_time;
 
    void Start()
    {
        step_time = 0.0f;
    }
 
    void Update()
    {
        step_time += Time.deltaTime;

        if (step_time >= 1.0f)
        {
            SceneManager.LoadScene("Title");
        }
    }
}
