using UnityEngine;

public class Doppler_Source : MonoBehaviour
{
    public float Velocity_Source = 45f;
    public Doppler_WaveEmitter doppler_WaveEmitterScript;
    public bool isPaused = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPaused) // Check if the game is not paused
        {
            doppler_WaveEmitterScript.isPausedEmitting = false;
            Move(); // Moves the source bit by bit 
            
        }
        else
        {
            doppler_WaveEmitterScript.isPausedEmitting = true;
            
        }
    }
    public void ChangeFreq(float periodTime)
    {
        doppler_WaveEmitterScript.PeriodTime = periodTime;
    }

    public void DeleteAllWaves()
    {
        doppler_WaveEmitterScript.RemoveAllCurrentWaves();
    }
    void Move()
    {
        Vector3 movement = new Vector3(1, 0, 0f);

        // Add the movement vector to the current position
        transform.position += movement * Velocity_Source * Time.deltaTime;
    }
}
