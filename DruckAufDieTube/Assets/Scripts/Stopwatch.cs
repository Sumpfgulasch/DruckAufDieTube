using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

public class Stopwatch : SerializedMonoBehaviour
{
    float time = 0f;
    [ShowInInspector] public float Time {get => time;}

    bool isRunning = false;
    [ShowInInspector] public bool IsRunning { get => isRunning; }


    public event Action OnStopwatchStart;
    public event Action OnStopwatchStop;
    public event Action OnStopwatchReset;
    public event Action<float> OnTimeChange;
    private void Update()
    {
        if(isRunning)
        {
            time += UnityEngine.Time.deltaTime;
            OnTimeChange?.Invoke(time);
        }
    }

    [Button]
    public void StartStopwatch()
    {
        if (isRunning == true)
        {
            Debug.LogWarning("Timer is already running");
        }
        isRunning = true;
        OnStopwatchStart?.Invoke();
    }

    [Button]
    public void StopStopwatch()
    {
        isRunning = false;
        OnStopwatchStop?.Invoke();
    }

    [Button]
    public void ResetStopwatch()
    {
        time = 0f;
        OnStopwatchReset?.Invoke();
    }
}
