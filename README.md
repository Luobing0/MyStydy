# Platform-For-FSM-and-Behavior-Tree
## 平台控制
 
本项目的移动输入借鉴了2D平台游戏《蔚蓝》的部分设计思路：土狼时间、预输入缓冲，已经一些运动的参数可以调节。
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
