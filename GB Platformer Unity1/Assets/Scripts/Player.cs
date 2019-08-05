using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private float hp = 100;
    [SerializeField] private float speed = 4;
    [SerializeField] private float jumpForce = 15;

    private int EnemyGroundLayer = 11;
    private int LootLayer = 13;
    private int GroundLayer = 10;
    private int CheckPointLayer = 14;
    public int Keys = 0;
    public bool FaceRight=true;
    private Rigidbody2D PlayerRigidbody;
    [SerializeField] private Transform CheckPoint;
    [SerializeField] private Transform groundChecker;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bullet_spawn;
    [SerializeField] private Vector3 FaceLeftPosition_BulletSpawn;
    [SerializeField] private Vector3 FaceRightPosition_BulletSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        FaceLeftPosition_BulletSpawn.Set(-bullet_spawn.localPosition.x, bullet_spawn.localPosition.y, bullet_spawn.localPosition.z);
        FaceRightPosition_BulletSpawn.Set(bullet_spawn.localPosition.x, bullet_spawn.localPosition.y, bullet_spawn.localPosition.z);
        
    }

    void FixedUpdate()
    {
        
        var h = Input.GetAxis("Horizontal");
        if (h>0)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            gameObject.GetComponent<SpriteRenderer>().flipX=true;
            bullet_spawn.localPosition = FaceRightPosition_BulletSpawn;
            FaceRight = true;
        }
        if (h<0)
        {  
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            gameObject.GetComponent<SpriteRenderer>().flipX=false;
            bullet_spawn.localPosition = FaceLeftPosition_BulletSpawn;
            FaceRight = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1))
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
        if (CheckGround())
        {
            PlayerRigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    Boolean CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundChecker.position, 0.3f);
        foreach (Collider2D col in colliders)
        {
            if (col.gameObject.layer == GroundLayer)
            {
                return true;
            }
        }
        return false;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LootLayer)
        {
            collision.gameObject.GetComponent<Key>().Death();
            Debug.Log("Player take key");
            Keys++;
        }
        if (collision.gameObject.layer == CheckPointLayer)
        {
            CheckPoint = collision.gameObject.transform;
            Debug.Log("Player reach checkpoint"); 
        }
    }

    void Death()
    {
        if (CheckPoint != null)
        {
            gameObject.transform.position = CheckPoint.position;
            hp = 100;
        } 
        else 
        {
            Destroy(gameObject,2);
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}
