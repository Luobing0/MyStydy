using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundDetector : MonoBehaviour
{
    [SerializeField] float detectionRadius = 0.05f;
    [SerializeField] LayerMask grondLayer;
    Collider[] colliders = new Collider[1];
    public bool IsGround => Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, colliders, grondLayer) != 0;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
