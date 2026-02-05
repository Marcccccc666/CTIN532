using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHPUI : MonoBehaviour
{
    [SerializeField,ChineseLabel("敌人ID")] private int enemyID;
    [SerializeField,ChineseLabel("文字前缀")] private string prefix = "HP: ";
    [SerializeField,ChineseLabel("HP文本")] private TextMeshProUGUI HPText;

    private Dictionary<int, EnemyData> enemyDataDict => EnemyManager.Instance.GetEnemyDataDict;

    /// <summary>
    /// 是否已经订阅
    /// </summary>
    private bool isSubscribed = false;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        EnemyData enemyData = GetComponentInParent<EnemyData>();
        if(enemyData != null)
        {
            enemyID = enemyData.gameObject.GetInstanceID();
        }
    }


    private void OnEnable()
    {
        if(!enemyDataDict.ContainsKey(enemyID))
        {
            Debug.LogError("敌人数据未找到，请检查敌人ID是否正确！");
            return;
        }
        else
        {
            enemyDataDict[enemyID].OnDamage += (damage) => UpdateHPDisplay();
            enemyDataDict[enemyID].OnHeal += (heal) => UpdateHPDisplay();
            enemyDataDict[enemyID].OnDie += () => UpdateHPDisplay();
            isSubscribed = true;
            UpdateHPDisplay();
        }
    }

    void OnDisable()
    {
        if(isSubscribed)
        {
            enemyDataDict[enemyID].OnDamage -= (damage) => UpdateHPDisplay();
            enemyDataDict[enemyID].OnHeal -= (heal) => UpdateHPDisplay();
            enemyDataDict[enemyID].OnDie -= () => UpdateHPDisplay();
            isSubscribed = false;
        }
    }

    private void UpdateHPDisplay()
    {
        int currentHP = enemyDataDict[enemyID].CurrentHealth;
        int maxHP = enemyDataDict[enemyID].MaxHealth;
        HPText.text = $"{prefix}{currentHP} / {maxHP}";
    }

    private void OnValidate()
    {
        EnemyData enemyData = GetComponentInParent<EnemyData>();
        if(enemyData != null)
        {
            enemyID = enemyData.gameObject.GetInstanceID();
        }
    }
}
