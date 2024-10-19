using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockUI : MonoBehaviour
{
    private const float ROTATION_PER_DAY = 360f;
    [SerializeField] private GameObject clockHand;

    private float realSecondsPerDay = 5;
    public float dayFraction;
    public float dayTime;
    public bool isDay = true;

    // Update is called once per frame
    void Update()
    {
        dayFraction += Time.deltaTime / realSecondsPerDay;

        float dayNormalized = dayFraction % 1f;
        dayTime = Mathf.Floor(dayFraction * 24f);

        clockHand.transform.eulerAngles = new Vector3(0, 0, (-dayNormalized * ROTATION_PER_DAY) +90);

        if (isDay && dayTime > 12)
        {
            // Switch to night time
            Debug.Log("Night time!");
            isDay = false;
            DayNightController.Instance.TransitionToNight();
        }
        else if (!isDay && dayTime > 24)
        {
            // switch to day time
            Debug.Log("Day time!");
            isDay = true;
            dayFraction -= 1;
            DayNightController.Instance.TransitionToDay();
        }
        
    }
}
