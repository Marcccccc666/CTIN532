using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    [SerializeField, ChineseLabel("敌人数据")]private CHData enemyData;
    private GameManager gameManager => GameManager.Instance;

    private void Awake()
    {
        enemyData = GetComponent<CHData>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CharacterManager.GetCurrentPlayerCharacterData().CurrentHealth -= enemyData.CharacterBaseData.baseAttack;

            int playerHP = CharacterManager.GetCurrentPlayerCharacterData().CurrentHealth;
            if (playerHP <= 0)
            {
                gameManager.IsGameOver = true;
            }
        }
    }
}
