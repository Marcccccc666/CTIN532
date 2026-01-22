using UnityEngine;
using UnityHFSM;

public enum Character{}

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterHFSM : MonoBehaviour
{
    /// <summary>
    /// 角色数据
    /// </summary>
    [SerializeField,ChineseLabel("角色数据")]private CHData M_chData;

    [Header("攻击设置")]
    [SerializeField,ChineseLabel("枪口位置")]private Transform M_gunMuzzle;

    [SerializeField,ChineseLabel("子弹预制体")]private Rigidbody2D M_bulletPrefab;

    [Header("音频设置")]
    [SerializeField,ChineseLabel("攻击音效")]private AudioClip M_attackAudioClip;

    /// <summary>
    /// 角色刚体
    /// </summary>
    private Rigidbody2D M_rigidbody2D;

    /// <summary>
    /// 角色状态机
    /// </summary>
    private StateMachine<CharacterStateID, Character> stateMachine = new StateMachine<CharacterStateID, Character>();

    /// <summary>
    /// 输入数据
    /// </summary>
    private InputData InputData => InputData.Instance;

    public enum CharacterStateID
    {
        Idle,
        Move,
        Attack,
        Die
    }

    private void Awake()
    {
        CharacterManager.SetCurrentPlayerCharacterData(M_chData);
        M_rigidbody2D = GetComponent<Rigidbody2D>();

        M_chData.InitCharacterData();
        
        CharacterStateMachine();

        stateMachine.Init();
    }
    
    private void FixedUpdate()
    {
        M_rigidbody2D.angularVelocity = 0f;
        // 移动角色
        if(InputData.MoveDirection != Vector2.zero)
        {
            CharacterMove.MoveCharacter(M_rigidbody2D, InputData.MoveDirection, M_chData.CurrentMoveSpeed);
        }
    }

    private void Update()
    {
        stateMachine.OnLogic();
        M_chData.CharacterPosition = this.transform;

        // 旋转角色
        if(InputData.MoveDirection != Vector2.zero)
        {
            CharacterRotation.RotateTowardsMoveDirection(this.transform, InputData.MoveDirection, M_chData.CharacterBaseData.rotationSpeed);
        }
    }

    /// <summary>
    /// 角色状态机
    /// </summary>
    void CharacterStateMachine()
    {
        // 添加状态
            // 待机状态
                stateMachine.AddState(CharacterStateID.Idle, new Idle());
            
            // 移动状态
                stateMachine.AddState(CharacterStateID.Move, new Move());

            // 攻击状态
               stateMachine.AddState(CharacterStateID.Attack, new Attack(M_attackAudioClip, M_bulletPrefab, M_gunMuzzle, M_chData.BulletSpeed));

            // 死亡状态
                stateMachine.AddState(CharacterStateID.Die, new Die());

        // 转换条件
            // 待机 -> 攻击
                stateMachine.AddTransition(CharacterStateID.Idle, CharacterStateID.Attack, t => InputData.IsAttack);
            // 待机 -> 移动
                stateMachine.AddTransition(CharacterStateID.Idle, CharacterStateID.Move, t => InputData.MoveDirection != Vector2.zero);
            
            // 移动 -> 攻击
                stateMachine.AddTransition(CharacterStateID.Move, CharacterStateID.Attack, t => InputData.IsAttack);
            // 移动 -> 待机
                stateMachine.AddTransition(CharacterStateID.Move, CharacterStateID.Idle, t => InputData.MoveDirection == Vector2.zero);
            
            // 攻击 -> 待机
                stateMachine.AddTransition(CharacterStateID.Attack, CharacterStateID.Idle, t => InputData.MoveDirection == Vector2.zero);
            // 攻击 -> 移动
                stateMachine.AddTransition(CharacterStateID.Attack, CharacterStateID.Move, t => InputData.MoveDirection != Vector2.zero);

        // 设置初始状态
        stateMachine.SetStartState(CharacterStateID.Idle);
    }
}
