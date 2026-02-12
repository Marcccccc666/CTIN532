public class Enemy4_Attack : BaseState<Enemy4HFSM.Enemy4StateID>
{
    private readonly Enemy4HFSM enemy;

    public Enemy4_Attack(Enemy4HFSM enemy) : base()
    {
        this.enemy = enemy;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        enemy.EnterAttack();
    }
}
