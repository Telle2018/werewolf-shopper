using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private GameObject _endGameCanvas;

    private int _health = 50;
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
            _endGameCanvas.SetActive(true);
        }
    }

}
