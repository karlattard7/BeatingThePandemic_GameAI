using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class bossBehaviour : enemyBehaviour
{
    // Start is called before the first frame update
    private float maxHealth;
    public float startEndD;
    public GameObject book;

    public bool enraged = false;
    private int nextAttack = -1;
    private int[] rangedAttacks = { 2 };

    private EnemyHealthBar enemyHealthBar;


    protected override void Start()
    {
        aiPath = GetComponentInParent<AIPath>();
        animator = GetComponentInChildren<Animator>();
        maxHealth = health;
        startEndD = aiPath.endReachedDistance;
        enemyHealthBar = GameObject.FindGameObjectWithTag("BossHealthBar").GetComponent<EnemyHealthBar>();
        enemyHealthBar.SetMaxHealth(maxHealth);

    }

    public int[] GetRangedAttacks()
    {
        return rangedAttacks;
    }
    public int getNextAttack()
    {
        if (nextAttack == -1)
            nextAttack = Random.Range(0, 3);
        return nextAttack;
    }

    public bool nextAttackIsRanged()
    {
        foreach (int x in rangedAttacks)
            if (nextAttack == x)
                return true;
        return false;

    }

    // Update is called once per frame
    protected override void Update()
    {

    }

    public void attack()
    {
        if (!enraged)
            nextAttack = 0;

        if (nextAttack == -1)
            nextAttack = Random.Range(0, 3);
        switch (nextAttack)
        {
            case 0:
                animator.SetTrigger("attack");
                break;
            case 1:
                animator.SetTrigger("attack2");
                break;
            case 2:
                animator.SetTrigger("potionThrow");
                break;
            default:
                Debug.LogError("next Attack selector out of bounds! (check rand)");
                break;

        }
        if (!enraged)
            nextAttack = 0;
        else
            nextAttack = Random.Range(0, 3);
    }

    public override void TakeDamage(float d)
    {
        Debug.Log("boos took the damage");
        this.health -= d;
        enemyHealthBar.SetHealth(health);

        //if dies
        if (this.health <= 0)
        {

            if (DeathBloodPrefab != null)
            {
                GameObject blood = Instantiate(DeathBloodPrefab, gameObject.transform.position, Quaternion.identity);
                blood.transform.localScale = new Vector2(deathBloodSizeScale, deathBloodSizeScale);
                Destroy(blood, bloodTimeout);
            }
            Die();
        }


        if (health / maxHealth <= 0.5 && !enraged)
        {
            animator.SetTrigger("enrage");
            enraged = true;
        }

        //daze();

        if (!animator.GetBool("enraged"))
            animator.SetTrigger("hit");
        //StartCoroutine("flashBoss");

    }

    public override void Die()
    {
        dead = true;
        animator.SetTrigger("die");
        Instantiate(book, transform.position, Quaternion.identity);
    }

    //IEnumerator flashBoss()
    //{
    //    //for (int i = 0; i < 5; i++)
    //    //{
    //    Debug.Log("flashing Boss");
    //    GameObject head = GameObject.FindGameObjectWithTag("boss_head");
    //    head.GetComponent<Renderer>().material.color = Color.red;
    //    yield return new WaitForSeconds(.2f);
    //    head.GetComponent<Renderer>().material.color = Color.white;
    //    yield return new WaitForSeconds(.1f);
    //    //}
    //}

    public void startTextFlash()
    {
        StartCoroutine("flashText");
    }
    IEnumerator flashText()
    {
        while (enraged && animator)
        {
            GameObject.FindGameObjectWithTag("directionalText").GetComponent<directionalTextUpdater>().setColor(Color.red);
            yield return new WaitForSeconds(.3f);
            GameObject.FindGameObjectWithTag("directionalText").GetComponent<directionalTextUpdater>().setColor(Color.white);
            yield return new WaitForSeconds(.3f);
        }
        GameObject.FindGameObjectWithTag("directionalText").GetComponent<directionalTextUpdater>().setColor(Color.white);
    }
}
