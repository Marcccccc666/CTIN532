public class Enemy4_Idle : BaseState<Enemy4HFSM.Enemy4StateID>
{
    private readonly Enemy4HFSM enemy;

    public Enemy4_Idle(Enemy4HFSM enemy) : base()
    {
        this.enemy = enemy;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        enemy.EnterIdle();
    }
}
