using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Doppler_WaveRing : MonoBehaviour
{
    
    public int segments = 100;
    public float radius = 0.2f;
    public float expansionSpeed = 2f;
    public float maxRadius = 5f;

    private LineRenderer lr;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();

        lr.loop = true;
        lr.positionCount = segments;
        lr.useWorldSpace = false;
    }

    void Update()
    {
        radius += expansionSpeed * Time.deltaTime;

        DrawCircle();

        if(radius >= maxRadius)
            Destroy(gameObject);
    }

    void DrawCircle()
    {
        float angleStep = 360f / segments;

        for(int i = 0; i < segments; i++)
        {
            float angle = Mathf.Deg2Rad * angleStep * i;

            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            lr.SetPosition(i, new Vector3(x, y, 0));
        }
    }
}
