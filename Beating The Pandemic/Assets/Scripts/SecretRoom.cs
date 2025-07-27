using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SecretRoom : MonoBehaviour
{
  

    public GameObject SecretRoomMap;
    public GameObject LevelTileMap;
    public GameObject Bacteria;
    public GameObject Weapon;
    public GameObject panel;

    private LootSpawner lootChangeChance;

    private void Start()
    {
        lootChangeChance = GameObject.Find("GameManager").GetComponent<LootSpawner>();
    }

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Spawner").Length == 0 && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            GetComponent<TilemapCollider2D>().enabled = true;
            lootChangeChance.ChangeChance();
        }
    }


    //When player collides with exit door, tilemaps are changed
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            SoundManager.PlaySound("door");
            panel.GetComponent<BoxCollider2D>().enabled = false;
            SecretRoomMap.SetActive(true);
            LevelTileMap.SetActive(false);
            Bacteria.SetActive(true);
            if(GameObject.FindGameObjectWithTag("directionalText"))
                GameObject.FindGameObjectWithTag("directionalText").GetComponent<directionalTextUpdater>().setText("Fumigate the area!");
            //GameObject.FindGameObjectWithTag("levelLocker").SetActive(true);
            Instantiate(Weapon, col.gameObject.transform.position, Quaternion.identity);
            
        }
    }


    
}
