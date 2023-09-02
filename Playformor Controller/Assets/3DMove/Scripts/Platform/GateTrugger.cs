using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrugger : MonoBehaviour
{
    [SerializeField] AudioClip pickUpSFX;
    [SerializeField] ParticleSystem pickUpVFX;
    private void OnTriggerEnter(Collider other)
    {
        EventManager.Dispatch(Gate.EventName);
        SoundEffectPlayer.audioSource.PlayOneShot(pickUpSFX);
        Instantiate(pickUpVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


    private void OnDisable() {
        
    }
}
