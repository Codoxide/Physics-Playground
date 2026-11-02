using Unity.VisualScripting;
using UnityEditor.AdaptivePerformance.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AirColumn_GameUI : MonoBehaviour
{
    public AirColumn_HarmonicMaker airColumn_HarmonicMakerScript;

    public GameObject airColumn_OBJ;
    public GameObject airColumnWall_OBJ;
    public GameObject panelExplain;

    public Transform Scroll_Content;


    public Sprite Open2_Sprite;
    public Sprite OpenClose_Sprite;

    public Text LengthText;
    public Text TemperatureText;
    public Text HarmonicText;
    public Text FrequencyEmittedText;


    public Slider LengthSlider;
    public Slider TemperatureSlider;
    public Slider HarmonicSlider;

    public Toggle OpenOpen_Toggle;
    public Toggle CloseClose_Toggle;
    public Toggle CloseOpen_Toggle;
    
    Color coldColor  = new Color(0.5803921569f, 0.7254901961f, 1f); // Icy blue

    Color hotColor  = new Color(0.8431f, 0.5568627451f, 0.5568627451f); // Warm light red

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LengthSlider_SliderChange();
        HarmonicSlider_SliderChange();
        TemperatureSlider_SliderChange();
    }

    // Update is called once per frame
    void Update()
    {

        
    }


    public void LengthSlider_SliderChange()
    {
        LengthText.text = "Air Column Length: "+LengthSlider.value+" cm";

        airColumn_HarmonicMakerScript.ShrinkHarmonic(LengthSlider.normalizedValue);
        airColumn_HarmonicMakerScript.UpdateHarmonicWave();
        CalculateEmittedFrequency();

    }

    public void TemperatureSlider_SliderChange()
    {
        TemperatureText.text = "Air Temperature: "+TemperatureSlider.value+"ºC";

        float t = Mathf.InverseLerp(0f, 40f, TemperatureSlider.value);
        Camera.main.backgroundColor = Color.Lerp(coldColor, hotColor, t);
        CalculateEmittedFrequency();

    }    

    public void HarmonicSlider_SliderChange()
    {
        HarmonicText.text = "Harmonic Number: "+HarmonicSlider.value;
        if(airColumn_HarmonicMakerScript.current_airColumnType == AirColumnType.OpenClosed)
        {
            airColumn_OBJ.GetComponent<SpriteRenderer>().sprite = OpenClose_Sprite;
            airColumnWall_OBJ.SetActive(false);
            airColumn_HarmonicMakerScript.PhaseShift = 0;
            airColumn_HarmonicMakerScript.K_Value = 1/(0.25f+((HarmonicSlider.value-1)*0.5f));
        }
        else if(airColumn_HarmonicMakerScript.current_airColumnType == AirColumnType.OpenOpen)
        {
            airColumn_OBJ.GetComponent<SpriteRenderer>().sprite = Open2_Sprite;
            airColumnWall_OBJ.SetActive(false);
            airColumn_HarmonicMakerScript.PhaseShift = 90;
            airColumn_HarmonicMakerScript.K_Value = 1/(HarmonicSlider.value*0.5f);
        }
        else
        {
            airColumn_OBJ.GetComponent<SpriteRenderer>().sprite = OpenClose_Sprite;
            airColumnWall_OBJ.SetActive(true);
            airColumn_HarmonicMakerScript.K_Value = 1/(HarmonicSlider.value*0.5f);      
            airColumn_HarmonicMakerScript.PhaseShift = 0;
        }
        airColumn_HarmonicMakerScript.UpdateHarmonicWave();
        CalculateEmittedFrequency();
    }    

    public void CalculateEmittedFrequency()
    {
        float speedOfSound = 331.4f+(0.606f*TemperatureSlider.value);
        float WaveLength;
        float length_AirColumn_2Meters = LengthSlider.value/100;
        if(airColumn_HarmonicMakerScript.current_airColumnType == AirColumnType.OpenClosed)
        {

            WaveLength = (4*length_AirColumn_2Meters)/(2*HarmonicSlider.value-1);
        }
        else
        {
            WaveLength = (4*length_AirColumn_2Meters)/(2*HarmonicSlider.value);        
        }
        float Frequency = speedOfSound/WaveLength;
        string formatted_Freq = Mathf.Round(Frequency).ToString("N0");

        FrequencyEmittedText.text = "Frequency: "+formatted_Freq+ " Hz";


    }

    public void OpenOpenFunc()
    {

        airColumn_HarmonicMakerScript.current_airColumnType = AirColumnType.OpenOpen;
        HarmonicSlider_SliderChange();

    }
    public void CloseCloseFunc()
    {

        airColumn_HarmonicMakerScript.current_airColumnType = AirColumnType.ClosedClosed;
        HarmonicSlider_SliderChange();

    }
    public void OpenCloseFunc()
    {
        airColumn_HarmonicMakerScript.current_airColumnType = AirColumnType.OpenClosed;
        HarmonicSlider_SliderChange();
    }        

    public void OpenPanel()
    {
        panelExplain.SetActive(true);
    }
    public void ClosePanel()
    {
        Scroll_Content.transform.position = new Vector3(0,0,0);
        panelExplain.SetActive(false);
    }
    public void ExitButton()
    {
        SceneManager.LoadScene(0);
    }   
}
