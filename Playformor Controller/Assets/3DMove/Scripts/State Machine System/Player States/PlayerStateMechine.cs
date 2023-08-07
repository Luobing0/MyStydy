using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMechine : StateMechine
{

    [SerializeField] PlayerState[] states;
    Animator animator;
    PlayerInput input;
    PlayerController controller;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        input = GetComponent<PlayerInput>();
        controller = GetComponent<PlayerController>();
        //TODO:���״̬����ʼ��
        stateTable = new Dictionary<System.Type, IState>(states.Length);
        foreach (var item in states)
        {
            item.Initialize(animator, input, controller, this);
            stateTable.Add(item.GetType(), item);
        }
    }

    private void Start()
    {
        SwichOn(stateTable[typeof(PlayerState_Idle)]);
    }
}
