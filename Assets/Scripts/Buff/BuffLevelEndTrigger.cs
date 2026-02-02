using UnityEngine;

public class BuffLevelEndTrigger : MonoBehaviour
{
    [SerializeField] private bool triggerOnLastLevel = false;

    private GameManager gameManager => GameManager.Instance;
    private EnemyManager enemyManager => EnemyManager.Instance;
    private CharacterManager characterManager => CharacterManager.Instance;
    private LevelManager levelManager => LevelManager.Instance;
    private BuffManager buffManager => BuffManager.Instance;

    private int lastTriggeredLevelIndex = -1;

    private void OnEnable()
    {
        gameManager.GameCheckout += OnGameCheckout;
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            gameManager.GameCheckout -= OnGameCheckout;
        }
    }

    private void OnGameCheckout()
    {
        if (buffManager.HasPendingSelection)
        {
            return;
        }

        if (!IsLevelClear())
        {
            return;
        }

        int currentLevel = levelManager.CurrentLevelIndex;
        if (currentLevel == lastTriggeredLevelIndex)
        {
            return;
        }

        if (!triggerOnLastLevel && levelManager.IsLastLevel())
        {
            return;
        }

        lastTriggeredLevelIndex = currentLevel;
        buffManager.RequestBuffSelection();
    }

    private bool IsLevelClear()
    {
        if (characterManager.GetCurrentPlayerCharacterData == null)
        {
            return false;
        }

        if (characterManager.GetCurrentPlayerCharacterData.CurrentHealth <= 0)
        {
            return false;
        }

        return enemyManager.GetEnemyDataDict.Count == 0;
    }
}
