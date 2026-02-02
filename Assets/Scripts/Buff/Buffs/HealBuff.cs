using UnityEngine;

[CreateAssetMenu(fileName = "HealBuff", menuName = "Buffs/Heal Buff")]
public class HealBuff : BuffDefinition
{
    [SerializeField, ChineseLabel("生命上限增加")] private int maxHealthIncrease = 2;

    public override void Apply(CharacterDate target)
    {
        if (target == null)
        {
            return;
        }

        target.MaxHealth = target.MaxHealth + maxHealthIncrease;
        target.CurrentHealth = target.MaxHealth;
    }
}
