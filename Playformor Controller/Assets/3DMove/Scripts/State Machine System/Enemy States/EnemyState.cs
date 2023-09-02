using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : ScriptableObject, IState {
    [SerializeField] string stateName;
    [SerializeField, Range(0f, 1f)] float transDuration;
    protected Animator animator;
    protected EnemyStateMechine stateMechine;
    protected EnemyController  enemyController;
    public void Initialize(Animator animator, EnemyController enemyController, EnemyStateMechine stateMechine) {
        this.animator = animator;
        this.stateMechine = stateMechine;
        this.enemyController = enemyController;
    }
    public void Enter() {
        
    }

    public void Exit() {
        
    }

    public void LogicUpdate() {
        
    }

    public void PhysicUpdate() {
        
    }
}
