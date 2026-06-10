using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Choice : MonoBehaviour
{
    Button button;

    void Start()
    {
        GameObject button = GameObject.Find("Canvas/ButtonSummary/Button");
        EventTrigger trigger = button.GetComponent<EventTrigger>();
        if (trigger) trigger.enabled = false;
        button.GetComponent<Button>().Select();
        if (trigger) trigger.enabled = true;
    }
}