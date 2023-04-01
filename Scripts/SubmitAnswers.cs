using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubmitAnswers : MonoBehaviour
{
    public TextMeshProUGUI[] _input;
    [SerializeField] GameObject[] _question;
    private int _index = 0;
    [SerializeField] private GameObject finishScreen;
    [SerializeField] private GameObject cTest;
    [SerializeField] private GameObject confettiParticle;
    public void OnSubmit()
    {
        Debug.Log("submitted : " + _input[_index].text);
        
        {
            _question[_index].SetActive(false);
            if(_index < _question.Length -1)
            {
                _question[_index + 1].SetActive(true);
            }
            _index++;

            if(_index == _question.Length)
            {
                Debug.Log("finished");
                cTest.SetActive(false);
                finishScreen.SetActive(true);
                confettiParticle.SetActive(true);
            }
            
        }
    }

    IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        
    }
}
