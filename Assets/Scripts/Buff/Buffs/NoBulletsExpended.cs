using UnityEngine;

[CreateAssetMenu(fileName = "NoBulletsExpended", menuName = "Buffs/No Bullets Expended(不消耗子弹)")]
public class NoBulletsExpended : BuffDefinition
{
    [SerializeField, Range(0,1), ChineseLabel("有多少概率不消耗子弹")] private float probability = 0.5f;
    public override void Apply()
    {
        BuffManager.Instance.BeforeAttackTriggered += OnBeforeAttack;
    }

    private void OnBeforeAttack(WeaponData weaponData)
    {
        if(weaponData is GunData gunData)
        {
            if(Random.value < probability)
            {
                gunData.IsConsumingBullet = false; // 本次攻击不消耗子弹
            }
            else
            {
                gunData.IsConsumingBullet = true; // 本次攻击正常消耗子弹
            }
        }
    }
}
