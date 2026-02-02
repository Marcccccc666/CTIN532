using UnityEngine;
using UnityHFSM;

public enum Enemy2
{
}

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(EnemyData))]
public class Enemy2HFSM : MonoBehaviour
{
    [SerializeField] private Transform detectionPoint;
    [SerializeField] private Transform visualRoot;
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float attackCommitDuration = 1.05f;

    private EnemyData enemyData;
    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private Enemy_Combat combat;
    private float baseVisualScaleX;

    private float attackCooldownTimer;
    private float attackLockTimer;
    private bool attackLocked;
    private bool attackDamageApplied;
    private bool isStunned;
    private float stunTimer;

    private readonly StateMachine<Enemy2StateID, Enemy2> stateMachine = new();

    public enum Enemy2StateID
    {
        Idle,
        Move,
        Attack,
        Die
    }

    private Transform playerTransform
    {
        get
        {
            var playerData = CharacterManager.Instance.GetCurrentPlayerCharacterData;
            return playerData != null ? playerData.transform : null;
        }
    }

    private Vector2 DetectionPosition =>
        detectionPoint != null ? (Vector2)detectionPoint.position : (Vector2)transform.position;

    private void Awake()
    {
        enemyData = GetComponent<EnemyData>();
        enemyData.InitObjectData();

        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        combat = GetComponentInChildren<Enemy_Combat>();
        if (visualRoot == null)
        {
            visualRoot = transform;
        }
        baseVisualScaleX = Mathf.Abs(visualRoot.localScale.x);
        if (baseVisualScaleX <= 0f)
        {
            baseVisualScaleX = 1f;
        }

        var gizmo = GetComponentInChildren<AttackRangeGizmo>();
        if (gizmo != null)
        {
            attackRange = gizmo.GetAttackRange;
        }
        else if (combat != null && combat.AttackRange > 0f)
        {
            attackRange = combat.AttackRange;
        }

        if (enemyData != null && enemyData.CurrentAttackInterval > 0f)
        {
            attackCooldown = enemyData.CurrentAttackInterval;
        }

        BuildStateMachine();
    }

    private void Start()
    {
        stateMachine.Init();
    }

    private void Update()
    {
        UpdateTimers();
        stateMachine.OnLogic();
        UpdateFacing();
    }

    private void FixedUpdate()
    {
        rigidbody2D.angularVelocity = 0f;

        if (stateMachine.ActiveStateName == Enemy2StateID.Move && ShouldChase())
        {
            MoveTowardsPlayer();
        }
        else
        {
            rigidbody2D.linearVelocity = Vector2.zero;
        }
    }

    private void BuildStateMachine()
    {
        stateMachine.AddState(Enemy2StateID.Idle, new Enemy2_Idle(this));
        stateMachine.AddState(Enemy2StateID.Move, new Enemy2_Move(this));
        stateMachine.AddState(Enemy2StateID.Attack, new Enemy2_Attack(this));
        stateMachine.AddState(Enemy2StateID.Die, new Enemy2_Die(this));

        stateMachine.AddTransition(Enemy2StateID.Idle, Enemy2StateID.Move, _ => ShouldChase());
        stateMachine.AddTransition(Enemy2StateID.Idle, Enemy2StateID.Attack, _ => CanAttack());

        stateMachine.AddTransition(Enemy2StateID.Move, Enemy2StateID.Attack, _ => CanAttack());
        stateMachine.AddTransition(Enemy2StateID.Move, Enemy2StateID.Idle, _ => ShouldIdle());

        stateMachine.AddTransition(Enemy2StateID.Attack, Enemy2StateID.Move, _ => !attackLocked && ShouldChase());
        stateMachine.AddTransition(Enemy2StateID.Attack, Enemy2StateID.Idle, _ => !attackLocked && !ShouldChase());

        stateMachine.SetStartState(Enemy2StateID.Idle);
    }

    private void UpdateTimers()
    {
        if (attackCooldownTimer > 0f)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        if (attackLocked)
        {
            attackLockTimer -= Time.deltaTime;
            if (attackLockTimer <= 0f)
            {
                attackLockTimer = 0f;
                attackLocked = false;
            }
        }

        if (isStunned)
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0f)
            {
                isStunned = false;
                stunTimer = 0f;
            }
        }
    }

    private bool HasPlayer()
    {
        return playerTransform != null;
    }

    private float DistanceToPlayer()
    {
        if (!HasPlayer())
        {
            return float.MaxValue;
        }

        return Vector2.Distance(transform.position, playerTransform.position);
    }

    private float DetectionDistanceToPlayer()
    {
        if (!HasPlayer())
        {
            return float.MaxValue;
        }

        return Vector2.Distance(DetectionPosition, playerTransform.position);
    }

    private bool IsPlayerInDetectionRange()
    {
        if (!HasPlayer())
        {
            return false;
        }

        if (detectionRadius <= 0f)
        {
            return true;
        }

        return DetectionDistanceToPlayer() <= detectionRadius;
    }

    private bool IsPlayerInAttackRange()
    {
        if (!HasPlayer())
        {
            return false;
        }

        float range = attackRange;
        Vector2 origin = transform.position;
        if (combat != null && combat.AttackPoint != null)
        {
            origin = combat.AttackPoint.position;
            range = combat.AttackRange;
        }

        if (range <= 0f)
        {
            return false;
        }

        return Vector2.Distance(origin, playerTransform.position) <= range;
    }

    private bool CanAttack()
    {
        return !isStunned && IsPlayerInAttackRange() && attackCooldownTimer <= 0f;
    }

    private bool ShouldChase()
    {
        return !isStunned && IsPlayerInDetectionRange() && !IsPlayerInAttackRange();
    }

    private bool ShouldIdle()
    {
        return isStunned || !IsPlayerInDetectionRange();
    }

    private void MoveTowardsPlayer()
    {
        if (!HasPlayer())
        {
            return;
        }

        Vector2 direction =
            ((Vector2)playerTransform.position - (Vector2)transform.position).normalized;
        ObjectMove.MoveObject(rigidbody2D, direction, enemyData.CurrentMoveSpeed);
    }

    private void UpdateFacing()
    {
        if (!HasPlayer())
        {
            return;
        }

        float deltaX = playerTransform.position.x - transform.position.x;
        if (Mathf.Abs(deltaX) < 0.001f)
        {
            return;
        }

        float targetScaleX = baseVisualScaleX * (deltaX >= 0f ? 1f : -1f);
        Vector3 scale = visualRoot.localScale;
        if (!Mathf.Approximately(scale.x, targetScaleX))
        {
            scale.x = targetScaleX;
            visualRoot.localScale = scale;
        }
    }

    public void EnterIdle()
    {
        SetAnimatorState(Enemy2StateID.Idle);
    }

    public void EnterMove()
    {
        SetAnimatorState(Enemy2StateID.Move);
    }

    public void EnterAttack()
    {
        SetAnimatorState(Enemy2StateID.Attack);
        attackLocked = true;
        attackLockTimer = Mathf.Max(attackCommitDuration, 0.01f);
        attackDamageApplied = false;
        attackCooldownTimer = Mathf.Max(attackCooldown, 0.01f);
    }

    public void EnterDie()
    {
        SetAnimatorState(Enemy2StateID.Die);
    }

    private void SetAnimatorState(Enemy2StateID state)
    {
        if (animator == null)
        {
            return;
        }

        animator.SetBool("isIdle", state == Enemy2StateID.Idle);
        animator.SetBool("isChasing", state == Enemy2StateID.Move);
        animator.SetBool("isAttacking", state == Enemy2StateID.Attack);
        animator.SetBool("isDead", state == Enemy2StateID.Die);
    }

    public void TryAttack()
    {
        if (!CanAttack())
        {
            return;
        }

        attackCooldownTimer = Mathf.Max(attackCooldown, 0.01f);

        if (combat != null)
        {
            combat.Attack();
        }
    }

    public void OnAttackAnimationEvent()
    {
        if (attackDamageApplied)
        {
            return;
        }

        attackDamageApplied = true;
        if (combat != null)
        {
            combat.Attack();
        }
    }

    public void SetStunned(float duration)
    {
        if (duration <= 0f)
        {
            return;
        }

        isStunned = true;
        stunTimer = duration;
        rigidbody2D.linearVelocity = Vector2.zero;
    }

    public bool IsAttacking => stateMachine.ActiveStateName == Enemy2StateID.Attack;
}
