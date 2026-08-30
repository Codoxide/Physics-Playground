using System;
using UnityEngine;

public class PulleySystemComplexScript : MonoBehaviour
{


    public Transform MassA_EndPoint;
    public Transform MassB_EndPoint;
    public Transform MassA_StartPoint;
    public Transform MassB_StartPoint;

    public Transform MassA;
    public Transform MassB;

    public LineRenderer MassA_LineRender;
    public LineRenderer MassB_LineRender;

    public float GravityForce_Abs = 9.81f;

    public float FrictionCoEfficient;    

    public float Mass1_Kg;
    public float Mass2_Kg; 

    public bool IsPaused = true;
    public bool CanMove = true;

    float velocity=0;  
    float tension;   
    float acceleration; 

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
        SetValues();
 
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
        
}
