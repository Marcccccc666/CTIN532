using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCountController : MonoBehaviour
{
    [SerializeField, ChineseLabel("子弹UI预制体")] private Transform bulletUIPrefab;

    [SerializeField, ChineseLabel("子弹UI父物体")] private Transform bulletUIParent;

    private WeaponData currentWeaponData;
    
    private Queue<Transform> bulletUIInstances = new();

    private WeaponManager weaponManager => WeaponManager.Instance;
    private PoolManager poolManager => PoolManager.Instance;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        // 初始化时清除现有的子弹UI
        foreach (Transform child in bulletUIParent)
        {
            Destroy(child.gameObject);
        }
        bulletUIInstances?.Clear();
    }

    private void OnEnable()
    {
        weaponManager.OnWeaponSwitched += UpdateBulletCountUI;

    }

    private void OnDisable()
    {
        if(weaponManager)
        {
            weaponManager.OnWeaponSwitched -= UpdateBulletCountUI;
        }
    }

    /// <summary>
    /// 显示当前武器的子弹数量UI
    /// </summary>
    /// <param name="weaponProfab"> 武器预设 </param>
    /// <param name="weaponData"> 武器数据 </param>
    private void UpdateBulletCountUI(WeaponData weaponProfab,WeaponData weaponData)
    {
        if (weaponData == null || weaponData == currentWeaponData)
        {
            return;
        }

        currentWeaponData = weaponData;

        if(weaponData is GunData gunData)
        {
            gunData.OnBulletCountAdded += AddBulletUI;
            gunData.OnBulletCountDecreased += RecycleBulletUIInstances;
            if(gunData.WeaponBaseData is GunBaseData gunBaseData)
            {
                int bulletCount = weaponManager.GetFinalBulletCount(gunBaseData.MaxBulletCount);
                for (int i = 0; i < bulletCount; i++)
                {
                    Transform bulletUI = poolManager.Spawn(
                        prefab:bulletUIPrefab, 
                        position: bulletUIParent.position,
                        rotation: bulletUIParent.rotation,
                        defaultCapacity: bulletCount,
                        maxSize: 20,
                        autoActive: true,
                        parent:bulletUIParent);
                    bulletUIInstances.Enqueue(bulletUI);
                }
            }
        }
    }

    /// <summary>
    /// 回收子弹UI实例
    /// </summary>
    private void RecycleBulletUIInstances(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if(bulletUIInstances.TryDequeue(out Transform bulletUI))
            {
                poolManager.Release(bulletUIPrefab, bulletUI);
            }
        }
    }

    /// <summary>
    /// 增加子弹UI显示
    /// </summary>
    public void AddBulletUI(int count)
    {
        if(currentWeaponData is GunData gunData)
        {
            if(gunData.WeaponBaseData is GunBaseData gunBaseData)
            {
                int bulletCount = weaponManager.GetFinalBulletCount(gunBaseData.MaxBulletCount);
                for (int i = 0; i < count; i++)
                {
                    Transform bulletUI = poolManager.Spawn(
                        prefab:bulletUIPrefab, 
                        position: bulletUIParent.position,
                        rotation: bulletUIParent.rotation,
                        defaultCapacity: bulletCount,
                        maxSize: 20,
                        autoActive: true,
                        parent:bulletUIParent);
                    bulletUIInstances.Enqueue(bulletUI);
                }
            }
        }
    }

    /// <summary>
    /// 自动更新子弹UI显示
    /// </summary>
    private IEnumerator UpdateBulletCountUI()
    {
        while (true)
        {
            if(currentWeaponData is GunData gunData)
            { 
            }
        }
    }
}
