using UnityEngine;

public class PulleySystem_Simple : MonoBehaviour
{

    public LineRenderer MassA_LineRender;
    public LineRenderer MassB_LineRender;

    public LineRenderer MassA_Gravity_LineRender;
    public LineRenderer MassA_Tension_LineRender;
    public LineRenderer MassB_Gravity_LineRender;
    public LineRenderer MassB_Tension_LineRender;

    public Transform MassA;
    public Transform MassB;
    
    public Transform MassA_Gravity_Arrowhead;
    public Transform MassA_Tension_Arrowhead;
    public Transform MassB_Gravity_Arrowhead;
    public Transform MassB_Tension_Arrowhead;

    public Transform MassA_EndPoint;
    public Transform MassB_EndPoint;
    public Transform MassA_StartPoint;
    public Transform MassB_StartPoint;

    public float GravityForce_Abs = 9.81f;

    public float Mass1_Kg;
    public float Mass2_Kg;

    public float tension;
    public float acceleration;
     // Arrow length limits
    public float minArrowLength = 0.5f;
    public float maxArrowLength = 3f;    

    public float ArrowSizeScaler =2;
    public float arrowGap = 0.2f;

    public bool IsPaused = true;
    public bool CanMove = true;


    float velocity=0;
    float StartYVal;


    void Awake()
    {
        StartYVal = MassA.transform.position.y;        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetValues();
    }

    // Update is called once per frame
    void Update()
    {
        DrawLines();
        DrawForceArrows();
    }

    void FixedUpdate()
    {
        MoveMasses();
    }

    void DrawLines()
    {
        // Set the rope for Mass A
        MassA_LineRender.positionCount = 2;
        MassA_LineRender.SetPosition(0,MassA_StartPoint.position);
        MassA_LineRender.SetPosition(1,MassA_EndPoint.position);
        // Set the rope for Mass B
        MassB_LineRender.positionCount = 2;
        MassB_LineRender.SetPosition(0,MassB_StartPoint.position);
        MassB_LineRender.SetPosition(1,MassB_EndPoint.position);
    }


    float CalculateAcceleration()
    {
        float ForceGravity_Mass1 = Mass1_Kg*GravityForce_Abs;
        float ForceGravity_Mass2 = Mass2_Kg*GravityForce_Abs;
        float acceleration = (-ForceGravity_Mass1+ForceGravity_Mass2)/(Mass1_Kg+Mass2_Kg);
        return acceleration;        
    }

    float CalculateTensionForce()
    {
        float tension = Mass1_Kg*CalculateAcceleration()+ Mass1_Kg*GravityForce_Abs;
        return tension;
    }    

    
    public void SetValues()
    {
        tension = CalculateTensionForce();        
        acceleration = CalculateAcceleration();
    }

    public void ResetPulleySim()
    {
        velocity = 0;
        MassA.transform.position = new Vector3(MassA.transform.position.x, StartYVal, MassA.transform.position.z);
        MassB.transform.position = new Vector3(MassB.transform.position.x, StartYVal, MassB.transform.position.z);

    }
    void DrawForceArrows()
    {
        // Gravity force arrow — Mass A
        float gravityForceMassA = GravityForce_Abs * Mass1_Kg;

        float gravityArrowLength = gravityForceMassA * ArrowSizeScaler;

        Vector3 gravityStartPoint = MassA.transform.position;
        Vector3 gravityEndPoint =
            gravityStartPoint + Vector3.down * (gravityArrowLength+arrowGap);

        LineRenderer gravityLine =
            MassA_Gravity_LineRender.GetComponent<LineRenderer>();

        gravityLine.SetPosition(0, gravityStartPoint);
        gravityLine.SetPosition(1, gravityEndPoint);

        MassA_Gravity_Arrowhead.transform.position = gravityEndPoint;


        // Tension force arrow — Mass A
        float tensionArrowLength = ArrowSizeScaler * tension;

        Vector3 tensionStartPoint = MassA.transform.position;
        Vector3 tensionEndPoint =
            tensionStartPoint + Vector3.up * (tensionArrowLength+arrowGap);

        LineRenderer tensionLine =
            MassA_Tension_LineRender.GetComponent<LineRenderer>();

        tensionLine.SetPosition(0, tensionStartPoint);
        tensionLine.SetPosition(1, tensionEndPoint);

        MassA_Tension_Arrowhead.transform.position = tensionEndPoint;

        // Gravity force arrow — Mass B
        float gravityForceMassB = GravityForce_Abs * Mass2_Kg;

        float gravityArrowLength2 = gravityForceMassB * ArrowSizeScaler;

        Vector3 gravityStartPoint2 = MassB.transform.position;
        Vector3 gravityEndPoint2 =
            gravityStartPoint2 + Vector3.down * (gravityArrowLength2+arrowGap);

        LineRenderer gravityLine2 =
            MassB_Gravity_LineRender.GetComponent<LineRenderer>();

        gravityLine2.SetPosition(0, gravityStartPoint2);
        gravityLine2.SetPosition(1, gravityEndPoint2);

        MassB_Gravity_Arrowhead.transform.position = gravityEndPoint2;


        // Tension force arrow — Mass A
        float tensionArrowLength2 = ArrowSizeScaler * tension;

        Vector3 tensionStartPoint2 = MassB.transform.position;
        Vector3 tensionEndPoint2 =
            tensionStartPoint2 + Vector3.up * (tensionArrowLength2+arrowGap);

        LineRenderer tensionLine2 =
            MassB_Tension_LineRender.GetComponent<LineRenderer>();

        tensionLine2.SetPosition(0, tensionStartPoint2);
        tensionLine2.SetPosition(1, tensionEndPoint2);

        MassB_Tension_Arrowhead.transform.position = tensionEndPoint2;        
    }
    void MoveMasses()
    {
        if(IsPaused)
        {
            return;
        }
        
        if(MassA.position.y < 1f-(MassA.localScale.y/2) && MassB.position.y < 1f-(MassB.localScale.y/2))
        {
            velocity += acceleration * Time.fixedDeltaTime;

            MassB.position += new Vector3(0, -velocity) * Time.fixedDeltaTime;   
            MassA.position += new Vector3(0, velocity) * Time.fixedDeltaTime;  
            CanMove = true;                       
        }
        else
        {
            CanMove = false;
        }
        
    }

}
