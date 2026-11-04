using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProjectileMotion_GameUI : MonoBehaviour
{
    [Header("Platform & Projectile")]
    public GameObject platform;
    public Transform resetPos;
    public ProjectileScript projectileScript;

    [Header("Sliders")]
    public Slider heightSlider;
    public Slider TimeScaleSlider;
    public Slider LaunchSlider;

    [Header("Button & Slider Text")]
    public Text Start_PauseButtonTxt;
    public Text TimescaleText;
    public Text HeightText;
    public Text LaunchText;

    [Header("Explanation Panel")]
    public GameObject ExplainPanelObj;
    public Transform Scroll_Content;

    [Header("Projectile Information")]
    public Text timerTxt;
    public Text xVelocity;
    public Text yVelocity;
    public Text Resultant_Velocity;
    public Text xDisplacement;

    [Header("Platform Settings")]
    public float HEightDivider;

    Vector3 originalScale;
    Vector3 originalPosition;
    float originalWorldHeight;


    // Initializes the platform and applies the starting slider values.
    void Start()
    {
        originalScale = platform.transform.localScale;
        originalPosition = platform.transform.position;
        originalWorldHeight = platform.GetComponent<Renderer>().bounds.size.y;

        PlatformHeightSliderChange();
        LaunchSpeedSliderChange();
        TimescaleSliderChange();
    }

    // Updates the projectile information displayed on the UI.
    void Update()
    {
        timerTxt.text = "Time: " + projectileScript.timerStart.ToString("F2") + "s";
        xVelocity.text = projectileScript.Velocity_X.ToString("F1") + " m/s [→]";
        yVelocity.text = (-1 * projectileScript.Velocity_Y).ToString("F1") + " m/s [↓]";
        Resultant_Velocity.text = FullAngleName(-projectileScript.angle);
        xDisplacement.text = (projectileScript.timerStart * projectileScript.Velocity_X).ToString("F1") + " m";
    }

    // Changes the platform height while keeping its bottom fixed.
    public void PlatformHeightSliderChange()
    {
        float targetHeight = Mathf.Clamp(heightSlider.value, 2f, heightSlider.maxValue);

        HeightText.text = "Height: " + heightSlider.value.ToString("F1") + " m";

        float scaleMultiplier = targetHeight / originalWorldHeight;

        platform.transform.localScale = new Vector3(
            originalScale.x,
            originalScale.y * scaleMultiplier,
            originalScale.z
        );

        float bottomY = originalPosition.y - originalWorldHeight / 2f;
        float newWorldHeight = platform.GetComponent<Renderer>().bounds.size.y;

        platform.transform.position = new Vector3(
            originalPosition.x,
            bottomY + newWorldHeight / 2f,
            originalPosition.z
        );

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

    // Starts or pauses the projectile simulation.
    public void StartSim()
    {
        projectileScript.isPaused = !projectileScript.isPaused;
        Start_PauseButtonTxt.text =
            projectileScript.isPaused ? "Start" : "Pause";
    }

    // Resets the projectile to its starting position.
    public void ResetSim()
    {
        projectileScript.ResetPos();

        Start_PauseButtonTxt.text =
            projectileScript.isPaused ? "Start" : "Pause";
    }

    // Converts the velocity angle into a readable direction.
    public string FullAngleName(float angle_)
    {
        if (angle_ == 0f)
        {
            return projectileScript.velocity.magnitude.ToString("F1")
                + " m/s [E]";
        }
        else if (angle_ <= 45f && angle_ > 0f)
        {
            return projectileScript.velocity.magnitude.ToString("F1")
                + " m/s ["
                + (Mathf.Abs(projectileScript.angle)).ToString("F1")
                + "° S of E]";
        }
        else
        {
            return projectileScript.velocity.magnitude.ToString("F1")
                + " m/s ["
                + (90-Mathf.Abs(projectileScript.angle)).ToString("F1")
                + "° E of S]";
        }
    }

    // Changes the simulation speed using the time scale slider.
    public void TimescaleSliderChange()
    {
        float timeScale = TimeScaleSlider.value * .1f;

        TimescaleText.text = "Time Lapse: " + timeScale + "x";
        Time.timeScale = timeScale;
    }

    // Changes the projectile's launch speed.
    public void LaunchSpeedSliderChange()
    {
        LaunchText.text = "Launch Speed: " + LaunchSlider.value + " m/s";

        projectileScript.Velocity_X = LaunchSlider.value;
        projectileScript.ResetPos();

        Start_PauseButtonTxt.text =
            projectileScript.isPaused ? "Start" : "Pause";
    }

    // Opens the explanation panel.
    public void OpenExplainPanel()
    {
        ExplainPanelObj.SetActive(true);
    }

    // Closes the explanation panel and resets its scroll position.
    public void CloseExplainPanel()
    {
        ExplainPanelObj.SetActive(false);
        Scroll_Content.transform.position = new Vector3(Scroll_Content.transform.position.x,0,Scroll_Content.transform.position.z);
    }
    public void ExitButton()
    {
        SceneManager.LoadScene(0);
    }       
}

