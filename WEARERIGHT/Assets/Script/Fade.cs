using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
	bool fadeIn;
	bool fadeOut;
	float alpha;
	float feedSpeed;
	string loadScene;

	void Start ()
	{
		fadeIn = true;
		alpha = 1.0f;
		fadeOut = false;
		loadScene = "";

		feedSpeed = 1.2f;
	}

	void Update ()
	{
		if (fadeIn)
		{
			alpha -= feedSpeed * Time.deltaTime;
			if (alpha < 0.0f)
			{
				alpha = 0.0f;
				fadeIn = false;
			}
			GetComponent<Image> ().color = new Color (1.0f, 1.0f, 1.0f, alpha);
		}

		if (fadeOut)
		{
			alpha += feedSpeed * Time.deltaTime;
			if (alpha > 1.0f)
			{
				alpha = 1.0f;
				fadeOut = false;
				SceneManager.LoadScene (loadScene);
			}
			GetComponent<Image> ().color = new Color (1.0f, 1.0f, 1.0f, alpha);
		}
	}

	public void TransitionScene (string sceneName)
	{
		if (fadeIn || fadeOut)
			return;

		fadeOut = true;
		loadScene = sceneName;
		alpha = 0.0f;
	}
}