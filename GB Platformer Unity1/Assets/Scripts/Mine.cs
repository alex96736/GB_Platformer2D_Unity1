using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private Sprite Mine1;
    [SerializeField] private Sprite Mine2;
    [SerializeField] private float RadiusHit = 4;
    private int PlayerLayer = 12;
    private int EnemyLayer = 9;
    [SerializeField] private float power = 750;
    [SerializeField] private float damage = 50;
    [SerializeField] private bool activated = false;
    private SpriteRenderer MineSpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        MineSpriteRenderer = GetComponent<SpriteRenderer>();
        if (activated == true)
        {
            InvokeRepeating("ChangeSprite",0,0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerExit2D(Collider2D collision){
        // проврека активации мины, игрок должен немног оотойти от нее
        // активная мина начинает мигать
        if (collision.gameObject.layer == PlayerLayer) {
          activated = true;
          InvokeRepeating("ChangeSprite",0,0.5f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        // Если мина аткивна, то идет проврека на взаимодействие с ней
        if(activated)
        {
             if (collision.gameObject.layer == PlayerLayer || collision.gameObject.layer == EnemyLayer) 
             {
                activated = false;
                Hit();
                GameObject.Destroy(gameObject);
             }
        }
    }

    /// <summary>
    /// Взрыв мины
    /// </summary>
    void Hit()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, RadiusHit);
        foreach (Collider2D collision in colliders)
        {
            if (collision.gameObject.layer == PlayerLayer || collision.gameObject.layer == EnemyLayer)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce((collision.gameObject.transform.position - gameObject.transform.position)*power);
                if(collision.gameObject.layer == PlayerLayer)
                {
                    collision.gameObject.GetComponent<Player>().TakeDamage(damage);
                } 
                if(collision.gameObject.layer == EnemyLayer)
                {
                    collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                }
            }
        }
    }

    /// <summary>
    /// Смена изображения мины (мигание)
    /// </summary>
    void ChangeSprite()
    {
        if(MineSpriteRenderer.sprite == Mine1)
        {
            MineSpriteRenderer.sprite = Mine2;
        } 
        else
        {
            MineSpriteRenderer.sprite = Mine1;
        }

    }
}

