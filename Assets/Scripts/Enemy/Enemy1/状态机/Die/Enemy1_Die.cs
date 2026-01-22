using UnityEngine;

public class Enemy1_Die : BaseState<Enemy1HFSM.Enemy1StateID>
{
    public Enemy1_Die() : base()
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("敌人1进入死亡状态");
    }
}
