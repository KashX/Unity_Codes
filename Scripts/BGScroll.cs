using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGScroll : MonoBehaviour
{
    [SerializeField] private RawImage _bG;
    [SerializeField] private float _x, _y;

    void Update()
    {
        _bG.uvRect = new Rect(_bG.uvRect.position + new Vector2(_x, _y), _bG.uvRect.size);
    }
}
