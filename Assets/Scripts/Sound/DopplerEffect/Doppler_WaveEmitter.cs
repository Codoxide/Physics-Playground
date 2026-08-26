using UnityEngine;

public class Doppler_WaveEmitter : MonoBehaviour
{
    public GameObject WavePrefab; // Copy of the wave ring
    public float PeriodTime = 0.1f; // The rate of the waves being emitted
    public bool isPausedEmitting = true;
    public bool isPausedMoved= false;

    public float time = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        if(!isPausedEmitting)
        {
            
            time += Time.deltaTime;

            if (time >= PeriodTime)
            {
                Instantiate(
                    WavePrefab,
                    new Vector3(transform.position.x, transform.position.y, 0f),
                    transform.rotation
                );

                time = 0f;
            }
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
    public void ChangePauseAllCurrentWaves()
    {
        GameObject[] waves = GameObject.FindGameObjectsWithTag("Wave");

        foreach (GameObject wave in waves)
        {
            wave.transform.GetComponent<Doppler_WaveRing>().isPaused = isPausedMoved;
        }        
        isPausedMoved = !isPausedMoved;

    }    
}
