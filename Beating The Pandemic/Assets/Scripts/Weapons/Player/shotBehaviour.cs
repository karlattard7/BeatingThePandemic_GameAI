using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage;
    public bool fancyGravity = true;
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            Debug.Log("Arrow Collided with: " + collision.gameObject.name+" =t: "+collision.gameObject.tag);
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {   
            collision.gameObject.GetComponent<enemyBehaviour>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (!collision.CompareTag("Player"))
    //    {
    //        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
    //    }
    //    if (collision.CompareTag("Enemy"))
    //    {
    //        collision.GetComponent<enemyBehaviour>().TakeDamage(damage);
    //        Destroy(gameObject);
    //    }
    //}

    public void setTimeout(float t)
    {
        Destroy(gameObject, t);
    }
    public void setDamage(float d)
    {
        this.damage = d;
    }
}
