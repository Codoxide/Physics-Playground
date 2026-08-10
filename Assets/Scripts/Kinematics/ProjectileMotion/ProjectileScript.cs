using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{

    public TrailRenderer trailRenderer;

    public GameObject X_ArrowHead;
    public GameObject Y_ArrowHead;
    public GameObject Result_ArrowHead;

    public GameObject X_Arrow;
    public GameObject Y_Arrow;
    public GameObject R_Arrow;


    public Transform ResetPosTransform;

    public float ArrowSizer;
    public float GravityAcceleration = 9.81f;

    public bool IsGround = false;
    bool KillSwitch = false;
    public bool isPaused = false;

    public float timerStart;


    public float displacement_X;
    public float initialVelocity_X;
    public float angle;
    public float finalVelocity_Y;

    public Vector2 velocity;
    Vector2 ProjectileMoveVector;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetArrowHeadColor();
        ResetPos();
        DrawArrows();     
    }

    void FixedUpdate()
    {
        if (isPaused || KillSwitch)
            return;

        if (IsGround)
        {
            finalVelocity_Y = 0f;

            // Keep moving horizontally
            ProjectileMoveVector = new Vector2(
                initialVelocity_X,
                0f
            );

            transform.position +=
                (Vector3)ProjectileMoveVector * Time.fixedDeltaTime;

            return;
        }

        // Falling
        finalVelocity_Y = timerStart * GravityAcceleration;

        ProjectileMoveVector = new Vector2(
            initialVelocity_X,
            finalVelocity_Y
        );

        transform.position +=
            (Vector3)ProjectileMoveVector * Time.fixedDeltaTime;

        timerStart += Time.fixedDeltaTime;
    }

    void Update()
    {
        DrawArrows();
    }

    public void ResetPos()
    {
        isPaused = true;  
        finalVelocity_Y = 0;  
        trailRenderer.Clear();
        trailRenderer.enabled = false;
        timerStart = 0;    
        transform.position = ResetPosTransform.position;
    }
    
    void SetArrowHeadColor()
    {
        LineRenderer lineX = X_Arrow.GetComponent<LineRenderer>();
        LineRenderer lineY = Y_Arrow.GetComponent<LineRenderer>();
        LineRenderer lineR = R_Arrow.GetComponent<LineRenderer>();
        
        
        X_ArrowHead.transform.GetComponent<SpriteRenderer>().color = lineX.endColor;
        Y_ArrowHead.transform.GetComponent<SpriteRenderer>().color = lineY.endColor;
        Result_ArrowHead.transform.GetComponent<SpriteRenderer>().color = lineR.endColor;

    }
    
    void DrawArrows()
    {
        // X velocity arrow
        float lengthX = initialVelocity_X * ArrowSizer;

        Vector3 startX = transform.transform.position;
        Vector3 endX = startX + Vector3.right * lengthX;

        LineRenderer lineX = X_Arrow.GetComponent<LineRenderer>();

        lineX.SetPosition(0, startX);
        lineX.SetPosition(1, endX);

        X_ArrowHead.transform.position = endX;

        // Y velocity arrow
        float lengthY = finalVelocity_Y * ArrowSizer;

        Vector3 startY = transform.position;
        Vector3 endY = startY + Vector3.up * lengthY;

        LineRenderer lineY = Y_Arrow.GetComponent<LineRenderer>();

        lineY.SetPosition(0, startY);
        lineY.SetPosition(1, endY);

        Y_ArrowHead.transform.position = endY;
        // Resultant
        LineRenderer line = R_Arrow.GetComponent<LineRenderer>();
        velocity = new Vector2(initialVelocity_X, finalVelocity_Y);

        float length = velocity.magnitude * ArrowSizer;

        Vector3 start = transform.position;
        Vector3 end = start + (Vector3)velocity.normalized * length;

        line.SetPosition(0, start);
        line.SetPosition(1, end);

        Result_ArrowHead.transform.position = end;

        angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        Result_ArrowHead.transform.rotation = Quaternion.Euler(0, 0, angle-90);                      
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        IsGround = true;
        if(collision.gameObject.CompareTag("Ground"))
        {
            KillSwitch = true;
        }
        else
        {
            trailRenderer.Clear();
            trailRenderer.enabled = false;            
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        IsGround = false;
        KillSwitch = false;
        trailRenderer.enabled = true;

        
    }
    
}
