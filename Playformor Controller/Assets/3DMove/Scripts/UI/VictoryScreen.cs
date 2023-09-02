using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    private void OnEnable() {
        EventManager.AddListener(ShowUI, EventNames.UIPassEvent);
    }

    private void OnDisable() {
        EventManager.RemoveListener( ShowUI, EventNames.UIPassEvent);
    }

    void ShowUI() {
        GetComponent<Canvas>().enabled = true;
        GetComponent<Animator>().enabled = true;
    }
}
