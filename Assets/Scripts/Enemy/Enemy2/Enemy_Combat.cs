using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    [SerializeField] private Transform attackpoint;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private int fallbackDamage = 1;

    private EnemyData enemyData;
    private GameManager gameManager => GameManager.Instance;
    private CharacterManager characterManager => CharacterManager.Instance;

    public float AttackRange => attackRange;
    public Transform AttackPoint => attackpoint;

    private void Awake()
    {
        if (enemyData == null)
        {
            enemyData = GetComponentInParent<EnemyData>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DealDamage();
        }
    }

    public void Attack()
    {
        if (attackpoint == null)
        {
            return;
        }

        Collider2D[] hitPlayers =
            Physics2D.OverlapCircleAll(attackpoint.position, attackRange, playerLayer);
        foreach (Collider2D player in hitPlayers)
        {
            if (player.CompareTag("Player"))
            {
                DealDamage();
            }
        }
    }

    private void DealDamage()
    {
        var playerData = characterManager.GetCurrentPlayerCharacterData;
        if (playerData == null)
        {
            return;
        }

        int damage = enemyData != null ? enemyData.CurrentAttack : fallbackDamage;
        playerData.Damage(damage);
    }

    public void OnDrawGizmosSelected()
    {
        if (attackpoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackpoint.position, attackRange);
    }
}
