using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class enemyRangedWeaponBehaviour : MonoBehaviour
{
    public float damage;
    public float range;
    public float projectileSpeed = 500f;
    public float attackSpeed;

    public float timeout = 120f;

    public GameObject projectilePrefab;
    public List<GameObject> shots;
    public float offset = 0f;
 
    public Transform shootPos;

    public Animator animator;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            Vector3 diff = player.transform.position - transform.position;
            float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);
        }
        if (shots != null && shots.Count>0)
            foreach(GameObject shot in shots.ToList())
            {
                if(shot == null)
                {
                    shots.Remove(shot);
                }
                else if (Vector2.Distance(gameObject.transform.position, shot.transform.position) > range){
                    shots.Remove(shot);
                    Destroy(shot);
                }
            }
        
    }
    public void use()
    {
        if(animator)
            animator.SetTrigger("attack");

        //play shoot animation
        if (player)
        {
            Vector3 playerLoc = player.transform.position;

            Vector3 dir = (playerLoc - shootPos.position).normalized;


            GameObject shot = Instantiate(projectilePrefab, shootPos.position, transform.rotation);

            shot.GetComponent<enemyShotBehaviour>().setTimeout(timeout);
            shot.GetComponent<enemyShotBehaviour>().setDamage(damage);
            shots.Add(shot);
            Rigidbody2D shotRB = shot.GetComponent<Rigidbody2D>();

            shotRB.AddForce(dir * projectileSpeed);
        }
        else
        {
            Debug.LogError("no player found to shoot at!");
        }

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
