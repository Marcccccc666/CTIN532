public class Enemy2_Die : BaseState<Enemy2HFSM.Enemy2StateID>
{
    private readonly Enemy2HFSM enemy;

    public Enemy2_Die(Enemy2HFSM enemy) : base()
    {
        this.enemy = enemy;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        enemy.EnterDie();
    }
}
