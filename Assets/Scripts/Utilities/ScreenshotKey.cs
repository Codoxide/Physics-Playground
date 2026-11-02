using UnityEngine;
using System.Collections;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ScreenshotKey : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        string dir = Path.Combine(Application.dataPath, "Screenshots");
        Directory.CreateDirectory(dir);
        string file = Path.Combine(dir, $"shot_{System.DateTime.Now:yyyyMMdd_HHmmss}.png");

        ScreenCapture.CaptureScreenshot(file, 1);
        yield return new WaitForEndOfFrame();
        yield return null;

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
        Debug.Log("Saved " + file);
    }
}