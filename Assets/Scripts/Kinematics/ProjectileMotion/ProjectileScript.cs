using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [Header("Trail")]
    public TrailRenderer trailRenderer;

    [Header("Velocity Arrows")]
    public GameObject X_ArrowHead;
    public GameObject Y_ArrowHead;
    public GameObject Result_ArrowHead;

    public GameObject X_Arrow;
    public GameObject Y_Arrow;
    public GameObject R_Arrow;

    [Header("Projectile Settings")]
    public Transform ResetPosTransform;
    public float ArrowSizer;
    public float GravityAcceleration = 9.81f;

    [Header("Projectile State")]
    public bool IsGround = false;
    public bool isPaused = false;

    bool KillSwitch = false;

    [Header("Projectile Data")]
    public float timerStart;
    public float displacement_X;
    public float Velocity_X;
    public float angle;
    public float Velocity_Y;

    public Vector2 velocity;

    Vector2 ProjectileMoveVector;


    // Initializes the projectile and velocity arrows.
    void Start()
    {
        SetArrowHeadColor();
        ResetPos();
        DrawArrows();
    }

    // Moves the projectile using fixed physics intervals.
    void FixedUpdate()
    {
        if (isPaused || KillSwitch)
            return;

        if (IsGround)
        {
            Velocity_Y = 0f;

            // Continue horizontal motion after reaching the ground.
            ProjectileMoveVector = new Vector2(
                Velocity_X,
                0f
            );

            transform.position +=
                (Vector3)ProjectileMoveVector * Time.fixedDeltaTime;

            return;
        }

        Velocity_Y = timerStart * GravityAcceleration;

        ProjectileMoveVector = new Vector2(
            Velocity_X,
            Velocity_Y
        );

        transform.position +=
            (Vector3)ProjectileMoveVector * Time.fixedDeltaTime;

        timerStart += Time.fixedDeltaTime;
    }

    // Updates the velocity arrows.
    void Update()
    {
        DrawArrows();
    }

    // Resets the projectile and clears its trail.
    public void ResetPos()
    {
        isPaused = true;
        Velocity_Y = 0;

        trailRenderer.Clear();
        trailRenderer.enabled = false;

        timerStart = 0;

        transform.position = ResetPosTransform.position;
    }

    // Matches each arrowhead's colour to its LineRenderer.
    void SetArrowHeadColor()
    {
        LineRenderer lineX = X_Arrow.GetComponent<LineRenderer>();
        LineRenderer lineY = Y_Arrow.GetComponent<LineRenderer>();
        LineRenderer lineR = R_Arrow.GetComponent<LineRenderer>();

        X_ArrowHead.transform.GetComponent<SpriteRenderer>().color =
            lineX.endColor;

        Y_ArrowHead.transform.GetComponent<SpriteRenderer>().color =
            lineY.endColor;

        Result_ArrowHead.transform.GetComponent<SpriteRenderer>().color =
            lineR.endColor;
    }

    // Draws the horizontal, vertical, and resultant velocity arrows.
    void DrawArrows()
    {
        // X velocity arrow
        float lengthX = Velocity_X * ArrowSizer;

        Vector3 startX = transform.transform.position;
        Vector3 endX = startX + Vector3.right * lengthX;

        LineRenderer lineX = X_Arrow.GetComponent<LineRenderer>();

        lineX.SetPosition(0, startX);
        lineX.SetPosition(1, endX);

        X_ArrowHead.transform.position = endX;


        // Y velocity arrow
        float lengthY = Velocity_Y * ArrowSizer;

        Vector3 startY = transform.position;
        Vector3 endY = startY + Vector3.up * lengthY;

        LineRenderer lineY = Y_Arrow.GetComponent<LineRenderer>();

        lineY.SetPosition(0, startY);
        lineY.SetPosition(1, endY);

        Y_ArrowHead.transform.position = endY;


        // Resultant velocity arrow
        LineRenderer line = R_Arrow.GetComponent<LineRenderer>();

        velocity = new Vector2(
            Velocity_X,
            Velocity_Y
        );

        float length = velocity.magnitude * ArrowSizer;

        Vector3 start = transform.position;
        Vector3 end = start + (Vector3)velocity.normalized * length;

        line.SetPosition(0, start);
        line.SetPosition(1, end);

        Result_ArrowHead.transform.position = end;

        angle =
            Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;

        Result_ArrowHead.transform.rotation =
            Quaternion.Euler(0, 0, angle - 90);
    }

    // Detects when the projectile enters a trigger.
    void OnTriggerEnter2D(Collider2D collision)
    {
        IsGround = true;

        if (collision.gameObject.CompareTag("Ground"))
        {
            KillSwitch = true;
        }
        else
        {
            trailRenderer.Clear();
            trailRenderer.enabled = false;
        }
    }

    // Detects when the projectile leaves a trigger.
    void OnTriggerExit2D(Collider2D collision)
    {
        IsGround = false;
        KillSwitch = false;
        trailRenderer.enabled = true;
    }
}

