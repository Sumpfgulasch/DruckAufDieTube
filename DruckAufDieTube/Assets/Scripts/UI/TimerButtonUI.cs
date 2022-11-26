using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Stopwatch), typeof(UIDocument))]
public class TimerButtonUI : MonoBehaviour
{
    public bool isLoggingButton = false;

    private UIDocument UI_Doc;
    private Button timeLogButton;

    private Stopwatch stopwatch;

    public void Start()
    {
        UI_Doc = this.transform.GetComponent<UIDocument>();
        stopwatch = this.transform.GetComponent<Stopwatch>();

        if (UI_Doc == null || stopwatch == null)
        {
            Debug.LogWarning("Couldnt find UI Document or Stopwatch - Fatal Error");
        }

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
