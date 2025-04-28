using UnityEngine;

public class OnComingCar : MonoBehaviour
{
    public float speed = 10.0f;
    public float turnSpeed = 100.0f; // Rotation speed for turning
    private float horizontalInput; // Horizontal input for turning (A/D or Left/Right)
    private float verticalInput; // Vertical input for moving forward/backward (W/S)

    // Update is called once per frame
    void Update()
    {
        // Check if the game has started
        if (GameManager.instance.isGameStarted)
        {
            // Get user input for horizontal and vertical movement
            horizontalInput = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow
            verticalInput = Input.GetAxis("Vertical"); // W/S or Up/Down arrow

            // Move the car forward/backward using W/S or Up/Down
            transform.Translate(0, 0, verticalInput * speed * Time.deltaTime);

            // Rotate the car using A/D or Left/Right for turning
            transform.Rotate(0, horizontalInput * turnSpeed * Time.deltaTime, 0);
        }

        // Check if the car's y position is below the game over threshold
        if (transform.position.y <= -2)
        {
            GameManager.instance.GameOver(); // Call Game Over method from GameManager
        }
    }
}