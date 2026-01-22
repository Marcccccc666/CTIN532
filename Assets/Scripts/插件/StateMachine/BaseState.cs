using UnityEngine;
using System;
using UnityHFSM;

public class BaseState<TStateId> : State<TStateId>
{
     protected bool RequestedExit = false;
    protected float ExitTime;

    protected readonly Action<State<TStateId>> onEnter;
    protected readonly Action<State<TStateId>> onLogic;
    protected readonly Action<State<TStateId>> onExit;
    protected readonly Func<State<TStateId>, bool> canExit;

       public BaseState(
        bool needsExitTime = false,
        float exitTime = 0.1f,
        bool isGhostState = false,
        Action<State<TStateId>> onEnter = null,
        Action<State<TStateId>> onLogic = null,
        Action<State<TStateId>> onExit = null,
        Func<State<TStateId>, bool> canExit = null
    ) : base(needsExitTime: needsExitTime, isGhostState: isGhostState)
    {
        ExitTime = exitTime;
        this.onEnter = onEnter;
        this.onLogic = onLogic;
        this.onExit = onExit;
        this.canExit = canExit;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        RequestedExit = false;
        timer.Reset();
        onEnter?.Invoke(this);
    }

    public override void OnLogic()
    {
        base.OnLogic();
        onLogic?.Invoke(this);

        // 如果已经申请了退出，就检查时间
        if (RequestedExit && timer.Elapsed >= ExitTime)
        {
            fsm.StateCanExit();
        }

        // 正常的 canExit 条件退出
        if (needsExitTime && canExit != null && fsm.HasPendingTransition && canExit(this))
        {
            fsm.StateCanExit();
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        onExit?.Invoke(this);
    }

    /// <summary>
    /// 请求退出
    /// </summary>
    public override void OnExitRequest()
    {
        // 1. 如果不需要 exit 时间：直接检查 canExit
        if (!needsExitTime)
        {
            if (canExit == null || canExit(this))
                fsm.StateCanExit();
            return;
        }

        // 2. 需要 exit 时间时：优先判断 canExit
        if (canExit != null && !canExit(this))
        {
            RequestedExit = true;
            timer.Reset();
            return;
        }

        // 3. canExit 通过，但是我们仍然要等 exitTime
        RequestedExit = true;
        //timer.Reset();
    }
}
