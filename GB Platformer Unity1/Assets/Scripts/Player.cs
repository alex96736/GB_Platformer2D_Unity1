using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private float hp = 100;
    [SerializeField] private float speed = 3;

    private int EnemyGroundLayer = 11;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform bullet_spawn;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        var h = Input.GetAxis("Horizontal");
        if (h>0)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if (h<0)
        {  
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
			Jump();
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            Shoot();
        }
    }

    void Jump()
    {
        
    }

    void Shoot()
    {
        var newBullet = GameObject.Instantiate(bullet, bullet_spawn.position, Quaternion.identity);
        Debug.Log($"Player shoot");
        Destroy(newBullet, 2);
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        Debug.Log($"Player take damage {damage}, hp = {hp}");
        if (hp<=0) 
        {
            Death();
            Debug.Log($"Player death");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == EnemyGroundLayer)
        {
            Debug.Log("Player collision EnemyGround");
            TakeDamage(100);
        }
    }

    void Death()
    {
        Destroy(gameObject, 3);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
