
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//状态机的功能：
//1.持有所有的状态机类，并对它们进行管理和切换
//2.负责进行当前状态的更新
public class StateMechine:MonoBehaviour
{
    IState currentState;

    protected Dictionary<System.Type, IState> stateTable;

    private void Update()
    {
        currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        currentState.PhysicUpdate();
    }

    protected void SwichOn(IState newState)
    {
        currentState = newState;
        currentState.Enter();
    }

    /// <summary>
    /// 切换状态机
    /// </summary>
    /// <param name="newState"></param>
    public void SwichState(IState newState)
    {
        currentState.Exit();
        SwichOn(newState);
    }

    public void SwichState(System.Type newStateType)
    {
        SwichState(stateTable[newStateType]);
    }

    public void ExitState() {
        currentState.Exit();
        currentState = null;
    }
}
