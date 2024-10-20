using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;

    private int _health = 200;
    public bool _isDead = false;

    private void Start()
    {
        _healthSlider.maxValue = _health;
        _healthSlider.value = _health;
    }

    public void TakeDamage(int damage)
    {
        SoundManager.Instance.PlayTakeDamage();

        _health -= damage;
        _healthSlider.value = _health;

        if (_health <= 0)
        {
            _isDead = true;
        }
    }

}
