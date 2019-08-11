using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	[SerializeField] private float hp = 25;
    [SerializeField] private float speed = 3;
    [SerializeField] private float startVelocity = 1;
    [SerializeField] private float maxSpeed = 5;
    private int PlayerLayer = 12;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform bullet_spawn;

    [SerializeField] private bool Angry = false;
    private Rigidbody2D EnemyRigidbody;
    public bool way = true;

    // ограничения движения
    public Pose left;
    public Pose right;
    
    // Start is called before the first frame update
    void Start()
    {
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
        var newBullet = GameObject.Instantiate(bullet, bullet_spawn.position, Quaternion.identity);
        Debug.Log($"Enemy shoot");
        Destroy(newBullet, 2);
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
			Destroy(gameObject);
        }
    }

    /// <summary>
    /// Разворот персонажа
    /// </summary>
    void Flip()
    {
        gameObject.GetComponent<SpriteRenderer>().flipX=!gameObject.GetComponent<SpriteRenderer>().flipX;
        //bullet_spawn.localPosition = new Vector3(-bullet_spawn.localPosition.x, bullet_spawn.localPosition.y, bullet_spawn.localPosition.z);
        startVelocity = -startVelocity;
        EnemyRigidbody.velocity = new Vector2(startVelocity,EnemyRigidbody.velocity.y);
        way = !way;
    }

    void Update()
    {
        if(Angry == false)
        {
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // проверка на игрока в поле зрения
        if(collision.gameObject.layer == PlayerLayer)
        {
            Angry = true;
            Debug.Log("Angry ON!");
            gameObject.transform.LookAt(collision.gameObject.transform);
        }   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // проверка на выход игрока из поле зрения
        if(collision.gameObject.layer == PlayerLayer)
        {
            Debug.Log("Angry OFF!");
            Angry = false;
        }
    }

}
