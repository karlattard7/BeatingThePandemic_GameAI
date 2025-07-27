using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class rangedWeaponBehaviour : MonoBehaviour
{
    public float damage;
    public string weaponName;
    public float range;
    public float projectileSpeed = 500f;
    public float attackSpeed;

    public float timeout = 120f;

    public GameObject projectilePrefab;

    public List<GameObject> shots;

    public float weaponOffset = 0f;
    public float projectileOffset = 0f;
 
    public Transform shootPos;

    public Animator animator;

    public bool lockRight = false;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + weaponOffset);

        if (shots != null && shots.Count>0)
            foreach(GameObject shot in shots.ToList())
            {
                if(shot == null)
                {
                    shots.Remove(shot);
                }
                else if (Vector2.Distance(gameObject.transform.position, shot.GetComponent<Rigidbody2D>().position) > range){
                    shots.Remove(shot);
                    Destroy(shot);
                }
            }
        
    }
    public virtual void use()
    {

        animator.SetTrigger("attack");

        //play shoot animation
        Vector2 mouseLoc = Input.mousePosition;//get player mouse position


        Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = (Input.mousePosition - sp).normalized;

        Quaternion shotRotation = transform.rotation * Quaternion.Euler(0f,0f,projectileOffset);
        GameObject shot = Instantiate(projectilePrefab, shootPos.position, shotRotation);

        shot.GetComponent<shotBehaviour>().setTimeout(timeout);
        shot.GetComponent<shotBehaviour>().setDamage(damage);
        shots.Add(shot);
        Rigidbody2D shotRB = shot.GetComponent<Rigidbody2D>();

        shotRB.AddForce(dir * projectileSpeed);

        //here add player pos, 
        //direction which is mouse and velocity which is this.projectileSpeed



        //maybe let the projectile check for collions then callback with what it hit ?? 
        //or just pass the damage amount to it and let the projectile script do the damage
    }
}
