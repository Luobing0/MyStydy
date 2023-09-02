using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseScreen : MonoBehaviour
{
    [SerializeField] Button retryBtn;
    [SerializeField] Button quitBtn;


    private void OnEnable() {
        retryBtn.onClick.AddListener(SceneLoader.ReloadScene);
        quitBtn.onClick.AddListener(SceneLoader.QuitGame);
        EventManager.AddListener(ShowUI, EventNames.UILoseEvent);
    }

    private void OnDisable() {
        EventManager.RemoveListener(ShowUI, EventNames.UILoseEvent);
        retryBtn.onClick.RemoveListener(SceneLoader.ReloadScene);
        quitBtn.onClick.RemoveListener(SceneLoader.QuitGame);
    }

    void ShowUI() {
        GetComponent<Canvas>().enabled = true;
        GetComponent<Animator>().enabled = true;
    }
}
