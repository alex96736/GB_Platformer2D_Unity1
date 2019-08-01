using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	 [SerializeField] private float hp = 25;
    [SerializeField] private float speed = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {

    }

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
