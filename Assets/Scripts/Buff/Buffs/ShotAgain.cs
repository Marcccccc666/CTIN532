using UnityEngine;

[CreateAssetMenu(fileName = "ShotAgain", menuName = "Buffs/Shot Again(再次射击)")]
public class ShotAgain : BuffDefinition
{
    [SerializeField,Range(0,1), ChineseLabel("再次攻击的概率")] private float probability = 0.5f;

    public override void Apply()
    {
        BuffManager.Instance.AfterAttackTriggered += OnAfterAttack;
    }

    public override void Remove()
    {
        BuffManager.Instance.AfterAttackTriggered -= OnAfterAttack;
    }

    private void OnAfterAttack(WeaponData attacker)
    {
        WeaponBase weapon = attacker.WeaponController;
        if(weapon)
        {
            if(Random.value < probability)
            {
                weapon.Attack();
            }
        }
    }
}
