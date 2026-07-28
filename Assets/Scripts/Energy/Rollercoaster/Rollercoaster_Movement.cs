using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Rollercoaster_Movement : MonoBehaviour
{

    bool moving = false;
    public float speed = 0f;
    public float GravityForce = 9.8f;
    public float Coefficient_Friction = 0.63f;
    int i =0;
    float maxDistance;
    public GameObject[] points;
    Vector2 StartingPoint;
    Vector2 CurrentPoint;
    public float launchAcceleration = 2;
    float launchTimer;

    void Start()
    {
        StartingPoint = transform.position;
        maxDistance = CheckDistanceThis();        

        StartCoroutine(MoveThroughPoints());
    }
    void Update()
    {
        if (launchTimer < 1f)
        {
            speed += launchAcceleration * Time.deltaTime;
            launchTimer += Time.deltaTime;
        }
        else
        {
            speed = returnSpeed();            
        }
        Debug.Log(CheckDistanceThis());
        

    }

    float returnSpeed()
    {
        if(i <= points.Length-1)
        {
            CurrentPoint = transform.position;
            float distance = Vector2.Distance(StartingPoint, CurrentPoint);
            Vector2 direction = (Vector2)points[i].transform.position - CurrentPoint;
            float angleDegrees = Mathf.Atan2(direction.y, direction.x);
            float velocitySquared = ((2*GravityForce)*(CheckDistanceBefore()-CheckDistanceThis()))-(2*Coefficient_Friction*Mathf.Cos(angleDegrees)*GravityForce*distance); 
            if(velocitySquared < 0)
            {
                velocitySquared = 0;
            }
            else
            {
                velocitySquared = ((2*GravityForce)*(CheckDistanceBefore()-CheckDistanceThis()))-(2*Coefficient_Friction*Mathf.Cos(angleDegrees)*GravityForce*distance); 
                
            }
            return Mathf.Sqrt(velocitySquared);            
        }
        else
        {
            return 0;
        }
     
    }

    float CheckDistanceThis()
    {
        int layerMask = 1 << 3;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, layerMask);

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
    float CheckDistanceBefore()
    {
        return maxDistance;
    }





    IEnumerator MoveThroughPoints()
    {
        foreach(GameObject point in points)
        {
            while(Vector3.Distance(transform.position, point.transform.position) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    point.transform.position,
                    speed * Time.deltaTime
                );

                yield return null;
            }
            i++;
        }
    }


}
