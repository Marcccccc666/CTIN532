public class Enemy3_Idle : BaseState<Enemy3HFSM.Enemy3StateID>
{
    private readonly Enemy3HFSM enemy;

    public Enemy3_Idle(Enemy3HFSM enemy) : base()
    {
        this.enemy = enemy;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        enemy.EnterIdle();
    }
}
