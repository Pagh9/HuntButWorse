using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyspawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public GameObject hivePrefab;
    public Transform player;

    public float maxspawnRadius = 30f;
    public float minSpawnRadius = 15f;
    public float spawnInterval;
    public int maxEnemies = 10;

    public float rareSpawnChance = 0.2f; // 20% chance for hive enemy

    private float spawnTimer = 0f;


    // Start is called before the first frame update
    private void Start()
    {

        
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogWarning("Player object not found");
            }
            
        }

    }

    // Update is called once per frame
    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval && CountEnemies() < maxEnemies)
        {
            SpawnEnemy();
            spawnTimer = 0f;

        }
    }

    void SpawnEnemy()
    {

        for (int i = CountEnemies(); i < maxEnemies; i++)
        {
            Vector3 randomPoint;

            // Generate a point within the spherical shell
            do
            {
                randomPoint = Random.insideUnitSphere * maxspawnRadius;
                randomPoint.y = 0; // Optional: keep objects on a plane
            }
            while (randomPoint.magnitude < minSpawnRadius);

            GameObject enemyPrefab = Random.value < rareSpawnChance ? hivePrefab : zombiePrefab;

            Instantiate(enemyPrefab, player.position + randomPoint, Quaternion.identity);
            


            //Vector3 spawnPosition = Vector3.zero;


            // Generate a random direction in the XZ plane
            //vector2 randomdirection = random.onunitsphere * maxspawnradius;

            //// generate a random distance between min and max spawn distances
            //float randomdistance = random.range(minspawnradius, maxspawnradius);

            //// calculate the spawn position based on the random direction and distance
            ////spawnposition = player.position + new vector3(randomdirection.x+ randomdistance, 0, randomdirection.y+ randomdistance);
            //spawnposition = player.position + new vector3(randomdirection.x, 0, randomdirection.y) * randomdistance;

            //// instantiate the enemy at the calculated position
            //instantiate(enemyprefab, spawnposition, quaternion.identity);

            //debug.log("random dir" + randomdirection);
            //debug.log("random distance" + randomdistance);

            }

        }

    int CountEnemies()
    {

        // Count the number of enemies currently in the scene
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(player.position, maxspawnRadius);

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(player.position, minSpawnRadius);

    }


}
