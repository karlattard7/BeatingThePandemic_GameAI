//using NUnit.Framework;
using System.Collections;
using System.Data.Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Vector2 = UnityEngine.Vector2;

public class playerBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 5f;

    public float health = 100f;

    public Rigidbody2D rb;
    public Animator animator;
    public Color collideColor = Color.white;
    public int potionsCount = 0; //potion system
    public GameObject hand; // loot system
    public float potionHealAmount = 10f;

    private float nextAttackTime = 0f;
    private Vector2 movement;
    public KeyCode attackCode = KeyCode.Space;
    private SpriteRenderer spRender;
    private PlayerHealthBar healthBar;

    public bool clickToAttack = false;
    private float maxHealth;

    private SetCounter potCounter;

    private OptionsMenu optMenu;

    public bool debug = false;


    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");

        if (objs.Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    public void syncHealthToBar()
    {
        if (healthBar)
            healthBar.SetHealth(health);
        else
        {
            if (GameObject.FindGameObjectWithTag("HealthBar"))
                healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<PlayerHealthBar>();

            if (!healthBar)
                Debug.LogError("HEALTHBAR NOT FOUND!");
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetHealth(health);
        }
    }
    private void Start()
    {
        //    if (GameObject.FindGameObjectWithTag("potCounter"))
        //        potCounter = GameObject.FindGameObjectWithTag("potCounter").GetComponent<SetCounter>();
        //    else
        //        Debug.LogError("potion ocunter ui not found ! add relevant tag please");

        //if (GameObject.FindGameObjectWithTag("optionsMenu"))
        //    optMenu = GameObject.FindGameObjectWithTag("optionsMenu").GetComponent<OptionsMenu>();
        //else
        //    Debug.LogError("options menu ui not found ! add relevant tag please");

        //potCounter.setCounter(potionsCount);




        spRender = GetComponentInChildren<SpriteRenderer>();
        maxHealth = health;
    }

    public void checkDebug()
    {
        if (debug)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                potionsCount += 1;
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                TakeDamage(maxHealth);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    if (enemy.GetComponent<enemyBehaviour>())
                        enemy.GetComponent<enemyBehaviour>().TakeDamage(100);
                }
            }
        }
    }
    // Update is called once per frame
    private void Update()
    {
        checkDebug();
        syncHealthToBar();
        checkPotions();
        if (OptionsMenu.isClick)
            attackCode = KeyCode.Mouse0;
        //this isnt in the start since the weapon might change
        meleeWeaponBehaviour[] meleeWeapons = GetComponentsInChildren<meleeWeaponBehaviour>();
        rangedWeaponBehaviour[] rangedWeapons = GetComponentsInChildren<rangedWeaponBehaviour>();



        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("speed", movement.sqrMagnitude);

        if (Input.GetKeyDown(attackCode))
        {
            if (nextAttackTime <= 0)
            {
                if (meleeWeapons.Length != 0)
                {
                    //meleeWeapon ATtack
                    meleeWeapons[0].use();
                    nextAttackTime = meleeWeapons[0].attackSpeed;
                }
                else if (rangedWeapons.Length != 0)
                {
                    //ranged weapon attack
                    rangedWeapons[0].use();

                    nextAttackTime = rangedWeapons[0].attackSpeed;
                }
                else
                {
                    Debug.LogError("Error, attack called with no weapon in hand!!! fix it ?");
                }
            }
        }
        nextAttackTime -= Time.deltaTime;


    }
    public void checkPotions()
    {
        if (Input.GetKeyDown(KeyCode.Q) && potionsCount > 0 && health < maxHealth)
        {
            Heal();
            SetCounter.counter = potionsCount;
        }
        SetCounter.counter = potionsCount;
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void Die()
    {
        SoundManager.PlaySound("scream");

        Scene scene = SceneManager.GetActiveScene();
        health = maxHealth;
        SceneManager.LoadScene(scene.name);
    }

    public void TakeDamage(float damage)
    {
        this.health -= damage;
        if (healthBar)
            healthBar.SetHealth(health);
        Debug.Log("player took " + damage + " damage");
        StartCoroutine("flashSptrite");
        if (this.health <= 0)
        {

            Die();
        }
        //instanitate Blood
    }

    public void EquipWeapon(GameObject weapon) // weapon change - loot system
    {


        int childCount = hand.transform.childCount;
        if (childCount > 0)
        {
            for (int i = 0; i < childCount; i++)
            {
                if (hand.transform.GetChild(i).GetComponent<meleeWeaponBehaviour>())
                    hand.transform.GetChild(i).GetComponent<meleeWeaponBehaviour>().lockRight = true;

                if (hand.transform.GetChild(i).GetComponent<fumigator>())
                    hand.transform.GetChild(i).GetComponent<fumigator>().lockRight = true;

                Destroy(hand.transform.GetChild(i).gameObject);
            }
        }


        if (weapon)
            if (weapon.tag == "Weapon")
            {
                if (weapon.GetComponent<meleeWeaponBehaviour>())
                {
                    Debug.Log("melee!");
                    weapon.GetComponent<meleeWeaponBehaviour>().hand = hand;
                    weapon.GetComponent<meleeWeaponBehaviour>().enabled = true; // this activates weapon script, otherwise there is an exception error with swords pick-up
                                                                                
                }

            }
        if (weapon.GetComponent<fumigator>())
        {
            Debug.Log("fumigat!");
            weapon.GetComponent<fumigator>().enabled = true; // this activates weapon script, otherwise there is an exception error with swords pick-up
        }
        else if (weapon.GetComponent<rangedWeaponBehaviour>())
        {
            Debug.Log("ranged!");
            weapon.GetComponent<rangedWeaponBehaviour>().enabled = true; // this activates weapon script, otherwise there is an exception error with swords pick-up
        }



        weapon.transform.parent = hand.transform;
        weapon.transform.localPosition = Vector3.zero;
    }






    private void Heal() // potion system
    {
        SoundManager.PlaySound("potion");
        potionsCount -= 1;

        if (health < maxHealth)
        {
            health += potionHealAmount;
            if (health > maxHealth)
                health = maxHealth;
            Debug.Log("My health is " + health);
        }
    }

    // Functions to be used as Coroutines MUST return an IEnumerator
    IEnumerator flashSptrite()
    {
        spRender.material.color = collideColor;
        yield return new WaitForSeconds(.1f);
        spRender.material.color = Color.white;
        yield return new WaitForSeconds(.1f);
    }

}