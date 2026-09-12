using Unity.Mathematics;
using UnityEngine;

public class RampSystem_ForcesScript : MonoBehaviour
{
    public Transform Mass;

    public Transform StartPoint;
    public Transform EndPoint;


    public Transform RampTransform;

    public float MassWeight = 14.0f;
    public float GravityForce_Abs = 9.81f;

    public float velocity;
    public float radius = 0.25f;

    public LineRenderer lineRend;

    public int line_segments;

    
    public bool IsPaused;
    public bool CanMove;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DrawAngle();    
    }
    void FixedUpdate()
    {
        MoveMasses(CalculateAcceleration());
    }

    float CalculateAcceleration()
    {
        float ForceGravity_Mass = MassWeight*GravityForce_Abs;
        float acceleration = ForceGravity_Mass*Mathf.Sin(RampTransform.rotation.z);
        return acceleration;        
    }

    public void DrawAngle()
    {
        lineRend.positionCount = line_segments + 1;

        lineRend.SetPosition(0, StartPoint.position);

        float totalAngle = RampTransform.transform.eulerAngles.z;

        for (int i = 1; i < line_segments; i++)
        {
            float currentAngle = totalAngle * i / line_segments;

            float x = Mathf.Cos(currentAngle * Mathf.Deg2Rad) * radius;
            float y = Mathf.Sin(currentAngle * Mathf.Deg2Rad) * radius;

            Vector3 point = StartPoint.position + new Vector3(x, y, 0);

            lineRend.SetPosition(i, point);
        }

        lineRend.SetPosition(line_segments, EndPoint.position);
    }

    void MoveMasses(float acceleration)
    {
        if(IsPaused)
        {
            return;
        }
        
        if(Mass.position.x > -5.5f)
        {
            velocity += acceleration * Time.fixedDeltaTime;

            Mass.position += Mass.TransformDirection(Vector3.left) * velocity * Time.fixedDeltaTime;

            CanMove = true;                       
        }
        else
        {
            CanMove = false;
        }
        
    }    
}
