using System;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : Singleton<BuffManager>
{
    [SerializeField] private BuffPool buffPool;
    [SerializeField] private int selectionCount = 3;

    private readonly List<BuffDefinition> currentSelection = new List<BuffDefinition>();
    private bool hasPendingSelection;

    public event Action<IReadOnlyList<BuffDefinition>> BuffSelectionRequested;

    public bool HasPendingSelection => hasPendingSelection;
    public IReadOnlyList<BuffDefinition> CurrentSelection => currentSelection;

    public void RequestBuffSelection()
    {
        if (hasPendingSelection)
        {
            return;
        }

        if (buffPool == null || buffPool.Buffs == null || buffPool.Buffs.Count == 0)
        {
            Debug.LogWarning("Buff pool is empty. No selection can be created.");
            return;
        }

        currentSelection.Clear();

        int targetCount = Mathf.Clamp(selectionCount, 1, buffPool.Buffs.Count);
        List<BuffDefinition> temp = new List<BuffDefinition>(buffPool.Buffs);
        Shuffle(temp);

        for (int i = 0; i < targetCount; i++)
        {
            currentSelection.Add(temp[i]);
        }

        hasPendingSelection = true;
        BuffSelectionRequested?.Invoke(currentSelection);
    }

    public void ClearSelection()
    {
        hasPendingSelection = false;
        currentSelection.Clear();
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
