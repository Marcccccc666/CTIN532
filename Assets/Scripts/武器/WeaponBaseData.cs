using UnityEngine;

public class WeaponBaseData : ScriptableObject
{
    [Header("武器属性")]
    [SerializeField, ChineseLabel("武器名称")] private string weaponName;
    /// <summary>
    /// 武器名称
    /// </summary>
    public string WeaponName => weaponName;

    [SerializeField, ChineseLabel("武器介绍"), TextArea] private string weaponDescription;

    public string WeaponDescription => weaponDescription;
    [SerializeField, ChineseLabel("武器伤害")] private int weaponDamage;
    /// <summary>
    /// 武器伤害
    /// </summary>
    public int WeaponDamage => weaponDamage;
    [SerializeField, ChineseLabel("攻击间隔")] private float attackInterval;
    /// <summary>
    /// 攻击间隔
    /// </summary>
    public float AttackInterval => attackInterval;

    [SerializeField, ChineseLabel("武器旋转速度")] private float weaponRotationSpeed;

    /// <summary>
    /// 武器旋转速度
    /// </summary>
    public float WeaponRotationSpeed => weaponRotationSpeed;

    [SerializeField, ChineseLabel("初试弹道数量")] private int initialBallisticsCount = 1;
    /// <summary>
    /// 初始弹道数量
    /// </summary>
    public int InitialBallisticsCount => initialBallisticsCount;

    [SerializeField, ChineseLabel("弹道类型")] private BulletType ballisticsType;
    /// <summary>
    /// 弹道类型
    /// </summary>
    public BulletType BallisticsType => ballisticsType;

    [SerializeField, ChineseLabel("弹道间隔距离")] private float bulletIntervalDistance;
    /// <summary>
    /// 弹道间隔距离
    /// </summary>
    public float BulletIntervalDistance => bulletIntervalDistance;

    [SerializeField, ChineseLabel("扇形弹道角度")] private float fanShapedAngle;

    /// <summary>
    /// 扇形弹道角度
    /// <summary>
    public float FanShapedAngle => fanShapedAngle;

    [Header("子弹属性")]
    [SerializeField, ChineseLabel("子弹预制体")] private BulletAttack bulletPrefab;
    /// <summary>
    /// 子弹刚体预制体
    /// </summary>
    public BulletAttack BulletPrefab => bulletPrefab;
    [SerializeField, ChineseLabel("子弹速度")] private float bulletSpeed;
    /// <summary>
    /// 子弹速度
    /// </summary>
    public float BulletSpeed => bulletSpeed;

    [SerializeField, ChineseLabel("子弹穿透力")] private int bulletPenetration;
    /// <summary>
    /// 子弹穿透力
    /// </summary>
    public int BulletPenetration => bulletPenetration;
}
