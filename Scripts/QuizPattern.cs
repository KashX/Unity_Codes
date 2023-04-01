using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuizPattern : MonoBehaviour
{
    [SerializeField] private GameObject _gameObjToDisable;
    [SerializeField] private GameObject _gameObjToEnable;
    [SerializeField] private GameObject _gameObjCorrect;
    [SerializeField] private GameObject _gameObjWrong;

    [SerializeField] private TextMeshProUGUI _scoreField;
    [SerializeField] private int _totalScore;
    private int _score = 0;


    private void Update()
    {
        _scoreField.text = _score.ToString() + " / " + _totalScore.ToString();
    }

    public void ChoosedCorrect()
    {
        _score += 1;

        _gameObjCorrect.SetActive(true);
        StartCoroutine(Waiting(1.5f));
    }

    public void ChoosedWrong()
    {
        _score -= 1;

        _gameObjWrong.SetActive(true);
        StartCoroutine(Waiting(1.5f));
    }

    IEnumerator Waiting(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        _gameObjToDisable.SetActive(false);
        _gameObjToEnable.SetActive(true);
        _gameObjCorrect.SetActive(false);
        _gameObjWrong.SetActive(false);
    }
}
