using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField, ChineseLabel("角色数据")]private ObjectData M_chData;
    private InputData InputData => InputData.Instance;
    private MultiTimerManager MultiTimerManager => MultiTimerManager.Instance;
    private WeaponManager weaponManager => WeaponManager.Instance;

    void Awake()
    {
        Debug.Log(weaponManager.GetCurrentWeapon.AttackInterval);
        // 注册计时器
        MultiTimerManager.Create_DownTime("AttackCooldown", weaponManager.GetCurrentWeapon.AttackInterval);
    }

    void OnMove(InputValue direction)
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
        {
            return;
        }

        Vector2 moveDirection = direction.Get<Vector2>();
        InputData.MoveDirection = moveDirection;
    }

    void OnAttack()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
        {
            return;
        }

        if(MultiTimerManager.IsDownTimerComplete("AttackCooldown"))
        {
            InputData.IsAttack = true;
            MultiTimerManager.Pause_DownTimer("AttackCooldown");
            MultiTimerManager.Start_DownTimer("AttackCooldown", weaponManager.GetCurrentWeapon.AttackInterval);
        }
    }
}


