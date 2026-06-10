using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	private float time = 0.0f;
    private float time_breaking = 0.0f;
    public float speed;
	public float angle;
	public float limitTime;
	public bool mode_Homing = false;
    public bool mode_Bounding = false;
    public bool mode_Breaking = false;
    public bool bounded = false;
    public GameObject playerObj;
    public GameObject VanishParticlePrefab;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] private Vector3 velocity;

    private void Start()
    {
        if (playerObj != null)
        {
            var target = playerObj.transform.position - transform.position;
            angle += Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        }
    }

    void Update ()
	{
        time += Time.deltaTime;
        time_breaking += Time.deltaTime;
        float rad = angle * Mathf.Deg2Rad;

        velocity = Vector3.zero;

        if (mode_Homing)
		{
			if (playerObj != null && time <= limitTime)
			{
				Vector2 target = playerObj.transform.position;
                angle = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x) * Mathf.Rad2Deg;
                var rb = transform.GetComponent<Rigidbody>();
                rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.AngleAxis(angle - 90f, Vector3.forward), 0.8f * Time.deltaTime);
                rb.velocity = transform.up * speed;
            }
        }
        else if (mode_Bounding)
        {
            //Debug.Log(angle);
            var rb = transform.GetComponent<Rigidbody>();
            rb.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            rb.velocity = transform.up * speed;
            if (!bounded)
            {
                if (transform.position.x <= -10.5f)
                {
                    angle = 180.0f - angle;
                    bounded = true;
                }
                
                 
                if (transform.position.y <= -4.2f || transform.position.y >= 3.8f)
                {
                    angle = 180.0f - angle;
                    bounded = true;
                }
                    
            }
            
        }
        else if (mode_Breaking)
        {
            var rb = transform.GetComponent<Rigidbody>();
            rb.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            rb.velocity = transform.up * speed;
            if (time_breaking > 2.0f)
            {
                time_breaking -= 2.0f;

                GameObject obj = Instantiate(VanishParticlePrefab);
                obj.transform.position = gameObject.transform.position;

                Shot(4.0f, 45f, transform.forward, 0f, 0f, 0f);
                Shot(4.0f, 135f, transform.forward, 0f, 0f, 0f);
                Shot(4.0f, -45f, transform.forward, 0f, 0f, 0f);
                Shot(4.0f, -135f, transform.forward, 0f, 0f, 0f);

                Destroy(gameObject);
            }
        }
        else
        {
            var rb = transform.GetComponent<Rigidbody>();
            rb.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            rb.velocity = transform.up * speed;
        }
	}

    void Shot(float speed, float angle, Vector3 direction, float x, float y, float z)
    {
        var pos = transform.position + x * transform.forward + y * transform.up + z * transform.right;
        pos.z = 0f;
        var obj = Instantiate(bulletPrefab, pos, Quaternion.Euler(direction) * Quaternion.Euler(0f, 0f, 90f));
        obj.name = "EnemyBullet";
        Destroy(obj, 5.0f);
        obj.GetComponent<Bullet>().speed = speed;
        obj.GetComponent<Bullet>().angle = angle;
        obj.GetComponent<Bullet>().mode_Homing = false;
    }
}