using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private float hp = 100;
    [SerializeField] private float speed = 5;
    [SerializeField] private float speedX_max = 5;
    [SerializeField] private float speedY_max = 5;
    [SerializeField] private float jumpForce = 15;

    private int EnemyGroundLayer = 11;
    private int LootLayer = 13;
    private int GroundLayer = 10;
    private int CheckPointLayer = 14;
    private int BoxLayer = 17;
    private int EnemyLayer = 9;


    public int Keys = 0;
    public bool FaceRight=true;
    private Rigidbody2D PlayerRigidbody;
    [SerializeField] private Transform CheckPoint;
    [SerializeField] private Transform groundChecker;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject mine;
    [SerializeField] Transform bullet_spawn;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        PlayerRigidbody.freezeRotation = true;
        
    }

    /// <summary>
    /// Разворот персонажа
    /// </summary>
    void Flip()
    {
        gameObject.GetComponent<SpriteRenderer>().flipX=!gameObject.GetComponent<SpriteRenderer>().flipX;
        bullet_spawn.localPosition = new Vector3(-bullet_spawn.localPosition.x, bullet_spawn.localPosition.y, bullet_spawn.localPosition.z);
        FaceRight = !FaceRight;
        PlayerRigidbody.velocity = new Vector2(0,PlayerRigidbody.velocity.y);
    }
    void FixedUpdate()
    {
        // движение игрока
        var h = Input.GetAxis("Horizontal");
        if (h>0)
        {
            PlayerRigidbody.AddForce(Vector2.right * speed * Time.deltaTime);
            // блокировка разгона
            if (PlayerRigidbody.velocity.x > speedX_max)
            {
                PlayerRigidbody.velocity = new Vector2(speedX_max, PlayerRigidbody.velocity.y);
            }
            // проверка на разворот персонажа
            if (!FaceRight)
            {
                Flip();
            }
        }
        if (h<0)
        {  
            PlayerRigidbody.AddForce(Vector2.left * speed * Time.deltaTime);
            // блокировка разгона
            if (PlayerRigidbody.velocity.x < -speedX_max)
            {
                PlayerRigidbody.velocity = new Vector2(-speedX_max, PlayerRigidbody.velocity.y);
            }
            // проверка на разворот персонажа
            if (FaceRight)
            {
                Flip();
            }
        }

        // Пробел или ПКМ, прыжок
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1))
        {
			Jump();
            if (PlayerRigidbody.velocity.y > speedY_max)
            {
                PlayerRigidbody.velocity = new Vector2(PlayerRigidbody.velocity.x, speedY_max);
            }
        }

        // СКМ или Е, заложить мину
        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(2))
        {
			Mine();
        }
        // ЛКМ, выстрел
        if (Input.GetMouseButtonDown(0)) 
        {
            Shoot();
        }
    }

    /// <summary>
    /// Прыжок персонажа
    /// </summary>
    void Jump()
    {
        if (CheckGround())
        {
            PlayerRigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// Проврека нахождения на земле
    /// </summary>
    Boolean CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundChecker.position, 0.3f);
        foreach (Collider2D col in colliders)
        {
            if (col.gameObject.layer == GroundLayer || col.gameObject.layer == EnemyLayer || col.gameObject.layer == BoxLayer)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Заложить мину
    /// </summary>
    void Mine()
    {
        var newMine = GameObject.Instantiate(mine, gameObject.transform.position, Quaternion.identity);
        Debug.Log($"Mine planted");
    }

    /// <summary>
    /// Выстрел пулей
    /// </summary>
    void Shoot()
    {
        var newBullet = GameObject.Instantiate(bullet, bullet_spawn.position, Quaternion.identity);
        Debug.Log($"Player shoot");
        Destroy(newBullet, 2);
    }

    /// <summary>
    /// Получение урона
    /// </summary>
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
        // проверка столкновеия с EnemyGround
        if (collision.gameObject.layer == EnemyGroundLayer)
        {
            Debug.Log("Player collision EnemyGround");
            TakeDamage(100);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // проверка взаимодействия с ключами
        if (collision.gameObject.layer == LootLayer)
        {
            collision.gameObject.GetComponent<Key>().Death();
            Debug.Log("Player take key");
            Keys++;
        }
        // проверка взаимодействия с чекпоинтами
        if (collision.gameObject.layer == CheckPointLayer)
        {
            CheckPoint = collision.gameObject.transform;
            Debug.Log("Player reach checkpoint"); 
        }
    }

    /// <summary>
    /// Смерть персонажа
    /// </summary>
    void Death()
    {
        // проверка последнего чекпоинта
        if (CheckPoint != null)
        {
            gameObject.transform.position = CheckPoint.position;
            PlayerRigidbody.velocity = Vector2.zero;
            hp = 100;
        } 
        else 
        {
            Destroy(gameObject,2);
        }

    }
    /// <summary>
    /// Получение значения HP
    /// </summary>
    public float GetHP()
    {
        return hp;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
