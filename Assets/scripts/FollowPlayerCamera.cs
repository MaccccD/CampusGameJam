using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform

    public Vector3 offset = new Vector3(0f, 2f, -5f); // Adjust this to position the camera relative to the player

    void LateUpdate()
    {
        // Ensure the camera follows the player's position
        transform.position = player.position + offset;

        // Rotate the camera to match the player's rotation
        transform.rotation = player.rotation;
    }
}
