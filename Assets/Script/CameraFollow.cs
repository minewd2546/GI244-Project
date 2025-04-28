using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;
    public float fallThreshold = -2f; // Threshold for when the car falls below this height

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            offset = new Vector3(0, 4, -5);
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            offset = new Vector3(0, 1, 2);
        }
    }

    void LateUpdate()
    {
        // Check if the car has fallen below the threshold
        if (player.transform.position.y > fallThreshold)
        {
            // Only update the camera position if the car has not fallen
            transform.position = player.transform.position + offset;
        }
    }
}