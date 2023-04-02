using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public SpriteRenderer _wireEnd;
    Vector3 _startPoint;
    Vector3 _startPosition;
    public LayerMask _layerMask;

    void Start()
    {
        _startPoint = transform.parent.position;
        _startPosition = transform.position;
    }

    private void OnTouchDrag()
    {
        // touch position to world point
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;

        // check for nearby connection points
        Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, .2f, _layerMask);
        foreach(Collider2D col in colliders)
        {
            // make sure not my own collider
            if(col.gameObject != gameObject)
            {
                // update wire to connection point position
                UpdateWire(col.transform.position);

                // check is the wires are of same type
                if(transform.parent.name.Equals(col.transform.parent.name))
                {
                    //count connection
                    // finish step
                    col.GetComponent<Wire>()?.Done();
                    Done();
                    
                }
                return;
            }
        }

        // update wire
        UpdateWire(newPosition);
    }

    private void Done()
    {
        Debug.Log("Matched");

        Destroy(this);
    }

    private void OnTouchUp()
    {
        // reset wire position
        UpdateWire(_startPosition);
    }

    private void UpdateWire(Vector3 newPosition)
    {
        // update position
        transform.position = newPosition;

        // update direction
        Vector3 direction = newPosition - _startPoint;
        transform.right = direction * transform.lossyScale.x;

        // update scale
        float dist = Vector3.Distance(_startPoint, newPosition);
        _wireEnd.size = new Vector2(dist, _wireEnd.size.y);
    }
}
