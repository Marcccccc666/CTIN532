using UnityEngine;

/// <summary>
/// 武器管理器
/// </summary>
public class WeaponManager: Singleton<WeaponManager>
{
    /// <summary>
    /// 当前武器
    /// </summary>
    [SerializeField]private WeaponBaseData currentWeapon;

    [SerializeField, ChineseLabel("武器伤害倍率")] private float damageMultiplier = 1f;
    [SerializeField, ChineseLabel("武器伤害加成")] private int damageBonus = 0;

    /// <summary>
    /// 获取当前武器
    /// </summary>
    public WeaponBaseData GetCurrentWeapon
    {
        get => currentWeapon;
    }

    /// <summary>
    /// 切换武器
    /// </summary>
    public void SwitchWeapon(WeaponBaseData newWeapon)
    {
        currentWeapon = newWeapon;
    }

    /// <summary>
    /// 获取最终伤害
    /// </summary>
    public int GetFinalDamage(int baseDamage)
    {
        float scaled = baseDamage * damageMultiplier;
        int total = Mathf.RoundToInt(scaled) + damageBonus;
        return Mathf.Max(0, total);
    }

    /// <summary>
    /// 增加伤害倍率（例如 0.2 表示 +20%）
    /// </summary>
    public void AddDamageMultiplier(float delta)
    {
        damageMultiplier = Mathf.Max(0f, damageMultiplier + delta);
    }

    /// <summary>
    /// 增加伤害数值
    /// </summary>
    public void AddDamageBonus(int bonus)
    {
        damageBonus += bonus;
    }


}
