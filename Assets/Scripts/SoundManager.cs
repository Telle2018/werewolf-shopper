using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioSource _dayMusic;
    [SerializeField] private AudioSource _nightMusic;
    [SerializeField] private AudioSource _transition;
    [SerializeField] private AudioSource _attack;
    [SerializeField] private AudioSource _takeDamage;
    [SerializeField] private AudioSource _foundItem;

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

    public void PlayDayMusic(bool play)
    {
        if (play)
        {
            //_dayMusic.Play();
            Debug.Log("Day music starts");
        }
        else
        {
            //_dayMusic.Stop();
            Debug.Log("Day music ends");
        }
    }

    public void PlayNightMusic(bool play)
    {
        if (play)
        {
            //_nightMusic.Play();
            Debug.Log("Night music starts");
        }
        else
        {
            //_nightMusic.Stop();
            Debug.Log("Night music stops");
        }
    }

    public void PlayTransition()
    {
        //_transition.Play();
        Debug.Log("Transition sound");
    }

    public void PlayTakeDamange()
    {
        //_takeDamage.Play();
        Debug.Log("Take damage");
    }
}
