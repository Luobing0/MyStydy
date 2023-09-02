using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public static string EventName = "Gate_Enter";
    public  void Enter(){
        Debug.Log("jintu");
        Destroy(gameObject);

    }

    private void OnEnable() {
        EventManager.AddListener(Enter, EventName);
    }

    private void OnDisable() {
        //EventManager.RemoveListener(AllEventType.Platform, Enter, EventName);
    }


}
