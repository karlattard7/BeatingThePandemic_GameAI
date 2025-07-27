using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Open_Trigger : MonoBehaviour
{
    public TilemapRenderer closedTrigger;
    public TilemapRenderer openTrigger;
    public TilemapRenderer openDoor;

    // Update is called once per frame
    void Update()
    {

        //If all enemies are dead: - input condition for enemies
        if (GameObject.FindGameObjectsWithTag("Spawner").Length == 0 && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            closedTrigger.enabled = false;
            openTrigger.enabled = true;
            openDoor.enabled = true;
        }
    }

    //Testing Method
    //private void OnTriggerEnter2D(Collider2D col)
    //{

    //    if (col.gameObject.CompareTag("Player"))
    //    {
    //        closedTrigger.enabled = false;
    //        openTrigger.enabled = true;
    //        openDoor.enabled = true;
    //    }

    //}
}
