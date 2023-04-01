using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CNextButtton : MonoBehaviour
{
    [SerializeField] private GameObject[] _gameObj;
    [SerializeField] private GameObject _exitObj;
    private int _index = 0;

    public void NextUpdate()
    {
        if(_index <= _gameObj.Length - 2)
        {
            _gameObj[_index + 1].SetActive(true);
            _gameObj[_index].SetActive(false);
            _index++;
        }
        else
        {
            _exitObj.SetActive(true);
            _gameObj[_index].SetActive(false);
        }
        
    }
}
