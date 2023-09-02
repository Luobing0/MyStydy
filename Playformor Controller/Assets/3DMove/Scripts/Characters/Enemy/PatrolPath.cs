using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    const float wayPointsGizmosRadius = 0.3f;
    void OnDrawGizmos() {
        for (int i = 0; i < transform.childCount; i++) {
            int j = GetNextWaypoint(i);
            Gizmos.DrawSphere(GetWaypoint(i), wayPointsGizmosRadius);
            Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
        }
    }

    public int GetNextWaypoint(int i) {
        return i + 1 >= transform.childCount ? 0 : i + 1;
    }

    public Vector3 GetWaypoint(int i) {
        return transform.GetChild(i).position;
    }
}
