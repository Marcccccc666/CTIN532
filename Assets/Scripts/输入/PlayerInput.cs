using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField, ChineseLabel("角色数据")]private CHData M_chData;
    private InputData InputData => InputData.Instance;
    private MultiTimerManager MultiTimerManager => MultiTimerManager.Instance;

    void Awake()
    {
        // 注册计时器
        MultiTimerManager.Create_DownTime("AttackCooldown", M_chData.CurrentAttackInterval);
    }

    void OnMove(InputValue direction)
    {
        Vector2 moveDirection = direction.Get<Vector2>();
        InputData.MoveDirection = moveDirection;
    }

    void OnAttack()
    {
        if(MultiTimerManager.IsDownTimerComplete("AttackCooldown"))
        {
            InputData.IsAttack = true;
            MultiTimerManager.Reset_DownTimer("AttackCooldown", M_chData.CurrentAttackInterval);
            MultiTimerManager.Start_DownTimer("AttackCooldown", M_chData.CurrentAttackInterval);
        }
    }
}


