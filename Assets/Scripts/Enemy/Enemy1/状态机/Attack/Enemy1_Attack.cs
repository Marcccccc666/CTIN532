using UnityEngine;

public class Enemy1_Attack : BaseState<Enemy1HFSM.Enemy1StateID>
{
    private Enemy1HFSM M_enemy1HFSM;

    /// <summary>
    /// 冲撞距离
    /// </summary>
    private float collisionDistance;

    /// <summary>
    /// 起点位置
    /// </summary>
    private Vector2 startPosition;

    public Enemy1_Attack(Enemy1HFSM enemy1, float collisionDistance) : base()
    {
        M_enemy1HFSM = enemy1;
        this.collisionDistance = collisionDistance;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("敌人1进入攻击状态");

        startPosition = M_enemy1HFSM.transform.position;
        M_enemy1HFSM.SetCanStartAttack(true);
    }

    public override void OnLogic()
    {
        base.OnLogic();

        // 检查是否达到冲撞距离
        if (HasReachedCollisionDistance())
        {
            M_enemy1HFSM.SetCanStartAttack(false);
        }
    }

    private bool HasReachedCollisionDistance()
    {
        float distanceTraveled = Vector2.Distance(startPosition, M_enemy1HFSM.transform.position);
        return distanceTraveled >= collisionDistance;
    }
}
