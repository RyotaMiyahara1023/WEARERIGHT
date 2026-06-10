using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private float moveSpeed = 5.0f;
	public GameObject bulletPrefab;
    public GameObject LaserPrefab;
    private GameObject LaserInstance;
    public int health;
    private int maxHealth;
    public float invisibleTime;
	public Image      image_PlayerHealth;
    public GameObject Enemy;
    public GameObject PVanishParticlePrefab;
    public GameObject P2VanishParticlePrefab;
    public GameObject PEVanishParticlePrefab;
    public GameObject PHVanishParticlePrefab;
    public GameObject PBVanishParticlePrefab;
    public GameObject PRBVanishParticlePrefab;
    public AudioSource se_shot;
    public AudioSource se_laser;
    public AudioSource se_damage;
    public Vector2 playerPos;
    float maxScreenPos = 190f;
    float minScreenPos = -40f;
    float maxRotate = 45;
    public float adRotate = 180;
    float tmpRotate = 0;
    float startRotation;
    public int mode = 1;
    GameObject player;
    public float energy;
    private float maxEnergy;
    public int number;
    float time_waiting;
    public GameObject lose;

    void Start ()
	{
        maxHealth = health;
        startRotation = this.transform.rotation.eulerAngles.z;
        this.player = GameObject.Find("Player");
        energy = 0;
        maxEnergy = 20;
    }

	void Update ()
    {
        Vector3 playerPos = this.player.transform.position;

        if (invisibleTime > 0.0f)
        {
            invisibleTime -= Time.deltaTime;
            if (invisibleTime <= 0)
            {
                var material = this.GetComponent<MeshRenderer>().material;
                material.color = Color.white;
                GetComponent<BoxCollider>().enabled = true;
            }
        }
        if (mode == 1)
        {
            float dx = Input.GetAxisRaw("Horizontal");
            float dy = Input.GetAxisRaw("Vertical");
            velocity = Vector3.zero;
            if (dx > 0) velocity.x += 1;
            if (dx < 0) velocity.x -= 1;
            if (dy > 0)
            {
                velocity.y += 1;
                this.transform.Rotate(new Vector3(0, 0, adRotate) * Time.deltaTime);
                tmpRotate += (adRotate * Time.deltaTime);
                if (tmpRotate >= maxRotate)
                {
                    this.transform.rotation = Quaternion.Euler(0, 90, startRotation + 45);
                    tmpRotate = 45;
                }
            }
            if (dy < 0)
            {
                velocity.y -= 1;
                this.transform.Rotate(new Vector3(0, 0, -adRotate) * Time.deltaTime);
                tmpRotate -= (adRotate * Time.deltaTime);
                if (tmpRotate <= maxRotate * -1)
                {
                    this.transform.rotation = Quaternion.Euler(0, 90, startRotation - 45);
                    tmpRotate = -45;
                }
            }
            if (dy == 0)
            {
                tmpRotate += (startRotation - tmpRotate) * Time.deltaTime * 4f;
                this.transform.rotation = Quaternion.Euler(0, 90, startRotation + tmpRotate);
            }
            velocity = velocity.normalized * moveSpeed * Time.deltaTime;
            if (velocity.magnitude > 0) transform.position += velocity;
        }
        
        if (energy == 20 && Input.GetButtonDown("Fire3") || Input.GetButtonDown("Jump"))
        {
            mode = 2;
            se_laser.Play();
            //LinvisibleTime(0.5f);
        }
        if (mode == 1)
        {
            if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2"))
            {
                var pos = transform.position + (1.4f) * transform.forward + (0.0f) * transform.right + (0.0f) * transform.up;
                var obj = Instantiate(bulletPrefab, pos, Quaternion.Euler(transform.forward) * Quaternion.Euler(0f, 0f, 90f));
                Destroy(obj, 5.0f);
                obj.transform.position = transform.position;
                obj.name = "PlayerBullet";
                obj.GetComponent<Bullet>().speed = 6.0f;
                obj.GetComponent<Bullet>().angle = 0.0f;
                obj.GetComponent<Bullet>().limitTime = 5.0f;
                se_shot.Play();

                //energy++;

                if (energy == 20)
                {
                    if (Input.GetButtonDown("Fire3") || Input.GetButtonDown("Jump"))
                    {
                        mode = 2;
                    }
                }
            }
        }
        if (mode == 2)
        {
            energy -= 20.0f * Time.deltaTime;

            if (energy <= 20.0f && energy > 0)
            {
                var pos = transform.position + (0.7f) * transform.forward + (0.0f) * transform.right + (0.0f) * transform.up;
                var obj = Instantiate(LaserPrefab, pos, Quaternion.Euler(transform.forward) * Quaternion.Euler(0f, 90f, 0f));
                Destroy(obj, 1.0f);
                obj.name = "Laser";
                obj.GetComponent<Laser>().playerPos = pos;
            }
            if (energy <= 0)
            {
                time_waiting += Time.deltaTime;
                if (time_waiting > 0.85f)
                {
                    time_waiting = 0;
                    mode = 1;
                }
            }
        }
        energy = Mathf.Clamp(energy, 0.0f, maxEnergy);
        gameManager.SetEnergyUI(energy, maxEnergy);

        playerPos = transform.position;
        playerPos.x = Mathf.Clamp(playerPos.x, -10.0f, 10.0f);
        playerPos.y = Mathf.Clamp(playerPos.y, -4.2f, 3.8f);
        transform.position = new Vector2(playerPos.x, playerPos.y);

        if (maxScreenPos <= playerPos.x)
        {
            transform.position = new Vector2(minScreenPos, playerPos.y);
        }
    }

    void OnCollisionEnter(Collision colision)
    {
        Debug.Log(colision.gameObject.tag);

        if (colision.gameObject.name == "PlayerBullet")
        {
            return;
        }
        /*
        if (mode == 2)
        {
            se_damage.Play();
            GameObject obj = Instantiate(VanishParticlePrefab);
            obj.transform.position = colision.gameObject.transform.position;
            Destroy(obj, 3.0f);
            Destroy(colision.gameObject);
            return;
        }
        */

        if (colision.gameObject.tag == "EB")
        {
            GameObject obj = Instantiate(PVanishParticlePrefab);
            obj.transform.position = colision.gameObject.transform.position;
            Destroy(obj, 3.0f);
        }
        if (colision.gameObject.tag == "EB2")
        {
            GameObject obj = Instantiate(P2VanishParticlePrefab);
            obj.transform.position = colision.gameObject.transform.position;
            Destroy(obj, 3.0f);
        }
        if (colision.gameObject.tag == "EEB")
        {
            GameObject obj = Instantiate(PEVanishParticlePrefab);
            obj.transform.position = colision.gameObject.transform.position;
            Destroy(obj, 3.0f);
        }
        if (colision.gameObject.tag == "HB")
        {
            GameObject obj = Instantiate(PHVanishParticlePrefab);
            obj.transform.position = colision.gameObject.transform.position;
            Destroy(obj, 3.0f);
        }
        if (colision.gameObject.tag == "BB")
        {
            GameObject obj = Instantiate(PBVanishParticlePrefab);
            obj.transform.position = colision.gameObject.transform.position;
            Destroy(obj, 3.0f);
        }
        if (colision.gameObject.tag == "RB")
        {
            GameObject obj = Instantiate(PRBVanishParticlePrefab);
            obj.transform.position = colision.gameObject.transform.position;
            Destroy(obj, 3.0f);
        }
        if (colision.gameObject.tag == "BackE")
        {
            GameObject obj = Instantiate(PVanishParticlePrefab);
            obj.transform.position = colision.gameObject.transform.position;
            Destroy(obj, 3.0f);
        }

        if (colision.gameObject.tag == "Boss")
        {
            health--;
        }
        else if (invisibleTime <= 0f)
        {
            Destroy(colision.gameObject);
            health--;
        }
        else if (invisibleTime > 0f || Enemy == null)
        {
            Destroy(colision.gameObject);
        }
        gameManager.SetPlayerHealthUI(health, maxHealth);
        if (health >= 1)
        {
            se_damage.Play();
        }
        if (health <= 0)
        {
            Destroy(gameObject);
            var pos = gameObject.transform.position;
            Instantiate(lose, pos, Quaternion.Euler(transform.forward) * Quaternion.Euler(0f, 0f, 0f));
        }
        /*
        invisibleTime = 1.5f;
        var material = this.GetComponent<MeshRenderer>().material;
        material.color = Color.yellow;
        GetComponent<BoxCollider>().enabled = false;
        */
        LinvisibleTime(1.5f);
    }
        
    void LinvisibleTime(float time)
    {
        invisibleTime = time;
        var material = this.GetComponent<MeshRenderer>().material;
        material.color = Color.yellow;
        GetComponent<BoxCollider>().enabled = false;
    }

    private void OnParticleCollision(GameObject obj)
    {
        Debug.Log("tag = " + obj.gameObject.tag);
        if (obj.gameObject.tag == "Laser")
        {
            return;
        }

        else if (obj.gameObject.tag == "EL")
        {
            Debug.Log("name = " + obj.name);
            Debug.Log("tag = " + obj.tag);

            var particle = obj.GetComponent<ParticleSystem>();
            var collisionEvents = new List<ParticleCollisionEvent>();
            var numCollisionEvents = particle.GetCollisionEvents(gameObject, collisionEvents);
            for (int i = 0; i < numCollisionEvents; i++)
            {
                Debug.Log(collisionEvents[i].intersection);
                GameObject obj2 = Instantiate(PVanishParticlePrefab);
                obj2.transform.position = collisionEvents[i].intersection;
                Destroy(obj2, 3.0f);
            }
            Destroy(obj);
            if (invisibleTime > 0.0f)
                return;

            health--;
            gameManager.SetPlayerHealthUI(health, maxHealth);
        }
        if (health >= 1)
        {
            se_damage.Play();
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        invisibleTime = 1.5f;
    }
}