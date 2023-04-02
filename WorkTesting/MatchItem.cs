using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MatchItem : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler
{
    static MatchItem _hoverItem;

    public GameObject _linePrefab;
    public string _itemName;

    private GameObject _line;

    public void OnPointerDown(PointerEventData eventData)
    {
        _line = Instantiate(_linePrefab, transform.position, Quaternion.identity, transform.parent.parent);
        UpdateLine(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateLine(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(!this.Equals(_hoverItem) && _itemName.Equals(_hoverItem._itemName))
        {
            UpdateLine(_hoverItem.transform.position);
            Destroy(_hoverItem);
            Destroy(this);
            MatchLogic.AddPoint();
        }
        else
        {
            Destroy(_line);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _hoverItem = this;
    }

    private void UpdateLine(Vector3 position)
    {
        // Update direction
        Vector3 direction = position - transform.position;
        _line.transform.right = direction;

        // Update Scale
        _line.transform.localScale = new Vector3(direction.magnitude, 1, 1);
    }
}
