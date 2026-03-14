using UnityEngine;

public class WeaponData : MonoBehaviour, IPoolable
{
    protected WeaponManager weaponManager => WeaponManager.Instance;
    
    [SerializeField, ChineseLabel("武器基础数据")] protected WeaponBaseData weaponBaseData;
    /// <summary>
    /// 武器基础数据
    /// </summary>
    public WeaponBaseData WeaponBaseData => weaponBaseData;

    [SerializeField, ChineseLabel("武器控制器")] protected WeaponControllerBase weaponController;

    /// <summary>
    /// 获取武器控制器
    /// </summary>
    public WeaponControllerBase WeaponController => weaponController;

    public void SetWeaponController(WeaponControllerBase controller)
    {
        weaponController = controller;
    }

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

    #region 对象池管理

    protected IMyPool pool;

    public void SetPool(IMyPool pool)
    {
        this.pool = pool;
    }

    public virtual void Release()
    {
        pool?.Release(this);
    }

    #endregion
}
