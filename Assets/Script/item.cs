using UnityEngine;

public class item : MonoBehaviour
{
    public GameObject coinBlast, diamondBlast;
    private bool hasCollided = false; // Track if the item has collided with the player

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Destroy the item after 3 seconds if it doesn't collide with the player
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        // You can add additional update logic here if necessary
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player collided with the item
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Coin"))
            {
                GameManager.instance.GetCoin();
                Instantiate(coinBlast, transform.position, Quaternion.identity); // Create coin blast effect
            }

            if (gameObject.CompareTag("Diamond"))
            {
                GameManager.instance.GetGem();
                Instantiate(diamondBlast, transform.position, Quaternion.identity); // Create diamond blast effect
            }

            // Mark the item as collided and prevent it from being destroyed after the timer
            hasCollided = true;

            // Destroy the item immediately after the collision
            Destroy(gameObject);
        }
    }

    // This will override the Destroy call if the item has collided with the player
    private void OnDestroy()
    {
        if (!hasCollided)
        {
            // If the item wasn't collected, we can manually destroy it after 3 seconds
            Destroy(gameObject);
        }
    }
}