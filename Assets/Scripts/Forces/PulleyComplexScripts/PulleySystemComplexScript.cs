using System;
using UnityEngine;

public class PulleySystemComplexScript : MonoBehaviour
{

    public LineRenderer MassA_LineRender;
    public LineRenderer MassB_LineRender;

    public LineRenderer MassA_Gravity_LineRender;
    public LineRenderer MassA_Tension_LineRender;
    public LineRenderer MassB_Gravity_LineRender;
    public LineRenderer MassB_Tension_LineRender;
    public LineRenderer MassA_Friction_LineRender;
    public LineRenderer MassA_Normal_LineRender;


    public Transform MassA_Gravity_Arrowhead;
    public Transform MassA_Tension_Arrowhead;
    public Transform MassB_Gravity_Arrowhead;
    public Transform MassB_Tension_Arrowhead;

    public Transform MassA_Friction_Arrowhead;
    public Transform MassA_Normal_Arrowhead;


    public Transform MassA_EndPoint;
    public Transform MassB_EndPoint;
    public Transform MassA_StartPoint;
    public Transform MassB_StartPoint;

    public Transform MassA;
    public Transform MassB;

    public float GravityForce_Abs = 9.81f;

    public float FrictionCoEfficient;    

    public float Mass1_Kg;
    public float Mass2_Kg; 

    public bool IsPaused = true;
    public bool CanMove = true;

    public float minArrowLength = 0.5f;
    public float maxArrowLength = 3f;    

    public float ArrowSizeScaler =2;
    public float arrowGap = 0.2f;

    public float acceleration; 
    public float velocity=0;  
    public float tension;   
    float StartXVal;
    float StartYVal;

  
    void Awake()
    {
        StartXVal = MassA.transform.position.x; 
        StartYVal = MassB.transform.position.y;        

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
        float ForceNormal_Mass1 = Mass1_Kg*GravityForce_Abs;
        float ForceFriction_Mass1 = ForceNormal_Mass1*FrictionCoEfficient;
        float ForceGravity_Mass2 = Mass2_Kg*GravityForce_Abs;
        float acceleration = (-ForceFriction_Mass1+ForceGravity_Mass2)/(Mass1_Kg+Mass2_Kg);
        return acceleration;        
    }    
    float CalculateTensionForce()
    {
        float tension = (Mass2_Kg*GravityForce_Abs)-(Mass2_Kg*CalculateAcceleration());
        return tension;
    }    
    public void SetValues()
    {
        tension = CalculateTensionForce();        
        acceleration = CalculateAcceleration();
    }    

    void MoveMasses()
    { 
        CanMove = true;           
        if(IsPaused)
        {
            return;
        }
        
        if(MassA.position.x < -1.8f-(MassA.localScale.y/2))
        {
            velocity += acceleration * Time.fixedDeltaTime;
            if(velocity < 0)
            {
                velocity = 0;
            }

            MassB.position += new Vector3(0, -velocity) * Time.fixedDeltaTime;   
            MassA.position += new Vector3(velocity, 0) * Time.fixedDeltaTime;                     
        }
        else
        {
            CanMove = false;
        }
        
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

        MassA_Gravity_Arrowhead.transform.position =  new Vector3(gravityEndPoint.x, gravityEndPoint.y, MassA_Gravity_Arrowhead.transform.position.z);


        // Tension force arrow — Mass A
        float tensionArrowLength = ArrowSizeScaler * tension;

        Vector3 tensionStartPoint = MassA.transform.position;
        Vector3 tensionEndPoint =
            tensionStartPoint + Vector3.right * (tensionArrowLength+arrowGap);

        LineRenderer tensionLine =
            MassA_Tension_LineRender.GetComponent<LineRenderer>();

        tensionLine.SetPosition(0, tensionStartPoint);
        tensionLine.SetPosition(1, tensionEndPoint);

        MassA_Tension_Arrowhead.transform.position = new Vector3(tensionEndPoint.x, tensionEndPoint.y, MassA_Tension_Arrowhead.transform.position.z);

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

        MassB_Gravity_Arrowhead.transform.position = new Vector3(gravityEndPoint2.x, gravityEndPoint2.y, MassB_Gravity_Arrowhead.transform.position.z);


        // Tension force arrow — Mass A
        float tensionArrowLength2 = ArrowSizeScaler * tension;

        Vector3 tensionStartPoint2 = MassB.transform.position;
        Vector3 tensionEndPoint2 =
            tensionStartPoint2 + Vector3.up * (tensionArrowLength2+arrowGap);

        LineRenderer tensionLine2 =
            MassB_Tension_LineRender.GetComponent<LineRenderer>();

        tensionLine2.SetPosition(0, tensionStartPoint2);
        tensionLine2.SetPosition(1, tensionEndPoint2);

        MassB_Tension_Arrowhead.transform.position = new Vector3(tensionEndPoint2.x, tensionEndPoint2.y, MassB_Tension_Arrowhead.transform.position.z);      

        
        
        // Normal force arrow — Mass A

        gravityStartPoint = MassA.transform.position;
        gravityEndPoint =
            gravityStartPoint + Vector3.up * (gravityArrowLength+arrowGap);

        LineRenderer normalLineRend =
            MassA_Normal_LineRender.GetComponent<LineRenderer>();

        normalLineRend.SetPosition(0, gravityStartPoint);
        normalLineRend.SetPosition(1, gravityEndPoint);

        MassA_Normal_Arrowhead.transform.position =  new Vector3(gravityEndPoint.x, gravityEndPoint.y, MassA_Normal_Arrowhead.transform.position.z);

        
        
        
        
        // Friction force arrow — Mass A
        

        float frictionForce = gravityForceMassA * FrictionCoEfficient;
        if(frictionForce == 0)
        {
            MassA_Friction_Arrowhead.gameObject.SetActive(false);
            MassA_Friction_LineRender.gameObject.SetActive(false);
            return;            
        }
        MassA_Friction_Arrowhead.gameObject.SetActive(true);     
        MassA_Friction_LineRender.gameObject.SetActive(true);           
        
        float frictionForceLength = frictionForce *ArrowSizeScaler;

        Vector3 FrictionStartPoint = MassA.transform.position;
        Vector3 FrictionEndPoint =
            FrictionStartPoint + Vector3.left * (frictionForceLength+arrowGap);

        LineRenderer frictionLine =
            MassA_Friction_LineRender.GetComponent<LineRenderer>();

        frictionLine.SetPosition(0, FrictionStartPoint);
        frictionLine.SetPosition(1, FrictionEndPoint);

        MassA_Friction_Arrowhead.transform.position = new Vector3(FrictionEndPoint.x, FrictionEndPoint.y, MassA_Friction_Arrowhead.transform.position.z);      


    }

    public void ResetPulleySim()
    {
        velocity = 0;
        MassA.transform.position = new Vector3(StartXVal, MassA.transform.position.y, MassA.transform.position.z);
        MassB.transform.position = new Vector3(MassB.transform.position.x, StartYVal, MassB.transform.position.z);

    }
}
