using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StageSelectManager : MonoBehaviour
{
	public Text text_BestTime_1;
    public Text text_BestTime_2;
    public Text text_BestTime_3;
    [SerializeField] Button button_0;
    [SerializeField] Button button_1;
    [SerializeField] Button button_2;
    [SerializeField] Button button_3;
    bool f = true;
    //bool soundflag = false;
    Data data;

    void Start ()
	{
		data = GameObject.Find ("DataManager").GetComponent<Data> ();
		text_BestTime_1.text = "BestTime : \n" + data.BestTime_01.ToString ("F2");
		text_BestTime_2.text = "BestTime : \n" + data.BestTime_02.ToString ("F2");
		text_BestTime_3.text = "BestTime : \n" + data.BestTime_03.ToString ("F2");
	}

    void Update()
    {
        if (f)
        {
            if (data.stage == 0) button_0.Select();
            if (data.stage == 1) button_1.Select();
            if (data.stage == 2) button_2.Select();
            if (data.stage == 3) button_3.Select();

            f = false;
        }
    }

    public void TransitionScene_Stage1 ()
	{
        //if (soundflag) return;
        //soundflag = true;
		GameObject.Find ("FadeManager").GetComponent<Fade> ().TransitionScene ("Stage1");
		GetComponent<AudioSource> ().Play ();
        data.stage = 1;
	}

	public void TransitionScene_Stage2 ()
	{
        //if (soundflag) return;
        //soundflag = true;
        GameObject.Find ("FadeManager").GetComponent<Fade> ().TransitionScene ("Stage2");
		GetComponent<AudioSource> ().Play ();
        data.stage = 2;
    }

	public void TransitionScene_Stage3 ()
	{
        //if (soundflag) return;
        //soundflag = true;
        GameObject.Find ("FadeManager").GetComponent<Fade> ().TransitionScene ("Stage3");
		GetComponent<AudioSource> ().Play ();
        data.stage = 3;
    }
	public void TransitionScene_Title ()
	{
        //if (soundflag) return;
        //soundflag = true;
        GameObject.Find ("FadeManager").GetComponent<Fade> ().TransitionScene ("Title");
		GetComponent<AudioSource> ().Play ();
        data.stage = 0;
    }
	public void OnSelect(BaseEventData eventData)
    {
        //if (soundflag) return;
        if (!f)
        {
            GetComponent<AudioSource>().Play();
        }
    }
}