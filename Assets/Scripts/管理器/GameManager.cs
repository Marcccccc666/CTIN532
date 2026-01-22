using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // 游戏是否结束
    private bool isGameOver = false;

    public bool IsGameOver
    {
        get { return isGameOver; }
        set { isGameOver = value; }
    }
}
