using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class platfrom : MonoBehaviour
{
    public GameObject platfromBlast;

    public GameObject gem, coin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int randNumber = Random.Range(1, 21);
        Vector3 tempPos = transform.position;
        tempPos.y += 0.65f;

        if (randNumber < 4)
        {
            Instantiate(coin, tempPos, coin.transform.rotation);
        }

        if (randNumber == 7)
        {
            Instantiate(gem, tempPos, gem.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player collided with the platform
        if (collision.gameObject.CompareTag("Player"))
        {
            // Delay the platform destruction by 1 second
            Invoke("DestroyPlatform", 0.5f);
        }
    }

    void DestroyPlatform()
    {
        // Instantiate the platform blast effect at the platform's position
        Instantiate(platfromBlast, transform.position, Quaternion.identity);

        // Optionally, apply physics to the platform (if needed)
        GetComponent<Rigidbody>().isKinematic = false;

        // Destroy the platform object after the delay
        Destroy(gameObject);
    }
}