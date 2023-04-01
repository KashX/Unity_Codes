using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ArrowUpdate : MonoBehaviour
{
    [SerializeField] private GameObject[] _arrow;
    private int _index = 0;
    private int _number;

    [SerializeField] private TextMeshProUGUI _ans;
    [SerializeField] private GameObject[] _ansOptions;
    private int _index2 = 0;
    private int i = 0;

    [SerializeField] private GameObject _checkRight;     // Ui for choosing right answer
    [SerializeField] private GameObject _checkWrong;     // // Ui for choosing wrong answer
    [SerializeField] private GameObject _scoreBoard;
    [SerializeField] private GameObject _optionQ;


    private void Start()
    {
        foreach(GameObject g in _arrow)
        {
            g.SetActive(false);
        }
        _arrow[_index].SetActive(true);
    }

    private void Update()
    {
        _number = _index + 1;
        _ans.text = _number.ToString();
    }

    public void UpdateArrowPosition()
    {
        if(_index <= _arrow.Length -2 && _index >= 0)
        {
            _arrow[_index + 1].SetActive(true);
            _arrow[_index].SetActive(false);
            _index++;
        }
    }
    public void ReverseUpdateArrowPosition()
    {
        if (_index <= _arrow.Length - 1 && _index > 0)
        {
            _arrow[_index - 1].SetActive(true);
            _arrow[_index].SetActive(false);
            _index--;
        }
    }

    public void UpdateQuestionOptions()
    {
        int[] ans = { 4, 1, 6, 7, 5, 2, 3 };

        if (_index2 <= _ansOptions.Length - 1 && _index2 >= 0)
        {
            if (_index2 == _ansOptions.Length -1 && _index + 1 == ans[i])
            {
                _checkRight.SetActive(true);
                StartCoroutine(Wait(1f));

                _optionQ.SetActive(false);
                _scoreBoard.SetActive(true);
            }

            if (_index + 1 == ans[i] && _index2 <= _ansOptions.Length - 2)
            {
                _checkRight.SetActive(true);
                StartCoroutine(Wait(1f));

                _ansOptions[_index2 + 1].SetActive(true);
                _ansOptions[_index2].SetActive(false);
                _index2++;
                i++;
                Debug.Log(_index2);
            }
            else
            {
                _checkWrong.SetActive(true);
                StartCoroutine(Wait(1f));
            }
        }
    }

    IEnumerator Wait(float wait)
    {
        yield return new WaitForSeconds(wait);
        _checkWrong.SetActive(false);
        _checkRight.SetActive(false);
    }
}
