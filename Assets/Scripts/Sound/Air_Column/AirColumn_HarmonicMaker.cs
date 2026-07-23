using System;
using System.Linq;
using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.InputSystem;

public class AirColumn_HarmonicMaker : MonoBehaviour
{
    public LineRenderer lineRend1;
    public LineRenderer lineRend2;
    public Transform spriteMask;
    public int NumOfPoints = 25;
    float length = 14.2f;
    public float Amplitude = 5;
    public GameObject circle;
    public float K_Value = 3;
    float PeriodAngle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void UpdateHarmonicWave()
    {
        length = 14.2f+spriteMask.transform.position.x; // Update the length of the wave to match the air columm's length
        PeriodAngle = 360/K_Value; // Convert the k value, horizontal stretch/compression, to a angle
        DrawHalfOne(); // Draw the first sine wave
        DrawHalfTwo(); // Draw the second flipped sine wave
    }
    void DrawHalfOne()
    {
        lineRend1.positionCount = NumOfPoints+1; // the position count of the line renderer equals the number of points why we want plus 1
        for (int i = 0; i <= NumOfPoints; i++) // For loop
        {
            float percent = (float)i / NumOfPoints; // Some Math

            float x = transform.position.x + (percent*length); //
            float y = Mathf.Sin(percent*PeriodAngle*Mathf.Deg2Rad)*Amplitude;//
            
            lineRend1.SetPosition(i, new Vector3(x,y,0f)); // Plug our points into the line render so it connects all those points, the more points, the smoother it will look
        }
    }
    void DrawHalfTwo()
    {
        // Exactly the same as the previous func but the amplitude is reversed
        lineRend2.positionCount = NumOfPoints+1;
        for (int i = 0; i <= NumOfPoints; i++)
        {
            float percent = (float)i / NumOfPoints;

            float x = transform.position.x + (percent*length);
            float y = Mathf.Sin(percent*PeriodAngle*Mathf.Deg2Rad)*-Amplitude; // This makes the sine flipped
            lineRend2.SetPosition(i, new Vector3(x,y,0f));
        }
    }    
}
