using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aspect : MonoBehaviour
{
    public Camera maincamera;
    public float baseWidth = 16.0f;
    public float baseHeight = 9.0f;

        void Awake()
    {
        // アスペクト比固定
        var scale = Mathf.Min(Screen.height / this.baseHeight, Screen.width / this.baseWidth);
        var width = (this.baseWidth * scale) / Screen.width;
        var height = (this.baseHeight * scale) / Screen.height;
        this.maincamera.rect = new Rect((1.0f - width) * 0.5f, (1.0f - height) * 0.5f, width, height);
    }
}
