using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Rollercoaster_GameUI : MonoBehaviour
{

    public GameObject ExplainPanelObj;

    public Transform Scroll_Content;
    public Transform CircleObj;
    public Transform StartResetPos;

    public Rollercoaster_Movement rollercoaster_MovementScript;
    public Text Start_PauseButtonTxt;
    
    public Text CurrentHeightText;
    public Text CurrentSpeedText;


    public Text TotalEnergyText;
    public Text KineticEnergyText;
    public Text PotentialEnergyText;    

    public Text CurrentMassText;
    public Text TimescaleText;

    public Slider KineticEnergySlide;
    public Slider PotentialEnergySlide;    

    public Slider MassSlider;
    public Slider TimeScaleSlider;

    float currentMass;
    float timeScale;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MassSliderChange();
        TimescaleSliderChange();
        UpdateUI();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(rollercoaster_MovementScript.PauseState == false)
        {
            UpdateUI();   
            
        }
    }

    void UpdateUI()
    {
        float totalEnergy = rollercoaster_MovementScript.TotalEnergy(currentMass);
        float kineticEnergy = rollercoaster_MovementScript.KineticEnergy(currentMass);

        float potentialEnergy = totalEnergy - kineticEnergy;
        float currentHeight = rollercoaster_MovementScript.CheckDistanceThis();
        float currentSpeed = rollercoaster_MovementScript.speed; // Replace with your speed variable if different.

        TotalEnergyText.text = totalEnergy.ToString("F1") + " J";
        KineticEnergyText.text = kineticEnergy.ToString("F1") + " J";
        PotentialEnergyText.text = potentialEnergy.ToString("F1") + " J";

        CurrentHeightText.text = currentHeight.ToString("F1") + " m";
        CurrentSpeedText.text = currentSpeed.ToString("F1") + " m/s";

        if (totalEnergy > 0f)
        {
            KineticEnergySlide.normalizedValue = kineticEnergy / totalEnergy;
            PotentialEnergySlide.normalizedValue = potentialEnergy / totalEnergy;
        }
        else
        {
            KineticEnergySlide.normalizedValue = 0f;
            PotentialEnergySlide.normalizedValue = 0f;
        }
    }
    public void MassSliderChange()
    {
        currentMass = MassSlider.value*.5f;
        CurrentMassText.text = "Mass: "+currentMass+" kg";
        UpdateUI();

    } 
    public void TimescaleSliderChange()
    {
        timeScale = TimeScaleSlider.value*.1f;
        TimescaleText.text = "Time Lapse: "+timeScale+"x";
        Time.timeScale = timeScale;
    }     

    public void StartSim()
    {
        rollercoaster_MovementScript.PauseState = !rollercoaster_MovementScript.PauseState; // Reverse bool
        Start_PauseButtonTxt.text = rollercoaster_MovementScript.PauseState ? "Start": "Pause";
    }
    public void ResetSim()
    {
        CircleObj.position = StartResetPos.position;
        rollercoaster_MovementScript.ResetScript();
        UpdateUI();
        Start_PauseButtonTxt.text = rollercoaster_MovementScript.PauseState ? "Start": "Pause";
    }

    public void OpenExplainPanel()
    {
        ExplainPanelObj.SetActive(true);
    }

    public void CloseExplainPanel()
    {
        ExplainPanelObj.SetActive(false);
        Scroll_Content.transform.position = new Vector3(0,0,0);
    }    
}
