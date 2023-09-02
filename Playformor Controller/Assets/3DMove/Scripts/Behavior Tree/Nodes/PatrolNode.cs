using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class PatrolNode : ActionNode {

    [SerializeField] float speed;
    [SerializeField] PatrolPath patrolPath;
    [SerializeField] float waypointTolerance = 5f;
    [SerializeField] float patrolWaitingTime = 2f;
    [SerializeField] private float chaseDistance = 2f;
    [SerializeField] float patrolTime = 3f;
    GameObject player;
    int currentWaypointIndex = 0;
    Vector3 guardPos;
    EnemyController enemy;

    private float timeSincePatrol = 0;
    private float timeSinceLastWayPoint = Mathf.Infinity;
    protected override void OnStart() {
        enemy = body.GetComponent<EnemyController>();
        player = GameObject.FindGameObjectWithTag("Player");
        guardPos = body.transform.position;
        Debug.Log("Ñ²Âß");
    }

    protected override void OnStop() {

    }

    protected override NodeState OnUpdate() {
        timeSincePatrol += Time.deltaTime;
        timeSinceLastWayPoint += Time.deltaTime;

        if(timeSincePatrol > patrolTime) {
            enemy.StopAllCoroutines();
            enemy.Cancel();
            return NodeState.Success;
        }
        else {
            if (InAttackRangeOfPlayer()) {
                Debug.Log("¹¥»÷");
                enemy.StopAllCoroutines();
                enemy.AttackAnim();
                return NodeState.Running;
            }
            else {
                PatrolBehavior();
                return NodeState.Running;
            }
        }



    }
    private bool InAttackRangeOfPlayer() {
        return Vector3.Distance(body.transform.position, player.transform.position) < chaseDistance;
    }
    private void PatrolBehavior() {
        Debug.Log("ÕýÔÚÑ²Âß");
        Vector3 nextPos = guardPos;
        if (patrolPath != null) {
            if (AtWaypoint()) {
                CycleWaypoint();
            }
            nextPos = GetCurrentWaypoint();
        }
        if (timeSinceLastWayPoint > patrolWaitingTime) {
            if(enemy.maxSpeed == 0) {
                enemy.maxSpeed = speed;
            }
            enemy.MoveToPos(nextPos,currentWaypointIndex);

        }
        else {

        }

    }

    private Vector3 GetCurrentWaypoint() {
        return patrolPath.GetWaypoint(currentWaypointIndex);
    }

    private void CycleWaypoint() {
        currentWaypointIndex = patrolPath.GetNextWaypoint(currentWaypointIndex);
        timeSinceLastWayPoint = 0;
    }

    private bool AtWaypoint() {
        float distanceToWaypoint = Vector3.Distance(body.transform.position, GetCurrentWaypoint());
        return distanceToWaypoint < waypointTolerance;
    }

}
