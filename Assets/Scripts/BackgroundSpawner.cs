using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;
    public float spawnModifier = 3f;
    public Player player;

    public GameObject backObjectsPrefab;
    public GameObject doctorPrefab;
    private bool doctor = false;
    private int doctorDistance = 1;

    private float currentSpawnRate = 0;
    private float startModifier = 3f;
    

    /* This code is a copy of ObstacleSpawner.cs - Look at that for detailed notes on
     * each line of code..
     * Comments added to changes - /especially/ for code that deals with the "doctor"
     * (level up object) which I've added notes to.
     */



    void Start()
    {
        // - 0.3f and - 0.7f to get a background object on the screen quickly and before
        // the first obstacle is generated - looks nicer then.
        currentSpawnRate = Random.Range(minSpawnRate - 0.3f, maxSpawnRate - 0.7f);

        StartCoroutine(SpawnLoop());
    }

    // Update is called once per frame
    void Update()
    {
        // Initial spawn rate is slightly higher to get more background than obstacles.
        // player.GetScore() changed to 5 to keep background object at a constant spawn rate.
        spawnModifier = startModifier - (4 / 600f);  
        spawnModifier = Mathf.Clamp(spawnModifier, 1, startModifier);

        // Distance is increased by 1 each frame. Doctor appears after a set distance NOT score.
        if(player.distance / doctorDistance == 1000)
        {
            doctor = true;
            doctorDistance++;
        }
        
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentSpawnRate);

            // Fixed doctor appearing more than once.
            if (doctor == true)
            {
                Instantiate(doctorPrefab, transform.position, Quaternion.identity, transform);
                doctor = false;
                
            }
            else
            {
                    Instantiate(backObjectsPrefab, transform.position, Quaternion.identity, transform);
            }

            currentSpawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        }

    }
}
