using System;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileMotion_GameUI : MonoBehaviour
{
    public GameObject platform;
    public Transform resetPos;

    public ProjectileScript projectileScript;
    public Slider heightSlider;
    public Text timerTxt;
    public Text xVelocity;
    public Text yVelocity;
    public Text Resultant_Velocity;
    public Text xDisplacement;

    Vector3 originalScale;
    Vector3 originalPosition;

    public float HEightDivider;
    void Start()
    {
        originalScale = platform.transform.localScale;
        originalPosition = platform.transform.position;
        ChangePlatformHeight();
    }

    // Update is called once per frame
    void Update()
    {
        timerTxt.text = "Time: "+(projectileScript.timerStart).ToString("F2")+"s";
        xVelocity.text = (projectileScript.initialVelocity_X).ToString("F1")+" m/s [→]";
        yVelocity.text = (-1*projectileScript.finalVelocity_Y).ToString("F1")+" m/s [↓]";
        Resultant_Velocity.text = projectileScript.velocity.magnitude.ToString("F1") +" m/s  [30.2° N of E]";
    }

    public void ChangePlatformHeight()
    {
        float newHeight = Mathf.Clamp(heightSlider.value*HEightDivider, 2, heightSlider.maxValue);
        Debug.Log(newHeight);
        // Change height
        platform.transform.localScale = new Vector3(
            originalScale.x,
            newHeight,
            originalScale.z
        );

        // Move it upward by half the added height
        platform.transform.position = new Vector3(
            originalPosition.x,
            originalPosition.y + (newHeight - originalScale.y) / 2f,
            originalPosition.z
        );
        resetPos.transform.localPosition = new Vector3(
            resetPos.transform.localPosition.x,
            newHeight / 2f,
            resetPos.transform.localPosition.z
        );        
        projectileScript.ResetPos();
    }    
}
