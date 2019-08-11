using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformVertical : MonoBehaviour
{
    public float speed = 30;
    public float maxSpeed = 2;
    public float StartVelocity = 1;

    // ограничения движения
    public Pose up;
    public Pose down;
    private float x;
    private Rigidbody2D PlatformRigidbody;

    public bool way = false;

    void Start()
    {
         PlatformRigidbody = GetComponent<Rigidbody2D>();
         PlatformRigidbody.freezeRotation = true;
         x = PlatformRigidbody.position.x;
         
    }

    void Update()
    {// Движение платформы

        // блокировка по оси Х
        PlatformRigidbody.position = new Vector2(x, PlatformRigidbody.position.y);

        if (PlatformRigidbody.position.y < up.position.y && way == false)
        {
            // движение вверх
            PlatformRigidbody.AddForce(Vector2.up * speed * Time.deltaTime);
            // блокировка сильного разгона платформы
            if (PlatformRigidbody.velocity.y > maxSpeed)
            {
                PlatformRigidbody.velocity = new Vector2(PlatformRigidbody.velocity.x, maxSpeed);
            }
            // смена направления
            if(PlatformRigidbody.position.y > up.position.y-1)
            {
                PlatformRigidbody.velocity = new Vector2(PlatformRigidbody.velocity.x, -StartVelocity);
                way = true;
            }
        }
        else if (PlatformRigidbody.position.y > down.position.y && way == true)
        {
            // движение вниз
            PlatformRigidbody.AddForce(Vector2.down * speed * Time.deltaTime);

            // блокировка сильного разгона платформы
            if (PlatformRigidbody.velocity.y < -maxSpeed)
            {
                PlatformRigidbody.velocity = new Vector2(PlatformRigidbody.velocity.x, -maxSpeed);
            }
            // смена направления
            if(PlatformRigidbody.position.y < down.position.y+1)
            {
                PlatformRigidbody.velocity = new Vector2(PlatformRigidbody.velocity.x, StartVelocity);
                way = false;
            }
        }
    }
}
