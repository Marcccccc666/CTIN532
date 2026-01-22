using UnityEngine;
using UnityEngine.SceneManagement;
public class ReStart : MonoBehaviour
{
    [SerializeField, ChineseLabel("结算页面")] private GameObject gameOverPanel;
    [SerializeField, ChineseLabel("玩家角色数据")] private CHData playerCharacterData;
    private GameManager gameManager => GameManager.Instance;

    private void OnEnable()
    {
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (gameManager.IsGameOver)
        {
            gameOverPanel.SetActive(true);
        }
    }
    public void RestartGame()
    {
        gameManager.IsGameOver = false;
        CharacterManager.ClearEnemyCharacterData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
