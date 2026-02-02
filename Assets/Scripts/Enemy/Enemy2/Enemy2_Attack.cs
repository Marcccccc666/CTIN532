public class Enemy2_Attack : BaseState<Enemy2HFSM.Enemy2StateID>
{
    private readonly Enemy2HFSM enemy;

    public Enemy2_Attack(Enemy2HFSM enemy) : base()
    {
        this.enemy = enemy;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        enemy.EnterAttack();
    }
}
