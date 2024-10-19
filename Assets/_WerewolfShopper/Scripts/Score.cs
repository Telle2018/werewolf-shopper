using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _scoreText;
    private int _score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _scoreText.text = _score.ToString();
    }

    public void EarnPoints(int points)
    {
        _score += points;
        _scoreText.text = _score.ToString();
    }
}
