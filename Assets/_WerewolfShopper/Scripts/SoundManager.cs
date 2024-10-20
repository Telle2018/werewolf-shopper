using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _dayMusic;
    [SerializeField] private AudioClip _nightMusic;
    [SerializeField] private AudioClip _transitionSound;
    [SerializeField] private AudioClip _attack;
    [SerializeField] private AudioClip _takeDamage;
    [SerializeField] private AudioClip _foundItem;

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

    public void PlayDayMusic()
    {
        _audioSource.clip = _dayMusic;
        _audioSource.Play();
    }

    public void PlayNightMusic()
    {
        _audioSource.clip = _nightMusic;
        _audioSource.Play();
    }

    public void PlayTakeDamange()
    {
        _audioSource.PlayOneShot(_takeDamage);
    }

    public void PlayTransitionSound()
    {
        _audioSource.PlayOneShot(_transitionSound);
    }
}
