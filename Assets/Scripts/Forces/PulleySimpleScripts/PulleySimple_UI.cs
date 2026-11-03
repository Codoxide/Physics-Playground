using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PulleySimple_UI : MonoBehaviour
{
    public Slider MassA_Slide;
    public Slider MassB_Slide;
    public Slider TimeScale_Slide;

    public Text ButtonStartText;

    public Text MassA_Text;
    public Text MassB_Text;
    public Text Timescale_Text;
    
    public Text AcclerationText;
    public Text TensionText;


    public PulleySystem_Simple pulleySystem_Simple;

    public GameObject MassA;
    public GameObject MassB;

    public GameObject ExplainPanelObj;
    public GameObject Scroll_Content;


    public float MaxMass_Size;
    public float MinMass_Size;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ButtonStartText.text = pulleySystem_Simple.IsPaused ? "Start": "Pause";
        MassA_SlideChange();
        MassB_SlideChange();
    }

    public void TimeScale_SlideChange()
    {
        float timeScaleVal = TimeScale_Slide.value*0.1f;
        Timescale_Text.text = "Time Lapse: "+timeScaleVal.ToString("F1") + "x";
        Time.timeScale = timeScaleVal;
    }


    public void MassB_SlideChange()
    {
        float scaleClamped = MinMass_Size + (MassB_Slide.normalizedValue * (MaxMass_Size - MinMass_Size));
        MassB.transform.localScale = new Vector3(scaleClamped, scaleClamped, 1);        
        pulleySystem_Simple.Mass2_Kg = MassB_Slide.value * 0.5f;
        MassB_Text.text = "Mass B: "+MassB_Slide.value * 0.5f+" kg";
        pulleySystem_Simple.SetValues();
        pulleySystem_Simple.IsPaused = true;
        ButtonStartText.text = pulleySystem_Simple.IsPaused ? "Start": "Pause";        
        ResetButton();
        UpdateUI();
    } 

    public void MassA_SlideChange()
    {
        float scaleClamped = MinMass_Size + (MassA_Slide.normalizedValue * (MaxMass_Size - MinMass_Size));
        MassA.transform.localScale = new Vector3(scaleClamped, scaleClamped, 1);
        pulleySystem_Simple.Mass1_Kg = MassA_Slide.value * 0.5f;
        MassA_Text.text = "Mass A: "+MassA_Slide.value * 0.5f+" kg";
        pulleySystem_Simple.SetValues();
        pulleySystem_Simple.IsPaused = true;
        ButtonStartText.text = pulleySystem_Simple.IsPaused ? "Start": "Pause";        
        ResetButton();
        UpdateUI();
    }

    void UpdateUI()
    {
        AcclerationText.text = Math.Abs(pulleySystem_Simple.acceleration).ToString("F1")+" m/s";
        TensionText.text = pulleySystem_Simple.tension.ToString("F1")+" N [↑]";
    }

    public void StartSim()
    {
        pulleySystem_Simple.IsPaused = !pulleySystem_Simple.IsPaused; // Reverse bool
        ButtonStartText.text = pulleySystem_Simple.IsPaused ? "Start": "Pause";
    }    

    public void ResetButton()
    {
        pulleySystem_Simple.ResetPulleySim();
    }
    public void OpenExplainPanel()
    {
        ExplainPanelObj.SetActive(true);
    }

    public void CloseExplainPanel()
    {
        ExplainPanelObj.SetActive(false);
        Scroll_Content.transform.position = new Vector3(Scroll_Content.transform.position.x,0,Scroll_Content.transform.position.z);
    }       
    public void ExitButton()
    {
        SceneManager.LoadScene(0);
    }       
}
