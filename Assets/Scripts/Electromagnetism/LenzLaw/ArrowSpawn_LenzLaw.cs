using System.Collections;
using UnityEngine;

public class ArrowSpawn_LenzLaw : MonoBehaviour
{
    [Header("Label Elements")]
    public GameObject ParentLabel;
    public GameObject ForceArrow;
    public TextMesh LeftTxt;
    public TextMesh RightTxt;

    [Header("Arrow Settings")]
    public GameObject arrowPrefab;
    public float removeRate = 0.2f;
    public float spacing = 1f;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public bool moveUp = true;

    [Header("Control")]
    public bool pause = false;

    private bool lastDirection;
    private float spawnRate;
    private float spawnTimer;
    private Vector3 startPosition;


    void Start()
    {
        startPosition = transform.localPosition;

        lastDirection = moveUp;

        // Time required for an arrow to travel the desired spacing distance
        spawnRate = Mathf.Abs(spacing / moveSpeed);

        // Spawn one immediately
        spawnTimer = spawnRate + 2f;
    }


    void Update()
    {
        // Pause hides all arrows and stops movement/spawning
        if (pause)
        {
            HideChildren(true);
            ClearChildren();
            ParentLabel.SetActive(false);
            spawnTimer = spawnRate + 2f;
            return;
        }
        else
        {
            ParentLabel.SetActive(true);            
            HideChildren(false);
        }


        // Detect direction change
        if (lastDirection != moveUp)
        {
            ClearChildren();

            spawnTimer = spawnRate + 2f;
            lastDirection = moveUp;
        }


        // Change field direction
        SetDirection();


        // Spawn arrows based on speed and spacing
        if (spawnTimer >= spawnRate)
        {
            SpawnArrow();
            spawnTimer = 0;
        }
        else
        {
            spawnTimer += Time.deltaTime;
        }
    }


    void SetDirection()
    {
        if (!moveUp)
        {
            transform.localPosition = new Vector3(
                startPosition.x,
                Mathf.Abs(startPosition.y),
                startPosition.z
            );
            
            moveSpeed = -Mathf.Abs(moveSpeed);
            spacing = -Mathf.Abs(spacing);
            ForceArrow.transform.rotation = Quaternion.Euler(0,0,90);
            LeftTxt.text = "S";
            RightTxt.text = "N";
        }
        else
        {
            transform.localPosition = new Vector3(
                startPosition.x,
                -Mathf.Abs(startPosition.y),
                startPosition.z
            );

            moveSpeed = Mathf.Abs(moveSpeed);
            spacing = Mathf.Abs(spacing);
            ForceArrow.transform.rotation = Quaternion.Euler(0,0,-90);
            LeftTxt.text = "N";
            RightTxt.text = "S";       
        }
    }


    void SpawnArrow()
    {
        GameObject arrow = Instantiate(
            arrowPrefab,
            Vector2.zero,
            transform.rotation,
            transform
        );

        // Starting position relative to the parent
        arrow.transform.localPosition = new Vector3(0, 0, -3);


        // Flip arrow depending on field direction
        arrow.transform.localScale = moveUp
            ? new Vector3(1, 1, 1)
            : new Vector3(1, -1, 1);


        StartCoroutine(MoveArrow(arrow));

        Destroy(arrow, removeRate);
    }


    IEnumerator MoveArrow(GameObject arrow)
    {
        while (arrow != null)
        {
            arrow.transform.localPosition +=
                Vector3.up * moveSpeed * Time.deltaTime;

            yield return null;
        }
    }


    void ClearChildren()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }


    void HideChildren(bool hide)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(!hide);
        }
    }
}