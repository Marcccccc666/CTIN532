using UnityEngine;

public class WeaponData : MonoBehaviour
{
    protected WeaponManager weaponManager => WeaponManager.Instance;
    
    [SerializeField, ChineseLabel("武器基础数据")] protected WeaponBaseData weaponBaseData;
    /// <summary>
    /// 武器基础数据
    /// </summary>
    public WeaponBaseData WeaponBaseData => weaponBaseData;

    /// <summary>
    /// 初始化武器数据
    /// </summary>
    public virtual void Initialize()
    {
        // 可以在这里添加一些通用的初始化逻辑
    }

    /// <summary>
    /// 武器被换掉时调用
    /// </summary>
    public virtual void OnUnequip()
    {
        // 可以在这里添加一些通用的卸载逻辑
    }
}
