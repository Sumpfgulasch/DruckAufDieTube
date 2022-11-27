using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Stopwatch), typeof(UIDocument))]
public class TimerUI : MonoBehaviour
{
    private UIDocument UI_Doc;
    private Stopwatch stopwatch;

    private Label timerLabel;

    public void Start()
    {
        Debug.Log("Start");
        UI_Doc = this.transform.GetComponent<UIDocument>();
        stopwatch = this.transform.GetComponent<Stopwatch>();

        if (UI_Doc == null || stopwatch == null)
        {
            Debug.LogWarning("Couldnt find UI Document or Stopwatch - Fatal Error");
        }

        var rootElement = UI_Doc.rootVisualElement;


        timerLabel = rootElement.Q<Label>("TemporaryTimer");
        timerLabel.text = stopwatch.Time.ToString();

        stopwatch.OnTimeChange += OnStopwatchTimeChanged;

        StartCoroutine(FadeText());
    }

    private void OnDestroy()
    {
        stopwatch.OnTimeChange -= OnStopwatchTimeChanged;
    }

    private void OnStopwatchTimeChanged(float time)
    {
        timerLabel.text = Mathf.Floor(20f - time).ToString();
    }

    IEnumerator FadeText()
    {
        yield return new WaitForSeconds(3f);
        float time = 0f;

        while (time < 1f)
        {
            time += Time.deltaTime;
            timerLabel.style.color = new Color(timerLabel.style.color.value.r, timerLabel.style.color.value.g, timerLabel.style.color.value.b, 1f - time);
            yield return new WaitForEndOfFrame();
        }
    }
}
