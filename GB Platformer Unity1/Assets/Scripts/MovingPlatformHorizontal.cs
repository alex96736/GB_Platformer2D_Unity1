using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformHorizontal : MonoBehaviour
{
    public float speed = 3;
    public Pose left;
    public Pose right;

    public bool way = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x < right.position.x && way == false)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            if(gameObject.transform.position.x >= right.position.x)
            {
                way = true;
            }
        }
        else if (gameObject.transform.position.x > left.position.x && way == true)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            if(gameObject.transform.position.x <= left.position.x)
            {
                way = false;
            }
        }
    }
}
