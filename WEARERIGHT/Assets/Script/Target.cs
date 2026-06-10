using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int stage;
    public GameObject boss;
    public Vector3 pos;
    public float time;
    private uint flashCount;
    private float flashAlpha;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        flashCount = 0;
        flashAlpha = 1;
        sprite = transform.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (stage == 1)
        {
            var pos = new Vector3(boss.transform.position.x - 1f, -0.65f, -2);
            gameObject.transform.position = pos;

            if (gameObject.transform.position.x <= 4f)
            {
                gameObject.transform.position = new Vector3(boss.transform.position.x - 1f, boss.transform.position.y + 0.355f, -2);

                flashAlpha = Mathf.Sin(Time.time * 20) / 2 + 0.5f;
                
                var color = sprite.color;
                color.a = flashAlpha;
                sprite.color = color;
                
                flashCount++;
                
                if (flashCount > 40)
                {
                    Destroy(gameObject);
                }
            }
        }
        else if (stage == 2)
        {
            var pos = new Vector3(boss.transform.position.x - 1f, 0f, -2);
            gameObject.transform.position = pos;

            if (gameObject.transform.position.x <= 4f)
            {
                gameObject.transform.position = new Vector3(boss.transform.position.x - 1f, boss.transform.position.y, -2);

                flashAlpha = Mathf.Sin(Time.time * 20) / 2 + 0.5f;

                var color = sprite.color;
                color.a = flashAlpha;
                sprite.color = color;

                flashCount++;

                if (flashCount > 40)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
