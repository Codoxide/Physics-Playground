using System;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public GameObject X_ArrowHead;
    public GameObject Y_ArrowHead;
    public GameObject Result_ArrowHead;


    public GameObject X_Arrow;
    public GameObject Y_Arrow;
    public GameObject R_Arrow;


    public Transform ResetPosTransform;

    public float ArrowSizer;
    public float speed;
    public float StartingVelocityX;
    public float GravityAcceleration = 9.81f;

    bool IsGround = false;
    bool KillSwitch = false;
    public bool isPaused = false;

    float timerStart;
    float recordPos;


    float displacement_X;
    float initialVelocity_X;
    float finalVelocity_X;

    float displacement_Y;
    float initialVelocity_Y;
    float finalVelocity_Y;

    Vector2 ProjectileMoveVector;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetArrowHeadColor();
        ResetPos();
        initialVelocity_X = StartingVelocityX;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPaused)
        {                
            if(!KillSwitch)
            {
                if(IsGround)
                {
                    finalVelocity_Y = 0;
                    timerStart = 0;
                }
                else
                {
                    timerStart += Time.deltaTime;
                    finalVelocity_Y = timerStart * GravityAcceleration;
                }
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
                Vector2 velocity = new Vector2(initialVelocity_X, finalVelocity_Y);

                float length = velocity.magnitude * ArrowSizer;

                Vector3 start = transform.position;
                Vector3 end = start + (Vector3)velocity.normalized * length;

                line.SetPosition(0, start);
                line.SetPosition(1, end);

                Result_ArrowHead.transform.position = end;

                float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
                Result_ArrowHead.transform.rotation = Quaternion.Euler(0, 0, angle-90);                


                ProjectileMoveVector = new Vector2(initialVelocity_X, finalVelocity_Y);
                transform.position += (Vector3)ProjectileMoveVector * speed * Time.deltaTime;            
            }
            else
            {
            }
        }

        
    }

    void ResetPos()
    {
        transform.position = ResetPosTransform.position;
    }

    float FindHeight()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity);

        if (hit.collider != null&&hit.collider.CompareTag("Ground"))
        {
            if((float)Math.Round(hit.distance, 2)-0.5f < 0)
            {
                return 0;
            }
            else
            {
                return (float)Math.Round(hit.distance, 2)-0.5f;
            }
        }
        else
        {
            return 0;
        }           
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
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        IsGround = true;
        if(collision.gameObject.CompareTag("Ground"))
        {
            KillSwitch = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        IsGround = false;
        recordPos = transform.position.x;
        
    }
    
}
