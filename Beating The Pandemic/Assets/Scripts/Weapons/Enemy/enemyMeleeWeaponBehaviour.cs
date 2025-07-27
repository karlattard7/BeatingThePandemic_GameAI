using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class enemyMeleeWeaponBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage;
    public float attackSpeed;
    public float range;

    //public bool damageOverTime;
    //public float timeDamage;

    public Animator animator;
    public LayerMask PlayerLayer;
    public Transform hitPoint;
    
    private float xNormal = 1f;
    public Boolean flipX;

    private GameObject player;


        void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (flipX)
        {
            xNormal = -1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player){
            if (player.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector2(-xNormal, 1f);
            }
            else
            {
                transform.localScale = new Vector2(xNormal, 1f);
            }
        }
    }

    public void use()
    {
        //play animation
        if(animator)
            animator.SetTrigger("attack"); 

        //check for collisions in this.range
        Collider2D[] hits = Physics2D.OverlapCircleAll(hitPoint.position, range, PlayerLayer); 
        foreach(Collider2D hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                hit.GetComponent<playerBehaviour>().TakeDamage(this.damage);
            }
        }
        //if collision.tag = enemy { collider.damage(this.damage));
        

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitPoint.position, range);

    }

    public static GameObject FindParentWithTag(GameObject childObject, string tag)
    {
        Transform t = childObject.transform;
        while (t.parent != null)
        {
            if (t.parent.tag == tag)
            {
                return t.parent.gameObject;
            }
            t = t.parent.transform;
        }
        return null; // Could not find a parent with given tag.
    }

}
