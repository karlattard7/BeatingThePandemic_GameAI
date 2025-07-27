using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelLocker : MonoBehaviour
{
    //not nice implementation but depsperate sorry :) 

    playerBehaviour player;
    public GameObject weaponPrefab;
    GameObject oldWeap;
    public bool working = true;
    void Start()
    {
        if (working)
        {
            if (GameObject.FindGameObjectWithTag("Player"))
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerBehaviour>();

                if (player.GetComponentInChildren<rangedWeaponBehaviour>())
                    oldWeap = player.GetComponentInChildren<rangedWeaponBehaviour>().gameObject;
                if (player.GetComponentInChildren<meleeWeaponBehaviour>())
                    oldWeap = player.GetComponentInChildren<meleeWeaponBehaviour>().gameObject;

                player.EquipWeapon(weaponPrefab);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
            if (!player.GetComponentInChildren<rangedWeaponBehaviour>() && working)
            {
                GameObject item = Instantiate(weaponPrefab, player.transform.position, Quaternion.identity);
                player.EquipWeapon(item);
            }

    }

    public void returnWeapAndDisable()
    {
        working = false;
        if (player)
            player.EquipWeapon(oldWeap);
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        working = false;
        if (player)
            player.EquipWeapon(oldWeap);
    }
}
