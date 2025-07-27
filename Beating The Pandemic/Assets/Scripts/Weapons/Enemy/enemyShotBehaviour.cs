using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShotBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage;
    public bool fancyGravity = true;
    public bool destroyOnFirstAnyCollide = false;
    public bool flipX = false;
    void Start()
    {
        if (flipX)
            gameObject.transform.localScale = new Vector3(-1f, 1, 1);
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.CompareTag("Player"))
        {   
            collision.gameObject.GetComponent<playerBehaviour>().TakeDamage(damage);
            Destroy(gameObject);
        }

        if (!collision.gameObject.CompareTag("Enemy"))
        {
            if (destroyOnFirstAnyCollide)

                Destroy(gameObject);
            else
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }

    public void setTimeout(float t)
    {
        Destroy(gameObject, t);
    }
    public void setDamage(float d)
    {
        this.damage = d;
    }
}
