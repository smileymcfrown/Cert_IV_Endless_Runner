using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    public Player player;
    public GameObject obstaclePrefab;

    private float minSpawnRate = 2f;
    private float maxSpawnRate = 5f;
    private float spawnModifier = 3f;

    private float currentSpawnRate = 0f;
    private float startModifier = 4f;
    
    void Start()
    {
        // Generate the first amount of time to wait for first obstacle.
        // -0.5f from the min and max because the wait with an empty background was too long.

        currentSpawnRate = Random.Range(minSpawnRate - 0.5f, maxSpawnRate - 0.5f);
        
        Debug.Log("First spawnRate: " + currentSpawnRate);  // Just handy extra info.
        
        // Starts the SpawnLoop as a Coroutine which means it runs in parallel to the
        // rest of the code. Which it needs to because SpawnLoop uses an infinite while loop.
        StartCoroutine(SpawnLoop());
    }

    
    void Update()
    {
        
         /* This is old code I used when the score would increase by 1 for each frame and
         * that would make the objects start spawning more frequently.
         * 
         * I don't like this method because eventually the objects will be generated
         * so close together that they are impossible to jump.
         * 
         * It would be better to instead increase the speed at which the objects move, and then
         * /maybe/ also increase the rate at which the objects spawn slightly.
         * 
         * Also, I was thinking it would be better to have a fixed modifier that only changes
         * with each heart delivery (level up) instead of slowly increasing with the score; 
         * however, I have added a bonus 1000 points at level up which means the below method
         * makes the level get progressively harder, and then ALSO jumps in difficulty when 
         * the player makes a delivery.
         */

        // Set value of the modifier to the spawn rate using the current score / 1000
        // spawnModifier = startModifier - (player.GetScore() / 1000f);

        //  Mathf.Clamp(x, y, z):
        //    spawnMod will stay the same (x) as long as it's not <1 (y) or >startMod (z) 
        // spawnModifier = Mathf.Clamp(spawnModifier, 2f, startModifier);
        
    }

    private IEnumerator SpawnLoop()
    {
        // Infinite loop: pause -> spawn obstacle -> change pause time by a random amount -> repeat
                
        while (true)
        {
            
            Debug.Log("Wait for " + currentSpawnRate * spawnModifier + " seconds.");
            // Pause IENumerator Coroutine for x seconds.
            yield return new WaitForSeconds(currentSpawnRate); // * spawnModifier);

            // Create the obstacle
            Instantiate(obstaclePrefab, transform.position, Quaternion.identity, transform);

            // Generate a new random wait time for the next obstacle
            currentSpawnRate = Random.Range(minSpawnRate, maxSpawnRate);

            Debug.Log("Current rate: " + currentSpawnRate);  // Just handy extra info.


        }
    }
    
}
