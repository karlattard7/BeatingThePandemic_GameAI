using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Loot
{
    public GameObject item;
    public float chance;
}

public class LootSpawner : MonoBehaviour
{





    public Loot[] lootTable;
    public float dropChance;
    private int sceneNo;

    public List<GameObject> spawned;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");

        if (objs.Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    public void CalculateLoot(Transform trans)
    {
        float calc_dropChance = Random.Range(0f, 101f);

        if (calc_dropChance > dropChance)
        {
            Debug.Log("Sorry, no loot this time");
            return;
        }
        if (calc_dropChance <= dropChance)
        {
            Debug.Log("Loot should be dropped");
            float itemWeight = 0f;

            for (int i = 0; i < lootTable.Length; i++)
            {
                itemWeight += lootTable[i].chance;
            }

            float random = Random.Range(0f, itemWeight);

            for (int i = 0; i < lootTable.Length; i++)
            {
                if (random <= lootTable[i].chance)
                {

                    if (lootTable[i].item.GetComponent<meleeWeaponBehaviour>())
                        lootTable[i].item.GetComponent<meleeWeaponBehaviour>().enabled = false;
                    if (lootTable[i].item.GetComponent<rangedWeaponBehaviour>())
                        lootTable[i].item.GetComponent<rangedWeaponBehaviour>().enabled = false;

                    //esnures that no wepaons spawn in level 3 or 2 secret room
                    //if ((!GameObject.FindGameObjectWithTag("levelLocker") || GameObject.FindGameObjectWithTag("levelLocker").activeSelf == true) && (lootTable[i].item.GetComponent<meleeWeaponBehaviour>() || lootTable[i].item.GetComponent<rangedWeaponBehaviour>()))
                    //{

                    GameObject item = Instantiate(lootTable[i].item, trans.position, Quaternion.identity);
                    spawned.Add(item);
                    if (item.GetComponent<PickMeUp>())
                        item.GetComponent<PickMeUp>().destroyable = true;
                    //}
                    break;
                }

                random -= lootTable[i].chance;
                Debug.Log("Random value decreased " + random);
            }
        }
    }

    public void ChangeChance()
    {
            dropChance += 20f;
            for (int i = 0; i < lootTable.Length; i++)
            {
                if (lootTable[i].item.tag == "Weapon" || lootTable[i].item.tag == "RangedWeapon")
                {
                    lootTable[i].chance = 0;
                }
                else if (lootTable[i].item.tag == "Potion")
                {
                    lootTable[i].chance += 10f;
                }
                Debug.Log("Item loot chance:" + lootTable[i].chance);
            }
            Debug.Log("Total drop chance:" + dropChance);
    }
}
