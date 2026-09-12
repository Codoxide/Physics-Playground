using UnityEngine;

public class lineMaker : MonoBehaviour
{
    public Transform parentObj;
    public int _segments;
    public float spacing;
    float spacerVal;
    public GameObject BaseObj;
    public float width;
    public float height;
    public bool ToggleBool = true;
    bool PrevBool = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spacerVal = width/2;
        
    }

    void MakeTiles()
    {
        
        for (int i = 0; i < _segments; i++)
        {
            GameObject onj = Instantiate(BaseObj);
            onj.transform.parent = parentObj.transform;
            onj.transform.localScale = new Vector3(width, height, 1);
            onj.transform.position = new Vector3(transform.position.x+spacerVal, transform.position.y, transform.position.z);
            spacerVal += spacing;            
        }

    }
    public void DeleteAllChildren(Transform parentTransform)
    {
        // Loop backwards to safely remove elements without breaking the index
        for (int i = parentTransform.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(parentTransform.GetChild(i).gameObject);
        }
    }    
    // Update is called once per frame
    void Update()
    {
        if(ToggleBool != PrevBool)
        {
            spacerVal = width/2;
            DeleteAllChildren(parentObj.transform);
            MakeTiles();
            ToggleBool = PrevBool;
        }
    }
}
