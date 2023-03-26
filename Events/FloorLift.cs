using System.Collections.Generic;
using UnityEngine;

public class FloorLift : MonoBehaviour
{
    [SerializeField] private LiftTrigger[] liftTrigger;                                // object of LiftTrigger class which sends the event
    [SerializeField] private Transform liftPlat;                    // transform of LiftPlat
    [SerializeField] private Transform liftMech;                    // transform of LiftMech
    private float platYPosition;                                    // Y position of LiftPlat
    private float mechYScale;                                       // Y scale of LiftMech

    void Start()
    {
        // for(int i = 0; i < leverTriggerObj.Length; i++)
        // {
        //     Debug.Log($"i is {i}");
        //     liftTrigger[i] = new LiftTrigger();
        //     liftTrigger[i] = leverTriggerObj[i].GetComponent<LiftTrigger>();
        // }
    }
    void OnEnable() 
    {
        EventManager.StartListening("LiftUp", OnLeverUp);
        EventManager.StartListening("LiftDown", OnLeverDown);
    }

    void OnDisable() {
        EventManager.StopListening("LiftUp", OnLeverUp);
        EventManager.StopListening("LiftDown", OnLeverDown);
    }
    
    void OnLeverUp(Dictionary<string, object> message) 
    {
        Debug.Log("Hello");
        platYPosition = liftPlat.localPosition.y + 3f;
        mechYScale = liftMech.localScale.y + 29;
        LeanTween.moveLocalY(liftPlat.gameObject, platYPosition, 3f);
        LeanTween.scaleY(liftMech.gameObject, mechYScale, 3.1f).setOnComplete(SetLeverStatus);
    }

    void OnLeverDown(Dictionary<string, object> message)
    {
        platYPosition = liftPlat.localPosition.y - 3f;
        mechYScale = liftMech.localScale.y - 29;
        LeanTween.moveLocalY(liftPlat.gameObject, platYPosition, 3f);
        LeanTween.scaleY(liftMech.gameObject, mechYScale, 3.1f).setOnComplete(SetLeverStatus);
    }

    private void SetLeverStatus()
    {
       for(int i = 0; i < liftTrigger.Length; i++)
       {
            liftTrigger[i].ColTrigger.enabled = true;
       } 
        
    }
}