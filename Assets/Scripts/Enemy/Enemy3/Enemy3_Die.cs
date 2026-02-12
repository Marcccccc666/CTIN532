public class Enemy3_Die : BaseState<Enemy3HFSM.Enemy3StateID>
{
    private readonly Enemy3HFSM enemy;

    public Enemy3_Die(Enemy3HFSM enemy) : base()
    {
        this.enemy = enemy;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        enemy.EnterDie();
    }
}
