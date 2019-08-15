using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	[SerializeField] private float hp = 25;
    [SerializeField] private float speed = 3;
    private float TempSpeed;
    [SerializeField] private float startVelocity = 1;
    [SerializeField] private float maxSpeed = 5;
    [SerializeField] private float AngryModeRadius = 4;
    private int PlayerLayer = 12;
    private int EnemyGroundLayer = 11;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform bullet_spawn;

    [SerializeField] private bool Angry = false;
    [SerializeField] private Transform target;
    [SerializeField] private float shoot_distance=2;
    private Rigidbody2D EnemyRigidbody;
    private AudioSource SoundPlayer;

    [SerializeField] private AudioClip ShootSound;
    [SerializeField] private AudioClip DeathSound;
    public bool way = true;
    private bool fire = true;

    // ограничения движения
    public Pose left;
    public Pose right;
    
    // Start is called before the first frame update
    void Start()
    {
        TempSpeed = maxSpeed;
        SoundPlayer = GetComponent<AudioSource>();
        EnemyRigidbody = GetComponent<Rigidbody2D>();
        EnemyRigidbody.freezeRotation = true;
    }

    void FixedUpdate()
    {

    }

    /// <summary>
    /// Выстрел пулей
    /// </summary>
    void Shoot()
    {
        SoundPlayer.PlayOneShot(ShootSound, 0.8f);
        var newBullet = GameObject.Instantiate(bullet, bullet_spawn.position, Quaternion.identity);
        if(transform.position.x > target.position.x)
        {
            newBullet.GetComponent<BulletEnemy>().FaceRight = false;
        } 
        else
        {
            newBullet.GetComponent<BulletEnemy>().FaceRight = true;
        }
        fire = false;
        Invoke("FireLock", 1);
        Debug.Log($"Enemy shoot");
        Destroy(newBullet, 2);
    }

    /// <summary>
    /// Задержка для выстрелов
    /// </summary>
    private void FireLock()
    {
        fire = true;
    } 


    /// <summary>
    /// Получение урона
    /// </summary>
    public void TakeDamage(float damage)
    {
        hp -= damage;
        Debug.Log($"Enemy take damage {damage}, hp = {hp}");
        if (hp<=0) 
        {
            Debug.Log($"Enemy death");
            SoundPlayer.PlayOneShot(DeathSound, 0.3f);
            EnemyRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            Invoke("Death",0.3f);
        }
    }

    private void Death()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject);
    }

    /// <summary>
    /// Разворот персонажа
    /// </summary>
    void Flip()
    {
        gameObject.GetComponent<SpriteRenderer>().flipX=!gameObject.GetComponent<SpriteRenderer>().flipX;
        bullet_spawn.localPosition = new Vector3(-bullet_spawn.localPosition.x, bullet_spawn.localPosition.y, bullet_spawn.localPosition.z);
        startVelocity = -startVelocity;
        EnemyRigidbody.velocity = new Vector2(startVelocity,EnemyRigidbody.velocity.y);
        way = !way;
    }

    void Update()
    {
        Angry = AngryMode();
        if(Angry == false)
        {
            maxSpeed = TempSpeed;
            // движение в правую сторону
            if (EnemyRigidbody.position.x < right.position.x && way == true)
            {
                EnemyRigidbody.AddForce(Vector2.right * speed * Time.deltaTime);
                // блокировка сильного разгона платформы
                if (EnemyRigidbody.velocity.x > maxSpeed)
                {
                    EnemyRigidbody.velocity = new Vector2(maxSpeed, EnemyRigidbody.velocity.y);
                }
                // смена направления
                if(EnemyRigidbody.position.x > right.position.x-1)
                {
                    Flip();
                }
            }

            if (EnemyRigidbody.position.x > left.position.x && way == false)
            {
                // движение в левую сторону
                EnemyRigidbody.AddForce(Vector2.left * speed * Time.deltaTime);
                // блокировка сильного разгона платформы
                if (EnemyRigidbody.velocity.x < -maxSpeed)
                {
                    EnemyRigidbody.velocity = new Vector2(-maxSpeed, EnemyRigidbody.velocity.y);
                }
                // смена направления
                if(EnemyRigidbody.position.x < left.position.x+1)
                {
                    Flip();
                }
            }
        } 
        else
        {
            // движение в правую сторону
            if (EnemyRigidbody.position.x < right.position.x && gameObject.transform.position.x < target.position.x)
            {
                if(gameObject.GetComponent<SpriteRenderer>().flipX == true)
                {
                    Flip();
                }

                // торможение
                if(EnemyRigidbody.position.x > right.position.x-1)
                {
                    maxSpeed = 0;
                }
                else
                {
                    maxSpeed = TempSpeed;
                    EnemyRigidbody.AddForce(Vector2.right * speed * Time.deltaTime);
                }
                // блокировка сильного разгона платформы
                if (EnemyRigidbody.velocity.x > maxSpeed)
                {
                    EnemyRigidbody.velocity = new Vector2(maxSpeed, EnemyRigidbody.velocity.y);
                }
                // проверка дистанции для стрельбы
                if(Vector2.Distance(transform.position, target.position) < shoot_distance && fire == true)
                {
                    Shoot();
                }
            }

            // движение в левую сторону
            if (EnemyRigidbody.position.x > left.position.x && gameObject.transform.position.x > target.position.x)
            {
                if(gameObject.GetComponent<SpriteRenderer>().flipX == false)
                {
                    Flip();
                }
                // торможение
                if(EnemyRigidbody.position.x < left.position.x+1)
                {
                    maxSpeed = 0;
                    EnemyRigidbody.velocity = new Vector2(-EnemyRigidbody.velocity.x,EnemyRigidbody.velocity.y);
                }
                // движение
                else
                {
                    maxSpeed = TempSpeed;
                    EnemyRigidbody.AddForce(Vector2.left * speed * Time.deltaTime);
                }
                // блокировка сильного разгона платформы
                if (EnemyRigidbody.velocity.x > -maxSpeed)
                {
                    EnemyRigidbody.velocity = new Vector2(-maxSpeed, EnemyRigidbody.velocity.y);
                }
                // проверка дистанции для стрельбы
                if(Vector2.Distance(transform.position, target.position) < shoot_distance && fire == true)
                {
                    Shoot();
                }
            }
            
        }

    }

    /// <summary>
    /// Проверка видимости игрока врагом
    /// </summary>
    public bool AngryMode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, AngryModeRadius);
        foreach (Collider2D col in colliders)
        {
            if (col.gameObject.layer == PlayerLayer)
            {
                target = col.gameObject.transform;
                return true;
            }
        }
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // проверка столкновеия с EnemyGround
        if (collision.gameObject.layer == EnemyGroundLayer)
        {
            Debug.Log("Enemy collision EnemyGround");
            TakeDamage(100);
        }
    }
}
