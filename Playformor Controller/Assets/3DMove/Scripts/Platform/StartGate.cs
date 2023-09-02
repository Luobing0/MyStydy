using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGate : MonoBehaviour
{
    public static string EventName = "Start_Gate";
    private void Enter() {
        Destroy(gameObject);

    }

    private void OnEnable() {
        EventManager.AddListener(Enter, EventName);
    }

    private void OnDisable() {
        EventManager.RemoveListener(Enter, EventName);
    }
}
