using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Text MainHeaderText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        MainHeaderText.text = "<color=#F2F4F7>Physics</color><color=#FFB454> Playground</color>";
    }

    public void AirColumn_Button()
    {
        SceneManager.LoadScene(1);
    }
    public void DopplerEffect_Button()
    {
        SceneManager.LoadScene(2);
    }
    public void Rollercoaster_Button()
    {
        SceneManager.LoadScene(3);
    }    
    public void ProjectileMotion_Button()
    {
        SceneManager.LoadScene(4);
    }        
    public void TablePulley_Button()
    {
        SceneManager.LoadScene(5);
    }  
    public void AtwoodPulley_Button()
    {
        SceneManager.LoadScene(6);
    }              
    public void InclinedPlane_Button()
    {
        SceneManager.LoadScene(7);
    }        
    public void LenzsLaw_Button()
    {
        SceneManager.LoadScene(8);
    }        
}
