using UnityEngine;

[CreateAssetMenu(fileName = "HealBuff", menuName = "Buffs/Heal Buff")]
public class HealBuff : BuffDefinition
{
    [SerializeField, ChineseLabel("生命上限增加")] private int maxHealthIncrease = 2;

    public override void Apply()
    {
        var characterDate = CharacterManager.Instance.GetCurrentPlayerCharacterData;
        if (characterDate == null)
        {
            return;
        }

        characterDate.MaxHealth = characterDate.MaxHealth + maxHealthIncrease;
        int healAmount = Mathf.Max(0, characterDate.MaxHealth - characterDate.CurrentHealth);
        characterDate.Heal(healAmount);
    }

    public override void Remove()
    {
        var characterDate = CharacterManager.Instance.GetCurrentPlayerCharacterData;
        if (characterDate == null)
        {
            return;
        }

        characterDate.MaxHealth = Mathf.Max(1, characterDate.MaxHealth - maxHealthIncrease);
        if (characterDate.CurrentHealth > characterDate.MaxHealth)
        {
            characterDate.CurrentHealth = characterDate.MaxHealth;
        }
    }
}
