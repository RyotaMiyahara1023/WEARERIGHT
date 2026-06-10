using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExManager : MonoBehaviour
{
    public void TransitionScene_Title ()
	{
        GameObject.Find ("FadeManager").GetComponent<Fade> ().TransitionScene ("Title");
		GetComponent<AudioSource> ().Play ();
	}
	public void OnSelect(BaseEventData eventData)
    {
        GetComponent<AudioSource> ().Play ();
    }
}
