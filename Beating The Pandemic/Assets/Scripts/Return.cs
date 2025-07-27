using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Return : MonoBehaviour
{
    public GameObject LevelTileMap;
    public GameObject SecretRoomMap;
    public GameObject roomEntrance;
    public GameObject panel;


    //When player collides with secret room door, tilemaps are changed
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            SoundManager.PlaySound("door");
            panel.GetComponent<BoxCollider2D>().enabled = true;
            Destroy(SecretRoomMap);
            Destroy(roomEntrance);
            LevelTileMap.SetActive(true);
            if (GameObject.FindGameObjectWithTag("directionalText"))
                GameObject.FindGameObjectWithTag("directionalText").GetComponent<directionalTextUpdater>().setText("");
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                Destroy(enemy);
            //GameObject.FindGameObjectWithTag("levelLocker").GetComponent<levelLocker>().returnWeapAndDisable();
        }
    }
}
