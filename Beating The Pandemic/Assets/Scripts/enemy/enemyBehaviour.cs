using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Linq;
using Random = UnityEngine.Random;

public class enemyBehaviour : MonoBehaviour
{
    public bool destroyOnContact;
    public bool DamageOnContact = false;
    public bool dropLootOnDeath = true;

    public Animator animator;
    public float health;
    public float attackSpeed;
    public bool waitBeforeFirstAttack = true;
    private bool firstAttack = false;

    public AIPath aiPath;
    public bool sound = true;


    public GameObject HitBloodPrefab;
    public GameObject DeathBloodPrefab;
    public float bloodTimeout = 30f;
    public float destroyAfterDeathTimeout = 0f;
    public float bloodSizeScale = 1f;
    public float deathBloodSizeScale = 1f;
    public bool randomBlood = false;


    public float dazedMultiplier = 0.2f;
    public bool dazeOnHit = true;
    public float dazeTime = 2f;


    public Color collideColor = Color.white;
    private SpriteRenderer spRender;

    public bool flipSpriteOnMove = false;
    public bool flipX = false;

    public bool distanceBased;
    public float playerFollowDistance;

    public bool lineOfSightBased;


    protected bool dead = false;
    protected bool dazed = false;
    protected float unDazeTime;
    protected float maxSpeed;
    protected float nextAttackTime;
    protected float normalScale = 1f;

    protected GameObject player;
    protected GameObject[] bloodPrefabs;
    protected LootSpawner dropLoot; // loot system

    protected virtual void Start()
    {
         //loot system
        if (GameObject.Find("GameManager"))
        {
            dropLoot = GameObject.Find("GameManager").GetComponent<LootSpawner>();
        }
        else
        {
            Debug.LogError("Game manager was not found !!");
        }
        spRender = GetComponentInChildren<SpriteRenderer>();

        if (flipX)
            normalScale = -transform.localScale.x;
        else
            normalScale = transform.localScale.x;
        if (aiPath)
            maxSpeed = aiPath.maxSpeed;

        player = GameObject.FindGameObjectWithTag("Player");

        if (randomBlood)
        {
            bloodPrefabs = Resources.LoadAll<GameObject>("BloodPrefabs");
        }


    }


    // Update is called once per frame
    protected virtual void Update()
    {

        if (flipSpriteOnMove)
        {
            if (aiPath.desiredVelocity.x >= 0.01f)
            {
                transform.localScale = new Vector2(normalScale, transform.localScale.y);
            }
            else if (aiPath.desiredVelocity.x <= -0.01f)
            {
                transform.localScale = new Vector2(-normalScale, transform.localScale.y);
            }



        }

        if (distanceBased)
        {
            float distance = Vector2.Distance(player.transform.position, gameObject.transform.position);
            if (distance < playerFollowDistance)
            {
                aiPath.canMove = aiPath.canSearch = false;
            }
            else
            {
                aiPath.canMove = aiPath.canSearch = true;
            }
        }

        if (lineOfSightBased)
        {
            RaycastHit hit;
            Vector2 direction = player.transform.position - transform.position;
            if (Physics.Raycast(transform.position, direction, out hit))
            {
                if (hit.transform == player)
                    aiPath.canMove = aiPath.canSearch = true;
                else
                    aiPath.canMove = aiPath.canSearch = false;
            }

            float distance = Vector2.Distance(player.transform.position, gameObject.transform.position);
            if (distance < playerFollowDistance)
            {
                aiPath.canMove = aiPath.canSearch = false;
            }
            else
            {
                aiPath.canMove = aiPath.canSearch = true;
            }
        }


        if (dazed && Time.time >= unDazeTime)
            unDaze();
        if (aiPath)
            if (aiPath.reachedEndOfPath)
            {
                if (waitBeforeFirstAttack && firstAttack)
                {
                    nextAttackTime = Time.time + attackSpeed;
                    firstAttack = false;
                }

                if (Time.time >= nextAttackTime)
                {
                    if (animator)
                    {
                        animator.SetTrigger("attack");
                        animator.SetBool("attack", true);
                    }
                    enemyMeleeWeaponBehaviour[] melees = GetComponentsInChildren<enemyMeleeWeaponBehaviour>();
                    enemyRangedWeaponBehaviour[] rangeds = GetComponentsInChildren<enemyRangedWeaponBehaviour>();
                    if (melees.ToList().Count > 0)
                    {
                        melees[0].use();
                        Debug.Log("enemy using melee!");
                    }
                    if (rangeds.ToList().Count > 0)
                    {
                        rangeds[0].use();
                        Debug.Log("enemy using ranged!");
                    }
                    nextAttackTime = Time.time + attackSpeed;
                }
                else if (!firstAttack)
                {
                    if (animator)
                        animator.SetTrigger("idle");
                }
            }
            else
            {
                firstAttack = true;
                if (animator != null && !this.dead)
                {
                    animator.SetBool("attack", false);
                    animator.SetTrigger("move");
                }

                    
                
            }

    }

    public virtual void TakeDamage(float x)
    {
        GameObject blood;
        if(sound)
            SoundManager.PlaySound("damage");  // sound
        if (randomBlood)
        {
            DeathBloodPrefab = bloodPrefabs[Random.Range(0, bloodPrefabs.Length - 1)];
            HitBloodPrefab = bloodPrefabs[Random.Range(0, bloodPrefabs.Length - 1)];
        }


        this.health -= x;

        //if dies
        if (this.health <= 0)
        {
            if (DeathBloodPrefab != null)
            {
                blood = Instantiate(DeathBloodPrefab, gameObject.transform.position, Quaternion.identity);
                blood.transform.localScale = new Vector2(deathBloodSizeScale, deathBloodSizeScale);
                Destroy(blood, bloodTimeout);
            }
            Die();
        }

        //if survives
        if (HitBloodPrefab != null)
        {
            blood = Instantiate(HitBloodPrefab, gameObject.transform.position, Quaternion.identity);
            blood.transform.localScale = new Vector2(bloodSizeScale, bloodSizeScale);
            Destroy(blood, bloodTimeout);
        }
        daze();
        StartCoroutine("flashSptrite");
    }

    public virtual void daze()
    {
        //maxSpeed = aiPath.maxSpeed;
        if (dazeOnHit)
        {
            aiPath.maxSpeed *= dazedMultiplier;
            dazed = true;
            unDazeTime = Time.time + dazeTime;
        }
    }
    public virtual void unDaze()
    {
        if (dazeOnHit)
        {
            aiPath.maxSpeed = maxSpeed;
            dazed = false;
        }
    }

    public virtual void Die()
    {
        if(dropLoot && dropLootOnDeath)
            dropLoot.CalculateLoot(gameObject.transform); // loot system
        AudioSource audio = GetComponent<AudioSource>();
        if (audio)        
            audio.Stop();
        
        GetComponent<Collider2D>().enabled = false;
        if (aiPath)
            aiPath.enabled = false;
        if (!dead && animator)
            animator.SetTrigger("die");
        this.dead = true;
        Destroy(gameObject, destroyAfterDeathTimeout);
    }

    public bool isDead()
    {
        return dead;
    }

    IEnumerator flashSptrite()
    {
        //for (int i = 0; i < 5; i++)
        //{
        Debug.Log("flashing enemy");
        spRender.material.color = collideColor;
        yield return new WaitForSeconds(.1f);
        spRender.material.color = Color.white;
        yield return new WaitForSeconds(.1f);
        //}
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!DamageOnContact)
            return;
        if (collision.collider.CompareTag("Player"))
        {
            GetComponentInChildren<enemyMeleeWeaponBehaviour>().use();
            if (destroyOnContact)
                Destroy(gameObject);
        }
    }

    public void setHealth(float h)
    {
        this.health = h;
    }
}
