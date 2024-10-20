using UnityEngine;

public class MoneyParticles : MonoBehaviour
{
    public static MoneyParticles Instance { get; private set; }
    [SerializeField] private ParticleSystem _particleSystem;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }

    public void Burst()
    {
        _particleSystem.Play();
    }
}
