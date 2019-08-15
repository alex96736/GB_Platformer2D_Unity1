using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletPlayer : MonoBehaviour
{
    public float speed = 5;
    private int EnemyLayer = 9;
    private int PlayerLayer = 12;
    private int CheckPointLayer = 14;
    private int GroundLayer = 10;
    private int BulletsLayer = 16;
    private int FinalDoorLayer = 15;
    private int EnemyGroundLayer = 11;
    public float damage = 10;
    [SerializeField] private float speedX_max = 5;
    private Rigidbody2D PlayerRigidbody;

    private Boolean FaceRight = true;
    private GameObject player;
    private AudioSource SoundPlayer;

    [SerializeField] private AudioClip HitSound;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        PlayerRigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
        player = GameObject.Find("Player_Snowman");
        FaceRight = player.GetComponent<Player>().FaceRight;
        SoundPlayer = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // проверка движениея пули, в зависимости от направления взгляда персонажа
        if (FaceRight)
        {
            PlayerRigidbody.AddForce(Vector2.right * speed * Time.deltaTime);
            if (PlayerRigidbody.velocity.x > speedX_max)
            {
                PlayerRigidbody.velocity = new Vector2(speedX_max, PlayerRigidbody.velocity.y);
            }

        } 
        else
        {
            PlayerRigidbody.AddForce(Vector2.left * speed * Time.deltaTime);
            if (PlayerRigidbody.velocity.x < -speedX_max)
            {
                PlayerRigidbody.velocity = new Vector2(-speedX_max, PlayerRigidbody.velocity.y);
            }

        }
    }

     private void OnTriggerEnter2D(Collider2D collision)
    {
    // проверка столкновения пули с другими объектами
      if (collision.gameObject.layer != PlayerLayer && collision.gameObject.layer != CheckPointLayer && collision.gameObject.layer != BulletsLayer && collision.gameObject.layer != FinalDoorLayer)
      {
        PlayerRigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
        SoundPlayer.PlayOneShot(HitSound, 0.5f);
        if (collision.gameObject.layer == EnemyLayer)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        if(SoundPlayer.isPlaying == false){
        Debug.Log ("Destroy snowball");
        Destroy(gameObject);}
      }
    }
}
