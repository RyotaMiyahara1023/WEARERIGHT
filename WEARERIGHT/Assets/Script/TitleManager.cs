using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR 
using UnityEditor; 
#endif
using UnityEngine.EventSystems;

public class TitleManager : MonoBehaviour
{
    //bool soundflag = false;

    public void TransitionScene ()
	{
        //if (soundflag) return;
        //soundflag = true;
        GameObject.Find ("FadeManager").GetComponent<Fade> ().TransitionScene ("Stageselect");
		GetComponent<AudioSource> ().Play ();
	}
	public void TransitionScene_explanation ()
	{
        //if (soundflag) return;
        //soundflag = true;
        GameObject.Find ("FadeManager").GetComponent<Fade> ().TransitionScene ("explanation");
		GetComponent<AudioSource> ().Play ();
	}
    public void OnClick()
    {
        //if (soundflag) return;
        //soundflag = true;
        GetComponent<AudioSource> ().Play ();
        #if UNITY_EDITOR 
        EditorApplication.isPlaying = false;
        #endif
        Application.Quit ();
    }
	public void OnSelect(BaseEventData eventData)
    {
        //if (soundflag) return;
        GetComponent<AudioSource> ().Play ();
    }
}
