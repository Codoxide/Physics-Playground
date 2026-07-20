using UnityEngine;

public class Doppler_Source : MonoBehaviour
{
    public float Velocity_Source = 45f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move(); // Moves the source bit by bit 
    }

    void Move()
    {
        Vector3 movement = new Vector3(1, 0, 0f);

        // Add the movement vector to the current position
        transform.position += movement * Velocity_Source * Time.deltaTime;
    }
}
