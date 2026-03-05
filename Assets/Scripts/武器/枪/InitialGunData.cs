using UnityEngine;



[CreateAssetMenu(fileName = "InitialGunData", menuName = "Scriptable Objects/Gun/InitialGunData")]
public class InitialGunData : GunBaseData, IInitialWeapon, IWeaponSpecificBuff
{
    [Space(10)]

    [SerializeField, ChineseLabel("枪升级分支")] private WeaponBranch[] weaponBrachs;
    /// <summary>
    /// 枪升级分支
    /// </summary>
    public WeaponBranch[] WeaponBrachs => weaponBrachs;

    [SerializeField, ChineseLabel("枪Buff")] private BuffPool WeaponSpecificBuffs;
    /// <summary>
    /// 枪Buff
    /// </summary>
    public BuffPool GetWeaponSpecificBuffs => WeaponSpecificBuffs;
}
