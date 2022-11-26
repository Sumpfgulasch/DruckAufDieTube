using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

public class Stopwatch : SerializedMonoBehaviour
{
    float timeSinceStart = 0f;
    public float Roundtime = 20f;

    float time = 0f;
    [ShowInInspector] public float Time {get => time;}

    [ShowInInspector] public bool IsRunning { get => TimeCounting != null; }

    public bool startOnPlay = true;



    public event Action OnStopwatchStart;
    public event Action OnStopwatchStop;
    public event Action OnStopwatchReset;
    public event Action OnRoundFinished;
    public event Action<float> OnTimeChange;

    Coroutine TimeCounting = null;
    Coroutine TimeSinceStartupCounting = null;

    private void Start()
    {
        if (startOnPlay)
        {
            StartStopwatch();
        }
    }

    IEnumerator CountTime()
    {
        while (true)
        {
            time += UnityEngine.Time.deltaTime;
            OnTimeChange?.Invoke(time);
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator CountTimeSinceStartup()
    {
        while (true)
        {
            timeSinceStart += UnityEngine.Time.deltaTime;
            if (timeSinceStart >= Roundtime)
            {
                StopCoroutine(TimeSinceStartupCounting);
                OnRoundFinished?.Invoke();
            }
            yield return new WaitForEndOfFrame();
        }
    }

    [Button]
    public void StartStopwatch()
    {
        if (TimeCounting != null)
        {
            Debug.LogWarning("Time is already running");
            return;
        }

        TimeCounting = StartCoroutine(CountTime());
        TimeSinceStartupCounting = StartCoroutine(CountTimeSinceStartup());

        OnStopwatchStart?.Invoke();
    }

    [Button]
    public void StopStopwatch()
    {
        if (TimeCounting == null)
        {
            Debug.LogWarning("Time has already been stopped");
            return;
        }

        StopCoroutine(TimeCounting);
        TimeCounting = null;

        OnStopwatchStop?.Invoke();
    }

    [Button]
    public void ResetStopwatch()
    {
        time = 0f;
        timeSinceStart = 0f;


        StopCoroutine(TimeCounting);
        StopCoroutine(TimeSinceStartupCounting);

        TimeCounting = null;
        TimeSinceStartupCounting = null;

        OnStopwatchReset?.Invoke();
    }
}
