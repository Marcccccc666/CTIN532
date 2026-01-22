using UnityEngine;

public class CHData : MonoBehaviour
{
    [SerializeField, ChineseLabel("角色数据")]private CharacterBaseData characterBaseData;

    /// <summary>
    /// 角色数据容器
    /// </summary>
    public CharacterBaseData CharacterBaseData
    {
        get { return characterBaseData; }
        set { characterBaseData = value; }
    }

    #region 初始化
    public void InitCharacterData()
    {
        if(CharacterBaseData != null)
        {
            MaxHealth = CharacterBaseData.maxHealth;
            //初始化生命值
            CurrentHealth = CharacterBaseData.maxHealth;
            //初始化攻击力
            CurrentAttack = CharacterBaseData.baseAttack;
            //初始化攻击间隔
            CurrentAttackInterval = CharacterBaseData.attackInterval;
            //初始化移动速度
            CurrentMoveSpeed = CharacterBaseData.moveSpeed;
        }
        else
        {
            Debug.LogError("角色数据未设置！");
        }
    }
#endregion

#region 角色位置
    private Transform cachedTransform;
    /// <summary>
    /// 角色位置
    /// </summary>
    public Transform CharacterPosition
    {
        get { return cachedTransform; }
        set { cachedTransform = value; }
    }
#endregion

#region 生命值
    private int maxHealth;
    /// <summary>
    /// 最大生命值
    /// </summary>
    public int MaxHealth
    {
        get
        {
            if(CharacterBaseData != null)
            {
                maxHealth = CharacterBaseData.maxHealth;
                return maxHealth;
            }
            else
            {
                Debug.LogError("角色数据未设置！");
                return 0;
            }
        }
        set
        {
            if(value > 0)
            {
                maxHealth = value;
            }
            else
            {
                Debug.LogError("最大生命值必须大于0！");
            }
        }
    }
    private int currentHealth;
    /// <summary>
    /// 当前生命值
    /// </summary>
    public int CurrentHealth
    {
        get
        {
            if(CharacterBaseData != null)
            {
                return currentHealth;
            }
            else
            {
                Debug.LogError("角色数据未设置！");
                return 0;
            }
        }
        set
        {
            if(value >= maxHealth)
            {
                currentHealth = maxHealth;
            }
            else if(value < 0)
            {
                currentHealth = 0;
            }
            else
            {
                currentHealth = value;
            }
        }
    }

#endregion

#region 攻击
    [SerializeField,ChineseLabel("子弹移动速度")] private float bulletSpeed = 500f;

    /// <summary>
    /// 子弹移动速度
    /// </summary>
    public float BulletSpeed
    {
        get { return bulletSpeed; }
    }
    private int currentAttack;
    /// <summary>
    /// 当前攻击力
    /// </summary>
    public int CurrentAttack
    {
        get
        {
            if(CharacterBaseData != null)
            {
                return currentAttack;
            }
            else
            {
                Debug.LogError("角色数据未设置！");
                return 0;
            }
        }
        set
        {
            currentAttack = value;
        }
    }

    private float currentAttackInterval;
    /// <summary>
    /// 当前攻击间隔
    /// </summary>
    public float CurrentAttackInterval
    {
        get
        {
            if(CharacterBaseData != null)
            {
                return currentAttackInterval;
            }
            else
            {
                Debug.LogError("角色数据未设置！");
                return 0;
            }
        }
        set
        {
            currentAttackInterval = value;
        }
    }
#endregion

#region 移动速度
    private float currentMoveSpeed;
    /// <summary>
    /// 当前移动速度
    /// </summary>
    public float CurrentMoveSpeed
    {
        get
        {
            if(CharacterBaseData != null)
            {
                return currentMoveSpeed;
            }
            else
            {
                Debug.LogError("角色数据未设置！");
                return 0;
            }
        }
        set
        {
            currentMoveSpeed = value;
        }
    }
#endregion
}
