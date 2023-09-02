using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitoryGem : MonoBehaviour
{
     [SerializeField] AudioClip pickUpSFX;
    [SerializeField] ParticleSystem pickUpVFX;
    private void OnTriggerEnter(Collider other)
    {
        EventManager.Dispatch(EventNames.UIPassEvent);
        SoundEffectPlayer.audioSource.PlayOneShot(pickUpSFX);
        Instantiate(pickUpVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
