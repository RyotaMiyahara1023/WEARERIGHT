using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image image_PlayerHealth;
    public Image image_BossHealth;
    public GameObject playerObj;
    public GameObject bossObj;
    private bool afterFinish;
    private float afterFinishTime;
    public Text text_battleTime;
    private float battleTime;
    public Text text_Result;
    public Text text_Start;
    public float time;
    public int num;
    public AudioSource se_win;
    public AudioSource se_lose;
    public Image EnergyGauge;
    [SerializeField] AudioSource se_destroy;

    public void SetPlayerHealthUI(int health, int maxHealth)
    {
        float ratio;
        ratio = (float)health / maxHealth;
        image_PlayerHealth.fillAmount = ratio;
    }

    public void SetBossHealthUI(int health, int maxHealth)
    {
        float ratio;
        ratio = (float)health / maxHealth;

        image_BossHealth.fillAmount = ratio;
    }
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 2.5)
        {
            time = 0;
            text_Start.text = "";
        }

        if (!afterFinish)
        {
            battleTime += Time.deltaTime;
            text_battleTime.text = "Time : " + battleTime.ToString("000.00");
        }

        if (!afterFinish)
        {
            if (playerObj == null)
            {
                text_Result.text = "Player Lose...";
                afterFinish = true;
                afterFinishTime = 0.0f;
                se_lose.Play();
            }

            else if (bossObj == null)
            {
                text_Result.text = "Player Win!!";
                afterFinish = true;
                afterFinishTime = 0.0f;
                se_win.Play();
                Data data = GameObject.Find("DataManager").GetComponent<Data>();
                if (num == 1)
                {
                    if (battleTime < data.BestTime_01)
                    {
                        data.BestTime_01 = battleTime;
                    }
                }
                if (num == 2)
                {
                    if (battleTime < data.BestTime_02)
                    {
                        data.BestTime_02 = battleTime;
                    }
                }
                if (num == 3)
                {
                    if (battleTime < data.BestTime_03)
                    {
                        data.BestTime_03 = battleTime;
                    }
                }
            }
        }
        else
        {
            afterFinishTime += Time.deltaTime;
            if (afterFinishTime > 2.0f)
            {
                GameObject.Find("FadeManager").GetComponent<Fade>().TransitionScene("Stageselect");
            }
        }
    }

    void CheckBestTime()
    {
        GameObject dataObj = GameObject.Find("DataManager");
        if (dataObj == null)
            return;
        Data data = dataObj.GetComponent<Data>();

        if (num == 1)
        {
            if (battleTime < data.BestTime_01)
                data.BestTime_01 = battleTime;
        }
        else if (num == 2)
        {
            if (battleTime < data.BestTime_02)
                data.BestTime_02 = battleTime;
        }
        else if (num == 3)
        {
            if (battleTime < data.BestTime_03)
                data.BestTime_03 = battleTime;
        }
    }

    public void TransitionScene_Select()
    {
        GameObject.Find("FadeManager").GetComponent<Fade>().TransitionScene("Stageselect");
        GetComponent<AudioSource>().Play();
    }

    public void SetEnergyUI(float energy, float maxEnergy)
    {
        float ratio;
        ratio = energy / maxEnergy;

        EnergyGauge.fillAmount = ratio;
    }

    public void Sound_BackE()
    {
        se_destroy.Play();
    }
}