using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LiftTrigger : MonoBehaviour
{
    private Player _playerInput;
    [SerializeField] private Transform _lever;
    private Vector3 _leverUpRotation;
    private Vector3 _leverDownRotation;
    [SerializeField] private bool _canUseLever = false;
    [SerializeField] private bool _leverIsUp = false; 
    private bool _leverTriggered = false;
    private Collider _colTrigger;
    private GameObject leverObj;

    public Collider ColTrigger {get {return _colTrigger;} set {_colTrigger = value;}}
    void Awake()
    {
        _leverUpRotation = new Vector3(60f, 0f, 0f);
        _leverDownRotation = new Vector3(-60f, 0f, 0f);
        _colTrigger = GetComponent<BoxCollider>();

        _playerInput = new Player();
        _playerInput.PlayerMain.Hold.started += OnPressShift;
        _playerInput.PlayerMain.Hold.canceled += OnPressShift;

        if(!_leverIsUp)
        {
            LeanTween.rotateLocal(_lever.gameObject, _leverDownRotation, 0f);
        }
        else
        {
            LeanTween.rotateLocal(_lever.gameObject, _leverUpRotation, 0f);
        }
    }

    void Update()
    {
        if(_canUseLever)
        {
            if(!_leverIsUp && _leverTriggered)
            {
                _canUseLever = false;
                _colTrigger.enabled = false;
                LeanTween.rotateLocal(_lever.gameObject, _leverUpRotation, 1f).setOnComplete(LeverUp);
            }
            else if(_leverIsUp && _leverTriggered)
            {
                _canUseLever = false;
                _colTrigger.enabled = false;
                LeanTween.rotateLocal(_lever.gameObject, _leverDownRotation, 1f).setOnComplete(LeverDown);
            }
        }
    }

    private void OnPressShift(InputAction.CallbackContext context)
    {
        _leverTriggered = context.ReadValueAsButton();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            _canUseLever = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if(col.tag == "Player")
        {
            _canUseLever = false;
        }
    }

    void LeverUp()
    {
        EventManager.TriggerEvent("LiftUp", null);
        // _canUseLever = false;
        _leverIsUp = true;
    }
    void LeverDown()
    {
        EventManager.TriggerEvent("LiftDown", null);
        // _canUseLever = false;
        _leverIsUp = false;
    }

    private void OnEnable() 
    {
        _playerInput.PlayerMain.Enable();    
    }
    private void OnDisable() 
    {
        _playerInput.PlayerMain.Disable();
    }
}
