using UnityEngine;

public class InputData : Singleton<InputData>
{
    void Update()
    {
        //Debug.Log("攻击状态：" + IsAttack);
    }
    
    private Vector2 moveDirection;
    /// <summary>
    /// 移动方向
    /// </summary>
    public Vector2 MoveDirection
    {
        get { return moveDirection; }
        set { moveDirection = value; }
    }

    
    private bool isAttack;

    /// <summary>
    /// 是否攻击
    /// </summary>
    public bool IsAttack
    {
        get { return isAttack; }
        set { isAttack = value; }
    }
}
