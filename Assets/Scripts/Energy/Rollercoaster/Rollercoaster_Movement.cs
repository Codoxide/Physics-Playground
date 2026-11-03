using System;
using System.Collections;
using UnityEngine;

public class Rollercoaster_Movement : MonoBehaviour
{
    public bool PauseState = true; // Starts off paused
    public float speed = 0f; // Initial speed
    public float GravityForce = 9.81f; // Force of gravity, 9.81 m/s^2
    float initialHeight;
    public GameObject[] points; // The nodes where the ball moves towards
    public float launchSpeedAcceleration; // The speed of push
    float launchSpeed=0f;

    void Start()
    {
        initialHeight = CheckDistanceThis();  

        StartCoroutine(MoveThroughPoints());
    }
    void Update()
    {
        if(!PauseState)
        {
            AddSpeed(); // Launch ball in the start 
        }               
    }
    void AddSpeed()
    {
        // Gives the ball a small initial push, then lets physics control its movement.
        if(launchSpeed <= .1) // the duration of the pushh
        {
            launchSpeed += Time.deltaTime;
            speed += launchSpeedAcceleration * Time.deltaTime; // increases speed
        }
        else
        {
            speed = returnSpeed();
        }           
    }

    public float TotalEnergy(float mass)
    {
        return mass*CheckDistanceBefore()*GravityForce; // Calcualate total energy from the begining, the formula is Et = Ek+Eg, but since in the begining the kinetic energy is 0, so we can just use Eg which is mgh
    }

    public float KineticEnergy(float mass)
    {
        return (0.5f)*(speed*speed)*mass; // From the kinetic energy formula, Ek = (1/2)(m)(v^2)
    }
    public float PotentialEnergy(float mass)
    {
        return mass*CheckDistanceThis()*GravityForce; // From the potential energy formula, Eg = mgh
    }    


    public float returnSpeed()
    {
        float KineticEnergy = (CheckDistanceBefore()*GravityForce)-(CheckDistanceThis()*GravityForce); // Check the kinetic energy
        float velocitySolved = Mathf.Sqrt(2*KineticEnergy); // solved for v, which is velocity, from Ek = (1/2)(m)(v^2)
        return velocitySolved;
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
        return initialHeight;
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
