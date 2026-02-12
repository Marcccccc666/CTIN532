public class Enemy3_Attack : BaseState<Enemy3HFSM.Enemy3StateID>
{
    private readonly Enemy3HFSM enemy;

    public Enemy3_Attack(Enemy3HFSM enemy) : base()
    {
        this.enemy = enemy;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        enemy.EnterAttack();
    }
}
