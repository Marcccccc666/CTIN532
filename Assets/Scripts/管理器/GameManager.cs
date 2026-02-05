using System;
using UnityEngine;

/// <summary>
/// 管理游戏状态
/// </summary>
public class GameManager : Singleton<GameManager>
{

#region 游戏暂停
    /// <summary>
    /// 暂停游戏
    /// </summary>
    private bool isGamePaused = false;

    /// <summary>
    /// 游戏是否暂停
    /// </summary>
    public bool IsGamePaused => isGamePaused;

    public Action GamePausedAction;

    /// <summary>
    /// 设置游戏是否暂停
    /// </summary>
    public void SetGamePaused(bool paused)
    {
        isGamePaused = paused;
        if (isGamePaused)
        {
            GamePausedAction?.Invoke();
        }
    }
#endregion
}
