using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformVertical : MonoBehaviour
{
    public float speed = 3;
    public Pose up;
    public Pose down;

    public bool way = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < up.position.y && way == false)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            if(gameObject.transform.position.y >= up.position.y)
            {
                way = true;
            }
        }
        else if (gameObject.transform.position.y > down.position.y && way == true)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            if(gameObject.transform.position.y <= down.position.y)
            {
                way = false;
            }
        }
    }
}
