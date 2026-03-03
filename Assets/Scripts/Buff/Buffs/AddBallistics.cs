using UnityEngine;

[CreateAssetMenu(fileName = "AddBallistics", menuName = "Buffs/Add Ballistics")]
public class AddBallistics : BuffDefinition
{
    [SerializeField, ChineseLabel("添加弹道数")] private int BallisticsCount = 1;
    public override void Apply()
    {
        var weaponManager = WeaponManager.Instance;
        
        WeaponData currentWeapon = weaponManager.GetCurrentWeapon;
        
        weaponManager.AddBallisticsBonus(BallisticsCount);
    }
}
