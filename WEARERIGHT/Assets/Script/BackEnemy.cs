using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackEnemy : MonoBehaviour
{
    float time;
    [SerializeField] GameObject vanishParticlePrefab;
    [SerializeField] GameObject win;
    public GameObject playerObj;
    [SerializeField] GameObject bullet2Prefab;
    public GameManager gameManager;

    void Start()
    {
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(-5f, 0f, 0f);
        Destroy(gameObject, 10f);
        time = 0;
    }
    void Update()
    {
        time += Time.deltaTime;

        if (time > 2.0f)
        {
            time -= 2.0f;

            Shot_Bullet2(8.0f, 180f, transform.forward, 0.5f, 0f, 0f);
        }
    }

    void OnCollisionEnter(Collision colision)
    {
        var obj = Instantiate(vanishParticlePrefab);
        obj.transform.position = colision.gameObject.transform.position;
        Destroy(obj, 3.0f);

        

        if (colision.gameObject.name == "PlayerBullet")
        {
            Destroy(colision.gameObject);

            playerObj.GetComponent<Player>().energy++;

            gameManager.GetComponent<GameManager>().Sound_BackE();
            
            Destroy(gameObject);
            var pos = gameObject.transform.position;
            Instantiate(win, pos, Quaternion.Euler(transform.forward) * Quaternion.Euler(0f, 0f, 0f));
            
        }
        if (colision.gameObject.name == "Player")
        {
            Destroy(gameObject);

            gameManager.GetComponent<GameManager>().Sound_BackE();

            var pos = gameObject.transform.position;
            Instantiate(win, pos, Quaternion.Euler(transform.forward) * Quaternion.Euler(0f, 0f, 0f));

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
            Destroy(gameObject);

            gameManager.GetComponent<GameManager>().Sound_BackE();

            var pos = gameObject.transform.position;
            Instantiate(win, pos, Quaternion.Euler(transform.forward) * Quaternion.Euler(0f, 0f, 0f));
            playerObj.GetComponent<BoxCollider>().enabled = false;

        }

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
}
