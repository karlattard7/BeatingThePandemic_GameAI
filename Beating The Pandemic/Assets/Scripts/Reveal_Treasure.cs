using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Reveal_Treasure : MonoBehaviour
{

    public TilemapRenderer Treasure;
    public TilemapRenderer reward;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            Treasure.enabled = false;
            reward.enabled = true;

        }
    }



}
