using UnityEngine;

public class PulleySystem_Simple : MonoBehaviour
{

    public LineRenderer MassA_LineRender;
    public LineRenderer MassB_LineRender;


    public Transform MassA;
    public Transform MassB;
    
    public Transform MassA_EndPoint;
    public Transform MassB_EndPoint;
    public Transform MassA_StartPoint;
    public Transform MassB_StartPoint;

    public float GravityForce_Abs = 9.81f;

    public float Mass1_Kg;
    public float Mass2_Kg;

    public float tension;
    public  float acceleration;
    float velocity=0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetValues();
    }

    // Update is called once per frame
    void Update()
    {
        DrawLines();
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
    
    void MoveMasses()
    {
        if(MassA.position.y < 1f-(MassA.localScale.y/2) && MassB.position.y < 1f-(MassB.localScale.y/2))
        {
            velocity += acceleration * Time.fixedDeltaTime;

            MassB.position += new Vector3(0, velocity) * Time.fixedDeltaTime;   
            MassA.position += new Vector3(0, -velocity) * Time.fixedDeltaTime;             
        }
    }

}
