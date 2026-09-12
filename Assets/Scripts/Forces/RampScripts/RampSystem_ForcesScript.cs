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
        lineRend.positionCount = line_segments;

        Vector3 start = StartPoint.position;
        Vector3 end = EndPoint.position;

        // Controls how much the line curves
        Vector3 middle = ((start + end) / 2f);

        // Push the middle point perpendicular to the line
        Vector3 direction = (end - start).normalized;
        Vector3 perpendicular = new Vector3(-direction.y, direction.x, 0);

        middle += perpendicular * radius;

        for (int i = 0; i < line_segments; i++)
        {
            float t = (float)i / (line_segments - 1);

            Vector3 point =
                Mathf.Pow(1 - t, 2) * start +
                2 * (1 - t) * t * middle +
                Mathf.Pow(t, 2) * end;

            lineRend.SetPosition(i, point);
        }
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
