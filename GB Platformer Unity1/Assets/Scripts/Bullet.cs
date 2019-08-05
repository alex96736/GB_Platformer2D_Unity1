using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    public float speed = 5;
    private int EnemyLayer = 9;
    private int PlayerLayer = 12;
    private int GroundLayer = 10;
    private int EnemyGroundLayer = 11;
    public float damage = 10;

    private Boolean FaceRight = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FaceRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        } 
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            
      if (collision.gameObject.layer != PlayerLayer)
      {
        if (collision.gameObject.layer == EnemyLayer)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
        Debug.Log ("Destroy snowball");
        Destroy(gameObject);
      }
    }

}
