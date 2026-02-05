using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private InputData InputData => InputData.Instance;
    private MultiTimerManager MultiTimerManager => MultiTimerManager.Instance;
    private WeaponManager weaponManager => WeaponManager.Instance;
    private BuffManager buffManager => BuffManager.Instance;

    void Awake()
    {
        // 注册计时器
        MultiTimerManager.Create_DownTime("AttackCooldown", weaponManager.GetCurrentWeapon.AttackInterval);
    }

    void OnMove(InputValue direction)
    {
        if (buffManager && buffManager.IsBuffSelectionOpen)
        {
            return;
        }

        Vector2 moveDirection = direction.Get<Vector2>();
        InputData.MoveDirection = moveDirection;
    }

    void OnAttack()
    {
        if (buffManager && buffManager.IsBuffSelectionOpen)
        {
            return;
        }

        if(MultiTimerManager.IsDownTimerComplete("AttackCooldown"))
        {
            InputData.IsAttackAction?.Invoke();
            MultiTimerManager.Pause_DownTimer("AttackCooldown");
            MultiTimerManager.Start_DownTimer("AttackCooldown", weaponManager.GetCurrentWeapon.AttackInterval);
        }
    }
}


