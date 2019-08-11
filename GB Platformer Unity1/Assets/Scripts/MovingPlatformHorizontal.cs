using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformHorizontal : MonoBehaviour
{
    public float speed = 30;
    public float maxSpeed = 2;
    public float StartVelocity = 1;
    
    // ограничения движения
    public Pose left;
    public Pose right;

    private float y;
    private Rigidbody2D PlatformRigidbody;

    public bool way = true;


    void Start()
    {
        PlatformRigidbody = GetComponent<Rigidbody2D>();
        PlatformRigidbody.freezeRotation = true;
        y = PlatformRigidbody.position.y;
    }

    
    void Update()
    { // Движение платформы

        // блокировка по оси Y
        PlatformRigidbody.position = new Vector2(PlatformRigidbody.position.x, y);

        // движение в правую сторону
        if (PlatformRigidbody.position.x < right.position.x && way == true)
        {
            PlatformRigidbody.AddForce(Vector2.right * speed * Time.deltaTime);
            // блокировка сильного разгона платформы
            if (PlatformRigidbody.velocity.x > maxSpeed)
            {
                PlatformRigidbody.velocity = new Vector2(maxSpeed, PlatformRigidbody.velocity.y);
            }
            // смена направления
            if(PlatformRigidbody.position.x > right.position.x-1)
            {
                PlatformRigidbody.velocity = new Vector2(-StartVelocity, PlatformRigidbody.velocity.y);
                way = false;
            }
        }
        else if (PlatformRigidbody.position.x > left.position.x && way == false)
        {
            // движение в левую сторону
            PlatformRigidbody.AddForce(Vector2.left * speed * Time.deltaTime);
            // блокировка сильного разгона платформы
            if (PlatformRigidbody.velocity.x < -maxSpeed)
            {
                PlatformRigidbody.velocity = new Vector2(-maxSpeed, PlatformRigidbody.velocity.y);
            }
            // смена направления
            if(PlatformRigidbody.position.x < left.position.x+1)
            {
                PlatformRigidbody.velocity = new Vector2(StartVelocity, PlatformRigidbody.velocity.y);
                way = true;
            }
        }
    }
}
