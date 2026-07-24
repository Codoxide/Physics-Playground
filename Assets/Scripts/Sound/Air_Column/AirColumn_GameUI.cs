using UnityEditor.AdaptivePerformance.Editor;
using UnityEngine;
using UnityEngine.UI;

public class AirColumn_GameUI : MonoBehaviour
{
    public AirColumn_HarmonicMaker airColumn_HarmonicMakerScript;


    public Text LengthText;
    public Text TemperatureText;
    public Text HarmonicText;


    public Slider LengthSlider;
    public Slider TemperatureSlider;
    public Slider HarmonicSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        airColumn_HarmonicMakerScript.K_Value = 1/(0.25f+((HarmonicSlider.value-1)*0.5f));
        airColumn_HarmonicMakerScript.UpdateHarmonicWave();

    }    
}
