using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPressureFloating : MonoBehaviour
{
    public float UnderAirPressureDrag = 3f;
    public float UnderAirPressureAngularDrag = 1f;
    public float AirDrag = 0f;
    public float AirAngularDrag = 0.05f;
    public float FloatingPower = 15f;
    public float SourceHeight = 0f;

    Rigidbody Rb;
    bool UnderAirPressure;

    void Start()
    {
        Rb = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float diff = transform.position.y - SourceHeight;
        if (diff < 0)
        {
            Rb.AddForceAtPosition(Vector3.up * FloatingPower * Mathf.Abs(diff), transform.position, ForceMode.Force);

            if (!UnderAirPressure)
            {
                UnderAirPressure = true;
                SwitchState(true);
            }
        }

        if (UnderAirPressure)
        {
            UnderAirPressure = false;
            SwitchState(false);
        }
    }
    void SwitchState(bool isUnderAirPressure)
    {
        if (isUnderAirPressure)
        {
            Rb.drag = UnderAirPressureDrag;
            Rb.angularDrag = UnderAirPressureAngularDrag;
        }
        else
        {
            Rb.drag = AirDrag;
            Rb.angularDrag = AirAngularDrag;
        }
    }
}