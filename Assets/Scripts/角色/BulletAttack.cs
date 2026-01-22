using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    private GameManager gameManager => GameManager.Instance;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            int enemyID = collision.gameObject.GetInstanceID();
            CHData enemyData = CharacterManager.GetEnemyCharacterDataDict()[enemyID];
            enemyData.CurrentHealth -= CharacterManager.GetCurrentPlayerCharacterData().CharacterBaseData.baseAttack;
            if(enemyData.CurrentHealth <= 0)
            {
                CharacterManager.RemoveEnemyCharacterData(enemyID);

                // 检查是否所有敌人都已被消灭
                int numberOfEnemies = CharacterManager.GetEnemyCharacterDataDict().Count;
                if(numberOfEnemies <= 0)
                {
                    gameManager.IsGameOver = true;
                }
            }
            Destroy(gameObject);
        }
        else if(collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
