using UnityEditor.AdaptivePerformance.Editor;
using UnityEngine;
using UnityEngine.UI;

public class AirColumn_GameUI : MonoBehaviour
{
    public AirColumn_HarmonicMaker airColumn_HarmonicMakerScript;
    public GameObject airColumn_OBJ;
    public GameObject airColumnWall_OBJ;


    public Sprite Open2_Sprite;
    public Sprite OpenClose_Sprite;

    public Text LengthText;
    public Text TemperatureText;
    public Text HarmonicText;


    public Slider LengthSlider;
    public Slider TemperatureSlider;
    public Slider HarmonicSlider;

    public Toggle OpenOpen_Toggle;
    public Toggle CloseClose_Toggle;
    public Toggle CloseOpen_Toggle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LengthSlider_SliderChange();
        HarmonicSlider_SliderChange();
    }

    // Update is called once per frame
    void Update()
    {
        LengthText.text = "Air Column Length: "+LengthSlider.value+" cm";
        TemperatureText.text = "Air Temperature: "+TemperatureSlider.value+"ºC";

        HarmonicText.text = "Harmonic Number: "+HarmonicSlider.value;

    }

    public void LengthSlider_SliderChange()
    {
        airColumn_HarmonicMakerScript.ShrinkHarmonic(LengthSlider.normalizedValue);
        airColumn_HarmonicMakerScript.UpdateHarmonicWave();
    }

    public void HarmonicSlider_SliderChange()
    {
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
}
