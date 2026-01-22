using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class EnemyMaker : MonoBehaviour
{
    [SerializeField, ChineseLabel("生成的敌人预制体")] private CHData enemyPrefab;

    [SerializeField, ChineseLabel("生成最左高点")] private Transform topPoint;
    [SerializeField, ChineseLabel("生成最右低点")] private Transform bottomPoint;

    [SerializeField, ChineseLabel("生成敌人之间的距离")] private float spawnInterval = 3f;

    public void MakeEnemy(TextMeshProUGUI EnemyCount)
    {
        string text = Regex.Replace(EnemyCount.text, @"[^\d]", "");
        Debug.Log($"输入的敌人数量: {text}");
        if(!int.TryParse(text, out int enemyCount) || enemyCount <= 0)
        {
            Debug.LogError("请输入有效的敌人数量！");
            return;
        }

        

        for (int i = 0; i < enemyCount; i++)
        {
            Vector2 spawnPosition = RandomSpawnPosition();
            CHData enemy =  Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            CharacterManager.AddEnemyCharacterData(enemy.gameObject.GetInstanceID(), enemy);
        }
    }

    private Vector2 RandomSpawnPosition()
    {
        float randomY = Random.Range(bottomPoint.position.y, topPoint.position.y);
        float randomX = Random.Range(topPoint.position.x, bottomPoint.position.x);
        
        var EnemyDict = CharacterManager.GetEnemyCharacterDataDict();
        if(EnemyDict.Count > 0)
        {
            foreach (var enemy in EnemyDict)
            {
                if (Vector2.Distance(new Vector2(randomX, randomY), enemy.Value.gameObject.transform.position) < spawnInterval)
                {
                    return RandomSpawnPosition();
                }
            }
        }
        return new Vector2(randomX, randomY);
    }

}
