using UnityEngine;

public class Doppler_WaveEmitter : MonoBehaviour
{
    public GameObject WavePrefab; // Copy of the wave ring
    public float PeriodTime = 0.1f; // The rate of the waves being emitted
    public bool isPausedEmitting = true;
    float time = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        if(!isPausedEmitting)
        {
            if(time <= PeriodTime) // Check if it is time to create a new wave 
            {
                time += Time.deltaTime; // Keeps waiting
            }
            else
            {
                Instantiate(WavePrefab, new Vector3(transform.position.x, transform.position.y, 0f), transform.rotation); // Creates the wave at a specfic postion and rotation
                time = 0; // Reset timer
                
            }
        }
        else
        {
            time = PeriodTime;
            RemoveAllCurrentWaves();
        }

    }

    public void RemoveAllCurrentWaves()
    {
        GameObject[] waves = GameObject.FindGameObjectsWithTag("Wave");

        foreach (GameObject wave in waves)
        {
            Destroy(wave);
        }        
    }
}
