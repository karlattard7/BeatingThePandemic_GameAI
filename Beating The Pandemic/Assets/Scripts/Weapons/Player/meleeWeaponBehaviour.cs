using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class meleeWeaponBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage;
    public string weaponName;
    public float attackSpeed;
    public float range;

    public Animator animator;
    public LayerMask enemyLayer;
    public Transform hitPoint;
    public GameObject hand;

    public float xNormal = 1f;
    public Boolean flipX;

    private GameObject player;

    public bool currentlyFlipped = false;

    public bool lockRight = false;

    void Start()
    {
        if(gameObject.transform.parent)
            hand = gameObject.transform.parent.gameObject;

        player = GameObject.FindWithTag("Player");

        if (flipX)
        {
            xNormal = -1f;
        }
    }

    // Update is called once per frame
    void Update()
    {

        //update sword pos based on mouse pos 

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float handXPos = player.transform.position.x - 2 * transform.localPosition.x;
        if (mousePos.x > handXPos || lockRight)
        {
            currentlyFlipped = true;
            hand.transform.localScale = new Vector2(-xNormal, 1f);
        }
        else if (mousePos.x < handXPos)
        {
            currentlyFlipped = false;
            hand.transform.localScale = new Vector2(xNormal, 1f);
        }
    }

    public void use()
    {
        //play animation
        animator.SetTrigger("attack");

        //check for collisions in this.range
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(hitPoint.position, range, enemyLayer);

        //if collision.tag = enemy { collider.damage(this.damage));
        foreach (Collider2D enemy in enemiesHit)
        {
            Debug.Log("hit enemy! " + enemy.name);
            enemyBehaviour enemyScript = enemy.gameObject.GetComponent<enemyBehaviour>();
            if (enemyScript)
                enemyScript.TakeDamage(this.damage);
            else
                Debug.LogError("enemy script not found!");
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitPoint.position, range);

    }


}
