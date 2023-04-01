using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FillInTheBlanks : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreField;
    [SerializeField] private int _totalScore;
    private int _score = 0;

    [SerializeField] private GameObject[] _gameObjToEnable;
    
    [SerializeField] private GameObject _exitObj;
    [SerializeField] private GameObject _chooseCorrect;
    [SerializeField] private GameObject _chooseWrong;
    private int _index = 0;

    private void Start()
    {
        _score = 0;
        _index = 0;
    }
    private void Update()
    {
        _scoreField.text = _score.ToString() + " / " + _totalScore.ToString();
    }

    public void PickCorrect()
    {
        _score += 1;

        _chooseCorrect.SetActive(true);
        StartCoroutine(Waiting(1.5f));
    }

    public void PickWrong()
    {
        _chooseWrong.SetActive(true);
        StartCoroutine(Waiting(1.5f));
    }

    IEnumerator Waiting(float waitingTime)
    {
        if (_index <= _gameObjToEnable.Length - 2)
        {
            
            yield return new WaitForSeconds(waitingTime);
            _gameObjToEnable[_index].SetActive(false);
            _gameObjToEnable[_index + 1].SetActive(true);
            _index++;

            _chooseCorrect.SetActive(false);
            _chooseWrong.SetActive(false);
        }

        else
        {
            yield return new WaitForSeconds(waitingTime);
            _chooseCorrect.SetActive(false);
            _chooseWrong.SetActive(false);
            _exitObj.SetActive(true);
            _gameObjToEnable[_index].SetActive(false);
        }
    }

    
}
