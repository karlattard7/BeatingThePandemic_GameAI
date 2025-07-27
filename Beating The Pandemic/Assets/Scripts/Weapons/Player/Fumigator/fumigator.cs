using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fumigator : rangedWeaponBehaviour
{
    public GameObject particles;
    public float particleOffset;
    private GameObject spawnedParticles;
    private Quaternion particleRotate;
    private playerBehaviour playBeh;

    // Update is called once per frame
    protected override void Start()
    {
        base.Start();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player)
            playBeh = player.GetComponent<playerBehaviour>();
        //KeyCode fireButton = playBeh.attackCode;
    }
    protected override void Update()
    {
        float left = -1f;
        //if (flip)
        //    left = 1f;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float handXPos = GameObject.FindGameObjectWithTag("Player").transform.position.x - 2 * transform.localPosition.x;
        if (mousePos.x > handXPos || lockRight)
        {
            transform.localScale = new Vector2(1f, -left);
        }
        else if (mousePos.x < handXPos)
        {
            transform.localScale = new Vector2(1f, left);
        }



        //base.Update();
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + weaponOffset);
        particleRotate = Quaternion.Euler(0f, 0f, angle + particleOffset);


        

        if (Input.GetKey(playBeh.attackCode))
        {
            useFumig();
        }
    }

    public void useFumig()
    {
        animator.SetTrigger("isAttacking");

        //play shoot animation
        Vector2 mouseLoc = Input.mousePosition;//get player mouse position


        Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = (Input.mousePosition - sp).normalized;


        spawnedParticles = Instantiate(particles, shootPos.position, particleRotate);
        spawnedParticles.GetComponent<particleHandler>().damage = damage;
        Destroy(spawnedParticles,0.3f);
    }

    override public void use()
    {

    }

}
