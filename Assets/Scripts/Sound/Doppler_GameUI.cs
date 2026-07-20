using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Doppler_GameUI : MonoBehaviour
{
    public Doppler_Source doppler_SourceScript; // Source object's script

    public Transform sourceTransform; // Current source object's position

    public Transform source_Reset_Position; // Default source object's position

    public Slider Source_Velocity_Slider; // Source's velocity slider

    public Slider Source_Freq_Slider; // Source's frequency slider

    public Slider SpeedSound_Slider; //Speed of Sound Slider slider


    public Text Freq_Text; 
    public Text Vel_Text; 
    public Text SpeedSound_Text; 


    public float Max_PeriodTime = 0.2f;
    public float Max_Velocity = 10f;
    float SpeedSound_Int;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton()
    {
        doppler_SourceScript.ChangeFreq(0.3f-Source_Freq_Slider.normalizedValue*Max_PeriodTime);        
        doppler_SourceScript.Velocity_Source = Mathf.RoundToInt(Source_Velocity_Slider.normalizedValue*Max_Velocity);
        SpeedSound_Int = Mathf.RoundToInt(SpeedSound_Slider.value);
        ResetButton();
        doppler_SourceScript.isPaused = false;
    }

    public void ResetButton()
    {
        doppler_SourceScript.isPaused = true;
        sourceTransform.position = source_Reset_Position.position;
    }   


    public void FreqSlideChange() // Performs this func everytime the freq slider is changed
    {
        doppler_SourceScript.DeleteAllWaves();

        Freq_Text.text = "Source Frequency: "+Mathf.RoundToInt(Source_Freq_Slider.value) + "Hz";

        ResetButton();
    }

    public void SourceVelocity_SlideChange() // Performs this func everytime the velocity slider is changed
    {
        doppler_SourceScript.DeleteAllWaves();

        Vel_Text.text = "Source Speed: "+Mathf.RoundToInt(Source_Velocity_Slider.value)+" m/s";

        ResetButton();
    }   

    
    public void SpeedSound_SlideChange() // Performs this func everytime the speed of sound slider is changed
    {
        doppler_SourceScript.DeleteAllWaves();
        
        SpeedSound_Text.text = "Speed of Sound: "+Mathf.RoundToInt(SpeedSound_Slider.value)+" m/s";

        ResetButton();
    }    

}

