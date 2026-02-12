public class Enemy3_Cooldown : BaseState<Enemy3HFSM.Enemy3StateID>
{
    private readonly Enemy3HFSM enemy;

    public Enemy3_Cooldown(Enemy3HFSM enemy) : base()
    {
        this.enemy = enemy;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        enemy.EnterCooldown();
    }
}
