using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileMotion_GameUI : MonoBehaviour
{
    public GameObject platform;
    public Transform resetPos;

    public ProjectileScript projectileScript;
    public Slider heightSlider;
    public Slider TimeScaleSlider;
    public Slider LaunchSlider;


    public Text Start_PauseButtonTxt;
    public Text TimescaleText;
    public Text HeightText;
    public Text LaunchText;



    public Text timerTxt;
    public Text xVelocity;
    public Text yVelocity;
    public Text Resultant_Velocity;
    public Text xDisplacement;

    Vector3 originalScale;
    Vector3 originalPosition;
    float originalWorldHeight;

    public float HEightDivider;
    void Start()
    {
        originalScale = platform.transform.localScale;
        originalPosition = platform.transform.position;
        originalWorldHeight = platform.GetComponent<Renderer>().bounds.size.y;

        PlatformHeightSliderChange();
        LaunchSpeedSliderChange();
        TimescaleSliderChange();
    }

    // Update is called once per frame
    void Update()
    {
        timerTxt.text = "Time: "+(projectileScript.timerStart).ToString("F2")+"s";
        xVelocity.text = (projectileScript.initialVelocity_X).ToString("F1")+" m/s [→]";
        yVelocity.text = (-1*projectileScript.finalVelocity_Y).ToString("F1")+" m/s [↓]";
        Resultant_Velocity.text = FullAngleName(-projectileScript.angle);
        xDisplacement.text = (projectileScript.timerStart * projectileScript.initialVelocity_X).ToString("F1")+" m"; 
    }

    public void PlatformHeightSliderChange()
    {
        float targetHeight = Mathf.Clamp(heightSlider.value, 2f, heightSlider.maxValue);

        HeightText.text = "Height: " + heightSlider.value.ToString("F1") + " m";

        // Scale relative to the platform's actual original height
        float scaleMultiplier = targetHeight / originalWorldHeight;

        platform.transform.localScale = new Vector3(
            originalScale.x,
            originalScale.y * scaleMultiplier,
            originalScale.z
        );

        // Keep the bottom of the platform fixed
        float bottomY = originalPosition.y - originalWorldHeight / 2f;

        float newWorldHeight = platform.GetComponent<Renderer>().bounds.size.y;

        platform.transform.position = new Vector3(
            originalPosition.x,
            bottomY + newWorldHeight / 2f,
            originalPosition.z
        );

        // Put ResetPos at the actual top
        float topY = platform.GetComponent<Renderer>().bounds.max.y;

        resetPos.transform.position = new Vector3(
            resetPos.transform.position.x,
            topY + 0.25f,
            resetPos.transform.position.z
        );

        projectileScript.ResetPos();
        Start_PauseButtonTxt.text =
            projectileScript.isPaused ? "Start" : "Pause";
    }   
    public void StartSim()
    {
        projectileScript.isPaused = !projectileScript.isPaused; // Reverse bool
        Start_PauseButtonTxt.text = projectileScript.isPaused ? "Start": "Pause";


    }
    public void ResetSim()
    {

        projectileScript.ResetPos();
        Start_PauseButtonTxt.text = projectileScript.isPaused ? "Start": "Pause";
    } 


    public string FullAngleName(float angle_)
    {
        if (angle_ == 0f)
        {
            return projectileScript.velocity.magnitude.ToString("F1") + " m/s [E]";
        }
        else if (angle_ <= 45f && angle_ > 0f)
        {
            return projectileScript.velocity.magnitude.ToString("F1") + " m/s [" + (-projectileScript.angle).ToString("F1") + "° S of E]";
        }
        else
        {
            return projectileScript.velocity.magnitude.ToString("F1") +" m/s [" + (-projectileScript.angle).ToString("F1") + "° E of S]";
        }
    }

    public void TimescaleSliderChange()
    {
        float timeScale = TimeScaleSlider.value*.1f;
        TimescaleText.text = "Time Lapse: "+timeScale+"x";
        Time.timeScale = timeScale;
    }   
    
    public void LaunchSpeedSliderChange()
    {
        LaunchText.text = "Launch Speed: "+LaunchSlider.value+" m/s";

        projectileScript.initialVelocity_X = LaunchSlider.value;
        projectileScript.ResetPos();
        Start_PauseButtonTxt.text =
            projectileScript.isPaused ? "Start" : "Pause";        

    }



}
