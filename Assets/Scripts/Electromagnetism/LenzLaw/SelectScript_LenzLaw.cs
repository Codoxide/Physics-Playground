using System.Collections;
using UnityEngine;

public class SelectScript_LenzLaw : MonoBehaviour
{
    public ArrowSpawn_LenzLaw arrowSpawn_LenzLawScript;
    public Transform magnetObj;

    public GameObject Outline_Mag;
    public float DelayTimeArrow = 2f;

    Vector3 unchangedPos;
    private float lastX;
    float timer;
    float distance;
    // 0.27784, 0.19552
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastX = magnetObj.position.x;
        unchangedPos = magnetObj.position;
    }

    // Update is called once per frame
    void Update()
    {
        float dx = magnetObj.position.x - lastX;
        lastX = magnetObj.position.x;

        distance += dx;
        timer += Time.deltaTime;

        if (timer >= 0.05f)
        {
            float speed = distance / timer;
            arrowSpawn_LenzLawScript.SetSpeed(speed);
            distance = 0f;
            timer = 0f;
        }

    
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);   
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Magnet"))
            {
                if (Input.GetMouseButton(0))
                {
                    float currX = Mathf.Clamp(mousePos.x, 1.5f, 6f);
                    magnetObj.transform.position = new Vector2(currX,magnetObj.transform.position.y);
                    Outline_Mag.SetActive(false);                
                }
                else
                {
                    Outline_Mag.SetActive(true);                    
                }            
            }
        }
        else
        {
            Outline_Mag.SetActive(false);   
        }       

        if (unchangedPos.x != magnetObj.position.x)
        {
            StopAllCoroutines();

            arrowSpawn_LenzLawScript.pause = false;
            arrowSpawn_LenzLawScript.moveUp = unchangedPos.x < magnetObj.position.x;
            
            unchangedPos = magnetObj.position;
            StartCoroutine(PauseArrowSpawn());
        }
    
    }
    IEnumerator PauseArrowSpawn()
    {
        yield return new WaitForSeconds(DelayTimeArrow);
        arrowSpawn_LenzLawScript.pause = true;
    }
}
