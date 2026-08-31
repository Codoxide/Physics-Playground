using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;
public class PulleyComplex_UI : MonoBehaviour
{
    public Slider MassA_Slide;
    public Slider MassB_Slide;
    public Slider Friction_Slide;

    public Text ButtonStartText;

    public Text MassA_Text;
    public Text MassB_Text;
    public Text Friction_Text;
    
    public Text AcclerationText;
    public Text TensionText;


    public PulleySystemComplexScript pulleySystemScript;

    public GameObject MassA;
    public GameObject MassB;

    public GameObject ExplainPanelObj;
    public GameObject Scroll_Content;


    public float MaxMass_Size;
    public float MinMass_Size;

    float StartTimer = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ButtonStartText.text = pulleySystemScript.IsPaused ? "Start": "Pause";
        MassA_SlideChange();
        MassB_SlideChange();
        Friction_SlideChange();
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void Friction_SlideChange()
    {
        float CoEfficient = Friction_Slide.value*0.1f;
        Friction_Text.text = "Coefficent of Friction: "+CoEfficient;
        pulleySystemScript.FrictionCoEfficient = CoEfficient;
        pulleySystemScript.SetValues();
        ResetButton();
        UpdateUI();
    }


    public void MassB_SlideChange()
    {
        float scaleClamped = MinMass_Size + (MassB_Slide.normalizedValue * (MaxMass_Size - MinMass_Size));
        MassB.transform.localScale = new Vector3(scaleClamped, scaleClamped, 1);        
        pulleySystemScript.Mass2_Kg = MassB_Slide.value;
        MassB_Text.text = "Mass B: "+MassB_Slide.value+" kg";
        StartTimer = 0;
        pulleySystemScript.SetValues();
        pulleySystemScript.IsPaused = true;
        ButtonStartText.text = pulleySystemScript.IsPaused ? "Start": "Pause";     
        ResetButton();
        UpdateUI();
    } 

    public void MassA_SlideChange()
    {
        float scaleClamped = MinMass_Size + (MassA_Slide.normalizedValue * (MaxMass_Size - MinMass_Size));
        MassA.transform.localScale = new Vector3(scaleClamped, scaleClamped, 1);
        pulleySystemScript.Mass1_Kg = MassA_Slide.value ;
        MassA_Text.text = "Mass A: "+MassA_Slide.value +" kg";
        StartTimer = 0;
        pulleySystemScript.SetValues();
        pulleySystemScript.IsPaused = true;
        ButtonStartText.text = pulleySystemScript.IsPaused ? "Start": "Pause";        
        ReSizeMassA();
        ResetButton();
        UpdateUI();
    }

    void UpdateUI()
    {
        AcclerationText.text = Mathf.Abs(pulleySystemScript.acceleration).ToString("F1")+" m/s";
        TensionText.text = pulleySystemScript.tension.ToString("F1")+" N";
    }

    public void StartSim()
    {
        pulleySystemScript.IsPaused = !pulleySystemScript.IsPaused; // Reverse bool
        ButtonStartText.text = pulleySystemScript.IsPaused ? "Start": "Pause";
    }    

    public void ResetButton()
    {
        StartTimer = 0;
        pulleySystemScript.ResetPulleySim();
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

    void ReSizeMassA()
    {
        float NewY = 2.6f+(MassA.transform.localScale.y/2);
        MassA.transform.localPosition = new Vector3(MassA.transform.position.x,NewY,MassA.transform.position.z);
    }
}
