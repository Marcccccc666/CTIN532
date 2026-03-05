using UnityEngine;

public enum WeaponType
{
    /// <summary>
    /// 霰弹枪
    /// </summary>
    ShotGun,

    /// <summary>
    /// 狙击枪
    /// </summary>
    Sniper
}

[System.Serializable]
public class WeaponBranch
{
    public WeaponType Type;
    public WeaponData Data;
}


/// <summary>
/// 初始武器接口，定义了武器升级分支的属性
/// </summary>
public interface IInitialWeapon
{
    /// <summary>
    /// 武器升级分支
    /// </summary>
    WeaponBranch[] WeaponBrachs { get; }
}
