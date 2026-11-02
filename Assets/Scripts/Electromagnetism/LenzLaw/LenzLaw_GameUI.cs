using UnityEngine;
using UnityEngine.SceneManagement;

public class LenzLaw_GameUI : MonoBehaviour
{
    public GameObject ExplainPanel;
    public Transform Scroll_Content;

    public void OpenExplainPanel()
    {
        ExplainPanel.SetActive(true);
    }

    public void CloseExplainPanel()
    {
        ExplainPanel.SetActive(false);
        Scroll_Content.transform.position = new Vector3(0,0,0);
    }  
    public void ExitButton()
    {
        SceneManager.LoadScene(0);
    }
}
