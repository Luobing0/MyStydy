using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour {


    void LevelStart() {
        EventManager.Dispatch(StartGate.EventName);
        GetComponent<Canvas>().enabled = true;
        GetComponent<Animator>().enabled = true;
    }
}
