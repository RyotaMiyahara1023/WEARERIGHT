using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
	public float BestTime_01;	
	public float BestTime_02;
	public float BestTime_03;
    public int stage;

	void Start ()
	{
		DontDestroyOnLoad (gameObject);

		BestTime_01 = 999.99f;
		BestTime_02 = 999.99f;
		BestTime_03 = 999.99f;
        stage = 0;

        #if UNITY_EDITOR || UNITY_WEBGL
        #else
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        #endif
    }

    void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
        #endif
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) Quit();
    }
}
