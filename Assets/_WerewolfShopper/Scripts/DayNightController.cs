using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightController : MonoBehaviour
{
    public static DayNightController Instance { get; private set; }

    [SerializeField] GameObject nightPanel;
    [SerializeField]

    private void Awake()
    {
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

    public void TransitionToNight()
    {
        // Stop day music
        SoundManager.Instance.PlayDayMusic(false);

        // Transition sound
        SoundManager.Instance.PlayTransition();

        // Red panel overlay
        nightPanel.SetActive(true);

        // Change player image to werewolf

        // Start night music
        SoundManager.Instance.PlayNightMusic(true);

        // People start fleeing

        // Police arrive

    }

    public void TransitionToDay()
    {
        // Stop night music
        SoundManager.Instance.PlayNightMusic(false);

        // Transition sound
        SoundManager.Instance.PlayTransition();

        // Remove red panel overlay
        nightPanel.SetActive(false);

        // Change player image to human

        // Start day music
        SoundManager.Instance.PlayDayMusic(true);

        // People return to shopping

        // Police leave

    }
}
