using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    [SerializeField, ChineseLabel("敌人数据")]private EnemyData enemyData;
    private GameManager gameManager => GameManager.Instance;
    private CharacterManager characterManager => CharacterManager.Instance;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            int damage = enemyData != null ? enemyData.CurrentAttack : 1;

            characterManager.GetCurrentPlayerCharacterData.Damage(damage);
        }
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (enemyData == null)
        {
            enemyData = GetComponentInParent<EnemyData>();
        }
    }
#endif
}
