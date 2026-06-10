using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioClip BGM_title;
    public AudioClip BGM_battle1;
    public AudioClip BGM_battle2;
    public AudioClip BGM_battle3;
    private AudioSource source;

    public string beforeScene = "Load";

    void Start ()
    {

        DontDestroyOnLoad(gameObject);

        source = GetComponent<AudioSource> ();

        source.clip = BGM_title;
        source.Play();

        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    void OnActiveSceneChanged ( Scene prevScene, Scene nextScene )
    {
        if (beforeScene == "Load" && nextScene.name == " Title")
        {
            source.Stop ();
            source.clip = BGM_title;
            source.Play ();
        }
        if (beforeScene == "Stageselect" && nextScene.name == "Stage1")
        {
            source.Stop ();
            source.clip = BGM_battle1;
            source.Play ();
        }
        if (beforeScene == "Stage1" && nextScene.name == "Stageselect")
        {
            source.Stop ();
            source.clip = BGM_title;
            source.Play ();
        }
        if (beforeScene == "Stageselect" && nextScene.name == "Stage2")
        {
            source.Stop ();
            source.clip = BGM_battle2;
            source.Play ();
        }
        if (beforeScene == "Stage2" && nextScene.name == "Stageselect")
        {
            source.Stop ();
            source.clip = BGM_title;
            source.Play ();
        }
        if (beforeScene == "Stageselect" && nextScene.name == "Stage3")
        {
            source.Stop ();
            source.clip = BGM_battle3;
            source.Play ();
        }
        if (beforeScene == "Stage3" && nextScene.name == "Stageselect")
        {
            source.Stop ();
            source.clip = BGM_title;
            source.Play ();
        }

        beforeScene = nextScene.name;
    }
}