using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMechine/PlayerState/Float", fileName = "PlayerState_Float")]
public class PlayerState_Float : PlayerState
{
    [SerializeField] float floatintSpeed = 0.5f;
    [SerializeField] Vector3 floatingPositionOffset;
    [SerializeField] ParticleSystem vfx;
    [SerializeField] Vector3 vfxOffset;
     Vector3 floatingPosition;
    public override void Enter() {
        base.Enter();
        //EventManager.Dispatch(EventNames.PlayerLoseEvent);
        Transform playerTransform = playerController.transform;
        Vector3 vfxPosition = playerTransform.position + new Vector3(playerTransform.localScale.x * vfxOffset.x, playerTransform.localScale.y * vfxOffset.y, 0);
        Instantiate(vfx, vfxPosition, Quaternion.identity, playerTransform);
        floatingPosition = playerController.transform.position + floatingPositionOffset;
        Debug.Log("Íæ¼ÒÎ»ÖÃ£º" + floatingPosition);
    }

    public override void LogicUpdate() {
        Transform playerTransform = playerController.transform;
        if (Vector3.Distance(playerTransform.transform.position,floatingPosition) > floatintSpeed * Time.deltaTime) {
            Debug.Log(floatingPosition);
            playerTransform.position = Vector3.MoveTowards(playerTransform.position, floatingPosition, floatintSpeed * Time.deltaTime);
        }
        else {
            floatingPosition += (Vector3)Random.insideUnitCircle;
        }
       
    }

}
