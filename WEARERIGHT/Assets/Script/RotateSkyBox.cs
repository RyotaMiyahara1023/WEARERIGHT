using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class RotateSkyBox : MonoBehaviour
{
 
    [SerializeField] private float rotateSpeed = 0.5f;
    private Material skyboxMaterial;

	void Start ()
    {
        skyboxMaterial = RenderSettings.skybox;
    }
	
	void Update ()
    {
        skyboxMaterial.SetFloat("_Rotation", Mathf.Repeat(skyboxMaterial.GetFloat("_Rotation") + rotateSpeed * Time.deltaTime, 360f));
	}
}