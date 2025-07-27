using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    //this positions the player at the start of each level (entry door)

    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        PlayerSpawnPosition();
    }

    public void PlayerSpawnPosition()
    {
        SoundManager.PlaySound("door");
        player.transform.position = new Vector3(transform.position.x, transform.position.y);
    }
}
