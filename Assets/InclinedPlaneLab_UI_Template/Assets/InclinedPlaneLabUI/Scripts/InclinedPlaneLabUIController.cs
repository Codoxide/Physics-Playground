using UnityEngine;
using UnityEngine.UIElements;

public class InclinedPlaneLabUIController : MonoBehaviour
{
    [SerializeField] private UIDocument document;

    private Button runButton;
    private Button pauseButton;
    private Button resetButton;
    private Label status;
    private Label pausePill;

    private void OnEnable()
    {
        if (document == null)
            document = GetComponent<UIDocument>();

        var root = document.rootVisualElement;
        runButton = root.Q<Button>("runButton");
        pauseButton = root.Q<Button>("pauseButton");
        resetButton = root.Q<Button>("resetButton");
        status = root.Q<Label>(className: "status");
        pausePill = root.Q<Label>(className: "pause-pill");

        runButton.clicked += SetRunning;
        pauseButton.clicked += SetPaused;
        resetButton.clicked += ResetState;

        SetRunning();
    }

    private void OnDisable()
    {
        if (runButton != null) runButton.clicked -= SetRunning;
        if (pauseButton != null) pauseButton.clicked -= SetPaused;
        if (resetButton != null) resetButton.clicked -= ResetState;
    }

    private void SetRunning()
    {
        status.text = "● RUNNING";
        status.RemoveFromClassList("status-paused");
        status.AddToClassList("status-running");
        pausePill.style.display = DisplayStyle.None;
    }

    private void SetPaused()
    {
        status.text = "Ⅱ PAUSED";
        status.RemoveFromClassList("status-running");
        status.AddToClassList("status-paused");
        pausePill.style.display = DisplayStyle.Flex;
    }

    private void ResetState()
    {
        SetRunning();
    }
}
