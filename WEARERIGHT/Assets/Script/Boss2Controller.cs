using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boss2Controller : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject bullet1Prefab;
    [SerializeField] GameObject bullet2Prefab;
    [SerializeField] GameObject bulletEnergyPrefab;
    [SerializeField] GameObject bulletHormingPrefab;
    [SerializeField] GameObject bulletBreakPrefab;
    [SerializeField] GameObject bulletreflectionPrefab;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] GameObject SubLaserPrefab;
    [SerializeField] GameObject playerObj;
    [SerializeField] GameObject vanishParticlePrefab;
    [SerializeField] GameObject playerBullet;
    [SerializeField] AudioSource se_laser;
    [SerializeField] AudioSource se_damage;

    float time_move = 0f;
    float time_normalShot1 = 0f;
    float time_normalShot2 = 0f;
    float time_normalShot3 = 0f;
    float time_normalShot4 = 0f;
    float time_normalShot5 = 0f;
    float time_horming = 0f;
    float time_break = 0f;
    float time_reflection = 0f;
    float time_laser = 0f;
    float time_recovery;
    float time_nextstatus;
    float time_charge;
    float time_chargeS;

    //状態遷移関係
    List<Action> statusList;
    int status = 0;
    int pre_status = -1;

    //動きの制御関係
    int move_mode = 0;
    Vector3 move_startpos;
    Vector3 move_targetpos;
    float move_starttime = 0f;
    float move_speed = 2f;
    [SerializeField] AnimationCurve move_curve = null;
    public bool moved;
    public bool turned;
    bool charging;
    
    [SerializeField] float laser_sec = 0.7f;
    [SerializeField] int health;
    int maxHealth;
    public GameObject win;
    public GameObject charge;
    public GameObject chargeS;

    void Start()
    {
        time_move = Time.time;

        statusList = new List<Action>();
        statusList.Add(Status0);
        statusList.Add(Status1);
        statusList.Add(Status2);
        statusList.Add(Status3);
        statusList.Add(Status4);
        statusList.Add(Status5);
        statusList.Add(Status6);
        statusList.Add(Status7);

        maxHealth = health;
    }

    void UpdateShotTimer()
    {
        time_normalShot1 += Time.deltaTime;
        time_normalShot2 += Time.deltaTime;
        time_normalShot3 += Time.deltaTime;
        time_normalShot4 += Time.deltaTime;
        time_normalShot5 += Time.deltaTime;
        time_horming += Time.deltaTime;
        time_break += Time.deltaTime;
        time_reflection += Time.deltaTime;
        time_laser += Time.deltaTime;
        time_recovery += Time.deltaTime;
        time_charge += Time.deltaTime;
        time_chargeS += Time.deltaTime;

    }

    void ClearShotTimer()
    {
        time_normalShot1 = 0f;
        time_normalShot2 = 0f;
        time_normalShot3 = 0f;
        time_normalShot4 = 0f;
        time_normalShot5 = 0f;
        time_horming = 0f;
        time_break = 0f;
        time_reflection = 0f;
        time_laser = 0f;
        time_recovery += Time.deltaTime;
        time_charge = 0;
        time_chargeS = 0;

    }

    void Status0()
    {
        if (status != pre_status)
        {
            move_starttime = time_move;
            move_startpos = transform.position;

            move_mode = 0;
            move_targetpos = new Vector3(5, 0, 0);
            move_speed = 4f;

            pre_status = status;
        }

        UpdateShotTimer();

        time_move += Time.deltaTime;

        var t = move_curve.Evaluate(Mathf.Min(1f, (time_move - move_starttime) / move_speed));
        transform.position = Vector3.Lerp(move_startpos, move_targetpos, t);
        
        /*
            if (time_normalShot1 > 0.8f)
            {
                time_normalShot1 -= 1.6f;

                Shot_Bullet1(7.0f, GetAngleToPlayer(), transform.forward, 2f, 0f, -0.3f);
            }
            if (time_normalShot2 > 1.6f)
            {
                time_normalShot2 -= 1.6f;

                Shot_Bullet1(7.0f, GetAngleToPlayer(), transform.forward, 2f, 0f, 0.3f);
            }
            if (time_normalShot3 > 1.2f)
            {
                time_normalShot3 -= 2.4f;

                Shot_Bullet1(7.0f, GetAngleToPlayer(), transform.forward, 2f, -0.75f, 0.3f);
            }
            if (time_normalShot4 > 2.4f)
            {
                time_normalShot4 -= 2.4f;

                Shot_Bullet1(7.0f, GetAngleToPlayer(), transform.forward, 2f, -0.75f, -0.3f);
            }
        */

        if (transform.position.x <= 5)
        {
            ClearShotTimer();
            status = 1;
            Debug.Log("Status changed to " + status);
        }
    }

    void Status1()
    {
        

            if (status != pre_status)
            {
                move_starttime = time_move;
                move_startpos = transform.position;

                move_mode = 0;
                move_targetpos = new Vector3(5, 2, 0);
                move_speed = 2f;

                time_nextstatus = 5.0f;

                pre_status = status;
            }

            UpdateShotTimer();
            time_nextstatus -= Time.deltaTime;
            
            
            time_move += Time.deltaTime;

            var t = move_curve.Evaluate(Mathf.Min(1f, (time_move - move_starttime) / move_speed));
            transform.position = Vector3.Lerp(move_startpos, move_targetpos, t);

            if (Vector3.Distance(transform.position, move_targetpos) == 0.0f)
                {
                    move_starttime = time_move;
                    move_startpos = transform.position;

                    switch (move_mode)
                    {
                        case 0:
                            
                                
                    if (time_nextstatus <= 0)
                    {
                        move_targetpos = new Vector3(5, 0, 0);
                        move_mode = 3;
                    }
                    else
                    {
                        move_targetpos = new Vector3(5, -2, 0);
                        move_mode = 1;
                    }
                                move_speed = 1.5f;
                        
                                break;

                        case 1:
                    if (time_nextstatus <= 0)
                    {
                        move_targetpos = new Vector3(5, 0, 0);
                        move_mode = 3;
                    }
                    else
                    {
                        move_targetpos = new Vector3(5, 2, 0);
                        move_mode = 0;
                    }
                    move_speed = 1.5f;
                            break;
                        

                    }
                }

                if (time_normalShot1 > 0.8f)
                {
                    time_normalShot1 -= 1.6f;

                    Shot_Bullet1(7.0f, 0, transform.forward, 4f, 0f, -0.3f);
                }
                if (time_normalShot2 > 1.6f)
                {
                    time_normalShot2 -= 1.6f;

                    Shot_Bullet1(7.0f, 0, transform.forward, 4f, 0f, 0.3f);
                }
                if (time_normalShot3 > 1.2f)
                {
                    time_normalShot3 -= 2.4f;

                    Shot_Bullet1(7.0f, 0, transform.forward, 4f, -0.75f, 0.3f);
                }
                if (time_normalShot4 > 2.4f)
                {
                    time_normalShot4 -= 2.4f;

                    Shot_Bullet1(7.0f, 0, transform.forward, 4f, -0.75f, -0.3f);
                }
                if (time_normalShot5 > 1.0f)
                {
                    time_normalShot5 -= 1.0f;

                    Shot_Bullet2(8.0f, 180f, transform.forward, 4f, 0f, 0f);
                    Shot_Bullet2(8.0f, 180f, transform.forward, 4f, -0.75f, 0f);
                }


        if (move_mode == 3 && transform.position.y == 0)
            {
                ClearShotTimer();
                status = 2;
                Debug.Log("Status changed to " + status);
            }
            else if (health <= 125)
            {
                ClearShotTimer();
                status = 5;
                Debug.Log("Status changed to " + status);
            }
    }
    
    void Status2()
    {
        if (status != pre_status)
        {
            move_starttime = time_move;
            move_startpos = transform.position;

            move_mode = 0;
            move_targetpos = new Vector3(-7, 0, 0);
            move_speed = 2f;

            pre_status = status;
        }

        UpdateShotTimer();
        if (move_mode == 0)
        {
            time_move += Time.deltaTime;
            transform.rotation *= Quaternion.AngleAxis(360 * Time.deltaTime, Vector3.forward);
        }
        else
        {
            time_move -= Time.deltaTime;
        }

        var t = move_curve.Evaluate(Mathf.Min(1f, (time_move - move_starttime) / move_speed));
        transform.position = Vector3.Lerp(move_startpos, move_targetpos, t);
        
        if (move_mode == 1)
        {
            transform.rotation *= Quaternion.AngleAxis(90 * Time.deltaTime, Vector3.up);
        }
            if (move_mode == 0 && Vector3.Distance(transform.position, move_targetpos) == 0.0f)
            {
                move_starttime = time_move;
                move_startpos = transform.position;

                move_mode = 1;

            transform.rotation = Quaternion.AngleAxis(0f, Vector3.forward);
            transform.rotation = Quaternion.AngleAxis(-90f, Vector3.up);

            time_move = 2.0f;
            }

        Debug.Log(time_move);
        
        
        

        if (move_mode == 1 && time_move <= 0.0f)
        {
            transform.rotation = Quaternion.AngleAxis(90f, Vector3.up);
            ClearShotTimer();
            status = 3;
            Debug.Log("Status changed to " + status);
        }
    }
    
    void Status3()
    {
        if (status != pre_status)
        {
            move_starttime = time_move;
            move_startpos = transform.position;

            move_mode = 0;
            move_targetpos = new Vector3(-7, 2, 0);
            move_speed = 2f;

            time_nextstatus = 5.0f;

            pre_status = status;
        }

        UpdateShotTimer();

        time_nextstatus -= Time.deltaTime;
        
        time_move += Time.deltaTime;

        var t = move_curve.Evaluate(Mathf.Min(1f, (time_move - move_starttime) / move_speed));
        transform.position = Vector3.Lerp(move_startpos, move_targetpos, t);

        if (move_mode == 0)
        {
            transform.rotation *= Quaternion.AngleAxis(0 * Time.deltaTime, Vector3.forward);
        }
        if (move_mode == 1)
        {
            transform.rotation *= Quaternion.AngleAxis(0 * Time.deltaTime, Vector3.forward);
        }

            if (Vector3.Distance(transform.position, move_targetpos) == 0.0f)
            {
                move_starttime = time_move;
                move_startpos = transform.position;

                switch (move_mode)
                {
                    case 0:
                        if (time_nextstatus <= 0.0f)
                        {
                            move_targetpos = new Vector3(-7, 0, 0);
                            move_mode = 3;
                        }
                        else
                        {
                            move_targetpos = new Vector3(-7, -2, 0);
                            move_mode = 1;
                        }
                            move_speed = 1.5f;
                            break;
                        

                    case 1:
                        if (time_nextstatus <= 0.0f)
                        {
                            move_targetpos = new Vector3(-7, 0, 0);
                            move_mode = 3;
                        }
                        else
                        {
                            move_targetpos = new Vector3(-7, 2, 0);
                            move_mode = 0;
                        }
                        move_speed = 1.5f;
                        break;
                }
            }

            if (time_normalShot1 > 0.6f)
            {
                time_normalShot1 -= 0.6f;

                Shot_Bullet1(6.0f, 0f, transform.forward, 4f, -0.38f, 0f);
                Shot_Bullet1(6.0f, 15f, transform.forward, 4f, -0.38f, 0f);
                Shot_Bullet1(6.0f, -15f, transform.forward, 4f, -0.38f, 0f);
                Shot_Bullet1(6.0f, 30f, transform.forward, 4f, -0.38f, 0f);
                Shot_Bullet1(6.0f, -30f, transform.forward, 4f, -0.38f, 0f);
            }
            
        if (move_mode == 3 && transform.position.y == 0)
        {
            ClearShotTimer();
            status = 4;
            Debug.Log("Status changed to " + status);
        }
    }
    
    void Status4()
    {
        if (status != pre_status)
        {
            move_starttime = time_move;
            move_startpos = transform.position;

            move_mode = 0;
            move_targetpos = new Vector3(5, 0, 0);
            move_speed = 2f;

            pre_status = status;
        }

        UpdateShotTimer();

        if (move_mode == 0)
        {
            time_move += Time.deltaTime;
            transform.rotation *= Quaternion.AngleAxis(360 * Time.deltaTime, Vector3.forward);
        }
        else
        {
            time_move -= Time.deltaTime;
        }
        var t = move_curve.Evaluate(Mathf.Min(1f, (time_move - move_starttime) / move_speed));
        transform.position = Vector3.Lerp(move_startpos, move_targetpos, t);

        if (move_mode == 1)
        {
            transform.rotation *= Quaternion.AngleAxis(90 * Time.deltaTime, Vector3.up);
        }
            if (move_mode == 0 && Vector3.Distance(transform.position, move_targetpos) == 0.0f)
            {
                move_starttime = time_move;
                move_startpos = transform.position;

                move_mode = 1;

            transform.rotation = Quaternion.AngleAxis(0f, Vector3.forward);
            transform.rotation = Quaternion.AngleAxis(90f, Vector3.up);

            time_move = 2.0f;
            }

        if (move_mode == 1 && time_move <= 0.0f)
        {
            transform.rotation = Quaternion.AngleAxis(-90f, Vector3.up);
            ClearShotTimer();
            status = 1;
            Debug.Log("Status changed to " + status);
        }
    }
    
    void Status5()
    {
            if (status != pre_status)
            {
                pre_status = status;
            charging = false;
            time_laser = 0;
            }

            UpdateShotTimer();

            if (move_mode == 0 || move_mode == 1)
            {
                time_move += Time.deltaTime;
            }
            var t = move_curve.Evaluate(Mathf.Min(1f, (time_move - move_starttime) / move_speed));
            transform.position = Vector3.Lerp(move_startpos, move_targetpos, t);

            if (move_mode == 0)
            {
                transform.rotation *= Quaternion.AngleAxis(0 * Time.deltaTime, Vector3.forward);
            }
            if (move_mode == 1)
            {
                transform.rotation *= Quaternion.AngleAxis(0 * Time.deltaTime, Vector3.forward);
            }

            if (Vector3.Distance(transform.position, move_targetpos) == 0.0f)
            {
                move_starttime = time_move;
                move_startpos = transform.position;

                switch (move_mode)
                {
                    case 0:
                        move_mode = 1;
                        move_targetpos = new Vector3(5, -1, 0);
                        move_speed = 2f;
                        break;

                    case 1:
                        move_mode = 0;
                        move_targetpos = new Vector3(5, 1, 0);
                        move_speed = 2f;
                        break;
                }
            }

            if (time_normalShot1 > 1.3f)
            {
                time_normalShot1 -= 1.6f;

                Shot_Bullet1(7.0f, 0, transform.forward, 2f, 0f, -0.3f);
            }
            if (time_normalShot2 > 2.1f)
            {
                time_normalShot2 -= 1.6f;

                Shot_Bullet1(7.0f, 0, transform.forward, 2f, 0f, 0.3f);
            }
            if (time_normalShot3 > 1.7f)
            {
                time_normalShot3 -= 2.4f;

                Shot_Bullet1(7.0f, 0, transform.forward, 2f, -0.75f, 0.3f);
            }
            if (time_normalShot4 > 2.9f)
            {
                time_normalShot4 -= 2.4f;

                Shot_Bullet1(7.0f, 0, transform.forward, 2f, -0.75f, -0.3f);
            }

        /*if (move_mode == 0 || move_mode == 1)
        {
            Charge_Laser(transform.forward, 4.47f, 0.1f, 0.1f);

            if (time_laser > 1.5f)
            {
                time_laser -= 1;
                se_laser.Play();
                if (move_mode == 0)
                {
                    move_mode = 2;
                }
                else
                {
                    move_mode = 3;
                }
            }
        }
        else
        {
            if (time_laser < laser_sec)
            {
                Shot_Laser(transform.forward, 4.37f, -0.38f, 0.0f);
            }

            if (time_laser > laser_sec + 1f)
            {
                time_laser = 0;
                if (move_mode == 2) move_mode = 0;
                else move_mode = 1;
            }
        }*/
        if (move_mode == 0 || move_mode == 1)
        {
            if (time_laser > 1.5f)
            {
                charging = true;
                if (move_mode == 0)
                {
                    move_mode = 2;
                }
                else
                {
                    move_mode = 3;
                }
            }
        }
        else if (charging)
        {
            if (time_laser < 1.8f)
            {
                Charge_Laser(transform.forward, 4.47f, -0.3f, 0.1f);
            }
            else if (time_laser > 2.3f)
            {
                charging = false;
                se_laser.Play();
                time_laser = 0f;
            }
        }
        else
        {
            if (time_laser < laser_sec)
            {
                Shot_Laser(transform.forward, 4.37f, -0.38f, 0.0f);
            }

            if (time_laser > laser_sec + 1f)
            {
                time_laser = 0;
                if (move_mode == 2) move_mode = 0;
                else move_mode = 1;
            }
        }
        if (time_normalShot2 > 1.5f)
        {
            time_normalShot2 -= 1.0f;
            
            Shot_Energy(6f, +25.0f, transform.forward, 4.37f, -0.38f, 0.0f);
            Shot_Energy(6f, -25.0f, transform.forward, 4.37f, -0.38f, 0.0f);
            Shot_Energy(6f, 0, transform.forward, 4.37f, -0.38f, 0.0f);
        }

        if (health <= 100)
            {
                ClearShotTimer();
                status = 6;
                Debug.Log("Status changed to " + status);
            }
    }


    void Status6()
    {
        
            if (status != pre_status)
            {
                pre_status = status;
            }

            UpdateShotTimer();

            if (move_mode == 0 || move_mode == 1)
            {
                time_move += Time.deltaTime;
            }
            var t = move_curve.Evaluate(Mathf.Min(1f, (time_move - move_starttime) / move_speed));
            transform.position = Vector3.Lerp(move_startpos, move_targetpos, t);

            if (move_mode == 0)
            {
                transform.rotation *= Quaternion.AngleAxis(0 * Time.deltaTime, Vector3.forward);
            }
            if (move_mode == 1)
            {
                transform.rotation *= Quaternion.AngleAxis(0 * Time.deltaTime, Vector3.forward);
            }

            if (Vector3.Distance(transform.position, move_targetpos) == 0.0f)
            {
                move_starttime = time_move;
                move_startpos = transform.position;

                switch (move_mode)
                {
                    case 0:
                        move_mode = 1;
                        move_targetpos = new Vector3(5, -2, 0);
                        move_speed = 1.5f;
                        break;

                    case 1:
                        move_mode = 0;
                        move_targetpos = new Vector3(5, 2, 0);
                        move_speed = 1.5f;
                        break;

                }
            }

        if (move_mode == 0 || move_mode == 1)
        {
            Charge_LaserS(transform.forward, 4.47f, -0.38f, 0f);
            if (time_laser > 1.5f)
            {
                time_laser -= 1.5f;
                se_laser.Play();
                if (move_mode == 0)
                {
                    move_mode = 2;
                }
                else
                {
                    move_mode = 3;
                }
            }
        }
        else
        {
            if (time_laser < laser_sec)
            {
                Shot_LaserS1(transform.forward, 3.87f, -0.38f, 0.0f);
                Shot_LaserS2(transform.forward, 3.87f, -0.38f, 0.0f);
            }

            if (time_laser > laser_sec + 1f)
            {
                time_laser = 0;
                if (move_mode == 2) move_mode = 0;
                else move_mode = 1;
            }
        }

        if (time_normalShot1 > 1.3f)
            {
                time_normalShot1 -= 1.6f;

                Shot_Bullet1(7.0f, 0, transform.forward, 2f, 0f, -0.3f);
            }
            if (time_normalShot2 > 2.1f)
            {
                time_normalShot2 -= 1.6f;

                Shot_Bullet1(7.0f, 0, transform.forward, 2f, 0f, 0.3f);
            }
            if (time_normalShot3 > 1.7f)
            {
                time_normalShot3 -= 2.4f;

                Shot_Bullet1(7.0f, 0, transform.forward, 2f, -0.75f, 0.3f);
            }
            if (time_normalShot4 > 2.9f)
            {
                time_normalShot4 -= 2.4f;

                Shot_Bullet1(7.0f, 0, transform.forward, 2f, -0.75f, -0.3f);
            }
            if (time_horming > 2.5f)
            {
                time_horming -= 2.0f;

                Shot_Break(4f, 180.0f, transform.forward, 4f, -0.38f, 0.0f);
            }

            if (health <= 50)
            {
                ClearShotTimer();
                status = 7;
                Debug.Log("Status changed to " + status);
            }
    }
    
    void Status7()
    {
            if (status != pre_status)
            {
                pre_status = status;

            if (move_mode == 2)
            {
                move_mode = 0;
            }
            if (move_mode == 3)
            {
                move_mode = 1;
            }
        }

            UpdateShotTimer();

            if (move_mode == 0 || move_mode == 1)
            {
                time_move += Time.deltaTime;
            }
            var t = move_curve.Evaluate(Mathf.Min(1f, (time_move - move_starttime) / move_speed));
            transform.position = Vector3.Lerp(move_startpos, move_targetpos, t);

            if (move_mode == 0)
            {
                transform.rotation *= Quaternion.AngleAxis(0 * Time.deltaTime, Vector3.forward);
            }
            if (move_mode == 1)
            {
                transform.rotation *= Quaternion.AngleAxis(0 * Time.deltaTime, Vector3.forward);
            }

            if (Vector3.Distance(transform.position, move_targetpos) == 0.0f)
            {
                move_starttime = time_move;
                move_startpos = transform.position;

                switch (move_mode)
                {
                    case 0:
                        move_mode = 1;
                        move_targetpos = new Vector3(5, -3, 0);
                        move_speed = 1.25f;
                        break;

                    case 1:
                        move_mode = 0;
                        move_targetpos = new Vector3(5, 3, 0);
                        move_speed = 1.25f;
                        break;

                }
            }
        /*
        if (move_mode == 0 || move_mode == 1)
        {
            if (time_laser > 1.0f)
            {
                time_laser = 0;
                se_laser.Play();
                if (move_mode == 0)
                {
                    move_mode = 2;
                }
                else
                {
                    move_mode = 3;
                }
            }
        }
        else
        {
            if (time_laser < laser_sec)
            {
                Shot_LaserS1(transform.forward, 3.87f, -0.38f, 0.0f);
                Shot_LaserS2(transform.forward, 3.87f, -0.38f, 0.0f);
            }

            if (time_laser > laser_sec + 1f)
            {
                time_laser = 0;
                if (move_mode == 2) move_mode = 0;
                else move_mode = 1;
            }
        }
        

        if (time_normalShot1 > 1f)
            {
                time_normalShot1 -= 2f;

                Shot_Bullet1(7.0f, 0, transform.forward, 2f, 0f, -0.3f);
            }
            if (time_normalShot3 > 2f)
            {
                time_normalShot3 -= 2f;

                Shot_Bullet1(7.0f, 0, transform.forward, 2f, -0.75f, 0.3f);
            }
            */
        if (time_normalShot5 > 1.5f)
        {
            time_normalShot5 -= 1.5f;

            Shot_Bullet2(8.0f, 180f, transform.forward, 4f, 0f, 0f);
            Shot_Bullet2(8.0f, 180f, transform.forward, 4f, -0.75f, 0f);
        }
        if (time_reflection > 1.5f)
            {
                time_reflection -= 1.5f;

                Shot_Reflection(5f, 0, transform.forward, 1.57f, 0.0f, 0.1f);
            }
            if (time_break > 3.5f)
            {
                time_break -= 3.5f;

                //Shot_Break(3.5f, 180f, transform.forward, 1.57f, 0.0f, 0.1f);
            }
        if (time_normalShot2 > 1.5f)
        {
            time_normalShot2 -= 1.5f;

            Shot_Energy(6f, +25.0f, transform.forward, 4.37f, -0.38f, 0.0f);
            Shot_Energy(6f, -25.0f, transform.forward, 4.37f, -0.38f, 0.0f);
            Shot_Energy(6f, 0, transform.forward, 4.37f, -0.38f, 0.0f);
        }
    }

    void Update()
    {
        statusList[status]();
    }
    
    void Shot_Bullet1(float speed, float angle, Vector3 direction, float x, float y, float z)
    {
        if (playerObj == null) return;

        var pos = transform.position + x * transform.forward + y * transform.up + z * transform.right;
        pos.z = 0f;
        var obj = Instantiate(bullet1Prefab, pos, Quaternion.Euler(direction) * Quaternion.Euler(0f, 0f, 90f));
        obj.name = "EnemyBullet";
        Destroy(obj, 5.0f);
        obj.GetComponent<Bullet>().speed = speed;
        obj.GetComponent<Bullet>().angle = angle;
        obj.GetComponent<Bullet>().playerObj = playerObj;
    }

    void Shot_Bullet2(float speed, float angle, Vector3 direction, float x, float y, float z)
    {
        if (playerObj == null) return;

        var pos = transform.position + x * transform.forward + y * transform.up + z * transform.right;
        pos.z = 0f;
        var obj = Instantiate(bullet2Prefab, pos, Quaternion.Euler(direction) * Quaternion.Euler(0f, 0f, 90f));
        obj.name = "EnemyBullet";
        Destroy(obj, 5.0f);
        obj.GetComponent<Bullet>().speed = speed;
        obj.GetComponent<Bullet>().angle = angle;
        obj.GetComponent<Bullet>().mode_Homing = false;
    }

    void Shot_Energy(float speed, float angle, Vector3 direction, float x, float y, float z)
    {
        if (playerObj == null) return;

        var pos = transform.position + x * transform.forward + y * transform.up + z * transform.right;
        pos.z = 0f;
        var obj = Instantiate(bulletEnergyPrefab, pos, Quaternion.Euler(direction) * Quaternion.Euler(0f, 0f, 90f));
        obj.name = "EnemyBullet";
        Destroy(obj, 5.0f);
        obj.GetComponent<Bullet>().speed = speed;
        obj.GetComponent<Bullet>().angle = angle;
        obj.GetComponent<Bullet>().playerObj = playerObj;
    }

    void Shot_Laser(Vector3 direction, float x, float y, float z)
    {
        if (playerObj == null) return;

        var pos = transform.position + x * transform.forward + y * transform.up + z * transform.right;
        var obj = Instantiate(laserPrefab, pos, Quaternion.Euler(direction) * Quaternion.Euler(0f, -90f, 0f));
        obj.name = "ELaser";
        obj.GetComponent<ELaser>().BossPos = pos;
        Destroy(obj, 1.0f);
    }

    void Charge_Laser(Vector3 direction, float x, float y, float z)
    {
        if (playerObj == null) return;

        var pos = gameObject.transform.position + x * gameObject.transform.forward + y * gameObject.transform.up + z * gameObject.transform.right;
        var obj = Instantiate(charge, pos, Quaternion.Euler(direction) * Quaternion.Euler(0f, -90f, 0f));
        Destroy(obj, 0.5f);
    }

    void Shot_LaserS1(Vector3 direction, float x, float y, float z)
    {
        if (playerObj == null) return;

        var pos = transform.position + x * transform.forward + y * transform.up + z * transform.right;
        var obj = Instantiate(SubLaserPrefab, pos, Quaternion.Euler(direction) * Quaternion.Euler(40f, -90f, 140f));
        obj.name = "ELaser";
        obj.GetComponent<ELaser>().BossPos = pos;
        Destroy(obj, 1.0f);
    }

    void Shot_LaserS2(Vector3 direction, float x, float y, float z)
    {
        if (playerObj == null) return;

        var pos = transform.position + x * transform.forward + y * transform.up + z * transform.right;
        var obj = Instantiate(SubLaserPrefab, pos, Quaternion.Euler(direction) * Quaternion.Euler(-40f, -90f, -140f));
        obj.name = "ELaser";
        obj.GetComponent<ELaser>().BossPos = pos;
        Destroy(obj, 1.0f);
    }

    void Charge_LaserS(Vector3 direction, float x, float y, float z)
    {
        if (playerObj == null) return;

        var pos = gameObject.transform.position + x * gameObject.transform.forward + y * gameObject.transform.up + z * gameObject.transform.right;
        var obj = Instantiate(chargeS, pos, Quaternion.Euler(direction) * Quaternion.Euler(0f, -90f, 0f));
        Destroy(obj, 0.5f);
    }

    void Shot_Homing(float speed, float limitTime, Vector3 direction, float x, float y, float z)
    {
        if (playerObj == null) return;

        var pos = transform.position + x * transform.forward + y * transform.up + z * transform.right;
        pos.z = 0f;
        var obj = Instantiate(bulletHormingPrefab, pos, Quaternion.Euler(direction) * Quaternion.Euler(0f, 0f, 90f));
        obj.name = "EnemyBullet";
        obj.GetComponent<Bullet>().speed = speed;
        obj.GetComponent<Bullet>().limitTime = limitTime;
        obj.GetComponent<Bullet>().mode_Homing = true;
        obj.GetComponent<Bullet>().playerObj = playerObj;
    }

    void Shot_Break(float speed, float angle, Vector3 direction, float x, float y, float z)
    {
        if (playerObj == null) return;

        var pos = transform.position + x * transform.forward + y * transform.up + z * transform.right;
        pos.z = 0f;
        var obj = Instantiate(bulletBreakPrefab, pos, Quaternion.Euler(direction) * Quaternion.Euler(0f, 0f, 90f));
        obj.name = "EnemyBullet";
        obj.GetComponent<Bullet>().speed = speed;
        obj.GetComponent<Bullet>().angle = angle;
        obj.GetComponent<Bullet>().mode_Breaking = true;
    }

    void Shot_Reflection(float speed, float angle, Vector3 direction, float x, float y, float z)
    {
        if (playerObj == null) return;

        var pos = transform.position + x * transform.forward + y * transform.up + z * transform.right;
        pos.z = 0f;
        var obj = Instantiate(bulletreflectionPrefab, pos, Quaternion.Euler(direction) * Quaternion.Euler(0f, 0f, 90f));
        obj.name = "EnemyBullet";
        obj.GetComponent<Bullet>().speed = speed;
        obj.GetComponent<Bullet>().angle = angle;
        obj.GetComponent<Bullet>().mode_Bounding = true;
        obj.GetComponent<Bullet>().playerObj = playerObj;
    }

    void OnCollisionEnter(Collision colision)
    {
        var obj = Instantiate(vanishParticlePrefab);
        obj.transform.position = colision.gameObject.transform.position;
        Destroy(obj, 3.0f);

        if (colision.gameObject.name == "PlayerBullet")
        {
            Destroy(colision.gameObject);

            health--;
            gameManager.SetBossHealthUI(health, maxHealth);

            if (playerObj != null) playerObj.GetComponent<Player>().energy++;

            if (health >= 1)
            {
                se_damage.Play();
            }
            if (health <= 0)
            {
                Destroy(gameObject);
                if (playerObj != null) playerObj.GetComponent<BoxCollider>().enabled = false;
                var pos = gameObject.transform.position;
                Instantiate(win, pos, Quaternion.Euler(transform.forward) * Quaternion.Euler(0f, 0f, 0f));
            }
        }
    }

    private void OnParticleCollision(GameObject obj)
    {
        if (obj.gameObject.tag == "Laser")
        {
            var particle = obj.GetComponent<ParticleSystem>();
            var collisionEvents = new List<ParticleCollisionEvent>();
            var numCollisionEvents = particle.GetCollisionEvents(gameObject, collisionEvents);
            for (int i = 0; i < numCollisionEvents; i++)
            {
                Debug.Log(collisionEvents[i].intersection);
                GameObject obj2 = Instantiate(vanishParticlePrefab);
                obj2.transform.position = collisionEvents[i].intersection;
                Destroy(obj2, 3.0f);
            }

            Destroy(obj);

            health--;
            gameManager.SetBossHealthUI(health, maxHealth);

            if (health <= 0)
            {
                Destroy(gameObject);
                var pos = gameObject.transform.position;
                Instantiate(win, pos, Quaternion.Euler(transform.forward) * Quaternion.Euler(0f, 0f, 0f));
                if (playerObj != null) playerObj.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
