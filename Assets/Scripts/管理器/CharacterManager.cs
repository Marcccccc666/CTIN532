using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色管理器
/// </summary>
public class CharacterManager
{
#region 玩家
    /// <summary>
    /// 当前玩家控制的角色数据
    /// </summary>
    private static CHData CurrentPlayerCharacterData;

    /// <summary>
    /// 获取当前玩家控制的角色数据
    /// </summary>
    public static CHData GetCurrentPlayerCharacterData()
    {
        return CurrentPlayerCharacterData;
    }

    /// <summary>
    /// 设置当前玩家控制的角色数据
    /// </summary>
    /// <param name="characterData">角色数据</param>
    public static void SetCurrentPlayerCharacterData(CHData characterData)
    {
        CurrentPlayerCharacterData = characterData;
    }
#endregion

#region 敌人
    /// <summary>
    /// 敌人角色数据字典
    /// </summary>
    private static Dictionary<int, CHData> EnemyCharacterDataDict = new Dictionary<int, CHData>();

    /// <summary>
    /// 敌人角色数据字典
    /// </summary>
    public static Dictionary<int, CHData> GetEnemyCharacterDataDict()
    {
        return EnemyCharacterDataDict;
    }

    /// <summary>
    /// 添加敌人角色数据
    /// </summary>
    /// <param name="id">敌人ID</param>
    /// <param name="characterData">角色数据</param>
    public static void AddEnemyCharacterData(int id, CHData characterData)
    {
        if (!EnemyCharacterDataDict.ContainsKey(id))
        {
            EnemyCharacterDataDict.Add(id, characterData);
        }
    }

    /// <summary>
    /// 获取敌人角色数据
    /// </summary>
    /// <param name="id">敌人ID</param>
    /// <returns>角色数据</returns>
    public static CHData GetEnemyCharacterData(int id)
    {
        if (EnemyCharacterDataDict.TryGetValue(id, out CHData characterData))
        {
            return characterData;
        }
        else
        {
            Debug.LogError($"敌人ID为{id}的角色数据不存在");
            return null;
        }
    }

    /// <summary>
    /// 清除所有敌人角色数据
    /// </summary>
    public static void ClearEnemyCharacterData()
    {
        EnemyCharacterDataDict.Clear();
    }

    /// <summary>
    /// 移除敌人角色数据
    /// </summary>
    public static void RemoveEnemyCharacterData(int id)
    {
        if (EnemyCharacterDataDict.ContainsKey(id))
        {
            Object.Destroy(EnemyCharacterDataDict[id].gameObject);
            EnemyCharacterDataDict.Remove(id);
        }
    }
#endregion
}
