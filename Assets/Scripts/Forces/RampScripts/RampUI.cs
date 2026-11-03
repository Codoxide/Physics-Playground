using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RampUI : MonoBehaviour
{
    public Toggle FBD_Toggle;

    public Slider MassA_Slide;
    public Slider Ramp_Slide;
    public Slider Friction_Slide;

    public Text MassA_Text;
    public Text Ramp_Text;
    public Text Friction_Text;
    
    public Text AcclerationStatText;
    public Text FrictionStatText;  
    public Text NormalStatText;

    public Text ButtonStartText;

    public TextMesh AngleText;
    
    public Transform RampParent;
    public Transform Mass;

    public Transform Up_Point;
    public Transform Down_Point;
    public Transform Left_Point;
    public Transform Right_Point;

    public GameObject AngleDrawer;
    public GameObject PausedTEXT;

    public GameObject GravityArrowHead;
    public GameObject GravityArrow;

    public GameObject GravityArrowHeadY;
    public GameObject GravityArrowY;

    public GameObject GravityArrowHeadX;
    public GameObject GravityArrowX;

    public GameObject NormalArrowHead;
    public GameObject NormalArrow;    

    public GameObject FrictionArrowHead;
    public GameObject FrictionArrow; 

    public GameObject ArrowGroup;             

    public RampSystem_ForcesScript rampSystem_ForcesScript;

    public float ArrowSizer;

    public GameObject ExplainPanelObj;  
    public GameObject Scroll_Content;  


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Friction_SlideChange();
        MassA_SlideChange();
        RampAngle_SlideChange();
        DrawArrows();
    }

    // Update is called once per frame
    void Update()
    {
        DrawArrows();
    }


    public void Friction_SlideChange()
    {
        float CoEfficient = Friction_Slide.value*0.1f;
        Friction_Text.text = "Coefficent of Friction: "+CoEfficient;
        rampSystem_ForcesScript.CoefficentFriction = CoEfficient;
        rampSystem_ForcesScript.SetValues();
        ResetButton();
        UpdateUI();
    }

    public void ToggleChange()
    {
        ArrowGroup.SetActive(FBD_Toggle.isOn);
    }
    public void RampAngle_SlideChange()
    {
        float RampAngle = Ramp_Slide.value;
        Ramp_Text.text = "Ramp Angle: "+RampAngle+"º";
        AngleText.text = "θ = "+RampAngle+"º";
        RampParent.eulerAngles = new Vector3(0, 0, RampAngle);
        if(RampAngle == 0)
        {
            AngleDrawer.SetActive(false);
        }
        else
        {
            AngleDrawer.SetActive(true);
        }
        rampSystem_ForcesScript.DrawAngle();   
        rampSystem_ForcesScript.SetValues();   
        ResetButton();
        UpdateUI();
        DrawArrows();


    } 

    public void MassA_SlideChange()
    {
        rampSystem_ForcesScript.MassWeight = MassA_Slide.value ;
        MassA_Text.text = "Mass A: "+MassA_Slide.value +" kg";
        rampSystem_ForcesScript.SetValues();

        ResetButton();
        UpdateUI();
    }

    void UpdateUI()
    {
        AcclerationStatText.text = Mathf.Abs(rampSystem_ForcesScript.acceleration).ToString("F1")+" m/s²";
        FrictionStatText.text = rampSystem_ForcesScript.Friction_Force.ToString("F1")+" N";
        NormalStatText.text = rampSystem_ForcesScript.Normal_Force.ToString("F1")+" N";

                
    }    
    public void StartButton()
    {
        rampSystem_ForcesScript.IsPaused = !rampSystem_ForcesScript.IsPaused;
        UpdateStartButton();         
    }
    public void ResetButton()
    {
        rampSystem_ForcesScript.ResetPulleySim();
    }
    void UpdateStartButton()
    {
        ButtonStartText.text = rampSystem_ForcesScript.IsPaused ? "Start": "Pause";  
        if(rampSystem_ForcesScript.IsPaused)
        {
            PausedTEXT.SetActive(true);
        }
        else
        {
            PausedTEXT.SetActive(false);
        }
    }
    // Draws the horizontal, vertical, and resultant velocity arrows.
    void DrawArrows()
    {
        // Gravity arrow
        float lengthGrav = (rampSystem_ForcesScript.MassWeight*rampSystem_ForcesScript.GravityForce_Abs) * ArrowSizer;

        Vector3 GravStartX = Down_Point.position;
        Vector3 GraveEndX = GravStartX + Vector3.down * lengthGrav;

        LineRenderer lineGrav = GravityArrow.GetComponent<LineRenderer>();

        lineGrav.SetPosition(0, GravStartX);
        lineGrav.SetPosition(1, GraveEndX);

        GravityArrowHead.transform.position = GraveEndX;

        // y-comp of Gravity arrow
        float lengthGravY = (rampSystem_ForcesScript.MassWeight*rampSystem_ForcesScript.GravityForce_Abs)*Mathf.Cos(Ramp_Slide.value*Mathf.Deg2Rad) * ArrowSizer;

        Vector3 YGravStartX = Down_Point.position;
        Vector3 YGravEndX = YGravStartX + -Mass.transform.up * lengthGravY;

        LineRenderer lineGravY = GravityArrowY.GetComponent<LineRenderer>();

        lineGravY.SetPosition(0, YGravStartX);
        lineGravY.SetPosition(1, YGravEndX);

        GravityArrowHeadY.transform.position = YGravEndX;


        GravityArrowHeadY.transform.rotation =
            Quaternion.Euler(0, 0, 180+Ramp_Slide.value);      

        // X-comp of Gravity arrow

        LineRenderer lineGravX = GravityArrowX.GetComponent<LineRenderer>();

        lineGravX.SetPosition(0, GravityArrowHeadY.transform.position);
        lineGravX.SetPosition(1, GravityArrowHead.transform.position);

        GravityArrowHeadX.transform.position = GravityArrowHead.transform.position;


        GravityArrowHeadX.transform.rotation =
            Quaternion.Euler(0, 0, 90+Ramp_Slide.value);                


        // Force of Normal arrow
        float lengthNorm = (rampSystem_ForcesScript.MassWeight*rampSystem_ForcesScript.GravityForce_Abs)*Mathf.Cos(Ramp_Slide.value*Mathf.Deg2Rad) * ArrowSizer;

        Vector3 NormStartX = Up_Point.position;
        Vector3 NormEndX = NormStartX + Mass.transform.up * lengthNorm;

        LineRenderer lineNorm = NormalArrow.GetComponent<LineRenderer>();

        lineNorm.SetPosition(0, NormStartX);
        lineNorm.SetPosition(1, NormEndX);

        NormalArrowHead.transform.position = NormEndX;


        NormalArrowHead.transform.rotation =
            Quaternion.Euler(0, 0, Ramp_Slide.value); 

        // // Force of Friction arrow
        
        float lengthFric = rampSystem_ForcesScript.CoefficentFriction*(rampSystem_ForcesScript.MassWeight*rampSystem_ForcesScript.GravityForce_Abs)*Mathf.Cos(Ramp_Slide.value*Mathf.Deg2Rad) * ArrowSizer;
        if(lengthFric==0)
        {
            FrictionArrowHead.SetActive(false);
        }
        else
        {
            FrictionArrowHead.SetActive(true);
        }
        Vector3 FricStartX = Right_Point.position;
        Vector3 FricEndX = FricStartX + Mass.transform.right * lengthFric;

        LineRenderer lineFric = FrictionArrow.GetComponent<LineRenderer>();

        lineFric.SetPosition(0, FricStartX);
        lineFric.SetPosition(1, FricEndX);

        FrictionArrowHead.transform.position = FricEndX;


        FrictionArrowHead.transform.rotation =
            Quaternion.Euler(0, 0, -90+Ramp_Slide.value); 
    }    
    public void OpenExplainPanel()
    {
        rampSystem_ForcesScript.IsPaused=true;
        rampSystem_ForcesScript.CanMove=false;
        ExplainPanelObj.SetActive(true);
    }

    public void CloseExplainPanel()
    {
        rampSystem_ForcesScript.IsPaused=false;
        rampSystem_ForcesScript.CanMove=true;     
        UpdateStartButton();           
        ExplainPanelObj.SetActive(false);
        Scroll_Content.transform.position = new Vector3(0,0,0);
    }     
    public void ExitButton()
    {
        SceneManager.LoadScene(0);
    }       
}

