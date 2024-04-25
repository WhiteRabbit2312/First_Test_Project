using UnityEngine;
using TMPro;
using TraineeGame;
using System;

public class ScoreCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textScore;

    public static Action<int> OnSetScore;

    private int _score = 0;
    private bool _countPoints;

    private void Awake()
    {
        GameManager.onGameplay += CanCount;
        GameManager.onEndGame += StopCount;
        GameManager.onPreGame += Resetscore;
    }

    // Update is called once per frame
    void Update()
    {
        if (_countPoints)
        {
            CountPoints();
        }
    }

    private void CanCount() => _countPoints = true;
    private void StopCount() => _countPoints = false;
    private void Resetscore()
    {
        OnSetScore?.Invoke(_score);
        _score = 0;
    }

    private void CountPoints()
    {
        _score++;
        _textScore.text = _score.ToString();
    }

    private void OnDestroy()
    {
        GameManager.onGameplay -= CanCount;
        GameManager.onEndGame -= StopCount;
        GameManager.onPreGame -= Resetscore;
    }
}
