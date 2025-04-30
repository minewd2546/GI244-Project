using System.Collections;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platform;
    public Transform lastPlatform;
    public Transform player; // Reference to the player
    Vector3 lastPos;
    Vector3 newPos;

    public bool stop;
    public float platformSpawnDistance = 5f; // Distance to spawn the next platform

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastPos = lastPlatform.position;
        StartCoroutine(SpawnPlatforms()); // Start the coroutine when the game starts
    }

    // Update is called once per frame
    void Update()
    {
        // Optionally, you can put any update logic here if needed
    }

    // Generate new position for the platform
    void GeneratePos()
    {
        newPos = lastPos;
        int rand = Random.Range(0, 3);
        if (rand > 0)
        {
            newPos.x += 2f;
        }
        else
        {
            newPos.z += 1f;
        }
    }

    // Coroutine to spawn platforms
    IEnumerator SpawnPlatforms()
    {
        while (!stop)
        {
            GeneratePos();
            // Check if the player is close enough to the new platform's spawn point
            if (Vector3.Distance(player.position, newPos) <= platformSpawnDistance)
            {
                // Spawn the platform if the player is near enough
                Instantiate(platform, newPos, Quaternion.identity); // Spawn the platform
                lastPos = newPos; // Update the last position to the new platform's position
            }
            yield return new WaitForSeconds(0.11f); // Wait before checking again
        }
    }
}