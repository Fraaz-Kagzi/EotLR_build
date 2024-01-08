using System;
using System.Collections.Generic;
using UnityEngine;

public class SunCycleManager : MonoBehaviour
{
    [Header("Time Settings")]
    [SerializeField]
    private float rateOfTime; 

    [SerializeField]
    private float initialHour; 

    [Header("Sun Settings")]
    [SerializeField]
    private Light solarLight; 

    [SerializeField]
    private float dawnHour; 

    [SerializeField]
    private float duskHour; 

    private DateTime presentTime; 

    private TimeSpan dawnMoment; 

    private TimeSpan duskMoment; 

    // Initialization of variables
    void Start()
    {
        presentTime = DateTime.Now.Date + TimeSpan.FromHours(initialHour);
        dawnMoment = TimeSpan.FromHours(dawnHour);
        duskMoment = TimeSpan.FromHours(duskHour);
    }

    // Main loop logic
    void Update()
    {
        AdvanceGameTime();
        AdjustSolarAngle();
    }

    // Progress game time based on multiplier
    private void AdvanceGameTime() 
    {
        presentTime = presentTime.AddSeconds(Time.deltaTime * rateOfTime);
    }

    // Adjust angle of solar light
    private void AdjustSolarAngle() 
    {
        float solarAngle;

        if (presentTime.TimeOfDay > dawnMoment && presentTime.TimeOfDay < duskMoment)
        {
            TimeSpan daylightDuration = TimeDifference(dawnMoment, duskMoment);
            TimeSpan timePostDawn = TimeDifference(dawnMoment, presentTime.TimeOfDay);

            double daytimeProgress = timePostDawn.TotalMinutes / daylightDuration.TotalMinutes;

            solarAngle = Mathf.Lerp(0, 180, (float)daytimeProgress);
        }
        else
        {
            TimeSpan nighttimeDuration = TimeDifference(duskMoment, dawnMoment);
            TimeSpan timePostDusk = TimeDifference(duskMoment, presentTime.TimeOfDay);

            double nighttimeProgress = timePostDusk.TotalMinutes / nighttimeDuration.TotalMinutes;

            solarAngle = Mathf.Lerp(180, 360, (float)nighttimeProgress);
        }

        solarLight.transform.rotation = Quaternion.AngleAxis(solarAngle, Vector3.right);
    }

    // Calculate time span difference
    private TimeSpan TimeDifference(TimeSpan from, TimeSpan to) 
    {
        TimeSpan interval = to - from;

        if (interval.TotalSeconds < 0)
        {
            interval += TimeSpan.FromHours(24);
        }

        return interval;
    }
}

