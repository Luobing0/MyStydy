# Platform-For-FSM-and-Behavior-Tree
## 平台控制
 
本项目的移动输入借鉴了2D平台游戏《蔚蓝》的部分设计思路：土狼时间、预输入缓冲，以及一些运动的参数可以调节。
### 土狼时间
为了让玩家离开跳跃平台的边缘是，仍然有短短几帧的有效时间让玩家允许操作。
```csharp
    public override void LogicUpdate()
    {
        if (input.IsJump)
        {
            stateMechine.SwichState(typeof(PlayerState_JumpUp));
        }
        if (!input.IsMove || coyoteTime < StateDuration)
        {
            stateMechine.SwichState(typeof(PlayerState_Fall));
        }
    }
```

### 预输入缓冲
预土狼时间相同的设计思路，为了扩充玩家的有效输入时间，还有一种情况是当玩家在未落地之前的几帧按下了跳跃键，仍然执行该跳跃指令。
在掉落状态的逻辑更新脚本中
```csharp
    public override void LogicUpdate()
    {
        if (playerController.IsGround)
        {
            stateMechine.SwichState(typeof(PlayerState_Land));
        }
        if (input.IsJump)
        {
            //二段跳
            if (playerController.CanAirJump)
            {
                stateMechine.SwichState(typeof(PlayerState_AirJump));
                return;
            }
            input.SetJumpInputBufferTime();

        }
    }
```
在输入脚本中设置缓冲时间，使用协程延缓运行时间
```csharp
    public void SetJumpInputBufferTime()
    {
        StopCoroutine(nameof(JumpInputBufferCoroutine));
        StartCoroutine(nameof(JumpInputBufferCoroutine));
    }

    IEnumerator JumpInputBufferCoroutine()
    {
        hasJumpInputBuffer = true;
        yield return waitJumpInputBufferTime;
        hasJumpInputBuffer = false;
    }
```

## 状态机

状态机基类，玩家、敌人的状态机脚本继承于它
```csharp
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
```
在玩家的状态机脚本中进行对所有的状态进行初始化
```csharp
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        input = GetComponent<PlayerInput>();
        controller = GetComponent<PlayerController>();
        stateTable = new Dictionary<System.Type, IState>(states.Length);
        foreach (var item in states)
        {
            item.Initialize(animator, input, controller, this);
            stateTable.Add(item.GetType(), item);
        }
    }
```
使用ScriptableObject 对每个状态进行分离，只在每个状态中判断改状态的行为。

![M89HFC(6DW{9OHUJTZMRCPD](https://github.com/Luobing0/Platform-For-FSM-and-Behavior-Tree/assets/50789197/22310b6b-0f56-4391-bd6c-da7fbf5ff0b8)

![5D0DT 7N UNP5M24`KN@8VU](https://github.com/Luobing0/Platform-For-FSM-and-Behavior-Tree/assets/50789197/d7133bcb-2ec3-4295-aff3-06bbca8f202e)

## 行为树
