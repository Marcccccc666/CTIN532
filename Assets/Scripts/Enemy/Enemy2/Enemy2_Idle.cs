public class Enemy2_Idle : BaseState<Enemy2HFSM.Enemy2StateID>
{
    private readonly Enemy2HFSM enemy;

    public Enemy2_Idle(Enemy2HFSM enemy) : base()
    {
        this.enemy = enemy;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        enemy.EnterIdle();
    }
}
