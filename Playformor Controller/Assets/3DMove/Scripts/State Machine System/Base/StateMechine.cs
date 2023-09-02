
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//״̬���Ĺ��ܣ�
//1.�������е�״̬���࣬�������ǽ��й�����л�
//2.������е�ǰ״̬�ĸ���
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
    /// �л�״̬��
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
