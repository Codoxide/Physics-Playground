using System;
using System.Collections;
using UnityEditor.Build;
using UnityEditor.Media;
using UnityEngine;

public class Rollercoaster_Movement : MonoBehaviour
{
    public bool PauseState = true;
    public float speed = 0f;
    public float GravityForce = 9.8f;
    float maxDistance;
    public GameObject[] points;
    public float launchSpeedAccelearion = 2f;
    float launchSpeed=0f;

    void Start()
    {
        maxDistance = CheckDistanceThis();        

        StartCoroutine(MoveThroughPoints());
    }
    void Update()
    {
        if(!PauseState)
        {
            AddSpeed();
        }               
    }
    void AddSpeed()
    {
        if(launchSpeed <= .1)
        {
            launchSpeed += Time.deltaTime;
            speed += launchSpeedAccelearion * Time.deltaTime;
        }
        else
        {
            speed = returnSpeed();
        }           
    }

    public float TotalEnergy(float mass)
    {
        return mass*CheckDistanceBefore()*GravityForce;
    }

    public float KineticEnergy(float mass)
    {
        return (0.5f)*(speed*speed)*mass;
    }
    public float PotentialEnergy(float mass)
    {
        return mass*CheckDistanceThis()*GravityForce;
    }    


    public float returnSpeed()
    {

        float KineticEnergy = (CheckDistanceBefore()*GravityForce)-(CheckDistanceThis()*GravityForce);
        float velocitySquared =2*KineticEnergy;
        return Mathf.Sqrt(velocitySquared);
    }


    public float CheckDistanceThis()
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
                if(PauseState == false)
                {
                    transform.position = Vector3.MoveTowards(
                        transform.position,
                        point.transform.position,
                        speed * Time.deltaTime
                    );                    
                }


                yield return null;
            }
        }
    }

    public void ResetScript()
    {
        speed = 0;
        launchSpeed = 0;
        PauseState = true;
        StopAllCoroutines();
        StartCoroutine(MoveThroughPoints());


    }
}
