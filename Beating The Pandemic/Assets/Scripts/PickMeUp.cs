using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickMeUp : MonoBehaviour
{
    private float delay = 0;
    private float virusDamage = 20f;
    public float timeToDestroy = 15f;
    public bool destroyable = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        
        

        if (other.gameObject.tag != "Player") return;

        if (GameObject.FindGameObjectWithTag("GameManager"))
            if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<LootSpawner>().spawned.Contains(other.gameObject))
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<LootSpawner>().spawned.Remove(other.gameObject);

        destroyable = false;
        if (gameObject.tag == "Potion")
        {
            SoundManager.PlaySound("pick");
            other.gameObject.GetComponent<playerBehaviour>().potionsCount += 1;
            Debug.Log("I have potions: " + other.gameObject.GetComponent<playerBehaviour>().potionsCount);
            Destroy(gameObject);
        }
        else if (gameObject.tag == "Virus")
        {
            SoundManager.PlaySound("virus");
            other.gameObject.GetComponent<playerBehaviour>().TakeDamage(virusDamage);
            Debug.Log("Health after virus " + other.gameObject.GetComponent<playerBehaviour>().health);
            Destroy(gameObject);
        }
        else if (gameObject.tag == "Weapon" || gameObject.tag == "RangedWeapon")
        {
            //if (!GameObject.FindGameObjectWithTag("levelLocker") || GameObject.FindGameObjectWithTag("levelLocker").activeSelf == false) // sorry for this horrible implementation :( -  ethan
            //{
                SoundManager.PlaySound("equip");
                other.gameObject.GetComponent<playerBehaviour>().EquipWeapon(gameObject);
            //}
        }
        else if (gameObject.tag == "Book")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player)
                Destroy(player);
            SceneManager.LoadScene(7);
        }
        else
        {
            Debug.Log("I have no tag!");
        }
    }
}