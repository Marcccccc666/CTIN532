using System;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : Singleton<BuffManager>
{
    /// <summary>
    /// Buff池
    /// </summary>
    [SerializeField] private BuffPool buffPool;

    [SerializeField, ChineseLabel("玩家选择第几个 Buff")] private int selectedBuffIndex = -1;
    /// <summary>
    /// 玩家选择第几个 Buff
    /// </summary>
    public int SelectedBuffIndex
    {
        get => selectedBuffIndex;
        set => selectedBuffIndex = value;
    }

    /// <summary>
    /// 当前随机 3 个 Buff
    /// </summary>
    private readonly BuffDefinition[] currentSelection = new BuffDefinition[3];

    public Action OpenBuffSelectionUI;

    public IReadOnlyList<BuffDefinition> CurrentSelection => currentSelection;

    private bool isBuffSelectionOpen = false;
    /// <summary> 
    /// 是否正在选择 Buff 
    /// </summary>
    public bool IsBuffSelectionOpen => isBuffSelectionOpen;

    /// <summary>
    /// 设置是否正在选择 Buff
    /// </summary>
    public void SetIsBuffSelectionOpen(bool isOpen)
    {
        isBuffSelectionOpen = isOpen;
    }

    /// <summary>
    /// 触发 Buff 选择请求事件
    /// </summary>
    public void RequestBuffSelection()
    {
        SetIsBuffSelectionOpen(true);
        OpenBuffSelectionUI?.Invoke();
    }

    /// <summary>
    /// 请求生成 3 个随机 Buff 供玩家选择
    /// </summary>
    public void RequestCreateRandomBuff()
    {
        List<BuffDefinition> availableBuffs = new List<BuffDefinition>(buffPool.Buffs);
        Shuffle(availableBuffs);

        for (int i = 0; i < currentSelection.Length; i++)
        {
            currentSelection[i] = availableBuffs[i];
        }
    }

    private static void Shuffle(List<BuffDefinition> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}
