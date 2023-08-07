using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGem : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Collider collider;

    [SerializeField] float resetTime = 3f;
    [SerializeField] AudioClip pickUpSFX;
    [SerializeField] ParticleSystem pickUpVFX;

    AudioSource source;
    WaitForSeconds waitResetTime;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        collider = GetComponent<Collider>();
        waitResetTime = new WaitForSeconds(resetTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Player"))
        //{
        //    gameObject.GetComponent<PlayerController>().CanAirJump = true;

        //}
        if (other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            player.CanAirJump = true;
            meshRenderer.enabled = false;
            collider.enabled = false;
            source.PlayOneShot(pickUpSFX);
            Instantiate(pickUpVFX, transform.position, Quaternion.identity);
            //Invoke性能不太好，推荐使用协程
            //Invoke(nameof(Reset), 3f);
            StartCoroutine(RestCoroutine());
        }
    }

    IEnumerator RestCoroutine()
    {
        yield return waitResetTime;
        Reset();
    }

    private void Reset()
    {
        meshRenderer.enabled = collider.enabled = true;
    }
}
