using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LenzLaw_GameUI : MonoBehaviour
{
    public GameObject ExplainPanel;
    public Transform Scroll_Content;
    public ArrowSpawn_LenzLaw arrowSpawn_LenzLawScript;
    public SpriteRenderer MagnetSprite;
    public Sprite FlippedMagnet;
    public Sprite UnflippedMagnet;
    
    
    public void FlipMagnetChange()
    {
        arrowSpawn_LenzLawScript.IsMagnetFlipped=!arrowSpawn_LenzLawScript.IsMagnetFlipped;
        MagnetSprite.sprite = arrowSpawn_LenzLawScript.IsMagnetFlipped ? FlippedMagnet : UnflippedMagnet;
    }
    
    public void OpenExplainPanel()
    {
        ExplainPanel.SetActive(true);
    }

    public void CloseExplainPanel()
    {
        ExplainPanel.SetActive(false);
        Scroll_Content.transform.position = new Vector3(Scroll_Content.transform.position.x,0,Scroll_Content.transform.position.z);
    }  
    public void ExitButton()
    {        
        SceneManager.LoadScene(0);
    }
}
