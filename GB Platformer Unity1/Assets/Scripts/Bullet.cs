using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    public float speed = 5;
    private int EnemyLayer = 9;
    public float damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == EnemyLayer)
        {
            Debug.Log ("Destroy snowball");
            Destroy(gameObject);
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

}
