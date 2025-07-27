using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpawnerActivator : MonoBehaviour
{
    public bool whenAllEnemiesKilled = false;
    public Spawner spawner;
    public bool destroyOnEnable = false;
    private GameObject player;

    void Start()
    {
        spawner.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (spawner == null)
            Destroy(gameObject);

        if (whenAllEnemiesKilled)
        {
            bool clear = true;
            foreach (GameObject spn in GameObject.FindGameObjectsWithTag("Spawner"))
            {
                if (spn.GetComponent<Spawner>() != spawner)
                    clear = false;
            }
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && clear)
            {
                spawner.enabled = true;
                spawner.gameObject.SetActive(true);
                Debug.Log("enabling by lack of enemy");
                if (destroyOnEnable)
                    Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (whenAllEnemiesKilled)
            return;

        if (collision.CompareTag("Player"))
        {
            Debug.Log("enabling by trigger");
            spawner.enabled = true;
            spawner.gameObject.SetActive(true);
            if (destroyOnEnable)
                Destroy(gameObject);
        }
    }
}