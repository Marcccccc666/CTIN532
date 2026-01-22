using UnityEngine;

public class Enemy1_Rotation : BaseState<Enemy1HFSM.Enemy1StateID>
{
    public Enemy1_Rotation() : base()
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("敌人1进入旋转状态");
    }
}
