using UnityEngine;
using TMPro;
using System.Collections;

public class Score : MonoBehaviour
{
    public static Score Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _endGameScoreText;
    [SerializeField] private float pointBurstRate;
    private int _score = 0;

    private void Awake()
    {
        _scoreText.text = _score.ToString();

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

    public void EarnPoints(int points)
    {
        _score += points;
        _scoreText.text = _score.ToString();
        _endGameScoreText.text = _score.ToString();
    }

    public void PointBurst(int points)
    {
        StartCoroutine("PointBurstRoutine", points);
    }

    IEnumerator PointBurstRoutine(int pointsToAward)
    {
        int awardedPoints = 0;
        while (awardedPoints < pointsToAward)
        {
            EarnPoints(1);
            awardedPoints++;
            yield return new WaitForSeconds(pointBurstRate);
        }
    }
}
