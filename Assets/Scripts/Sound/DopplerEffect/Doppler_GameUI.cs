using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class Doppler_GameUI : MonoBehaviour
{
    public Doppler_Source doppler_SourceScript; // Source object's script

    public Doppler_WaveEmitter doppler_WaveEmitterScript; // Wave emitter script


    public Transform observerTransform; // Current source object's position

    public Transform sourceTransform; // Current source object's position

    public Transform source_Reset_Position; // Default source object's position

    public Slider Source_Velocity_Slider; // Source's velocity slider

    public Slider Source_Freq_Slider; // Source's frequency slider

    public Slider SpeedSound_Slider; //Speed of Sound Slider slider

    public GameObject panelExplain;

    public GameObject Scroll_Content;

    public Text Freq_Text; 
    public Text Vel_Text; 
    public Text SpeedSound_Text; 

    public Text Freq_Percieved_Text; 

    public Text StartButtonText; 


    public float Max_PeriodTime = 0.2f;
    public float Max_Velocity = 10f;
    bool DisplayFreq = false;
    float SpeedSound_Val;
    float Freq_Percieved_Val;
    float Source_Freq_Val;
    float Source_Vel_Val;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpeedSound_Val = Mathf.RoundToInt(SpeedSound_Slider.value);
        Source_Freq_Val = Mathf.RoundToInt(Source_Freq_Slider.value);
        Source_Vel_Val = Mathf.RoundToInt(Source_Velocity_Slider.value);

        Freq_Text.text = "Source Frequency: "+ Source_Freq_Val + "Hz"; // Keep updating the text
        Vel_Text.text = "Source Speed: "+Source_Vel_Val+" m/s"; //
        SpeedSound_Text.text = "Speed of Sound: "+SpeedSound_Val+" m/s"; // 
        if(DisplayFreq){
            if(observerTransform.position.x-sourceTransform.position.x > 0) // Object is approaching the observer
            {
                // Doppler Effect Formula for source approaching observer 
                Freq_Percieved_Val = (SpeedSound_Val/(SpeedSound_Val-Source_Vel_Val))*Source_Freq_Val; 

                Freq_Percieved_Text.text = "Perceived Frequency (What the observer hears): "+Mathf.RoundToInt(Freq_Percieved_Val) +" Hz";

            }
            else // Object is moving away from the observer
            {
                // Doppler Effect Formula for source moving away from observer 
                Freq_Percieved_Val = (SpeedSound_Val/(SpeedSound_Val+Source_Vel_Val))*Source_Freq_Val; 

                Freq_Percieved_Text.text = "Perceived Frequency (What the observer hears): "+ Mathf.RoundToInt(Freq_Percieved_Val) +" Hz";

            }
        }
        else
        {
            Freq_Percieved_Text.text = "Perceived Frequency (What the observer hears): ";
            
        }

    }

    public void StartButton()
    {
        DisplayFreq = true;
        doppler_SourceScript.ChangeFreq(
            0.35f - Source_Freq_Slider.normalizedValue * Max_PeriodTime
        );

        doppler_SourceScript.Velocity_Source =
            Source_Velocity_Slider.normalizedValue * Max_Velocity;

        bool shouldPause = !doppler_SourceScript.isPaused;

        doppler_SourceScript.isPaused = shouldPause;

        doppler_WaveEmitterScript.isPausedEmitting = shouldPause;
        doppler_WaveEmitterScript.isPausedMoved = shouldPause;

        GameObject[] waves = GameObject.FindGameObjectsWithTag("Wave");

        foreach (GameObject wave in waves)
        {
            Doppler_WaveRing waveScript =
                wave.GetComponent<Doppler_WaveRing>();

            if (waveScript != null)
            {
                waveScript.isPaused = shouldPause;
            }
        }

        StartButtonText.text = shouldPause ? "Start" : "Pause";
    }

    public void ResetButton()
    {
        DisplayFreq = false;
        doppler_SourceScript.isPaused = true;

        sourceTransform.position = source_Reset_Position.position;

        doppler_WaveEmitterScript.time = doppler_WaveEmitterScript.PeriodTime;

        doppler_WaveEmitterScript.isPausedMoved = true;
        doppler_WaveEmitterScript.isPausedEmitting = true;
        doppler_WaveEmitterScript.time = doppler_WaveEmitterScript.PeriodTime;
        doppler_WaveEmitterScript.RemoveAllCurrentWaves();

        StartButtonText.text = "Start";
    }


    public void FreqSlideChange() // Performs this func everytime the freq slider is changed
    {
        doppler_SourceScript.DeleteAllWaves();


        ResetButton();
    }

    public void SourceVelocity_SlideChange() // Performs this func everytime the velocity slider is changed
    {
        doppler_SourceScript.DeleteAllWaves();


        ResetButton();
    }   

    
    public void SpeedSound_SlideChange() // Performs this func everytime the speed of sound slider is changed
    {
        doppler_SourceScript.DeleteAllWaves();
        

        ResetButton();
    }    

    public void ExitOutOfSimulation()
    {
        
    }

    public void OpenPanel()
    {
        ResetButton();
        panelExplain.SetActive(true);
    }
    public void ClosePanel()
    {
        Scroll_Content.transform.position = new Vector3(0,0,0);
        ResetButton();
        panelExplain.SetActive(false);
    }
}

