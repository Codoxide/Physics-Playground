using System;
using UnityEngine;
using UnityEngine.UI;

public class PulleySimple_UI : MonoBehaviour
{
    public Slider MassA_Slide;
    public Slider MassB_Slide;

    public Text AcclerationText;
    public Text TensionText;

    public PulleySystem_Simple pulleySystem_Simple;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MassA_SlideChange();
        MassB_SlideChange();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MassB_SlideChange()
    {
        pulleySystem_Simple.Mass2_Kg = MassB_Slide.value * 0.25f;
        pulleySystem_Simple.SetValues();
        UpdateUI();
    } 

    public void MassA_SlideChange()
    {
        pulleySystem_Simple.Mass1_Kg = MassA_Slide.value * 0.25f;
        pulleySystem_Simple.SetValues();
        UpdateUI();
    }

    void UpdateUI()
    {
        AcclerationText.text = Math.Abs(pulleySystem_Simple.acceleration).ToString("F1")+" m/s";
        TensionText.text = (pulleySystem_Simple.tension).ToString("F1")+" N";

    }
}
