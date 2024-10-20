using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightController : MonoBehaviour
{
    public static DayNightController Instance { get; private set; }
    public bool IsDay { get; private set; }

    [SerializeField] GameObject nightPanel;
    [SerializeField] PoliceSpawner policeSpawner;

    private void Awake()
    {
        IsDay = true;

        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        SoundManager.Instance.PlayDayMusic();
    }

    public void TransitionToNight()
    {
        IsDay = false;

        // Transition sound
        SoundManager.Instance.PlayTransitionSound();

        // Red panel overlay
        nightPanel.SetActive(true);

        // Change player image to werewolf
        PlayerController.Instance.BecomeWolf();

        // Start night music
        SoundManager.Instance.PlayNightMusic();

        // People start fleeing

        // Police arrive
        policeSpawner.StartSpawning();

    }

    public void TransitionToDay()
    {
        IsDay = true;

        // Transition sound
        SoundManager.Instance.PlayTransitionSound();

        // Remove red panel overlay
        nightPanel.SetActive(false);

        // Change player image to human
        PlayerController.Instance.BecomeHuman();

        // Start day music
        SoundManager.Instance.PlayDayMusic();

        // People return to shopping

        // Police leave
        policeSpawner.StopSpawning();
    }
}
