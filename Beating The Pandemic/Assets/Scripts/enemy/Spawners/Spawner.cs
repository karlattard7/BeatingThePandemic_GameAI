using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform startPoint;
    public Transform endPoint;
    public GameObject enemyPrefab;
    public int enemiesAliveAtOnce;
    public float maxFrequency = 0f;
    public int totalEnemyCount;

    public bool customHealth = false;
    public float newHealth;

    public Quaternion rotation;

    public float minPlayerDistance = 1000f;
    public float maxPlayerDistance = 0f;
    //public bool lineOfSightRequired = false;

    public bool instantSpawnAll = false;

    private float nextSpawn;
    private int spawnedCount;
    private List<GameObject> alive = new List<GameObject>();
    private GameObject player;
    private float minDist, maxDist;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            float distanceFromStartToPlayer = Vector2.Distance(player.transform.position, startPoint.position);
            float distanceFromEndToPlayer = Vector2.Distance(player.transform.position, endPoint.position);
            minDist = Mathf.Max(distanceFromEndToPlayer, distanceFromStartToPlayer);
            maxDist = Mathf.Min(distanceFromEndToPlayer, distanceFromStartToPlayer);

            if(minDist>=minPlayerDistance && maxDist <= maxPlayerDistance)
            {
                if (Time.time >= nextSpawn || instantSpawnAll)
                {
                    foreach (GameObject obj in alive.ToList())                    
                        if (obj == null || obj.GetComponent<enemyBehaviour>().isDead())                        
                            alive.Remove(obj);
                        
                    

                    if (spawnedCount >= totalEnemyCount && alive.Count == 0)                    
                        Destroy(gameObject);
                    

                    if (alive.Count < enemiesAliveAtOnce && spawnedCount < totalEnemyCount)
                    {
                        float newX = Random.Range(startPoint.position.x, endPoint.position.x);
                        float newY = Random.Range(startPoint.position.y, endPoint.position.y);
                        float newZ = Random.Range(startPoint.position.z, endPoint.position.z);
                        Vector3 pos = new Vector3(newX, newY, newZ);

                        if (rotation==null)                        
                            rotation = Quaternion.identity;
                        
                        GameObject spawn = Instantiate(enemyPrefab, pos, rotation);
                        AIDestinationSetter aid = spawn.GetComponent<AIDestinationSetter>();
                        if(aid)
                            aid.target = player.transform;
                        if (customHealth)
                            spawn.GetComponent<enemyBehaviour>().health = newHealth;
                        alive.Add(spawn);
                        spawnedCount++;
                        nextSpawn = Time.time + maxFrequency;
                    }
                }
            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(startPoint.position, minPlayerDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(endPoint.position, minPlayerDistance);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(startPoint.position, maxPlayerDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(endPoint.position, maxPlayerDistance);

        Gizmos.color = Color.green;
        Vector3 center = new Vector3((endPoint.position.x+startPoint.position.x)/2, (endPoint.position.y + startPoint.position.y)/2, (endPoint.position.z + startPoint.position.z) / 2 );
        Vector3 size = new Vector3((endPoint.position.x-startPoint.position.x), (endPoint.position.y - startPoint.position.y), (endPoint.position.z - startPoint.position.z));
        Gizmos.DrawWireCube(center, size);

    }
}
