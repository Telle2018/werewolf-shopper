using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightController : MonoBehaviour
{
    public static DayNightController Instance { get; private set; }
    public bool IsDay { get; private set; }

    [SerializeField] GameObject nightPanel;
    [SerializeField] GameObject groceryList;
    [SerializeField] PoliceSpawner policeSpawner;
    [SerializeField] PersonSpawner shopperSpawner;

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
        groceryList.SetActive(true);
        shopperSpawner.StartSpawning();
    }

    public void TransitionToNight()
    {
        IsDay = false;

        // Transition sound
        SoundManager.Instance.PlayTransitionSound();

        // Red panel overlay
        nightPanel.SetActive(true);
        
        groceryList.SetActive(false);

        // Change player image to werewolf
        PlayerController.Instance.BecomeWolf();

        // Start night music
        SoundManager.Instance.PlayNightMusic();

        // Stop spawning shoppers
        shopperSpawner.StopSpawning();

        // Police arrive
        policeSpawner.StartSpawning();

    }

    public void TransitionToDay()
    {
        IsDay = true;

        // Remove red panel overlay
        nightPanel.SetActive(false);
        
        groceryList.SetActive(true);

        // Change player image to human
        PlayerController.Instance.BecomeHuman();

        // Start day music
        SoundManager.Instance.PlayDayMusic();

        // Start spawning shoppers again
        shopperSpawner.StartSpawning();


        // Police leave
        policeSpawner.StopSpawning();
    }
}
