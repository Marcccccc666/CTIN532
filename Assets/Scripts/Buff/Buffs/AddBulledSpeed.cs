using UnityEngine;

[CreateAssetMenu(fileName = "AddBulletSpeed", menuName = "Buffs/Add Bullet Speed")]
public class AddBulledSpeed : BuffDefinition
{
    [SerializeField, ChineseLabel("子弹速度加成")] private float bulletSpeedBonus = 1f;
    
    public override void Apply()
    {
        WeaponManager.Instance.AddBulletSpeedBonus(bulletSpeedBonus);
    }

    public override void Remove()
    {
        WeaponManager.Instance.AddBulletSpeedBonus(-bulletSpeedBonus);
    }
}
