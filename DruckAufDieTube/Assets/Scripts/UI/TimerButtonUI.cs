using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Stopwatch))]
public class TimerButtonUI : MonoBehaviour
{
    [SerializeField] private UIDocument UI_Doc;

    private Button timeLogButton;
    public bool isLoggingButton = false;

    private Stopwatch stopwatch;

    public void Start()
    {
        UI_Doc = this.transform.GetComponent<UIDocument>();
        if (UI_Doc == null)
        {
            Debug.LogWarning("Couldnt find UI Document - Fatal Error");
        }

        stopwatch = this.transform.GetComponent<Stopwatch>();

        var rootElement = UI_Doc.rootVisualElement;


        timeLogButton = rootElement.Q<Button>("TimeLogButton");
        timeLogButton.clickable.clicked += OnButtonClicked;
    }

    public void SetAsLoggingButton()
    { 
        //search all buttons in doc, and set all to none, while this is true.
    }

    private void OnDestroy()
    {
        timeLogButton.clickable.clicked -= OnButtonClicked;
    }

    private void OnButtonClicked()
    {
        if (isLoggingButton)
        {
            stopwatch.StopStopwatch();
            Debug.Log("Button Was Clicked and is Logging Button");
        }
        else
        {
            Debug.Log("Button Was Clicked and is NOT A Logging Button");
        }
    }
}
