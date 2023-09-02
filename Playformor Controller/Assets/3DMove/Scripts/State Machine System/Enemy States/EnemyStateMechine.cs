using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMechine : StateMechine {

    [SerializeField] EnemyState[] states;
    Animator animator;
    EnemyController controller;
    private void Awake() {
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<EnemyController>();
        //TODO:玩家状态机初始化
        stateTable = new Dictionary<System.Type, IState>(states.Length);
        foreach (var item in states) {
            item.Initialize(animator,controller, this);
            stateTable.Add(item.GetType(), item);
        }
    }

    private void Start() {
        SwichOn(stateTable[typeof(PlayerState_Idle)]);
    }
}
