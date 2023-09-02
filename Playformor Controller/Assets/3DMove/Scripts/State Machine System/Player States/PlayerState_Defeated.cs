using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMechine/PlayerState/Defeated", fileName = "PlayerState_Defeated")]
public class PlayerState_Defeated : PlayerState
{
    [SerializeField] ParticleSystem vfx;
    [SerializeField] AudioClip[] audios;

    public override void Enter() {
        base.Enter();
        Instantiate(vfx, playerController.transform.position, Quaternion.identity);

        AudioClip clip = audios[Random.Range(0, audios.Length)];
        playerController.PlayerSource.PlayOneShot(clip);

    }


    public override void LogicUpdate() {
        if (IsAnimationFinished) {
            stateMechine.SwichState(typeof(PlayerState_Float));
        }
    }
}
