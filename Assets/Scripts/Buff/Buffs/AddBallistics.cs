using UnityEngine;


public class AddBallistics : BuffDefinition
{
    [SerializeField, ChineseLabel("添加弹道数")] private int BallisticsCount = 1;
    public override void Apply(CharacterDate target)
    {
        var weaponManager = WeaponManager.Instance;
        if (weaponManager == null)
        {
            return;
        }
    }
}
